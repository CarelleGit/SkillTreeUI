using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPanel : MonoBehaviour
{
    public Transform panelVector;
    Vector3 tempPostision;
    public float buffer = 0.1f;
    Coroutine move;
    private void Start()
    {
        tempPostision = panelVector.transform.position;
        //StartCoroutine(movement(panelVector, transform.position));
    }
    public void OpenSkillTree()
    {
        OpenSkillTree(true);
    }
    public void CloseSkillPanel()
    {
        OpenSkillTree(false);
    }
      
    void OpenSkillTree(bool panelActivation)
    {
        //Debug.Log(panelActivation);
        if(move == null)
        {
            if (panelActivation) //moves panel into canvas
            {
                move = StartCoroutine(movement(panelVector, transform.position));
            }
            else
            {
                move = StartCoroutine(movement(panelVector, tempPostision));  //move panel away from the canvas
            }
        }
    }
    IEnumerator movement(Transform t, Vector3 position)
    {
        while(Vector3.Distance(t.position,position) >= buffer)
        {
            t.position = Vector3.Lerp(t.position, position, 0.1f);
            yield return null;
        }
        move = null;
        //while not at position
    }
}
