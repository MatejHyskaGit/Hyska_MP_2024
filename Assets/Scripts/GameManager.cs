using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;


public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager instance { get; private set; }
    [NonSerialized]
    public string lastSceneName;
    [NonSerialized]
    public string pos = "";
    public Animator transition;
    [NonSerialized]
    public float transitionTime = 1f;
    [SerializeField] private GameObject light2D;

    [NonSerialized]
    public bool loading;

    [NonSerialized]

    public List<string> Saves = new List<string> { "data1.game", "data2.game", "data3.game", "data4.game" };

    [NonSerialized]
    public int diceGameCount = 0;

    [NonSerialized]
    public int Volume;

    public List<Item> ItemListGM { get; set; } = new List<Item> { };

    [NonSerialized]
    public bool InitDialogue = false;

    [SerializeField] private Animator ItemGetAnimator;

    [SerializeField] private Image ItemImage;

    [SerializeField] private TextMeshProUGUI plusText;

    [SerializeField] private Sprite HeartSprite;

    [NonSerialized]
    public bool statueFixed;

    [NonSerialized]
    public bool puzzleOneIsFinished;

    public IDictionary<string, int> dialogueIndex = new Dictionary<string, int>();

    [NonSerialized]
    public bool NathanielGone = false;

    [NonSerialized]
    public bool gettingItem = false;

    [NonSerialized]
    public int heartCount = 3;

    [SerializeField] private Image[] heartImages;

    public string loadedScene = "Tavern";

    private Vector3 loadedPlayerPos;

    private bool justLoaded = false;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        ItemListGM = new List<Item> { };

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.sceneLoaded += OnSceneLoaded;
        lastSceneName = SceneManager.GetActiveScene().name;
        Volume = 5;
        puzzleOneIsFinished = false;
        updateHearts();
    }

    public void WriteIndex(string objName, int indexNum)
    {
        Debug.Log(objName);
        Debug.Log(indexNum);
        if (dialogueIndex.ContainsKey(objName))
        {
            dialogueIndex[objName] = indexNum;
        }
        else
        {
            dialogueIndex.Add(objName, indexNum);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        updateHearts();
        if (MovementManager.instance != null && justLoaded)
        {
            MovementManager.instance.gameObject.transform.position = loadedPlayerPos;
            justLoaded = false;
        }
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu": light2D.GetComponent<Light2D>().intensity = 0.035f; break;
            case "Tavern": RoomInitializer.instance.Initialize(); break;
            case "MalirRoom1": light2D.GetComponent<Light2D>().intensity = 0.4f; RoomInitializer.instance.Initialize(); break;
            case "MalirRoom3": light2D.GetComponent<Light2D>().intensity = 0f; RoomInitializer.instance.Initialize(); CrossFadeLoadScript.SceneUpdate(); break;
            default: light2D.GetComponent<Light2D>().intensity = 0.1f; break;
        }
    }

    public void LoadScene(string sceneName)
    {
        lastSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadLevel(sceneName));
    }
    public void LoadScene(string sceneName, string position)
    {
        lastSceneName = SceneManager.GetActiveScene().name;
        pos = position;
        //SceneManager.LoadScene(sceneName);
        StartCoroutine(LoadLevel(sceneName));
        //Debug.Log("Happem");
        //MovementScript = GameObject.Find("Player").GetComponent<GridMovement>();
        //MovementScript.movementAllowed = true;
    }


    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
        transition.SetTrigger("End");
    }

    public void AddItem(Item item)
    {
        plusText.text = "+";
        ItemListGM.Add(item);
        ItemImage.sprite = item.Icon;
        ItemGetAnimator.Play("GetItem");
    }

    public void GetHeart()
    {
        plusText.text = "+";
        Sprite hp = Resources.Load<Sprite>("Sprites/hp");
        ItemImage.sprite = hp;
        heartCount++;
        updateHearts();
        ItemGetAnimator.Play("GetItem");
    }

    public void LoseHeart()
    {
        plusText.text = "-";
        heartCount--;
        updateHearts();
        ItemGetAnimator.Play("LoseHeart");
    }

    public void RemoveItem(Item item)
    {
        int removedindex = ItemListGM.IndexOf(item);
        Debug.Log(removedindex);
        ItemListGM.Remove(item);
        MoveAll(removedindex);

    }
    private void MoveAll(int index)
    {
        if (ItemListGM.Count > index + 1)
        {
            if (ItemListGM[index + 1] == null) return;
            else
            {
                ItemListGM[index] = ItemListGM[index + 1];
                MoveAll(index + 1);
            }
        }
        return;
    }

    public void LoadData(GameData data)
    {
        this.heartCount = data.heartCount;
        this.loadedScene = data.lastScene;
        loadedPlayerPos = data.playerPosition;
        justLoaded = true;
        this.dialogueIndex = data.dialogueLog;
        //this.ItemListGM = data.ItemList;
    }

    public void SaveData(ref GameData data)
    {
        data.heartCount = this.heartCount;
        data.lastScene = SceneManager.GetActiveScene().name;
        data.dialogueLog = this.dialogueIndex;
        //data.ItemList = this.ItemListGM;
    }

    private void updateHearts()
    {
        Sprite transparentSprite = Resources.Load<Sprite>("Sprites/Transparent");
        Sprite emptyHeartSprite = Resources.Load<Sprite>("Sprites/emptyHeart");
        Sprite fullHeartSprite = Resources.Load<Sprite>("Sprites/fullHeart");
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            foreach (Image image in heartImages)
            {
                image.enabled = false;
                //image.sprite = transparentSprite;
            }
        }
        else
        {
            foreach (Image image in heartImages)
            {
                image.enabled = true;
                //image.sprite = transparentSprite;
            }
            heartImages[3].sprite = transparentSprite;
            for (int i = 0; i < 3; i++)
            {
                heartImages[i].sprite = emptyHeartSprite;
            }
            for (int i = 0; i < heartCount; i++)
            {
                heartImages[i].sprite = fullHeartSprite;
            }
        }

    }
}