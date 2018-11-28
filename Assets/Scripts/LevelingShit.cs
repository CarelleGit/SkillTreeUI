using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelingShit : MonoBehaviour
{
    public Text level;
    public Text mana;
    public int levelNumber = 0;
    public int manaAmount = 0;
    // Use this for initialization
    void Start()
    {
        mana.text = "Mana: " + manaAmount;
        level.text = "Level: " + levelNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.L))
        {
            levelNumber += 1;
            level.text = "Level: " + levelNumber;
        }
        if (Input.GetKey(KeyCode.M)) 
        {
            manaAmount++;
            mana.text = "Mana: " + manaAmount;
        }
    }
}
