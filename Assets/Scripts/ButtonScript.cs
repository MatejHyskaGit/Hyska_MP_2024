using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Animator buttonanimator;
    private Indestructible script;

    private Button SelectedButton;

    //right side

    public GameObject PlayButtonRight;
    private Button PlayButtonRightB;
    public GameObject TutorialText;
    public GameObject CreditsText;
    private Button BackButtonRight;

    //left side

    private Button PlayButtonLeft;
    private Button CreditsButtonLeft;
    private Button TutorialButtonLeft;
    private Button QuitButtonLeft;

    private Button LastLeftButton;

    //temp variables

    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.Find("Hugo").GetComponent<Indestructible>();

        PlayButtonRightB = GameObject.Find("PlayButtonRight").GetComponent<Button>();
        BackButtonRight = GameObject.Find("BackButton").GetComponent<Button>();

        PlayButtonLeft = GameObject.Find("PlayButtonLeft").GetComponent<Button>();
        CreditsButtonLeft = GameObject.Find("CreditsButton").GetComponent<Button>();
        TutorialButtonLeft = GameObject.Find("TutorialButton").GetComponent<Button>();
        QuitButtonLeft = GameObject.Find("ExitButton").GetComponent<Button>();

        ChangeSelected(PlayButtonLeft);
    }
    void Update()
    {
        System.Func<KeyCode, bool> inputFunction;
        inputFunction = Input.GetKeyDown;
        if (inputFunction(KeyCode.Return) || inputFunction(KeyCode.Space))
        {
            if (SelectedButton == PlayButtonLeft)
            {
                LastLeftButton = PlayButtonLeft;
                MoveScreenRight();
                ShowPlay();
                ChangeSelected(PlayButtonRightB);
            }
            else if (SelectedButton == TutorialButtonLeft)
            {
                LastLeftButton = TutorialButtonLeft;
                MoveScreenRight();
                ShowTutorial();
                ChangeSelected(BackButtonRight);
            }
            else if (SelectedButton == CreditsButtonLeft)
            {
                LastLeftButton = CreditsButtonLeft;
                MoveScreenRight();
                ShowCredits();
                ChangeSelected(BackButtonRight);
            }
            else if (SelectedButton == QuitButtonLeft)
            {
                Exit();
            }
            else if (SelectedButton == BackButtonRight)
            {
                MoveScreenLeft();
                ChangeSelected(LastLeftButton);
            }
            else if (SelectedButton == PlayButtonRightB)
            {
                Play();
            }
        }

        if (SelectedButton == PlayButtonLeft)
        {
            if (inputFunction(KeyCode.DownArrow))
            {
                ChangeSelected(TutorialButtonLeft);
            }
        }
        else if (SelectedButton == TutorialButtonLeft)
        {
            if (inputFunction(KeyCode.UpArrow))
            {
                ChangeSelected(PlayButtonLeft);
            }
            if (inputFunction(KeyCode.DownArrow))
            {
                ChangeSelected(CreditsButtonLeft);
            }
        }
        else if (SelectedButton == CreditsButtonLeft)
        {
            if (inputFunction(KeyCode.UpArrow))
            {
                ChangeSelected(TutorialButtonLeft);
            }
            if (inputFunction(KeyCode.DownArrow))
            {
                ChangeSelected(QuitButtonLeft);
            }
        }
        else if (SelectedButton == QuitButtonLeft)
        {
            if (inputFunction(KeyCode.UpArrow))
            {
                ChangeSelected(CreditsButtonLeft);
            }
        }
        if (SelectedButton == PlayButtonRightB)
        {
            if (inputFunction(KeyCode.DownArrow))
            {
                ChangeSelected(BackButtonRight);
            }
        }
        else if (SelectedButton == BackButtonRight)
        {
            if (PlayButtonRight.activeSelf)
            {
                if (inputFunction(KeyCode.UpArrow))
                {
                    ChangeSelected(PlayButtonRightB);
                }
            }

        }
    }

    public void ChangeSelected(Button newButton)
    {
        ColorBlock cb;
        //turn previously selected button white
        if (SelectedButton != null)
        {
            cb = SelectedButton.colors;
            cb.normalColor = new Color(1f, 1f, 1f, 1f);
            SelectedButton.colors = cb;
        }

        SelectedButton = newButton;
        cb = newButton.colors;
        cb.normalColor = new Color(1f, 0.92f, 0.016f, 1f);
        newButton.colors = cb;

        // SelectedButton.colors.normalColor = new Color(1f, 1f, 1f, 1f);
        // SelectedButton = newButton;
        // SelectedButton.colors.normalColor = new Color(1f, 0.92f, 0.016f, 1f);

    }
    public void MoveScreenRight()
    {
        buttonanimator.SetTrigger("Start");
    }
    public void MoveScreenLeft()
    {
        buttonanimator.SetTrigger("End");
    }
    public void Play()
    {
        script.LoadScene("MalirRoom1");
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    public void ShowPlay()
    {
        PlayButtonRight.SetActive(true);
        TutorialText.SetActive(false);
        CreditsText.SetActive(false);
    }
    public void ShowTutorial()
    {
        PlayButtonRight.SetActive(false);
        TutorialText.SetActive(true);
        CreditsText.SetActive(false);
    }
    public void ShowCredits()
    {
        PlayButtonRight.SetActive(false);
        TutorialText.SetActive(false);
        CreditsText.SetActive(true);
    }
}
