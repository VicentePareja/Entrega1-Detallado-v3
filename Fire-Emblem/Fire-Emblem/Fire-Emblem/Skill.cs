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

    // Cambio el método para aceptar Combat y el dueño de la habilidad
    public virtual void ApplyEffect(Combat combat, Character owner)
    {
        // Este método puede ser sobrescrito por clases derivadas.
        Console.WriteLine($"Applying {Name} to {owner.Name}");
    }

    public void PrintDetails()
    {
        Console.WriteLine($"Habilidad: {Name}\nDescripción: {Description}\n");
    }
}