namespace Fire_Emblem {
    public class SwiftSparrowSkill : Skill {
        public int Bonus { get; private set; }

        public SwiftSparrowSkill(string name, string description) : base(name, description) {
            Bonus = 6;
        }

        public override void ApplyEffect(Combat combat, Character owner) {
            if (combat._attacker == owner) {
                owner.AddTemporaryBonus("Atk", Bonus);
                owner.AddTemporaryBonus("Spd", Bonus);
            }
        }
    }
}