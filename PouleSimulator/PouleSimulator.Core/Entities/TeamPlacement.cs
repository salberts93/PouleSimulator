namespace PouleSimulator.Core.Entities
{
    public class TeamPlacement
    {
        public TeamPlacement(Poule poule, Team team)
        {
            Poule = poule;
            Team = team;
            team.Placement = this;
        }

        public Poule Poule { get; set; }
        public Team Team { get; set; }
        public int MatchesPlayed { get; set; }
        public int Points { get; set; }
        public int GoalDifference
        {
            get
            {
                return GoalsScored - GoalsAgainst;
            }
        }
        public int GoalsScored { get; set; }
        public int GoalsAgainst { get; set; }

        public int Compare(TeamPlacement teamPlacement)
        {
            if (Points != teamPlacement.Points)
            {
                return Points > teamPlacement.Points ? 1 : -1;
            }
            else if (GoalDifference != teamPlacement.GoalDifference)
            {
                return GoalDifference > teamPlacement.GoalDifference ? 1 : -1;
            }
            else if (GoalsScored != teamPlacement.GoalsScored)
            {
                return GoalsScored > teamPlacement.GoalsScored ? 1 : -1;
            }
            else if (GetIntermediateResult(teamPlacement.Team) != 0)
            {
                return GetIntermediateResult(teamPlacement.Team);
            }

            return Team.Name.CompareTo(teamPlacement.Team.Name);
        }

        public override string ToString()
        {
            return string.Format("{0}; Matches Played: {1}, Points: {2}; Goal Difference: {3}; Goals Scored: {4}; Goals Against: {5} ", 
                Team.Name, 
                MatchesPlayed,
                Points, 
                GoalDifference, 
                GoalsScored, 
                GoalsAgainst);
        }

        public void AddMatchResult(int points, int goalsScored, int goalsAgainst)
        {
            MatchesPlayed++;
            Points += points;
            GoalsScored += goalsScored;
            GoalsAgainst += goalsAgainst;
        }

        private int GetIntermediateResult(Team team)
        {
            var matchAsHomeTeam = Poule.Matches.Find(match => match.HomeTeam == Team && match.VisitingTeam == team);
            var matchAsVisitingTeam = Poule.Matches.Find(match => match.VisitingTeam == Team && match.HomeTeam == team);
            var isHomeTeam = matchAsHomeTeam != null;
            var match = isHomeTeam ? matchAsHomeTeam : matchAsVisitingTeam;

            if (match.IsDraw() || !match.IsPlayed)
            {
                return 0;
            }

            var hasWon = (isHomeTeam && match.IsHomeTeamWin()) || (!isHomeTeam && match.IsVisitingTeamWin());
            return hasWon ? 1 : -1;

        }
    }
}
