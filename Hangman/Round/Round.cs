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
            Console.WriteLine(Capital);
            Console.WriteLine(Country);
            Console.WriteLine(Guess);
            Console.WriteLine(Lives);
            string badString=  new string(Bad.ToArray());


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

            if (Letter.Length > 1)
            {
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

        }

        public void CheckLetter()
        {
            string Letter = Console.ReadLine();
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

            Trials++;

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

                    Console.WriteLine("Do you want to guess a (l)eter or (w)ord");
                    Console.WriteLine("(l/w)?");

                    letter = Console.ReadLine().ToCharArray()[0];

                    if (letter == 'l')
                    {
                        CheckLetter();
                    } else
                    {
                        CheckWord();
                    }
                    Console.Clear();
                }


                timeSpan = DateTime.Now - localDate;
                Console.WriteLine("Trials: {0}  Time: {1}", Trials, timeSpan);
                Console.WriteLine("Do you want to play a game? y/n");


            } while (Console.ReadLine() == "y");

            SaveRound();

            Console.WriteLine("Thats end");
        }

        public void Win()
        {
            Session = false;
            Console.WriteLine("You Win!");
            Console.WriteLine("The capital of " + Country);
        }


        public void Lose()
        {
            Session = false;
            Console.WriteLine("You Lose!");
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

            Console.Clear();

            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            string temp =  name+ " | " +localDate.ToString() + " | " + timeSpan.ToString() + " | " + Trials.ToString() + " | " + Capital;
            highscores.Add(temp.Split(" | "));


            Console.Clear();


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
