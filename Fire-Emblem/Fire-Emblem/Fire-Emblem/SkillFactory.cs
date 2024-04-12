namespace Fire_Emblem {
    public class SkillFactory : ISkillFactory {
        public Skill CreateSkill(string name) {
            switch (name) {
                case "HP +15":
                    return new HPIncreaseSkill(name, "+15 HP to maxHP and currentHP", 15);
                default:
                    return new GenericSkill(name, "Descripción no proporcionada");
            }
        }
    }
}