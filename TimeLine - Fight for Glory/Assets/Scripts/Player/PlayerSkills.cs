using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private List<Skill> skills = new List<Skill>();
    public void OnButtonPressed(Skill skill)
    {
        skills.Find(a => skill).UpgradeSkill();
    }
}
