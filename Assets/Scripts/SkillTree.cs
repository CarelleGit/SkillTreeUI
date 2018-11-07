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
    public KeyCode[] combo;
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
    public List<KeyCode> storedKeys;
    public List<Skills> skills;
    [SerializeField]
    LevelingShit leveling;

    bool isTimerRunning = false;

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


        if (storedKeys.Count > 0 && !isTimerRunning)
        {

            for (int i = 0; i < skills.Count; i++)
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
                    if (skills[i].combo[0] == storedKeys[0])
                    {
                        //Debug.Log("Timer started!");
                        isTimerRunning = true;
                        StartCoroutine(KeyReader());
                        break;
                    }
                }

                if (isTimerRunning == false)
                {
                    storedKeys.Clear();
                }
            }
        }

        /*
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
                for (int j = 0; j < skills[i].combo.Length; j++)
                {
                    if (Input.GetKeyDown(skills[i].combo[j])) //checks to see what key is pressed
                    {
                        StartCoroutine(KeyReader());
                    }
                }

            }
        }
        */
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
        if (e.isKey && Input.GetKeyDown(e.keyCode))
        {
            //Debug.Log("Key Press: " + e.keyCode);
            storedKeys.Add(e.keyCode);
        }
    }
    IEnumerator KeyReader()
    {
        yield return new WaitForSeconds(1);

        for(int i = 0; i < skills.Count; i++)
        {
            if(storedKeys.Count != skills[i].combo.Length) { continue; }

            bool match = true;
            for(int j = 0; j < skills[i].combo.Length; j++)
            {
                if(storedKeys[j] != skills[i].combo[j])
                {
                    match = false;
                    break;
                }
            }

            if(match)
            {
                Debug.Log(skills[i].skillName);
                break;
            }
        }

        storedKeys.Clear();
        //Debug.Log("Timer stopped!");
        isTimerRunning = false;
    }
}
