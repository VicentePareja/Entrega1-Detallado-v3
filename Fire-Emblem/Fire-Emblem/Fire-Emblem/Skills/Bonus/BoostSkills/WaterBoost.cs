namespace Fire_Emblem {
    public class WaterBoost : Skill {
        public int Bonus { get; private set; }

        public WaterBoost(string name, string description) : base(name, description) {
            Bonus = 6;
        }

        public override void ApplyEffect(Combat combat, Character owner) {
            Character otherCharacter;
            Character thisCharacter;

            if (owner == combat._attacker) {
                thisCharacter = combat._attacker;
                otherCharacter = combat._defender;
            } else {
                thisCharacter = combat._defender;
                otherCharacter = combat._attacker;
            }

            if (thisCharacter.CurrentHP >= otherCharacter.CurrentHP + 3) {
                owner.AddTemporaryBonus("Res", Bonus);
            }
        }
    }
}