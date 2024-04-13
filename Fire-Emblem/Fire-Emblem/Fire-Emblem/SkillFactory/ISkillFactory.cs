namespace Fire_Emblem {
    public interface ISkillFactory {
        Skill CreateSkill(string skillName, string description);
    }
}