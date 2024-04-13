// SkillFactory.cs
namespace Fire_Emblem {
    public class SkillFactory : ISkillFactory {
        public Skill CreateSkill(string name, string description) {
            switch (name) {
                case "HP +15":
                    return new HPIncreaseSkill(name, description);
                case "Fair Fight":
                    return new FairFightSkill(name, description);
                case "Death Blow":
                    return new DeathBlowSkill(name, description);
                case "Armored Blow":
                    return new ArmoredBlowSkill(name, description);
                case "Darting Blow":
                    return new DartingBlowSkill(name, description);
                case "Warding Blow":
                    return new WardingBlowSkill(name, description);
                case "Sturdy Blow":
                    return new SturdyBlowSkill(name, description);
                case "Steady Blow":
                    return new SteadyBlowSkill(name, description);
                case "Bracing Blow":
                    return new BracingBlowSkill(name, description);
                default:
                    return new GenericSkill(name, description);
            }
        }
    }
}