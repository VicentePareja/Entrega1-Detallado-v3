namespace Fire_Emblem {
    public class WardingBlowSkill : BlowSkill {
        public int Bonus { get; private set; }

        public WardingBlowSkill(string name, string description) : base(name, description) {
            Bonus = 8; 
        }

        protected override void ApplySpecificEffect(Character owner) {
            owner.AddTemporaryBonus("Res", Bonus);
        }
    }
}