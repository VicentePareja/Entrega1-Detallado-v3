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
            Console.WriteLine(); 
        }
    }
    
    public bool EsEquipoValido()
    {
        //ImprimirEquipo();
  
        if (Characters.Count < 1 || Characters.Count > 3)
        {
            return false; 
        }
        
        HashSet<string> nombresUnicos = new HashSet<string>();
        foreach (var character in Characters)
        {
            
            if (!nombresUnicos.Add(character.Nombre))
            {
                return false; 
            }
            
            if (!SonHabilidadesValidas(character))
            {
                return false; 
            }
        }

        return true;
    }

    private bool SonHabilidadesValidas(Character character)
    {
        
        if (character.Skills.Count > 2)
        {
            return false; 
        }
        
        HashSet<string> habilidadesUnicas = new HashSet<string>();
        foreach (var skill in character.Skills)
        {
            if (!habilidadesUnicas.Add(skill.Name))
            {
                return false; 
            }
        }

        return true;
    }

    
    public void ImprimirOpcionesDePersonajes()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            Console.WriteLine($"{i}: {Characters[i].Nombre}"); 
        }
    }

}
