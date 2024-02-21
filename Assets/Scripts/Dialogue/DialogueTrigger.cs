using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    GameObject PlayerObject;

    GridMovement MovementScript;

    Vector2 actualPos;
    BoxCollider2D bcollider;

    private void Start()
    {
        PlayerObject = GameObject.Find("Player");
        MovementScript = PlayerObject.GetComponent<GridMovement>();
        bcollider = GetComponent<BoxCollider2D>();
        actualPos = (Vector2)transform.position + bcollider.offset;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (MovementScript.VectRound(MovementScript.actualPos + DirToVect(MovementScript.Direction), 2) == MovementScript.VectRound(actualPos, 2) && !DialogueManager.instance.dialogueIsPlaying)
            {
                Debug.Log("Dialogue Triggered");
                DialogueManager.instance.EnterDialogueMode(inkJSON);
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
