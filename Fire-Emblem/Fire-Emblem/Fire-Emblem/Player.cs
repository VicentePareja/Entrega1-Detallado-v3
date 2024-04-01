namespace Fire_Emblem;

public class Player
{
    public Team Team { get; set; }

    public Player()
    {
        Team = new Team();
    }
}