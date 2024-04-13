namespace Fire_Emblem {
    public class DartingBlowSkill : BlowSkill {
        public int Bonus { get; private set; }

        public DartingBlowSkill(string name, string description) : base(name, description) {
            Bonus = 8; 
        }

        protected override void ApplySpecificEffect(Character owner) {
            owner.AddTemporaryBonus("Spd", Bonus);
        }
    }
}