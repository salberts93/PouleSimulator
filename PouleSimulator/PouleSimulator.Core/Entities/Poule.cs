using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PouleSimulator.Core.Entities
{
    public class Poule
    {
        public Poule(List<Team> teams)
        {
            CreateTeamPlacements(teams);
            GenerateMatches();
            SortPlacements();
        }

        public List<TeamPlacement> TeamPlacements { get; set; }
        public List<Match> Matches { get; set; }

        public void SimulateAll()
        {
            for (int round = 1; round < TeamPlacements.Count; round++)
            {
                SimulateRound(round);
            }
        }

        public void SimulateRound(int round)
        {
            foreach (var match in Matches.Where(match => match.Round == round))
            {
                match.Simulate();
            }
            SortPlacements();
        }

        public string PrintAllMatchResults()
        {
            var stringBuilder = new StringBuilder();
            for (int round = 1; round < TeamPlacements.Count; round++)
            {
                stringBuilder.Append(string.Format("{0} {1}", PrintRoundResults(round), Environment.NewLine));
            }
            return stringBuilder.ToString();
        }

        public string PrintRoundResults(int round)
        {
            var matches = Matches.Where(match => match.Round == round && match.IsPlayed);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("Round {1}: {0}", Environment.NewLine, round));
            foreach (var match in matches)
            {
                stringBuilder.Append(string.Format("{0} {1}", match.ToString(), Environment.NewLine));
            }
            return stringBuilder.ToString();
        }

        public string PrintRankings()
        {
            var stringBuilder = new StringBuilder();
            for (int i = 1; i <= TeamPlacements.Count; i++)
            {
                stringBuilder.Append(string.Format("{0}. {1} {2}", i, TeamPlacements[i - 1].ToString(), Environment.NewLine));
            }
            return stringBuilder.ToString();
        }

        private void CreateTeamPlacements(List<Team> teams)
        {
            TeamPlacements = new List<TeamPlacement>();
            foreach (var team in teams)
            {
                TeamPlacements.Add(new TeamPlacement(this, team));
            }
        }

        private void SortPlacements()
        {
            TeamPlacements.Sort((teamPlacement1, teamPlacement2) => teamPlacement2.Compare(teamPlacement1));
        }

        private void GenerateMatches()
        {
            Matches = new List<Match>()
            {
                new Match(TeamPlacements[0].Team, TeamPlacements[1].Team, round: 1),
                new Match(TeamPlacements[2].Team, TeamPlacements[3].Team, round: 1),
                new Match(TeamPlacements[0].Team, TeamPlacements[2].Team, round: 2),
                new Match(TeamPlacements[1].Team, TeamPlacements[3].Team, round: 2),
                new Match(TeamPlacements[0].Team, TeamPlacements[3].Team, round: 3),
                new Match(TeamPlacements[1].Team, TeamPlacements[2].Team, round: 3)
            };
        }

    }
}
