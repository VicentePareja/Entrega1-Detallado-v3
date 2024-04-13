namespace Fire_Emblem {
    public class SkillFactory : ISkillFactory {
        public Skill CreateSkill(string name, string description) {
            switch (name) {
                case "HP +15":
                    return new HPIncreaseSkill(name, description);
                case "Fair Fight":
                    return new FairFightSkill(name, description);
                default:
                    return new GenericSkill(name, description);
            }
        }
    }
}