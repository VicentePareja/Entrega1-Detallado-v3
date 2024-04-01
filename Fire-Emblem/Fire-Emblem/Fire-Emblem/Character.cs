using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fire_Emblem;

public class Characters
{
    [JsonPropertyName("Name")] public string Nombre { get; set; }
    [JsonPropertyName("Weapon")] public string Arma { get; set; }
    [JsonPropertyName("Gender")] public string Género { get; set; }
    [JsonPropertyName("HP")] public int HPmáximo { get; set; }
    public int HPactual { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Atk")] public int Atk { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Spd")] public int Spd { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Def")] public int Def { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Res")] public int Res { get; set; }
    public List<Skills> Skills { get; private set; }
    
    public Character()
    {
        Skills = new List<Skills>();
    }
    
    public void AddSkill(Skills skill)
    {
        Skills.Add(skill);
    }
}

