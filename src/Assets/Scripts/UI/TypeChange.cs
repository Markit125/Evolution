using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeChange : MonoBehaviour
{
    public Button b1, b2;
    public void ChangeType1()
    {
        GetComponent<Set>().t = true;
        GetComponent<Set>().Copy();
        GetComponent<Set>().StartData();
        b1.enabled = false;
        b2.enabled = true;
        b1.GetComponentInChildren<Text>().fontSize = 35;
        b2.GetComponentInChildren<Text>().fontSize = 25;
    }
    public void ChangeType2()
    {
        GetComponent<Set>().t = false;
        GetComponent<Set>().Copy();
        GetComponent<Set>().StartData();
        b2.enabled = false;
        b1.enabled = true;
        b1.GetComponentInChildren<Text>().fontSize = 25;
        b2.GetComponentInChildren<Text>().fontSize = 35;
    }
}
