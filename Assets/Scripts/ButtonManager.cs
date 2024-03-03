using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Header("Left Side")]
    [SerializeField] private GameObject[] leftButtonObjects;
    [SerializeField] private Animator leftAnimator;

    private Button[] leftButtons;

    [Header("Right Side")]
    [SerializeField] private GameObject[] rightButtonObjects;
    [SerializeField] private Animator rightAnimator;

    private Button[] rightButtons;

    [Header("New Game/Load Game Block")]
    [SerializeField] private GameObject[] saveFilesObjects;
    private Image[] saveFileImages;
    [SerializeField] private Sprite[] coloredSaveSprites;
    [SerializeField] private TextMeshProUGUI[] TimePlayedTexts;
    [SerializeField] private GameObject saveFilePart;
    [SerializeField] private GameObject yesNoPart;
    private Animator saveFileAnimator;
    private Animator yesNoAnimator;
    [SerializeField] private GameObject newGameTextObject;
    int leftFrom = 0;

    [Header("Options Block")]
    [SerializeField] private GameObject[] optionsObjects;


    int selectedInnerIndex = 0;
    bool selectedFile = false;
    int selectedFileNum = 0;




    [Header("Other")]
    [SerializeField] private Animator menuAnimator;

    private int selectedIndex;

    private bool side = false;

    private bool runCustomUpdate = false;


    // Start is called before the first frame update
    void Start()
    {
        saveFileAnimator = saveFilePart.GetComponent<Animator>();
        yesNoAnimator = yesNoPart.GetComponent<Animator>();

        saveFileImages = new Image[saveFilesObjects.Length];

        int index = 0;
        foreach (GameObject obj in saveFilesObjects)
        {
            saveFileImages[index] = obj.GetComponent<Image>();
            if (GameManager.instance.Saves[index])
            {
                saveFileImages[index].sprite = coloredSaveSprites[index];
                TimePlayedTexts[index].text = "08:51:12";
            }
            else
            {
                TimePlayedTexts[index].text = "";
            }

            index++;
        }

        leftButtons = new Button[leftButtonObjects.Length];
        rightButtons = new Button[rightButtonObjects.Length];

        index = 0;
        foreach (GameObject obj in rightButtonObjects)
        {
            rightButtons[index] = obj.GetComponent<Button>();
            index++;
        }
        index = 0;
        foreach (GameObject obj in leftButtonObjects)
        {
            leftButtons[index] = obj.GetComponent<Button>();
            index++;
        }

        selectedIndex = 0;
        leftAnimator.Play(selectedIndex.ToString());
    }

    void Update()
    {
        if (!GameManager.instance.loading)
        {
            if (runCustomUpdate)
            {
                NewGameLoop();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    if (!side) // pokud jsme na levé straně
                    {
                        switch (selectedIndex)
                        {
                            case 0://zmáčnuto tlačítko Start
                                ChangeSide();
                                break;
                            case 1: //zmáčnuto Quit
                                    // ukaž are you sure, po vybrání exit
                                throw new NotImplementedException();
                            default: return;
                        }
                        return;
                    }
                    else // pravá strana
                    {
                        switch (selectedIndex)
                        {
                            case 0: NewGame(); return;
                            case 1: break;
                            case 2: break;
                            case 3: break;
                            case 4: break;
                            case 5:
                                ChangeSide();
                                break;

                            default: return;
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    HandleButtonMove(KeyCode.LeftArrow);
                    return;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    HandleButtonMove(KeyCode.RightArrow);
                    return;
                }
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    HandleButtonMove(KeyCode.UpArrow);
                    return;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    HandleButtonMove(KeyCode.DownArrow);
                    return;
                }
            }
        }

    }

    void NewGameLoop()
    {
        if (!selectedFile) //4 obrázky s výběrem save filu
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                rightAnimator.enabled = false;
                rightAnimator.enabled = true;
                saveFilePart.SetActive(false);
                saveFilePart.SetActive(true);
                rightAnimator.speed = 1f;
                runCustomUpdate = false;
                selectedIndex = 0;
                rightAnimator.Play(selectedIndex.ToString());
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (selectedInnerIndex == 4)
                {
                    rightAnimator.enabled = false;
                    rightAnimator.enabled = true;
                    saveFilePart.SetActive(false);
                    saveFilePart.SetActive(true);
                    rightAnimator.speed = 1f;
                    runCustomUpdate = false;
                    selectedIndex = 0;
                    rightAnimator.Play(selectedIndex.ToString());
                    return;
                }
                yesNoPart.SetActive(true);
                if (GameManager.instance.Saves[selectedInnerIndex])
                {
                    newGameTextObject.GetComponent<TextMeshProUGUI>().text = "Přepsat stávající hru?";
                    UnityEngine.Vector3 pos = newGameTextObject.transform.position;
                    Debug.Log(pos);
                    pos.x = 2.75f;
                    Debug.Log(pos);
                    newGameTextObject.transform.position = pos;
                }
                else
                {
                    newGameTextObject.GetComponent<TextMeshProUGUI>().text = "Začít novou hru?";
                    UnityEngine.Vector3 pos = newGameTextObject.transform.position;
                    Debug.Log(pos);
                    pos.x = 3.9f;
                    Debug.Log(pos);
                    newGameTextObject.transform.position = pos;
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
                        Debug.Log("Starting game on file " + selectedFileNum.ToString());
                        GameManager.instance.LoadScene("Tavern");
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
    void NewGame()
    {
        rightAnimator.speed = 0f;

        selectedInnerIndex = 0;
        selectedFile = false;
        selectedFileNum = 0;
        saveFileAnimator.Play(selectedInnerIndex.ToString());
        runCustomUpdate = true;
    }

    void ChangeSide()
    {
        selectedIndex = 0;
        if (side) //vpravo
        {
            menuAnimator.Play("Menu_Left");
            side = false;
            leftAnimator.Play(selectedIndex.ToString());
        }
        else //vlevo
        {
            menuAnimator.Play("Menu_Right");
            yesNoPart.SetActive(false);
            side = true;
            rightAnimator.Play(selectedIndex.ToString());
        }
    }

    private void HandleButtonMove(KeyCode pressed)
    {
        if (selectedIndex == 0)
        {
            yesNoPart.SetActive(false);
        }
        if (pressed == KeyCode.LeftArrow)
        {
            if (!side)//left
            {
                if (selectedIndex != 1)
                {
                    selectedIndex++;
                    leftAnimator.Play(selectedIndex.ToString());
                }
            }
        }
        if (pressed == KeyCode.RightArrow)
        {
            if (!side)
            {
                if (selectedIndex != 0)
                {
                    selectedIndex--;
                    leftAnimator.Play(selectedIndex.ToString());
                }
            }
        }
        if (pressed == KeyCode.DownArrow)
        {
            if (side)
            {
                if (selectedIndex != 5)
                {
                    selectedIndex++;
                    rightAnimator.Play(selectedIndex.ToString());
                }
            }
        }
        if (pressed == KeyCode.UpArrow)
        {
            if (side)
            {
                if (selectedIndex != 0)
                {
                    selectedIndex--;
                    rightAnimator.Play(selectedIndex.ToString());
                }
            }

        }
    }
    public void Play()
    {
        GameManager.instance.LoadScene("Tavern");
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}