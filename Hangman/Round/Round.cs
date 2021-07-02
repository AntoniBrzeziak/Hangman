using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hangman.Round
{
    class Round
    {

        public List<string[]> Pairs = new List<string[]>();
        public int Lives;
        public string Capital;
        public string Country;
        public char[] Guess;


        public void PrintStatus()
        {
            Console.WriteLine(Capital);
            Console.WriteLine(Country);
            Console.WriteLine(Guess);
            Console.WriteLine(Lives);


        }

        public void NewGame()
        {

            Lives = 5;

            Random Rnd = new Random();

            int PairIndex = Rnd.Next(Pairs.Count);
            Capital = Pairs[PairIndex][1];
            Country = Pairs[PairIndex][0];
            Guess = Capital.ToCharArray();

            Array.Fill(Guess, '_');

        }

        public void Check()
        {

        }

        public void Play()
        {

        }

        public Round()
        {

            string fileName = @"countries_and_capitals.txt";

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





    }



}
