using System;
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
    Vector2[] actualPositions;
    BoxCollider2D[] bcolliders;

    [SerializeField] private GameObject NpcObject;

    private void Start()
    {
        //bcollider = GetComponent<BoxCollider2D>();
        //actualPos = (Vector2)transform.position + bcollider.offset;

        bcolliders = GetComponents<BoxCollider2D>();
        actualPositions = new Vector2[bcolliders.Length];
        int index = 0;
        foreach (var collider in bcolliders)
        {
            actualPositions[index] = (Vector2)transform.position + collider.offset;
            index++;
        }

    }

    public void StartInit()
    {
        if (SceneManager.GetActiveScene().name == "Tavern" && NpcObject.name == "InitDialogue" && !GameManager.instance.InitDialogue)
        {
            GameManager.instance.InitDialogue = true;
            DialogueManager.instance.EnterDialogueMode(inkJSONArray[0]);
        }
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && !PauseMenuManager.isPaused)
        {
            if (DiceGameManager.instance != null) if (DiceGameManager.instance.DiceGameOn) return;
            foreach (Vector2 pos in actualPositions)
            {
                Debug.Log(pos);
                Debug.Log(MovementManager.instance.VectRound(MovementManager.instance.actualPos + DirToVect(MovementManager.instance.Direction), 2));
                if ((MovementManager.instance.VectRound(MovementManager.instance.actualPos + DirToVect(MovementManager.instance.Direction), 2) == MovementManager.instance.VectRound(pos, 2)) && !DialogueManager.instance.dialogueIsPlaying)
                {
                    Debug.Log("Dialogue Triggered");
                    EnterDialogue();
                }
            }

        }
    }

    public void EnterDialogue()
    {
        if (GameManager.instance.loading) return;

        DialogueManager.instance.EnterDialogueMode(inkJSONArray[dialogueIndex]);
        if (inkJSONArray.Length > dialogueIndex + 1)
        {
            if (inkJSONArray[dialogueIndex + 1] != null) dialogueIndex++;
        }
        GameManager.instance.WriteIndex(NpcObject.name, dialogueIndex);
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