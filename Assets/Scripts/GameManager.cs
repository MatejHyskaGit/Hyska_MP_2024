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
    public int VolumeSFX;

    [NonSerialized]
    public int VolumeMusic;

    public List<Item> ItemListGM { get; set; } = new List<Item> { };

    [NonSerialized]
    public bool InitDialogue = false;

    [SerializeField] private Animator ItemGetAnimator;

    [SerializeField] private Image ItemImage;

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

    [NonSerialized]
    public bool justLoaded = false;

    [NonSerialized]
    public bool malirroom1item = false;

    [SerializeField]
    private GameObject heartCanvas;

    public List<Vector3> Room6PushPositions { get; set; } = new List<Vector3>();

    private Transform[] pushobjs;

    [NonSerialized]
    public bool InitDialogueM6 = false;

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

        Room6PushPositions = new List<Vector3> { };

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.sceneLoaded += OnSceneLoaded;
        lastSceneName = SceneManager.GetActiveScene().name;
        VolumeSFX = PlayerPrefs.GetInt("SFXVol", 5);
        VolumeMusic = PlayerPrefs.GetInt("MusicVol", 5);

        PlayerPrefs.GetInt("SFXVol");
        puzzleOneIsFinished = false;
        updateHearts();
        AudioManager.Instance.PlayMusic("menuMusic");


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
        if (SceneManager.GetActiveScene().name == "Ending") heartCanvas.SetActive(false);
        else heartCanvas.SetActive(true);

        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu": AudioManager.Instance.PlayMusic("menuMusic"); light2D.GetComponent<Light2D>().intensity = 0.035f; break;
            case "Tavern": AudioManager.Instance.PlayMusic("tavernMusic"); RoomInitializer.instance.Initialize(); break;
            case "MalirRoom1": if (instance.lastSceneName == "Tavern" || instance.lastSceneName == "Menu") AudioManager.Instance.PlayMusic("menuMusic"); light2D.GetComponent<Light2D>().intensity = 0.4f; RoomInitializer.instance.Initialize(); GameObject push = GameObject.Find("Pushable"); if (instance.malirroom1item && push) push.transform.position = new Vector3((float)-0.88, (float)-0.758, 0); break;
            case "MalirRoom3": if (instance.lastSceneName == "MalirRoom2" || instance.lastSceneName == "Menu") AudioManager.Instance.PlayMusic("basementMusic"); light2D.GetComponent<Light2D>().intensity = 0f; RoomInitializer.instance.Initialize(); CrossFadeLoadScript.SceneUpdate(); break;
            case "MalirRoom4": if (instance.lastSceneName == "MalirRoom5" || instance.lastSceneName == "Menu") AudioManager.Instance.PlayMusic("basementMusic"); break;
            case "MalirRoom5": if (instance.lastSceneName == "MalirRoom4" || instance.lastSceneName == "Menu") AudioManager.Instance.PlayMusic("menuMusic"); break;
            case "MalirRoom6":
                GameObject parent = GameObject.Find("MALIR_6"); pushobjs = parent.GetComponentsInChildren<Transform>(); Debug.Log(pushobjs); if (Room6PushPositions != null)
                {
                    for (int i = 0; i < Room6PushPositions.Count - 1; i++)
                    {
                        pushobjs[i].position = Room6PushPositions[i];
                    }
                }
                break;
            default: light2D.GetComponent<Light2D>().intensity = 0.1f; break;
        }
    }

    public void LoadScene(string sceneName)
    {
        lastSceneName = SceneManager.GetActiveScene().name;

        if (SceneManager.GetActiveScene().name == "MalirRoom6")
        {
            if (Room6PushPositions.Count == 0)
            {
                for (int i = 0; i < pushobjs.Length - 1; i++)
                {
                    Room6PushPositions.Add(pushobjs[i].position);
                }
            }
            for (int i = 0; i < pushobjs.Length - 1; i++)
            {
                Room6PushPositions[i] = pushobjs[i].position;
            }
        }
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
        ItemListGM.Add(item);
        ItemImage.sprite = item.Icon;
        ItemGetAnimator.Play("GetItem");
        AudioManager.Instance.PlaySound("itemGet");
    }

    public void GetHeart()
    {
        Sprite hp = Resources.Load<Sprite>("Sprites/hp");
        ItemImage.sprite = hp;
        heartCount++;
        updateHearts();
        ItemGetAnimator.Play("GetItem");
        AudioManager.Instance.PlaySound("itemGet");
    }

    public void LoseHeart()
    {
        heartCount--;
        updateHearts();
        ItemGetAnimator.Play("LoseHeart");
        AudioManager.Instance.PlaySound("lifeLose");
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
        this.ItemListGM = new List<Item>();
        this.InitDialogue = data.tavernInit;
        foreach (ItemIM item in data.ItemList)
        {
            Sprite sprite = Resources.Load<Sprite>("Sprites/" + item.IconName);
            this.ItemListGM.Add(new Item() { Name = item.Name, Icon = sprite, Description = item.Description });
        }
        statueFixed = data.statueFixed;
        puzzleOneIsFinished = data.puzzleOneFinished;
        Room6PushPositions = data.Room6PushPositions;
        malirroom1item = data.itemOneGrabbed;
        InitDialogueM6 = data.M6Init;
    }

    public void SaveData(ref GameData data)
    {
        data.heartCount = this.heartCount;
        data.lastScene = SceneManager.GetActiveScene().name;
        data.dialogueLog = this.dialogueIndex;
        data.ItemList = new List<ItemIM>();
        data.tavernInit = InitDialogue;
        foreach (Item item in this.ItemListGM)
        {
            data.ItemList.Add(new ItemIM() { Name = item.Name, IconName = item.Icon.name, Description = item.Description });
        }
        data.statueFixed = statueFixed;
        data.puzzleOneFinished = puzzleOneIsFinished;


        if (SceneManager.GetActiveScene().name == "MalirRoom6")
        {
            if (Room6PushPositions.Count == 0)
            {
                for (int i = 0; i < pushobjs.Length - 1; i++)
                {
                    Room6PushPositions.Add(pushobjs[i].position);
                }
            }
            for (int i = 0; i < pushobjs.Length - 1; i++)
            {
                Room6PushPositions[i] = pushobjs[i].position;
            }
        }
        data.Room6PushPositions = Room6PushPositions;
        data.itemOneGrabbed = malirroom1item;
        data.M6Init = InitDialogueM6;
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