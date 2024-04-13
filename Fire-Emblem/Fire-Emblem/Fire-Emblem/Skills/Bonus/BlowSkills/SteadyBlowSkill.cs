namespace Fire_Emblem {
    public class SteadyBlowSkill : BlowSkill {
        public int SpdBonus { get; private set; }
        public int DefBonus { get; private set; }

        public SteadyBlowSkill(string name, string description) : base(name, description) {
            SpdBonus = 6;
            DefBonus = 6;
        }

        protected override void ApplySpecificEffect(Character owner) {
            owner.AddTemporaryBonus("Spd", SpdBonus);
            owner.AddTemporaryBonus("Def", DefBonus);
        }
    }
}