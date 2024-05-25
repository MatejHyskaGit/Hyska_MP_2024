using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager Instance { get; private set; }

    private string fileNameString;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, "");
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    public void NewGame(string fileName)
    {
        fileNameString = fileName;
        this.gameData = new GameData();
    }

    public void LoadGame(string fileName)
    {
        fileNameString = fileName;
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileNameString);
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.LogWarning("No data found. Initializing defaults");
            NewGame(fileNameString);
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            Debug.Log("Calling load at object: " + dataPersistenceObj);
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame(string fileName)
    {
        fileNameString = fileName;
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileNameString);
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            Debug.Log("Calling save at object: " + dataPersistenceObj);
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }

    public void DeleteSave(string fileName)
    {
        fileNameString = fileName;
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileNameString);
        dataHandler.Delete();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();


        foreach (IDataPersistence item in dataPersistenceObjects)
        {
            Debug.Log(item);
        }

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
