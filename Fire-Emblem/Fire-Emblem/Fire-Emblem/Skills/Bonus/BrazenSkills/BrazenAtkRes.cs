namespace Fire_Emblem {
    public class BrazenAtkRes : Skill {
        public int Bonus { get; private set; }

        public BrazenAtkRes(string name, string description) : base(name, description) {
            Bonus = 10;
        }

        public override void ApplyEffect(Combat combat, Character owner) {
            if (owner.CurrentHP <= owner.MaxHP * 0.8) {
                owner.AddTemporaryBonus("Atk", Bonus);
                owner.AddTemporaryBonus("Res", Bonus);
            }
        }
    }
}