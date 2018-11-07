using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*TODO
Make a seperate script to handle UI

store keys that have been pressed as a keycode
make a corutin for the one second 

In the Script that handles the UI make it pop open a window that shows the damage and requirements 
get this to work with overing rather then clicking
*/
[System.Serializable]
public class Skills
{
    public string skillName;
    public ParticleSystem SkillEffects;
    public float damage;
    public Button skillButton;
    public KeyCode[] key;
    public bool choosenSkill = false;
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
        for (int i = 0; i < skills.Count; i++) //sets the names of the skills
        {
            skills[i].skillText.text = skills[i].skillName; //Sets text to teh skill name
            skills[i].SkillEffects.gameObject.SetActive(false);
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
            if (skills[i].choosenSkill != false) //needs a different check
            {
                for (int j = 0; j < skills[i].key.Length; i++)
                {

                    if (Input.GetKeyDown(skills[i].key[j])) //checks to see what key is pressed
                    {
                       
                    }
                }

            }
        }
    }

    public void BoolCheck(string skillName)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if(skillName == skills[i].skillName)
            {
                skills[i].choosenSkill = true;
            }
        }
    }
    private void OnGUI()
    {
        Event e = Event.current;
        if(e.isKey)
        {
            for(int i = 0; i < skills.Count; i++)
            {
                for (int j = 0; j < skills[i].key.Length; j++)
                {
                    if(skills[i].key[i] == e.keyCode)
                    {
                        skills[i].SkillEffects.gameObject.SetActive(true);
                        skills[i].SkillEffects.Play(true); //plays the partical effect
                    }
                }
            }
            Debug.Log("Key Press: " + e.keyCode);
        }
    }

}
