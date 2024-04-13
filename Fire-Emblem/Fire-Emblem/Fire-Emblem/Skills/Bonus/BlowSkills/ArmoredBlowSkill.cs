namespace Fire_Emblem;
public class ArmoredBlowSkill : BlowSkill {
    public int Bonus { get; private set; }

    public ArmoredBlowSkill(string name, string description) : base(name, description) {
        Bonus = 8; 
    }

    protected override void ApplySpecificEffect(Character owner) {
        owner.AddTemporaryBonus("Def", Bonus);
    }
}