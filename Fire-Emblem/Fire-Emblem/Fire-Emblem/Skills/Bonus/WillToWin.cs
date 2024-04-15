namespace Fire_Emblem {
    public class WillToWin : Skill {
        public int Bonus { get; private set; }

        public WillToWin(string name, string description) : base(name, description) {
            Bonus = 8;
        }

        public override void ApplyEffect(Combat combat, Character owner) {

            if (owner.CurrentHP <= owner.MaxHP * 0.5) {
                owner.AddTemporaryBonus("Atk", Bonus);
            }
        }
    }
}