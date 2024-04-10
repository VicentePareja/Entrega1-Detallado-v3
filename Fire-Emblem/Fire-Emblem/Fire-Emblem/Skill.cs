namespace Fire_Emblem;

public class Skill
{
    
    public string Name { get; set; }
    public string Description { get; set; }
    
    public Skill(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public void PrintDetails()
    {
        Console.WriteLine($"Habilidad: {Name}\nDescripción: {Description}\n");
    }
}




    