namespace Fire_Emblem;

public class Team

{
    public List<Character> Characters { get; set; }

    public Team()
    {
        Characters = new List<Character>();
    }
    
    public void ImprimirEquipo()
    {
        Console.WriteLine("Detalles del Equipo:");
        foreach (var character in Characters)
        {
            Console.WriteLine($"Nombre: {character.Nombre}, HP Máximo: {character.HPmáximo}, Ataque: {character.Atk}");
            // Agrega más detalles según sea necesario
        }
    }

    public void ImprimirEquipoHabilidades()
    {
        Console.WriteLine("Detalles del Equipo:");
        foreach (var character in Characters)
        {
            Console.WriteLine($"Nombre: {character.Nombre}, HP Máximo: {character.HPmáximo}, Ataque: {character.Atk}");
            Console.WriteLine("Habilidades:");
            if (character.Skills != null && character.Skills.Count > 0)
            {
                foreach (var skill in character.Skills)
                {
                    Console.WriteLine($"- {skill.Name}: {skill.Description}");
                }
            }
            else
            {
                Console.WriteLine("  Esta unidad no tiene habilidades asignadas.");
            }
            Console.WriteLine(); // Espacio para separar las unidades
        }
    }
    
    public bool EsEquipoValido()
    {
        // Primero verifica si el número de personajes está en el rango válido (1-3)
        if (Characters.Count < 1 || Characters.Count > 3)
        {
            return false; // El equipo no es válido si no tiene entre 1 y 3 personajes
        }

        // Luego verifica si hay nombres duplicados en el equipo
        HashSet<string> nombresUnicos = new HashSet<string>();
        foreach (var character in Characters)
        {
            // Intenta añadir el nombre del personaje al conjunto.
            if (!nombresUnicos.Add(character.Nombre))
            {
                return false; // Se encontró un nombre duplicado, el equipo no es válido
            }
        }

        return true; // El equipo es válido si pasa ambas verificaciones
    }
    
    public void ImprimirOpcionesDePersonajes(List<string> characterNames)
    {
        for (int i = 0; i < characterNames.Count; i++)
        {
            Console.WriteLine($"{i}: {characterNames[i]}");
        }
    }
}
