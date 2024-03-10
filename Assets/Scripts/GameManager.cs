using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public string lastSceneName;
    public string pos = "";
    public Animator transition;
    public float transitionTime = 1f;
    [SerializeField] private GameObject light2D;

    public bool loading;

    public List<bool> Saves = new() { false, false, true, false };

    public int diceGameCount = 0;

    public int Volume;

    public List<Item> ItemListGM { get; set; } = new List<Item> { };

    public bool InitDialogue { get; set; }

    [SerializeField] private Animator ItemGetAnimator;

    [SerializeField] private Image ItemGetImage;


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
        
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu": light2D.GetComponent<Light2D>().intensity = 0.035f; break;
            case "MalirRoom1": light2D.GetComponent<Light2D>().intensity = 0.4f; break;
            case "MalirRoom3": light2D.GetComponent<Light2D>().intensity = 0f; break;
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
        ItemListGM.Add(item);
        ItemGetImage.sprite = item.Icon;
        ItemGetAnimator.Play("GetItem");
    }
    public void RemoveItem(Item item)
    {
        int removedindex = ItemListGM.IndexOf(item);
        ItemListGM.Remove(item);
        MoveAll(removedindex);

    }
    private void MoveAll(int index)
    {
        if (ItemListGM[index + 1] == null) return;
        else
        {
            ItemListGM[index] = ItemListGM[index + 1];
            MoveAll(index + 1);
        }
    }
}