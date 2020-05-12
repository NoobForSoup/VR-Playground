using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillData", menuName = "New Skill Data")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public Texture2D image;

    public int level;
    public int experience;

    public void AddExperience(int experience)
    {
        experience += experience;

        //calculate level
    }
}
