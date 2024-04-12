namespace Fire_Emblem {
    public class HPIncreaseSkill : Skill {
        public int HPIncrease { get; private set; }

        public HPIncreaseSkill(string name, string description, int hpIncrease) : base(name, description) {
            HPIncrease = hpIncrease;
        }

        public override void ApplyEffect(Character character) {
            character.MaxHP += HPIncrease;
            character.CurrentHP += HPIncrease;
        }
    }
}