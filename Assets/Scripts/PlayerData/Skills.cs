using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public SkillData Fishing;
    public SkillData Cooking;
    public SkillData Mining;
    public SkillData Smithing;

    public void AddExperience(Enums.Skills skill, int experience)
    {
        switch(skill)
        {
            case Enums.Skills.Fishing:
                Fishing.AddExperience(experience);
                break;
            case Enums.Skills.Cooking:
                Cooking.AddExperience(experience);
                break;
            case Enums.Skills.Mining:
                Mining.AddExperience(experience);
                break;
            case Enums.Skills.Smithing:
                Smithing.AddExperience(experience);
                break;
        }
    }
}
