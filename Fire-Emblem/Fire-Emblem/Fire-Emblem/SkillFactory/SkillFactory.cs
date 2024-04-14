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
                case "Deadly Blade":
                    return new DeadlyBladeSkill(name, description);
                case "Swift Sparrow":
                    return new SwiftSparrowSkill(name, description);
                case "Mirror Strike":
                    return new MirrorStrikeSkill(name, description);
                case "Swift Strike":
                    return new SwiftStrikeSkill(name, description);
                case "Brazen Atk/Spd":
                    return new BrazenAtkSpd(name, description);
                case "Brazen Atk/Def":
                    return new BrazenAtkDef(name, description);
                case "Brazen Atk/Res":
                    return new BrazenAtkRes(name, description);
                case "Brazen Spd/Def":
                    return new BrazenSpdDef(name, description);
                case "Brazen Spd/Res":
                    return new BrazenSpdRes(name, description);
                case "Brazen Def/Res":
                    return new BrazenDefRes(name, description);
                default:
                    return new GenericSkill(name, description);
            }
        }
    }
}