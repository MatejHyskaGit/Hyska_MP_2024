using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int heartCount;
    public string lastScene;
    public Vector3 playerPosition;
    public IDictionary<string, int> dialogueLog;
    public List<ItemIM> ItemList;
    public bool tavernInit;
    public bool statueFixed;
    public bool puzzleOneFinished;
    public List<Vector3> Room6PushPositions;
    public bool itemOneGrabbed;
    public bool M6Init;
    public bool nathanielGone;

    public GameData()
    {
        this.heartCount = 3;
        this.lastScene = "Tavern";
        this.playerPosition = new Vector3((float)-1.04, (float)-1.44, 0);
        this.dialogueLog = new Dictionary<string, int>();
        this.ItemList = new List<ItemIM>();
        this.tavernInit = false;
        this.statueFixed = false;
        this.puzzleOneFinished = false;
        this.Room6PushPositions = new List<Vector3>();
        this.itemOneGrabbed = false;
        this.M6Init = false;
        this.nathanielGone = false;
    }
}
