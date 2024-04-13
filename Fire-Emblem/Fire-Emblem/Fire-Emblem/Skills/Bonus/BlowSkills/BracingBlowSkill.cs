namespace Fire_Emblem {
    public class BracingBlowSkill : Skill {
        public int DefBonus { get; private set; }
        public int ResBonus { get; private set; }

        public BracingBlowSkill(string name, string description) : base(name, description) {
            DefBonus = 6;
            ResBonus = 6;
        }

        public override void ApplyEffect(Combat combat, Character owner) {
            if (combat._attacker == owner) {
                owner.AddTemporaryBonus("Def", DefBonus);
                owner.AddTemporaryBonus("Res", ResBonus);
            }
        }
    }
}