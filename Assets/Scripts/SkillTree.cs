using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*TODO
        Add in cool downs
        Make a PlayerInfo script
        replace LevelingShit with PlayerInfo
        Make the Inspector UI look better end easier to navagate
        TEST
        Once Done Comment on how to hook everything up
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
    public Button[] requiredSkill;
    public int level;
    [Header("_|Skill Costs|___")]
    public int manaCost;
    public float coolDown;
    [Header("_|Texts|___")]
    public Text skillText;
    public Text LevelReqirement;
    public Text Damage;
    //[HideInInspector]
    public float cooldownHandle;

}

public class SkillTree : MonoBehaviour
{
    public List<KeyCode> storedKeys;
    public List<Skills> skills;
    [SerializeField]
    LevelingShit leveling;
    [SerializeField]
  

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
            skills[i].cooldownHandle = skills[i].coolDown;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (storedKeys.Count > 0 && !isTimerRunning)
        {
            for (int i = 0; i < skills.Count; i++)
            {
                for (int j = 0; j < skills[i].requiredSkill.Length; j++)
                {
                    if (skills[i].requiredSkill[j].interactable == true && skills[i].requiredSkill.Length >= 1)
                    {
                        if (skills[j].choosenSkill == true && leveling.levelNumber >= skills[i].level - 1)
                        {
                            skills[i].unlock = true;
                        }
                    }
                    else
                    {
                        skills[i].unlock = false;
                    }
                }
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
            }
        }

        for (int i = 0; i < skills.Count; i++)
        {

            if (leveling.levelNumber >= skills[i].level && skills[i].requiredSkill.Length == 0) //checks to see if the player is of the correct level
            {
                int levelinfo = skills[i].level - 1;

                skills[i].skillButton.interactable = true; //make button interactiable if level requarment is met
            }
            else if (skills[i].unlock == true && leveling.levelNumber >= skills[i].level - 1)
            {
                skills[i].skillButton.interactable = true;
            }
            else
            {
                skills[i].skillButton.interactable = false;
            }
        }

        if (isTimerRunning == false)//This does not belong in the for loop, doing so causes it to clear out the storedKeys way to early
        {
            storedKeys.Clear();
        }

    }
    public void BoolCheck(string skillName)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skillName == skills[i].skillName)
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

        for (int i = 0; i < skills.Count; i++)
        {
            if (storedKeys.Count != skills[i].combo.Length) { continue; }

            bool match = true;
            for (int j = 0; j < skills[i].combo.Length; j++)
            {
                if (storedKeys[j] != skills[i].combo[j])
                {
                    match = false;
                    break;
                }
            }
            if (match && skills[i].manaCost <= leveling.manaAmount && skills[i].cooldownHandle > 0)
            {
                StartCoroutine(CoolDown(skills[i].cooldownHandle, skills[i].coolDown));
 

                leveling.manaAmount -= skills[i].manaCost;
                leveling.mana.text = "Mana: " + leveling.manaAmount;

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
    IEnumerator CoolDown(float cooldownHandle, float cooldown)
    {
        while (cooldownHandle > 0)
        {
            
            cooldownHandle -= Time.deltaTime;
            yield return new WaitForSeconds(1);
        }
        if (cooldown <= 0)
        {
            cooldownHandle = cooldown;
        }
    }

}

