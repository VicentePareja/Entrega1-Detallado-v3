namespace Fire_Emblem {
    public class HPIncreaseSkill : Skill {
        public int HPIncrease { get; private set; }

        public HPIncreaseSkill(string name, string description) : base(name, description) {
            HPIncrease = 15;
        }

        public override void ApplyEffect(Combat combat, Character owner) {
            owner.MaxHP += HPIncrease;
            owner.CurrentHP += HPIncrease;
            Console.WriteLine($"{owner.Name}'s HP increased by {HPIncrease}");
        }
    }
}