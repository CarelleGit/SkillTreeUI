using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*TODO
        Make a UI Script
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
    public int defence;
    public KeyCode[] combo;
    [Header("_|Bool|___")]
    public bool unlockable = false;
    public bool unlockedSkill = false;
   
    [Header("_|Skill Costs|___")]
    public int manaCost;
    public float coolDown;
    [Header("_|Texts|___")]
    public Text skillText;
    public Text LevelReqirement;
    public Text Damage;
    public Text cooldown;
    [HideInInspector]
    public float cooldownHandle;
    [Header("_|Level Requirements|___")]
    public Button[] requiredSkill;
    public int level;
}

public class SkillTree : MonoBehaviour
{
    public List<KeyCode> storedKeys;
    public List<Skills> skills;
    [SerializeField]
    PlayerInfo leveling;
    [SerializeField]
    bool isTimerRunning = false;


    // Use this for initialization
    void Start()
    {
        leveling.playerLevel += LevelUp;
        leveling = GetComponent<PlayerInfo>();
        for (int i = 0; i < skills.Count; i++) //sets the names of the skills
        {
            skills[i].skillText.text = skills[i].skillName; //Sets text to teh skill name
            skills[i].SkillEffects.gameObject.SetActive(false);
            skills[i].skillButton.interactable = false;
            skills[i].cooldownHandle = skills[i].coolDown;
        }
    }
    void LevelUp(int level)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            for (int j = 0; j <= skills[i].requiredSkill.Length; j++)
            {
                if ((skills[i].requiredSkill.Length >= 1) && skills[j].unlockedSkill)
                {
                    skills[i].unlockable = true;
                }
            }

            if (skills[i].level <= level && skills[i].requiredSkill.Length <= 0)
            {
                skills[i].skillButton.interactable = true;
            }
            else if (skills[i].level <= level && skills[i].requiredSkill.Length >= 1)
            {
                UnlockedSkills(level, i);
                //if (skills[i].unlockable)
                //{
                //    skills[i].skillButton.interactable = true;
                //}
            }
            else
            {
                skills[i].unlockable = false;
                skills[i].skillButton.interactable = false;
            }

        }
        //for (int i = 0; i < skills.Count; i++)
        //{
        //    for (int j = 0; j < skills[i].requiredSkill.Length; j++)
        //    {
        //        if (skills[i].requiredSkill[j].interactable == true && skills[i].requiredSkill.Length >= 1)
        //        {
        //            if (skills[j].unlockedSkill == true && level >= skills[i].level - 1)
        //            {
        //                skills[i].skillButton.interactable = true;
        //                break;
        //            }
        //        }
        //        else
        //        {
        //            skills[i].unlockable = false;
        //        }
        //    }
        //    if (leveling.level >= skills[i].level)
        //    {
        //        skills[i].unlockable = true;
        //    }
        //    if (skills[i].unlockable && skills[i].requiredSkill.Length == 0) //checks to see if the player is of the correct level
        //    {
        //        skills[i].skillButton.interactable = true; //make button interactiable if level requarment is met
        //    }
        //    else
        //    {
        //        skills[i].skillButton.interactable = false;
        //    }
        //    if (skills[i].unlockable)
        //    {
        //        skills[i].skillButton.interactable = true;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (storedKeys.Count > 0 && !isTimerRunning)
        {
            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].unlockedSkill != false)
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
            if (!isTimerRunning)
            {
                storedKeys.Clear();
            }
        }


    }
    public void BoolCheck(string skillName)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].requiredSkill.Length >= 1 && skills[i].level <= leveling.level)
            {
                for (int j = 0; j <= skills[i].requiredSkill.Length; j++)
                {
                    if (skills[j].unlockedSkill)
                    {
                        skills[i].skillButton.interactable = true;
                        skills[i].unlockable = true;
                        Debug.Log(skills[j].skillName + " : " + skills[i].skillName);
                    }
                }
            }
            if (skillName == skills[i].skillName)
            {
                skills[i].unlockedSkill = true;

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
        Debug.Log("Key reader started.");
        yield return new WaitForSeconds(1);

        for (int i = 0; i < skills.Count; ++i)
        {
            if (storedKeys.Count == skills[i].combo.Length)
            {
                bool match = true;
                for (int j = 0; j < skills[i].combo.Length; j++)
                {
                    if (storedKeys[j] != skills[i].combo[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match && skills[i].manaCost <= leveling.manaAmount && skills[i].cooldownHandle >= skills[i].coolDown)
                {
                    StartCoroutine(CoolDown(i));
                    leveling.manaAmount -= skills[i].manaCost;
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
        }

        storedKeys.Clear();
        Debug.Log("Timer stopped!");
        isTimerRunning = false;
    }
    IEnumerator CoolDown(int i)
    {
        while (skills[i].cooldownHandle > 0)
        {
            skills[i].cooldownHandle -= 1;
            skills[i].cooldown.text = "Cool Down: " + skills[i].cooldownHandle;
            yield return new WaitForSeconds(1);

        }
        skills[i].cooldownHandle = skills[i].coolDown;

    }
    void UnlockedSkills(int level, int i)
    {

        for (int j = 0; j <= skills[i].requiredSkill.Length; j++)
        {
            if (skills[j].unlockedSkill && (skills[i].level <= level))
            {
                skills[i].skillButton.interactable = true;
            }
        }

    }
}

