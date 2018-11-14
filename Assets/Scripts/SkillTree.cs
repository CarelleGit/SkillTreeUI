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
    [Header("_|Next Skill|___")]
    public string skillName;
    public Button skillButton;
    public ParticleSystem SkillEffects;
    public float damage;    
    public KeyCode[] combo;
    [Header("_|Bool|___")]
    public bool unlock;
    public bool choosenSkill = false;
    [Header("_|Level Requirements|___")]
    public int level;
    public Button[] requiredSkill;
   
    [Header("_|Texts|___")]
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
            skills[i].skillButton.interactable = false;
            skills[i].unlock = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (storedKeys.Count > 0 && !isTimerRunning)
        {
            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].choosenSkill != false)
                {

                    if (skills[i].combo[0] == storedKeys[0])
                    {
                        //Debug.Log("Timer started!");
                        isTimerRunning = true;
                        StartCoroutine(KeyReader());
                        break;
                    }
                }
                if (skills[i].requiredSkill.Length == 0)
                {
                    if (leveling.levelNumber >= skills[i].level - 1) //checks to see if the player is of the correct level
                    {
                        skills[i].skillButton.interactable = true; //make button interactiable if level requarment is met
                    }
                    else
                    {
                        skills[i].skillButton.interactable = false; //other wise be false
                    }
                }
                if (skills[i].requiredSkill.Length > 0)
                {
                    skills[i].unlock = true;
                }
               
          

            }
            if (isTimerRunning == false)//This does not belong in the for loop, doing so causes it to clear out the storedKeys way to early
            {
                storedKeys.Clear();
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
                if (!skills[i].SkillEffects.gameObject.activeInHierarchy)
                {
                    skills[i].SkillEffects.gameObject.SetActive(true);
                }
                else
                {
                    skills[i].SkillEffects.Play();
                }
                break;
            }
        }
        storedKeys.Clear();
        //Debug.Log("Timer stopped!");
        isTimerRunning = false;
    }
}
