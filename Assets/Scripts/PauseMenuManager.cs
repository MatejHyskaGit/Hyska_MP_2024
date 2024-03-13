using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Main Screen")]
    [SerializeField] GameObject PauseCanvas;

    [SerializeField] GameObject PausePanel;

    [SerializeField] Animator pauseAnimator;

    [Header("Save Screen")]
    [SerializeField] GameObject SavePanel;
    [SerializeField] private GameObject[] saveFilesObjects;
    private Image[] saveFileImages;
    [SerializeField] private Sprite[] coloredSaveSprites;
    [SerializeField] private GameObject saveFilePart;
    [SerializeField] private GameObject yesNoPart;
    private Animator saveFileAnimator;
    private Animator yesNoAnimator;
    [SerializeField] private GameObject newGameTextObject;

    bool selectedFile = false;
    int selectedFileNum = 0;

    [Header("Options Screen")]

    [SerializeField] GameObject SettingsPanel;
    [SerializeField] Animator settingsAnimator;
    [SerializeField] Image[] optionsSoundImages;
    [SerializeField] private Image VsyncImageSwitch;
    [SerializeField] private Sprite volumeSpriteOn;
    [SerializeField] private Sprite volumeSpriteOff;
    [SerializeField] private Sprite switchSpriteOn;
    [SerializeField] private Sprite switchSpriteOff;

    [Header("Inventory Screen")]
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] Animator inventoryAnimator;
    [SerializeField] GameObject itemContainer;
    [SerializeField] Image itemImage;

    int leftFrom = 0;
    bool insideInventory = false;
    bool klicky = false;


    public static bool isPaused { get; private set; }

    private int selectedIndex = 0;

    int selectedInnerIndex = 0;

    bool runCustomUpdate = false;
    bool optionsUpdate = false;
    bool saveUpdate = false;
    bool inventoryUpdate = false;

    GameObject[] itemNeeders;

    bool papirZoom = false;

    void Start()
    {
        InventoryPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        PauseCanvas.SetActive(false);
        SavePanel.SetActive(false);

        saveFileAnimator = saveFilePart.GetComponent<Animator>();
        yesNoAnimator = yesNoPart.GetComponentInChildren<Animator>();

        saveFileImages = new Image[saveFilesObjects.Length];



        int index = 0;
        foreach (GameObject obj in saveFilesObjects)
        {
            saveFileImages[index] = obj.GetComponent<Image>();
            if (File.Exists(Path.Combine(Application.persistentDataPath, GameManager.instance.Saves[index])))
            {
                saveFileImages[index].sprite = coloredSaveSprites[index];
                //TimePlayedTexts[index].text = "08:51:12";
            }

            index++;
        }


        foreach (Image soundimage in optionsSoundImages)
        {
            soundimage.sprite = volumeSpriteOff;
        }
        for (int i = 0; i < GameManager.instance.Volume; i++)
        {
            optionsSoundImages[i].sprite = volumeSpriteOn;
        }
        if (QualitySettings.vSyncCount == 0)
        {
            VsyncImageSwitch.sprite = switchSpriteOff;
        }
        else if (QualitySettings.vSyncCount == 1)
        {
            VsyncImageSwitch.sprite = switchSpriteOn;
        }
        itemNeeders = new GameObject[0];
        itemNeeders = GameObject.FindGameObjectsWithTag("NeedItem");
    }

    void Update()
    {
        if (runCustomUpdate)
        {
            if (optionsUpdate) OptionsLoop();
            else if (saveUpdate) SaveGameLoop();
            else if (inventoryUpdate) InventoryLoop();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (selectedIndex != 0)
                {
                    selectedIndex--;
                    pauseAnimator.Play(selectedIndex.ToString());
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (selectedIndex != 4)
                {
                    selectedIndex++;
                    pauseAnimator.Play(selectedIndex.ToString());
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                return;
            }
            //Button Handle Logic
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                switch (selectedIndex)
                {
                    case 0: Resume(); break;
                    case 1: Inventory(); break;
                    case 2: Save(); break;
                    case 3: Options(); break;
                    case 4: MainMenu(); break;
                    default: return;
                }
            }
        }

    }

    void Pause()
    {
        if (!GameManager.instance.loading && !DialogueManager.instance.dialogueIsPlaying)
        {
            PauseCanvas.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
            selectedIndex = 0;
            pauseAnimator.Play(selectedIndex.ToString());
        }
    }

    void Resume()
    {
        PauseCanvas.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    void Save()
    {
        PausePanel.SetActive(false);
        SavePanel.SetActive(true);
        yesNoPart.SetActive(false);
        saveFilePart.SetActive(true);

        selectedFile = false;
        selectedFileNum = 0;

        runCustomUpdate = true;
        saveUpdate = true;

        selectedInnerIndex = 0;
        saveFileAnimator.Play(selectedInnerIndex.ToString());
    }

    void Options()
    {
        PausePanel.SetActive(false);
        SettingsPanel.SetActive(true);


        runCustomUpdate = true;
        optionsUpdate = true;

        selectedInnerIndex = 0;
        settingsAnimator.Play(selectedInnerIndex.ToString());
    }

    void Inventory()
    {
        PausePanel.SetActive(false);
        InventoryPanel.SetActive(true);

        runCustomUpdate = true;
        inventoryUpdate = true;

        InventoryManager.Instance.UpdateList();

        selectedInnerIndex = 0;
        inventoryAnimator.Play(selectedInnerIndex.ToString());

    }

    void SaveGameLoop()
    {
        if (!selectedFile) //4 obrázky s výběrem save filu
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SavePanel.SetActive(false);
                PausePanel.SetActive(true);
                selectedFile = false;
                selectedFileNum = 0;
                runCustomUpdate = false;
                saveUpdate = false;
                pauseAnimator.Play(selectedIndex.ToString());
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (selectedInnerIndex == 4)
                {
                    SavePanel.SetActive(false);
                    PausePanel.SetActive(true);
                    selectedFile = false;
                    selectedFileNum = 0;
                    runCustomUpdate = false;
                    saveUpdate = false;
                    pauseAnimator.Play(selectedIndex.ToString());
                    return;
                }
                yesNoPart.SetActive(true);
                if (File.Exists(Path.Combine(Application.persistentDataPath, GameManager.instance.Saves[selectedInnerIndex])))
                {
                    newGameTextObject.GetComponent<TextMeshProUGUI>().text = "Přepsat hru?";
                }
                else
                {
                    newGameTextObject.GetComponent<TextMeshProUGUI>().text = "Uložit hru?";
                }
                saveFilePart.SetActive(false);
                selectedFile = true;
                selectedFileNum = selectedInnerIndex + 1;
                selectedInnerIndex = 0;
                yesNoAnimator.Play(selectedInnerIndex.ToString());
                return;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (selectedInnerIndex == 0 || selectedInnerIndex == 1)
                {
                    selectedInnerIndex += 2;
                }
                else if (selectedInnerIndex == 2 || selectedInnerIndex == 3)
                {
                    leftFrom = selectedInnerIndex;
                    selectedInnerIndex = 4;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (selectedInnerIndex == 2 || selectedInnerIndex == 3)
                {
                    selectedInnerIndex -= 2;
                }
                else if (selectedInnerIndex == 4)
                {
                    selectedInnerIndex = leftFrom;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (selectedInnerIndex == 0 || selectedInnerIndex == 2)
                {
                    selectedInnerIndex++;
                }
                else if (selectedInnerIndex == 4)
                {
                    leftFrom = 3;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (selectedInnerIndex == 1 || selectedInnerIndex == 3)
                {
                    selectedInnerIndex--;
                }
                else if (selectedInnerIndex == 4)
                {
                    leftFrom = 2;
                }
            }
            saveFileAnimator.Play(selectedInnerIndex.ToString());
            return;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                switch (selectedInnerIndex)
                {
                    case 0:
                        Debug.Log("Saving game on file " + selectedFileNum.ToString());
                        DataPersistenceManager.Instance.SaveGame($"data{selectedFileNum}.game");

                        saveFilePart.SetActive(false);
                        yesNoPart.SetActive(false);
                        PausePanel.SetActive(true);
                        SavePanel.SetActive(false);
                        selectedFile = false;
                        selectedFileNum = 0;
                        runCustomUpdate = false;
                        saveUpdate = false;
                        pauseAnimator.Play(selectedIndex.ToString());
                        break;
                    case 1:
                        Debug.Log("no in confirmation");
                        saveFilePart.SetActive(true);
                        yesNoPart.SetActive(false);
                        selectedFile = false;
                        selectedInnerIndex = selectedFileNum - 1;
                        saveFileAnimator.Play(selectedInnerIndex.ToString());
                        selectedFileNum = 0;
                        return;
                    default: break;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                saveFilePart.SetActive(true);
                yesNoPart.SetActive(false);
                selectedFile = false;
                selectedInnerIndex = selectedFileNum - 1;
                saveFileAnimator.Play(selectedInnerIndex.ToString());
                selectedFileNum = 0;
                return;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (selectedInnerIndex == 1) selectedInnerIndex--;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (selectedInnerIndex == 0) selectedInnerIndex++;
            }
            yesNoAnimator.Play(selectedInnerIndex.ToString());
            return;
        }
    }

    void InventoryLoop()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (insideInventory)
            {
                selectedInnerIndex = 0;
                InventoryManager.Instance.Close();
                insideInventory = false;
                return;
            }
            else if (!insideInventory)
            {
                selectedInnerIndex = 0;
                klicky = false;
                itemContainer.SetActive(true);
                InventoryPanel.SetActive(false);
                PausePanel.SetActive(true);
                runCustomUpdate = false;
                inventoryUpdate = false;
                pauseAnimator.Play(selectedIndex.ToString());
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (insideInventory)
            {
                if (InventoryManager.Instance.ItemList[selectedInnerIndex].Name == "Papír" && !papirZoom)
                {
                    //zoomnout na papír

                    //load sprite
                    Sprite bigPaper = Resources.Load<Sprite>("Sprites/velkypapir");

                    //make image big
                    itemImage.rectTransform.sizeDelta = new Vector2(711, 300);

                    //set sprite
                    itemImage.sprite = bigPaper;
                    papirZoom = true;
                }
                else if (InventoryManager.Instance.ItemList[selectedInnerIndex].Name == "Papír" && papirZoom)
                {
                    //odzoomnout
                    itemImage.rectTransform.sizeDelta = new Vector2(300, 300);
                    InventoryManager.Instance.Close();
                    insideInventory = false;
                    papirZoom = false;
                }
                else
                {
                    itemImage.rectTransform.sizeDelta = new Vector2(300, 300);
                    InventoryManager.Instance.Close();
                    insideInventory = false;
                    klicky = false;
                    itemContainer.SetActive(true);
                    InventoryPanel.SetActive(false);
                    PausePanel.SetActive(true);
                    runCustomUpdate = false;
                    inventoryUpdate = false;
                    Resume();
                    foreach (GameObject obj in itemNeeders)
                    {
                        Debug.Log("Starting foreach");
                        BoxCollider2D[] bcolliders = obj.GetComponentsInChildren<BoxCollider2D>();
                        Vector2[] actualPositions = new Vector2[bcolliders.Length];
                        int index = 0;
                        foreach (var collider in bcolliders)
                        {
                            actualPositions[index] = (Vector2)obj.transform.position + collider.offset;
                            index++;
                        }
                        foreach (Vector2 pos in actualPositions)
                        {
                            if (MovementManager.instance.VectRound(MovementManager.instance.actualPos + InteractScript.DirToVect(MovementManager.instance.Direction), 2) == MovementManager.instance.VectRound(pos, 2))
                            {
                                Debug.Log("Found the position");
                                obj.GetComponentInChildren<ItemNeedScript>().UseItem(InventoryManager.Instance.ItemList[selectedInnerIndex], obj);
                                Destroy(obj.GetComponent<ItemNeedScript>());
                            }
                        }
                    }
                    selectedInnerIndex = 0;
                }

                return;
            }
        }
        if (!insideInventory)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (selectedInnerIndex != 6)
                {
                    if (InventoryManager.Instance.ItemList.Count >= selectedInnerIndex + 1)
                    {
                        if (InventoryManager.Instance.ItemList[selectedInnerIndex] != null)
                        {
                            insideInventory = true;
                            InventoryManager.Instance.OpenItem(InventoryManager.Instance.ItemList[selectedInnerIndex].Name);
                        }
                        return;
                    }
                }
                else if (!klicky)
                {
                    //druhý inventář - klíče
                    klicky = true;
                    inventoryAnimator.Play("klicky_pause");
                    itemContainer.SetActive(false);
                }
                else if (klicky)
                {
                    klicky = false;
                    inventoryAnimator.Play("6");
                    itemContainer.SetActive(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (klicky) return;
                if (selectedInnerIndex == 0 || selectedInnerIndex == 1 || selectedInnerIndex == 2)
                {
                    selectedInnerIndex += 3;
                    inventoryAnimator.Play(selectedInnerIndex.ToString());
                }
                else if (selectedInnerIndex == 6)
                {
                    leftFrom = 5;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (klicky) return;
                if (selectedInnerIndex == 3 || selectedInnerIndex == 4 || selectedInnerIndex == 5)
                {
                    selectedInnerIndex -= 3;
                    inventoryAnimator.Play(selectedInnerIndex.ToString());
                }
                else if (selectedInnerIndex == 6)
                {
                    leftFrom = 2;
                    inventoryAnimator.Play(selectedInnerIndex.ToString());
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (klicky) return;
                if (selectedInnerIndex == 1 || selectedInnerIndex == 2 || selectedInnerIndex == 4 || selectedInnerIndex == 5)
                {
                    selectedInnerIndex--;
                    inventoryAnimator.Play(selectedInnerIndex.ToString());
                }
                else if (selectedInnerIndex == 6)
                {
                    selectedInnerIndex = leftFrom;
                    inventoryAnimator.Play(selectedInnerIndex.ToString());
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (klicky) return;
                if (selectedInnerIndex == 0 || selectedInnerIndex == 3 || selectedInnerIndex == 1 || selectedInnerIndex == 4)
                {
                    selectedInnerIndex++;
                    inventoryAnimator.Play(selectedInnerIndex.ToString());
                }
                else if (selectedInnerIndex == 2 || selectedInnerIndex == 5)
                {
                    leftFrom = selectedInnerIndex;
                    selectedInnerIndex = 6;
                    inventoryAnimator.Play(selectedInnerIndex.ToString());
                }
            }
        }

    }

    void OptionsLoop()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (selectedInnerIndex != 2) selectedInnerIndex++;
            settingsAnimator.Play(selectedInnerIndex.ToString());
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (selectedInnerIndex != 0) selectedInnerIndex--;
            settingsAnimator.Play(selectedInnerIndex.ToString());
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (selectedInnerIndex == 0)
            {
                if (GameManager.instance.Volume != 0) GameManager.instance.Volume--;
                foreach (Image soundimage in optionsSoundImages)
                {
                    soundimage.sprite = volumeSpriteOff;
                }
                for (int i = 0; i < GameManager.instance.Volume; i++)
                {
                    optionsSoundImages[i].sprite = volumeSpriteOn;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (selectedInnerIndex == 0)
            {
                if (GameManager.instance.Volume != 10) GameManager.instance.Volume++;
                foreach (Image soundimage in optionsSoundImages)
                {
                    soundimage.sprite = volumeSpriteOff;
                }
                for (int i = 0; i < GameManager.instance.Volume; i++)
                {
                    optionsSoundImages[i].sprite = volumeSpriteOn;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {

            switch (selectedInnerIndex)
            {
                case 0:
                    break;
                case 1:
                    if (VsyncImageSwitch.sprite == switchSpriteOff)
                    {
                        VsyncImageSwitch.sprite = switchSpriteOn;
                        QualitySettings.vSyncCount = 1;
                    }
                    else if (VsyncImageSwitch.sprite == switchSpriteOn)
                    {
                        VsyncImageSwitch.sprite = switchSpriteOff;
                        QualitySettings.vSyncCount = 0;
                    }
                    break; //změň sprite, vypni vsync
                case 2:
                    SettingsPanel.SetActive(false);
                    PausePanel.SetActive(true);
                    runCustomUpdate = false;
                    optionsUpdate = false;
                    pauseAnimator.Play(selectedIndex.ToString());
                    return;
                default: return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsPanel.SetActive(false);
            PausePanel.SetActive(true);
            runCustomUpdate = false;
            optionsUpdate = false;
            pauseAnimator.Play(selectedIndex.ToString());
            return;
        }
    }

    void MainMenu()
    {
        Resume();
        GameManager.instance.LoadScene("Menu");
    }
}
