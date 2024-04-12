using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fire_Emblem;

public class Character
{
    [JsonPropertyName("Name")] public string Name { get; set; }
    [JsonPropertyName("Weapon")] public string Weapon { get; set; }
    [JsonPropertyName("Gender")] public string Gender { get; set; }
    [JsonPropertyName("HP")] public int MaxHP { get; set; }
    private int _currentHP;
    public int CurrentHP
    { 
        get => _currentHP; 
        set => _currentHP = Math.Max(value, 0); 
    }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Atk")] public int Atk { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Spd")] public int Spd { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Def")] public int Def { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Res")] public int Res { get; set; }
    public List<Skill> Skills { get; private set; }

    // Nueva propiedad para los bonos temporales
    public Dictionary<string, int> TemporaryBonuses { get; private set; }

    public Character()
    {
        Skills = new List<Skill>();
        TemporaryBonuses = new Dictionary<string, int>();
    }

    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }

    public void SetSkills(List<Skill> skills)
    {
        Skills = skills;
    }

    public void AddTemporaryBonus(string attribute, int value)
    {
        if (TemporaryBonuses.ContainsKey(attribute))
            TemporaryBonuses[attribute] += value;
        else
            TemporaryBonuses.Add(attribute, value);
    }

    public int GetEffectiveAttribute(string attribute)
    {
        int baseValue = attribute switch {
            "Atk" => Atk,
            "Spd" => Spd,
            "Def" => Def,
            "Res" => Res,
            _ => throw new ArgumentException($"Unknown attribute: {attribute}")
        };
        return baseValue + (TemporaryBonuses.ContainsKey(attribute) ? TemporaryBonuses[attribute] : 0);
    }

    public void CleanBonuses()
    {
        TemporaryBonuses.Clear();
    }
}
