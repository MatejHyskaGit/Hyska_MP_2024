using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    private GameObject[] Interactables;
    // Start is called before the first frame update
    void Start()
    {
        Interactables = GameObject.FindGameObjectsWithTag("Interactable");
        //Debug.Log(Interactables);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && !PauseMenuManager.isPaused && !DialogueManager.instance.dialogueIsPlaying && !GameManager.instance.loading)
        {
            if (DiceGameManager.instance != null) if (DiceGameManager.instance.DiceGameOn) return;
            foreach (var inter in Interactables)
            {
                // Debug.Log(MovementManager.instance.VectRound(MovementManager.instance.actualPos, 2));
                // Debug.Log(DirToVect(MovementManager.instance.Direction));
                // Debug.Log(MovementManager.instance.VectRound(MovementManager.instance.actualPos + DirToVect(MovementManager.instance.Direction), 2));
                // Debug.Log(MovementManager.instance.VectRound((Vector2)inter.transform.position, 2));
                if (MovementManager.instance.VectRound(MovementManager.instance.actualPos + DirToVect(MovementManager.instance.Direction), 2) == MovementManager.instance.VectRound((Vector2)inter.transform.position, 2))
                {
                    //Debug.Log("Hey, you interacted with me!!");
                    WhenInteracted interacted = inter.GetComponent<WhenInteracted>();

                    interacted.Interact(inter.name);
                }
            }

        }
    }

    public static Vector2 DirToVect(Direction direction)
    {
        Debug.Log(direction);
        switch (direction)
        {
            case Direction.down:
                return (Vector2.down * MovementManager.instance.gridSize);
            case Direction.right:
                return (Vector2.right * MovementManager.instance.gridSize);
            case Direction.up:
                return (Vector2.up * MovementManager.instance.gridSize);
            case Direction.left:
                return (Vector2.left * MovementManager.instance.gridSize);
            default:
                return new Vector2();
        }
    }
}