namespace PouleSimulator.Core.Entities
{
    public class Team
    {
        public Team(string name, int rating)
        {
            Name = name;
            Rating = rating;
        }
        public string Name { get; set; }
        public int Rating { get; set; }
        public TeamPlacement Placement { get; set; }
    }
}
