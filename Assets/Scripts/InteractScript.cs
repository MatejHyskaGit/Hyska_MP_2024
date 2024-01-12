using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    private GameObject[] Interactables;
    GridMovement MovementScript;
    // Start is called before the first frame update
    void Start()
    {
        Interactables = GameObject.FindGameObjectsWithTag("Interactable");
        Debug.Log(Interactables);
        MovementScript = GetComponent<GridMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        System.Func<KeyCode, bool> inputFunction;
        inputFunction = Input.GetKeyDown;
        if (inputFunction(KeyCode.Space))
        {
            foreach (var inter in Interactables)
            {
                if (MovementScript.VectRound(MovementScript.actualPos + DirToVect(MovementScript.Direction), 2) == MovementScript.VectRound((Vector2)inter.transform.position, 2))
                {
                    Debug.Log("Hey, you interacted with me!!");
                    WhenInteracted interacted = inter.GetComponent<WhenInteracted>();

                    interacted.Interact(inter.name);
                }
            }

        }
    }

    private Vector2 DirToVect(Direction direction)
    {
        Debug.Log(direction);
        switch (direction)
        {
            case Direction.down:
                return (Vector2.down * MovementScript.gridSize);
            case Direction.right:
                return (Vector2.right * MovementScript.gridSize);
            case Direction.up:
                return (Vector2.up * MovementScript.gridSize);
            case Direction.left:
                return (Vector2.left * MovementScript.gridSize);
            default:
                return new Vector2();
        }
    }
}
