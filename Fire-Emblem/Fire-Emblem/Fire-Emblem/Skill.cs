namespace Fire_Emblem;

public class Skill
{
    
    public string Name { get; set; }
    public string Description { get; set; }

    // Constructor
    public Skill(string name, string description)
    {
        Name = name;
        Description = description;
    }

    // Método para imprimir los detalles de la habilidad
    public void ImprimirDetalles()
    {
        Console.WriteLine($"Habilidad: {Name}\nDescripción: {Description}\n");
    }
}




    