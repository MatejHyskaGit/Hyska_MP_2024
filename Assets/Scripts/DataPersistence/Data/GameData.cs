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

    public GameData()
    {
        this.heartCount = 3;
        this.lastScene = "Tavern";
        this.playerPosition = new Vector3((float)-1.04, (float)-1.44, 0);
        this.dialogueLog = new Dictionary<string, int>();
        this.ItemList = new List<ItemIM>();
    }
}
