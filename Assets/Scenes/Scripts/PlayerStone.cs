using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();

        targetPosition = this.transform.position;
    }

    public Tile StartingTile;
    Tile currentTile;

    bool scoreMe = false;

    StateManager theStateManager;

    Tile[] moveQueue;
    int moveQueueIndex;

    bool isAnimating = false;

    Vector3 targetPosition;
    Vector3 velocity = Vector3.zero;
    float smoothTime = 0.25f;
    float smoothTimeVertical = 0.1f;
    float smoothDistance = 0.01f;
    float smoothHeight = 0.5f;
    
    // Update is called once per frame
    void Update()
    {
        
        if(isAnimating == false)
        {
            //Nothing for us to do.
            return;
        }

        if(Vector3.Distance(new Vector3(this.transform.position.x, targetPosition.y, this.transform.position.z), targetPosition) < smoothDistance)
        {
            // We've reached the target.
            
            if(moveQueue != null && moveQueueIndex == moveQueue.Length && this.transform.position.y > smoothDistance)
            {
                this.transform.position = Vector3.SmoothDamp(
                 this.transform.position, 
                 new Vector3(this.transform.position.x, 0, this.transform.position.z), 
                 ref velocity, 
                 smoothTimeVertical);
            }
            else
            {
                // Right position, Right height
                AdvanceMoveQueue();
            }
        }
        else if (this.transform.position.y < (smoothHeight - smoothDistance))
        {
            this.transform.position = Vector3.SmoothDamp(
                this.transform.position, 
                new Vector3(this.transform.position.x, smoothHeight, this.transform.position.z), 
                ref velocity, 
                smoothTimeVertical);
        }
        else
        { 
            this.transform.position = Vector3.SmoothDamp(
                this.transform.position, 
                new Vector3(targetPosition.x, smoothHeight, targetPosition.z), 
                ref velocity, 
                smoothTime);
        }
    }

    void AdvanceMoveQueue()
    {
        if(moveQueue != null && moveQueueIndex < moveQueue.Length)
            {   
                Tile nextTile = moveQueue[moveQueueIndex];
                if(nextTile == null)
                {
                    //scored
                    //TODO: Move us to the scored pile.
                    SetNewTargetPosition(this.transform.position + Vector3.right*10f);
                }
                else
                {
                    SetNewTargetPosition(nextTile.transform.position);
                    moveQueueIndex++;
                }
            }
            else
            {
                Debug.Log("Done animating");

                this.isAnimating = false;
                theStateManager.IsDoneAnimating = true;
            }
    }

    void SetNewTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
        velocity = Vector3.zero;
    }

    void OnMouseUp()
    {

        // Have we rolled the Dice
        if(!theStateManager.IsDoneRolling)
        {
            // Don't move the stone yet
            return;
        }

        if(theStateManager. IsDoneClicking == true)
        {
            // We've already done the move.
            return;
        }

        int spacesToMove = theStateManager.DiceTotal;

        if(spacesToMove == 0)
        {
            return;
        }

        // Move stones to tiles

        moveQueue = new Tile[spacesToMove];

        Tile finalTile = currentTile;

        for (int i = 0; i < spacesToMove; i++)
        {
            if(finalTile == null && scoreMe == false)
            {
                finalTile = StartingTile;
            }
            else
            {
                if(finalTile.NextTiles == null || finalTile.NextTiles.Length == 0)
                {
                    // We've reached the end.
                    //Debug.Log("SCORE!");
                    //Destroy(gameObject);
                    //return;
                    scoreMe = true;
                    finalTile = null;
                }
                else if (finalTile.NextTiles.Length == 1)
                {
                    // TODO: Branch based on player id.
                    finalTile = finalTile.NextTiles[0];
                }
                else
                {
                    finalTile = finalTile.NextTiles[0];
                }
            }

            moveQueue[i] = finalTile;
        }

        // TODO: Check to see if the destination is legal.

        moveQueueIndex = 0;
        currentTile = finalTile;
        theStateManager.IsDoneClicking = true;
        this.isAnimating = true;
    }
    
}
