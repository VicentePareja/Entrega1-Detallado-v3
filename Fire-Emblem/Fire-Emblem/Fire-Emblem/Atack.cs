using Fire_Emblem_View;
namespace Fire_Emblem;

public class Attack
{
    public Character Attacker { get; private set; }
    public Character Defender { get; private set; }
    private View _view;

    public Attack(Character attacker, Character defender, View view)
    {
        Attacker = attacker;
        Defender = defender;
        _view = view;
    }

    public void PerformAttack(string advantage)
    {
        double weaponTriangleBonus = advantage == "atacante" ? 1.2 : advantage == "defensor" ? 0.8 : 1.0;

        // Obtener los atributos efectivos usando GetEffectiveAttribute()
        int attackerAtk = Attacker.GetEffectiveAttribute("Atk");
        int defenderDef = Defender.GetEffectiveAttribute(Attacker.Weapon == "Magic" ? "Res" : "Def");

        int damage = (int)((attackerAtk * weaponTriangleBonus) - defenderDef);
        damage = Math.Max(damage, 0);

        _view.WriteLine($"{Attacker.Name} ataca a {Defender.Name} con {damage} de daño");

        Defender.CurrentHP -= damage;
    }

    public void PerformCounterAttack(string advantage)
    {
        double weaponTriangleBonus = advantage == "defensor" ? 1.2 : advantage == "atacante" ? 0.8 : 1.0;

        // Obtener los atributos efectivos usando GetEffectiveAttribute()
        int defenderAtk = Defender.GetEffectiveAttribute("Atk");
        int attackerDef = Attacker.GetEffectiveAttribute(Defender.Weapon == "Magic" ? "Res" : "Def");

        int damage = (int)((defenderAtk * weaponTriangleBonus) - attackerDef);
        damage = Math.Max(damage, 0);

        _view.WriteLine($"{Defender.Name} ataca a {Attacker.Name} con {damage} de daño");

        Attacker.CurrentHP -= damage;
    }
}