namespace Fire_Emblem;

public class Team
{
    public List<Character> Characters { get; set; }

    public Team()
    {
        Characters = new List<Character>();
    }
    
    public void PrintTeam()
    {
        Console.WriteLine("Detalles del Equipo:");
        foreach (var character in Characters)
        {
            Console.WriteLine($"Nombre: {character.Name}");
            Console.WriteLine($"Arma: {character.Weapon}");
            Console.WriteLine($"Género: {character.Gender}");
            Console.WriteLine($"HP Máximo: {character.MaxHP}");
            Console.WriteLine($"HP Actual: {character.CurrentHP}");
            Console.WriteLine($"Ataque (Atk): {character.Atk}");
            Console.WriteLine($"Velocidad (Spd): {character.Spd}");
            Console.WriteLine($"Defensa (Def): {character.Def}");
            Console.WriteLine($"Resistencia (Res): {character.Res}");
       
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
            Console.WriteLine(); 
        }
    }

    public void PrintTeamSkills()
    {
        Console.WriteLine("Detalles del Equipo:");
        foreach (var character in Characters)
        {
            Console.WriteLine($"Nombre: {character.Name}, HP Máximo: {character.MaxHP}, Ataque: {character.Atk}");
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
            Console.WriteLine(); 
        }
    }
    
    public bool IsTeamValid()
    {
        //PrintTeam();
  
        if (Characters.Count < 1 || Characters.Count > 3)
        {
            return false; 
        }
        
        HashSet<string> uniqueNames = new HashSet<string>();
        foreach (var character in Characters)
        {
            
            if (!uniqueNames.Add(character.Name))
            {
                return false; 
            }
            
            if (!AreSkillsValid(character))
            {
                return false; 
            }
        }

        return true;
    }

    private bool AreSkillsValid(Character character)
    {
        
        if (character.Skills.Count > 2)
        {
            return false; 
        }
        
        HashSet<string> uniqueSkills = new HashSet<string>();
        foreach (var skill in character.Skills)
        {
            if (!uniqueSkills.Add(skill.Name))
            {
                return false; 
            }
        }

        return true;
    }
    
    public void PrintCharacterOptions()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            Console.WriteLine($"{i}: {Characters[i].Name}"); 
        }
    }

}
