namespace Fire_Emblem {
    public class DartingBlowSkill : Skill {
        public int Bonus { get; private set; }

        public DartingBlowSkill(string name, string description) : base(name, description) {
            Bonus = 8; 
        }

        public override void ApplyEffect(Combat combat, Character owner) {
            if (combat._attacker == owner) {
                owner.AddTemporaryBonus("Spd", Bonus);
            }
        }
    }
}