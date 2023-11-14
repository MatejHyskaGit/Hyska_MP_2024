using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;


public class Push : MonoBehaviour
{
    [SerializeField]
    private Tilemap groundTilemap;
    [SerializeField]
    private Tilemap collisionTilemap;

    private GameObject[] ObjToPush;
    private bool isMoving;
    private Vector2 origPos, targetPos;
    private float timeToMove = 0.2f;
    private float gridSize = 0.16f;
    BoxCollider2D bcollider;
    // Start is called before the first frame update
    void Start()
    {
        ObjToPush = GameObject.FindGameObjectsWithTag("ObjToPush");
        bcollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        System.Func<KeyCode, bool> inputFunction;
        inputFunction = Input.GetKey;
        if (inputFunction(KeyCode.LeftShift))
        {
            timeToMove = 0.10f;
        }
        if (!isMoving)
        {
            timeToMove = 0.20f;
        }
    }

    public IEnumerator Move(Vector2 direction)
    {
        Debug.Log("Obj should Move");
        if (ObjCanMove(direction))
        {
            isMoving = true;

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

            isMoving = false;
        }
    }

    public bool ObjCanMove(Vector2 direction)
    {
        Vector2 newPos = VectRound((transform.position + (Vector3)bcollider.offset) + (Vector3)(direction * gridSize), 2);
        Vector3Int gridPosition = groundTilemap.WorldToCell(VectRound((transform.position + (Vector3)bcollider.offset) + (Vector3)(direction * gridSize), 2));
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition) || isMoving)
            return false;
        foreach (var objToPush in ObjToPush)
        {
            BoxCollider2D bcolliderpush = objToPush.GetComponent<BoxCollider2D>();
            if (Math.Round(objToPush.transform.position.x + bcolliderpush.offset.x, 2) == Math.Round(newPos.x, 2) && Math.Round(objToPush.transform.position.y + bcolliderpush.offset.y, 2) == Math.Round(newPos.y, 2))
            {
                return false;
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
}
