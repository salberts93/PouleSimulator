using System;

namespace PouleSimulator.Core.Entities
{
    public class Match
    {
        public Match(Team homeTeam, Team visitingTeam, int round)
        {
            HomeTeam = homeTeam;
            VisitingTeam = visitingTeam;
            Round = round;
            IsPlayed = false;
        }

        public Team HomeTeam { get; set; }
        public Team VisitingTeam { get; set; }
        public bool IsPlayed { get; set; }
        public int Round { get; set; }
        public int GoalsHomeTeam { get; set; }
        public int GoalsVisitingTeam { get; set; }
        public string Result
        {
            get
            {
                return string.Format("{0}-{1}", GoalsHomeTeam, GoalsVisitingTeam);
            }
        }

        public void Simulate()
        {
            if (!IsPlayed)
            {
                GoalsHomeTeam = GenerateGoalsScored(HomeTeam, VisitingTeam);
                GoalsVisitingTeam = GenerateGoalsScored(VisitingTeam, HomeTeam);
                IsPlayed = true;
                UpdateTeamPlacements();
            }
        }

        public bool IsHomeTeamWin()
        {
            return IsPlayed && GoalsHomeTeam > GoalsVisitingTeam;
        }

        public bool IsDraw()
        {
            return IsPlayed && GoalsHomeTeam == GoalsVisitingTeam;
        }

        public bool IsVisitingTeamWin()
        {
            return IsPlayed && GoalsHomeTeam < GoalsVisitingTeam;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} {2}", HomeTeam.Name, VisitingTeam.Name, Result);
        }

        private int GenerateGoalsScored(Team attackingTeam, Team defendingTeam)
        {
            Random random = new Random();
            int scoreBase = random.Next(0, 4);
            double ratingFactor = (double)attackingTeam.Rating / (double)defendingTeam.Rating;
            double correctedScore = ratingFactor * scoreBase;
            return (int)Math.Ceiling(correctedScore);
        }

        private void UpdateTeamPlacements()
        {
            HomeTeam.Placement.AddMatchResult(GetPointsForTeam(true), GoalsHomeTeam, GoalsVisitingTeam);
            VisitingTeam.Placement.AddMatchResult(GetPointsForTeam(false), GoalsVisitingTeam, GoalsHomeTeam);
        }

        private int GetPointsForTeam(bool isHomeTeam)
        {
            if (IsDraw())
            {
                return 1;
            }
            else if (isHomeTeam && IsHomeTeamWin())
            {
                return 3;
            }
            else if (!isHomeTeam && IsVisitingTeamWin())
            {
                return 3;
            }
            return 0;
        }
    }
}
