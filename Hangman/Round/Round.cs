using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hangman.Round
{
    class Round
    {

        public List<string[]> Pairs = new List<string[]>();

        public void PrintStatus()
        {

        }

        public void NewGame()
        {

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
