using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fire_Emblem;

public class Character
{
    [JsonPropertyName("Name")] public string Nombre { get; set; }
    [JsonPropertyName("Weapon")] public string Arma { get; set; }
    [JsonPropertyName("Gender")] public string Género { get; set; }
    [JsonPropertyName("HP")] public int HPmáximo { get; set; }
    private int _hpActual;
    public int HPactual 
    { 
        get => _hpActual; 
        set => _hpActual = Math.Max(value, 0); 
    }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Atk")] public int Atk { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Spd")] public int Spd { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Def")] public int Def { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Res")] public int Res { get; set; }
    public List<Skill> Skills { get; private set; }

    public Character()
    {
        Skills = new List<Skill>();
    }

    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }
    
    public void SetSkills(List<Skill> skills)
    {
        Skills = skills;
    }
}