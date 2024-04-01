namespace Fire_Emblem;

public class Player
{
    public Team Team { get; set; }
    public String Name { get; set; }

    public Player(string name)
    {
        Team = new Team();
        Name = name;
    }
    
}