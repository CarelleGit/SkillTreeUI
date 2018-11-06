using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*TODO
Make a seperate script to handle UI

In the Script that handles the UI make it pop open a window that shows the damage and requirements 
get this to work with overing rather then clicking
*/
[System.Serializable]
public class Skills
{
    public string skillName;
    public ParticleEmitter SkillEffects;
    public float damage;
    public Button skillButton;
    public KeyCode key;
    [Header("Level Requirements")]
    public int level;
    public Button[] requiredSkills;
    [Header("Texts")]
    public Text skillText;
    public Text LevelReqirement;
    public Text Damage;
}

public class SkillTree : MonoBehaviour
{
    public List<Skills> skills;
    [SerializeField]
     LevelingShit leveling;
   
    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < skills.Count; i++) //sets the names of the skills
        {
            skills[i].skillText.text = skills[i].skillName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < skills.Count; i++) //don't make this be checked every frame
        {
            if (leveling.levelNumber >= skills[i].level) //checks to see if the player is of the correct level
            {
                skills[i].skillButton.interactable = true; //make button interactiable if level requarment is met
            }
            else
            {
                skills[i].skillButton.interactable = false; //other wise be false
            }
        }
    }
       
}
