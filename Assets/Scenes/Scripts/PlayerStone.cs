using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theDiceRoller = GameObject.FindObjectOfType<DiceRoller>();
    }

    public Tile StartingTile;

    DiceRoller theDiceRoller;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp()
    {
        Debug.Log("Click!");

        // Have we rolled the Dice
        if(!theDiceRoller.IsDoneRolling)
        {
            // Don't move the stone yet
            return;
        }

        int spacesToMove = theDiceRoller.DiceTotal;
        
    }
    
}
