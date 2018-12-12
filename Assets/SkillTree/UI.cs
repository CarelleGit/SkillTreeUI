using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public Text playerLevel;
    public Text playerMana;
    public Text playerAC;
    // Use this for initialization
    void Start()
    {
        playerInfo.playerLevel += UpdateLevelUI;
        playerInfo = GetComponent<PlayerInfo>();
        playerLevel.text = "Level: " + playerInfo.level;
    }
    void UpdateLevelUI(int level)
    {
        playerLevel.text = "Level: " + level;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
