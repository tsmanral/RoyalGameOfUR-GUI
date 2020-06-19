using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceTotalDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theDiceRoller = GameObject.FindObjectOfType<DiceRoller>();
    }

    DiceRoller theDiceRoller;

    // Update is called once per frame
    void Update()
    {
        if(theDiceRoller.doneRolling == false) {
            GetComponent<Text>().text = "=?";
        }
        else {      
            GetComponent<Text>().text = "= " + theDiceRoller.DiceTotal;
        }
    }

}
