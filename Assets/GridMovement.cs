using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.SceneManagement;

public class GridMovement : MonoBehaviour
{
    [SerializeField]
    private Tilemap groundTilemap;
    [SerializeField]
    private Tilemap collisionTilemap;

    private GameObject[] ObjToPush;
    private GameObject[] Triggers;
    private GameObject[] SpawnPoints;
    public Direction Direction = Direction.down;
    private bool isMoving;
    private Vector2 origPos, targetPos;
    private float timeToMove = 0.20f;
    public float gridSize = 0.16f;
    public Vector2 actualPos;
    BoxCollider2D bcollider;
    [SerializeField]
    public Animator animator;
    public Indestructible script;
    // Start is called before the first frame update
    void Start()
    {

        ObjToPush = GameObject.FindGameObjectsWithTag("ObjToPush");
        Debug.Log(ObjToPush);
        Triggers = GameObject.FindGameObjectsWithTag("Trigger");
        Debug.Log(Triggers);
        bcollider = GetComponent<BoxCollider2D>();
        Debug.Log(bcollider);
        SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        Debug.Log(SpawnPoints);
        script = GameObject.Find("Hugo").GetComponent<Indestructible>();
        MovePlayerToSpawn(script.lastSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        actualPos = (Vector2)transform.position + bcollider.offset;
        System.Func<KeyCode, bool> inputFunction;
        inputFunction = Input.GetKey;

        if (!isMoving)
        {
            if (inputFunction(KeyCode.LeftShift))
            {
                timeToMove = 0.10f;
            }
            else
            {
                timeToMove = 0.20f;
            }

            if (inputFunction(KeyCode.W))
            {
                Direction = Direction.up;
                StartCoroutine(Move(Vector2.up));
            }
            else if (inputFunction(KeyCode.A))
            {
                Direction = Direction.left;
                StartCoroutine(Move(Vector2.left));
            }
            else if (inputFunction(KeyCode.S))
            {
                Direction = Direction.down;
                StartCoroutine(Move(Vector2.down));
            }
            else if (inputFunction(KeyCode.D))
            {
                Direction = Direction.right;
                StartCoroutine(Move(Vector2.right));
            }
        }
    }

    private IEnumerator Move(Vector2 direction)
    {
        animator.SetFloat("Direction", (float)Direction);
        if (CanMove(direction))
        {
            isMoving = true;
            animator.SetBool("isMoving", true);

            float elapsedTime = 0f;
            origPos = transform.position;
            targetPos = VectRound(origPos + (direction * gridSize), 2);

            while (elapsedTime < timeToMove)
            {
                transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPos;
            foreach (var trigger in Triggers)
            {
                Debug.Log(trigger.transform.position.x);
                Debug.Log(trigger.transform.position.y);
                if (Math.Round((transform.position.x + bcollider.offset.x), 2) == Math.Round(trigger.transform.position.x, 2) && Math.Round((transform.position.y + bcollider.offset.y), 2) == Math.Round(trigger.transform.position.y, 2))
                {
                    Debug.Log("trig out");
                    TriggerScript triggerscr = trigger.GetComponent<TriggerScript>();

                    triggerscr.Trigger(trigger.name);
                }
            }

            isMoving = false;
            animator.SetBool("isMoving", false);
        }
    }

    private bool CanMove(Vector2 direction)
    {
        //Debug.Log(bcollider.offset);
        Vector2 newPos = VectRound((transform.position + (Vector3)bcollider.offset) + (Vector3)(direction * gridSize), 2);
        Vector3Int gridPosition = groundTilemap.WorldToCell(VectRound((transform.position + (Vector3)bcollider.offset) + (Vector3)(direction * gridSize), 2));
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition) || isMoving)
            return false;
        foreach (var objToPush in ObjToPush)
        {
            BoxCollider2D bcolliderpush = objToPush.GetComponent<BoxCollider2D>();
            Debug.Log(newPos);
            Debug.Log(objToPush.transform.position + (Vector3)bcolliderpush.offset);
            if (Math.Round(objToPush.transform.position.x + bcolliderpush.offset.x, 2) == Math.Round(newPos.x, 2) && Math.Round(objToPush.transform.position.y + bcolliderpush.offset.y, 2) == Math.Round(newPos.y, 2))
            {
                timeToMove = 0.20f;
                Debug.Log("Hello");
                Push objpush = objToPush.GetComponent<Push>();
                Debug.Log(objpush);
                if (objpush && objpush.ObjCanMove(direction))
                {
                    Debug.Log("Hey Move pls");
                    StartCoroutine(objpush.Move(direction));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
    public Vector2 VectRound(Vector2 vector, int digits)
    {
        Vector2 vectrounded = new Vector2();
        vectrounded.x = (float)Math.Round(vector.x, digits);
        vectrounded.y = (float)Math.Round(vector.y, digits);
        return vectrounded;
    }

    public void MovePlayerToSpawn(string lastSceneName)
    {
        Scene scene = SceneManager.GetActiveScene();
        foreach (var spawnPoint in SpawnPoints)
        {
            if (scene.name == "MalirRoom1")
            {
                if (lastSceneName == "MalirRoom2")
                {
                    if (script.pos == "up")
                    {
                        if (spawnPoint.name == "SpawnPoint2")
                        {
                            transform.position = spawnPoint.transform.position - (Vector3)bcollider.offset;
                        }
                    }
                    else if (script.pos == "down")
                    {
                        if (spawnPoint.name == "SpawnPoint2d")
                        {
                            transform.position = spawnPoint.transform.position - (Vector3)bcollider.offset;
                        }
                    }
                }
                else if (lastSceneName == "Room1")
                {
                    if (script.pos == "up")
                    {
                        if (spawnPoint.name == "SpawnPoint1")
                        {
                            transform.position = spawnPoint.transform.position - (Vector3)bcollider.offset;
                        }
                    }
                    else if (script.pos == "down")
                    {
                        if (spawnPoint.name == "SpawnPoint1d")
                        {
                            transform.position = spawnPoint.transform.position - (Vector3)bcollider.offset;
                        }
                    }
                }
            }
            if (scene.name == "MalirRoom2")
            {
                if (script.pos == "up")
                {
                    if (spawnPoint.name == "SpawnPoint1")
                    {
                        transform.position = spawnPoint.transform.position - (Vector3)bcollider.offset;
                    }
                }
                else if (script.pos == "down")
                {
                    if (spawnPoint.name == "SpawnPoint1d")
                    {
                        transform.position = spawnPoint.transform.position - (Vector3)bcollider.offset;
                    }
                }
            }
        }
    }
}




public enum Direction
{
    down = 0,
    right = 1,
    up = 2,
    left = 3
}