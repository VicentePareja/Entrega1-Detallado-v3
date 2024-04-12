namespace Fire_Emblem {
    public class SkillFactory : ISkillFactory {
        public Skill CreateSkill(string name) {
            switch (name) {
                case "HP +15":
                    return new HPIncreaseSkill(name, "+15 HP to maxHP and currentHP");
                case "Fair Fight":
                    return new FairFightSkill(name,
                        "Si la unidad inicia el combate, otorga Atk+6 a la unidad y al rival durante el combate.");
                default:
                    return new GenericSkill(name, "Descripción no proporcionada");
            }
        }
    }
}