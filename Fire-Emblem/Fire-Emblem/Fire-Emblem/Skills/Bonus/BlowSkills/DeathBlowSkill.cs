namespace Fire_Emblem {
    public class DeathBlowSkill : BlowSkill {
        public int Bonus { get; private set; }

        public DeathBlowSkill(string name, string description) : base(name, description) {
            Bonus = 8;
        }

        protected override void ApplySpecificEffect(Character owner) {
            owner.AddTemporaryBonus("Atk", Bonus);
        }
    }
}