namespace Fire_Emblem {
    public class SteadyBlowSkill : Skill {
        public int SpdBonus { get; private set; }
        public int DefBonus { get; private set; }

        public SteadyBlowSkill(string name, string description) : base(name, description) {
            SpdBonus = 6;
            DefBonus = 6;
        }

        public override void ApplyEffect(Combat combat, Character owner) {
            if (combat._attacker == owner) {
                owner.AddTemporaryBonus("Spd", SpdBonus);
                owner.AddTemporaryBonus("Def", DefBonus);
            }
        }
    }
}