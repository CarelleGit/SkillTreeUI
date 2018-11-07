using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public float health = 10;
    public Text heathText;
    public Text damageTaken;
    public SkillTree skill;

    // Use this for initialization
    void Start()
    {
        heathText.text = "Health: " + health;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < skill.skills.Count; i++)
        {
            if (skill.skills[i].choosenSkill == true)
            {

                //if (Input.GetKeyDown(skill.skills[i].key))
                //{
                //    health -= skill.skills[i].damage;
                //    heathText.text = "Health: " + health;
                //    damageTaken.text = "Delt: " + skill.skills[i].damage;
                //}
            }
        }
    }
}
