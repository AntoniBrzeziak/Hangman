using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Hangman.Round
{
    class Round
    {

        public List<string[]> Pairs = new List<string[]>();
        public List<string[]> highscores = new List<string[]>();

        public int Lives;
        public string Capital;
        public string Country;
        public char[] Guess;
        public int Trials;
        public List<char> Bad = new List<char>();
        public List<char> Know = new List<char>();
        public List<int> Good = new List<int>();
        bool Session;
        DateTime localDate;
        TimeSpan timeSpan;





        public void PrintStatus()
        {
            Console.Clear();

            Console.WriteLine(Guess);
            Console.WriteLine("Your lives: "+Lives);
            string badString=  new string(Bad.ToArray());
////

            Console.WriteLine("Not in word: " + badString);


        }

        public void NewGame()
        {

            Lives = 5;

            Random Rnd = new Random();


            int PairIndex = Rnd.Next(Pairs.Count-1);
            Capital = Pairs[PairIndex][1];
            Capital = Capital.ToUpper();
            Country = Pairs[PairIndex][0];
            Guess = Capital.ToCharArray();

            Trials = 0;
            Bad.Clear();
            Know.Clear();
            Good.Clear();

            Session = true;

            Array.Fill(Guess, '_');

            localDate = DateTime.Now;

        }


        public void CheckWord()
        {
            string Letter = Console.ReadLine();
            Letter = Letter.ToUpper();



                if (Letter == Capital)
                {
                    Win();
                    return;
                }
                else
                {

                    Lives -= 2;
                    if (Lives <= 0)
                    {
                        Lose();
                    }
                    return;

                }
            

        }

        public void CheckLetter()
        {
            string Letter = Console.ReadLine();

            if (Letter.Length != 1)
            {
                Console.WriteLine("Its not a letter! Try one more time:");
                CheckLetter();
                return;
            }
            Letter = Letter.ToUpper();



            if (Know.Contains(Letter[0]))
            {
                Console.WriteLine("You have arledy tried it!");
                return;
            }


            int Start = 0;
            int At = 0;
            int End = Capital.Length;
            bool Succes = false;
            int Counter = 0;

            Capital = Capital.ToUpper();

            while ((Start <= End) && (At > -1))
            {

                Counter = End - Start;
                At = Capital.IndexOf(Letter, Start, Counter);
                if (At == -1) break;
                Start = At + 1;
                Good.Add(At);
                Succes = true;

            }

            Know.Add(Letter[0]);


            if(Succes == false)
            {
                Bad.Add(Letter[0]);
                Lives--;
                if(Lives <= 0)
                {
                    Lose();
                    return;

                }

            }
            else
            {
                foreach (int a in Good)
                {
                    Guess[a] = Capital[a];
                }

                if(Good.Count == Capital.Length)
                {
                    Win();
                    return;

                }

            }



        }

        public void Play()
        {
            do
            {
                NewGame();

                char letter = ' ';

                while (Good.Count != Capital.Length && Session == true)
                {
                    PrintStatus();

                    Trials++;

                    Console.WriteLine("Do you want to guess a (l)eter or (w)ord");
                    Console.WriteLine("(l/w)?");

                    try
                    {
                        letter = Console.ReadLine().ToCharArray()[0];

                    }
                    catch (Exception Ex)
                    {
                        continue;
                    }




                    if (letter == 'l')
                    {
                        CheckLetter();
                    } else if (letter == 'w')
                    {
                        CheckWord();
                    }
                    else
                    {

                        continue;
                    }

                }


                timeSpan = DateTime.Now - localDate;
                Console.WriteLine("Trials: {0}  Time: {1}", Trials, timeSpan);


                HighScores();

                Console.WriteLine("Do you want to play a game? y/n");


            } while (Console.ReadLine() == "y");


            Console.WriteLine("Thats end");
        }

        public void HighScores()
        {

            string fileName = @"..\\..\\..\\Files\\highscores.txt";

            Console.WriteLine("HIGHSCORES:");
            Console.WriteLine("name | date | guessing_time | guessing_tries | guessed_word");

            List<string> hs = new List<string>();

            string s = "";
            int i = 0;
            using ( StreamReader sr = File.OpenText(fileName))
            {
                while ((s = sr.ReadLine()) != null && i <= 10)
                {
                    Console.WriteLine(s);
                    i++;
                }

            }
        }

        public void Win()
        {
            Session = false;
            Console.Clear();
            Console.WriteLine("You Win!");
            SaveRound();

        }



        public void Lose()
        {
            Session = false;
            Console.Clear();

            Console.WriteLine("You Lose!");
            Console.WriteLine(Country);
        }
        public Round()
        {

            string fileName = @"..\\..\\..\\Files\\countries_and_capitals.txt";

            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {

                    string s = "";
                    string[] tab;

                    s = sr.ReadLine();

                    while ((s = sr.ReadLine()) != null)
                    {

                        tab = s.Split(" | ");
                        Pairs.Add(tab);

                    }
                }
            }

            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

        }


        public void SaveRound()
        {
            string fileName = @"..\\..\\..\\Files\\highscores.txt";
            

            if (File.Exists(fileName) == false)
            {
                using (StreamWriter fs = File.CreateText(fileName))
                {

      
                }
            }


            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            string temp =  name+ " | " +localDate.ToString() + " | " + timeSpan.ToString() + " | " + Trials.ToString() + " | " + Capital;
            highscores.Add(temp.Split(" | "));



            string[] readText = File.ReadAllLines(fileName);

            List<string>highscoreslist =  readText.ToList();

            

            foreach (string item in highscoreslist)
            {
                highscores.Add(item.Split(" | "));
            }
            
            List<string[]> sorted = highscores.OrderBy(order => order[2]).ToList();



            List<string> tempList = new List<string>();
            foreach (string[] t in sorted)
            {
                tempList.Add(String.Join(" | ", t));
            }
            File.WriteAllLines(fileName, tempList.ToArray());


        }


    }



}
