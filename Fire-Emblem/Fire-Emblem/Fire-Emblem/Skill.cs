namespace Fire_Emblem;

public class Skills
{
    
    {
    public string Name { get; set; }
    public string Description { get; set; }

    // Constructor
    public Skills(string name, string description)
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
}



    