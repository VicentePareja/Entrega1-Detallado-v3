namespace Fire_Emblem {
    public class DeadlyBladeSkill : Skill {
        public int Bonus { get; private set; }

        public DeadlyBladeSkill(string name, string description) : base(name, description) {
            Bonus = 8;
        }

        public override void ApplyEffect(Combat combat, Character owner) {
            if (combat._attacker == owner) {
                if (owner.Weapon == "Sword")
                {
                    owner.AddTemporaryBonus("Atk", Bonus);
                    owner.AddTemporaryBonus("Spd", Bonus);
                }
            }
        }
    }
}