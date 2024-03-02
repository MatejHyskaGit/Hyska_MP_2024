using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    GameObject PlayerObject;


    Vector2 actualPos;
    BoxCollider2D bcollider;

    private void Start()
    {
        PlayerObject = GameObject.Find("Player");
        bcollider = GetComponent<BoxCollider2D>();
        actualPos = (Vector2)transform.position + bcollider.offset;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (MovementManager.instance.VectRound(MovementManager.instance.actualPos + DirToVect(MovementManager.instance.Direction), 2) == MovementManager.instance.VectRound(actualPos, 2) && !DialogueManager.instance.dialogueIsPlaying)
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