using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DiceValues = new int[4];
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    StateManager theStateManager;

    public int[] DiceValues;

    public Sprite[] DiceImageZero;
    public Sprite[] DiceImageOne;

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void RollTheDice() {
        
        if(theStateManager.IsDoneRolling == true)
        {
            //We've already rolled this turn.
            return;
        }
        // Use Randon Number Generation to get the number of rolls in dice

        theStateManager.DiceTotal = 0;

        for (int i = 0; i < DiceValues.Length; i++)
        {
            DiceValues[i] = Random.Range(0, 2);
            theStateManager.DiceTotal += DiceValues[i];

            // Update images of dice w.r.t random number

            if(DiceValues[i] == 0) {
                this.transform.GetChild(i).GetComponent<Image>().sprite = DiceImageZero[Random.Range(0, DiceImageZero.Length)];
            }
            else {
                this.transform.GetChild(i).GetComponent<Image>().sprite = DiceImageOne[Random.Range(0, DiceImageOne.Length)];
            }

            theStateManager.IsDoneRolling = true;
        }

        // Debug.Log("Rolled:" + DiceTotal);
    }
}
