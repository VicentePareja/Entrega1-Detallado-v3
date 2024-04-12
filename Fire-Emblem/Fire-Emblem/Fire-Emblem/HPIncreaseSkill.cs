namespace Fire_Emblem {
    public class HPIncreaseSkill : Skill {
        public int HPIncrease { get; private set; }

        public HPIncreaseSkill(string name, string description, int hpIncrease) : base(name, description) {
            HPIncrease = hpIncrease;
        }

        public override void ApplyEffect(Combat combat, Character owner) {
            owner.MaxHP += HPIncrease;
            owner.CurrentHP += HPIncrease;
            Console.WriteLine($"{owner.Name}'s HP increased by {HPIncrease}");
        }
    }
}