namespace Fire_Emblem {
    public class WardingBlowSkill : Skill {
        public int Bonus { get; private set; }

        public WardingBlowSkill(string name, string description) : base(name, description) {
            Bonus = 8; 
        }

        public override void ApplyEffect(Combat combat, Character owner) {
            if (combat._attacker == owner) {
                owner.AddTemporaryBonus("Res", Bonus);
            }
        }
    }
}