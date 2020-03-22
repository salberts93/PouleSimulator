using PouleSimulator.Core.Entities;
using System;
using System.Collections.Generic;

namespace PouleSimulator.App.Menus
{
    public class MainMenu
    {
        private readonly Poule poule;
        private int lastRound = 0;
        public MainMenu()
        {
            poule = new Poule(GenerateTeams());
        }

        public int Display()
        {
            Console.Clear();
            Console.WriteLine(GetMenuText());
            int userInput = ReadInputInteger();
            return userInput;
        }

        public void NavigateTo(int userInput)
        {
            switch (userInput)
            {
                case 1:
                    SimulateAll();
                    break;
                case 2:
                    SimulateNextRound();
                    break;
                case 3:
                    ShowMatchResults();
                    break;
                case 4:
                    ShowRankings();
                    break;
                case 5:
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("The input is invalid. Please choose from 1 to 5...");
                    break;
            }
        }


        public int ReadInputInteger()
        {
            int userInput;
            while (!int.TryParse(Console.ReadLine(), out userInput))
            {
                Console.WriteLine("Please enter an integer");
            }
            return userInput;
        }
        private string GetMenuText()
        {
            string newLine = Environment.NewLine;
            return "1. Simulate all" + newLine
                + "2. Simulate next round" + newLine
                + "3. Show match results" + newLine
                + "4. Show rankings" + newLine
                + "5. Exit" + newLine
                + "Choose from 1 to 5...";
        }
        private List<Team> GenerateTeams()
        {
            return new List<Team>()
            {
                new Team("FC Barcelona", 5),
                new Team("Feyenoord", 4),
                new Team("Liverpool", 5),
                new Team("Bayer Leverkusen", 4)
            };
        }
        private void ShowRankings()
        {
            Console.WriteLine(poule.PrintRankings());
            Console.ReadLine();
        }

        private void ShowMatchResults()
        {
            Console.WriteLine(poule.PrintAllMatchResults());
            Console.ReadLine();
        }

        private void SimulateNextRound()
        {
            int currentRound = lastRound + 1;
            poule.SimulateRound(currentRound);
            lastRound++;
            Console.WriteLine("Round simulated, press enter to continue...");
            Console.ReadLine();
        }

        private void SimulateAll()
        {
            poule.SimulateAll();
            Console.WriteLine("Simulation complete, press enter to continue...");
            Console.ReadLine();
        }
    }
}
