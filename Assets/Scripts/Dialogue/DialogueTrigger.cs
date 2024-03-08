using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] public TextAsset[] inkJSONArray;

    public int dialogueIndex = 0;

    Vector2 actualPos;
    BoxCollider2D bcollider;

    [SerializeField] private GameObject NpcObject;

    private void Start()
    {
        bcollider = GetComponent<BoxCollider2D>();
        actualPos = (Vector2)transform.position + bcollider.offset;
    }

    public void StartInit()
    {
        if (SceneManager.GetActiveScene().name == "Tavern" && NpcObject.name == "InitDialogue")
        {
            DialogueManager.instance.EnterDialogueMode(inkJSONArray[0]);
        }
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && !PauseMenuManager.isPaused)
        {
            if (DiceGameManager.instance != null) if (DiceGameManager.instance.DiceGameOn) return;
            if (MovementManager.instance.VectRound(MovementManager.instance.actualPos + DirToVect(MovementManager.instance.Direction), 2) == MovementManager.instance.VectRound(actualPos, 2) && !DialogueManager.instance.dialogueIsPlaying)
            {
                Debug.Log("Dialogue Triggered");
                EnterDialogue();
            }
        }
    }

    public void EnterDialogue()
    {
        DialogueManager.instance.EnterDialogueMode(inkJSONArray[dialogueIndex]);
        if (inkJSONArray.Length > dialogueIndex + 1)
        {
            if (inkJSONArray[dialogueIndex + 1] != null) dialogueIndex++;
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