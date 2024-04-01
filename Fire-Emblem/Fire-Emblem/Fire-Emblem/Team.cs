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
            Console.WriteLine($"Nombre: {character.Nombre}");
            Console.WriteLine($"Arma: {character.Arma}");
            Console.WriteLine($"Género: {character.Género}");
            Console.WriteLine($"HP Máximo: {character.HPmáximo}");
            Console.WriteLine($"HP Actual: {character.HPactual}");
            Console.WriteLine($"Ataque (Atk): {character.Atk}");
            Console.WriteLine($"Velocidad (Spd): {character.Spd}");
            Console.WriteLine($"Defensa (Def): {character.Def}");
            Console.WriteLine($"Resistencia (Res): {character.Res}");
            // Si las habilidades son importantes para ser mostradas, las incluimos también
            if (character.Skills != null && character.Skills.Count > 0)
            {
                Console.WriteLine("Habilidades:");
                foreach (var skill in character.Skills)
                {
                    Console.WriteLine($"- {skill.Name}: {skill.Description}");
                }
            }
            else
            {
                Console.WriteLine("Este personaje no tiene habilidades asignadas.");
            }
            Console.WriteLine(); // Espacio para separar las unidades
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
        
            // Verifica las condiciones de las habilidades para cada personaje
            if (!SonHabilidadesValidas(character))
            {
                return false; // Las habilidades del personaje no son válidas
            }
        }

        return true; // El equipo es válido si pasa todas las verificaciones
    }

    private bool SonHabilidadesValidas(Character character)
    {
        // Verifica que el personaje no tenga más de 2 habilidades
        if (character.Skills.Count > 2)
        {
            return false; // No es válido tener más de 2 habilidades
        }

        // Verifica que no haya habilidades duplicadas
        HashSet<string> habilidadesUnicas = new HashSet<string>();
        foreach (var skill in character.Skills)
        {
            if (!habilidadesUnicas.Add(skill.Name))
            {
                return false; // Se encontró una habilidad duplicada, no es válido
            }
        }

        return true; // Las habilidades son válidas
    }

    
    public void ImprimirOpcionesDePersonajes(List<string> characterNames)
    {
        for (int i = 0; i < characterNames.Count; i++)
        {
            Console.WriteLine($"{i}: {characterNames[i]}");
        }
    }
}
