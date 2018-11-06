using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelingShit : MonoBehaviour
{
    public Text level;
    public int levelNumber = 0;
    // Use this for initialization
    void Start()
    {
        level.text = "Level: " + levelNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            levelNumber += 1;
            level.text = "Level: " + levelNumber;
        }
    }
}
