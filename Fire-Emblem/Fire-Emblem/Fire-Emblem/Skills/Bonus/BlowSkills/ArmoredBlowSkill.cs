namespace Fire_Emblem {
    public class ArmoredBlowSkill : Skill {
        public int Bonus { get; private set; }

        public ArmoredBlowSkill(string name, string description) : base(name, description) {
            Bonus = 8; 
        }

        public override void ApplyEffect(Combat combat, Character owner) {
            if (combat._attacker == owner) {
                owner.AddTemporaryBonus("Def", Bonus);
            }
        }
    }
}