using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceGameManager : MonoBehaviour
{
    public static DiceGameManager instance { get; private set; }

    [Header("Ally Side")]
    [SerializeField] private TextMeshProUGUI[] AllyDiceValues;
    [SerializeField] private TextMeshProUGUI allyDiceResult;
    [SerializeField] private TextMeshProUGUI allyName;

    [Header("Opponent Side")]
    [SerializeField] private TextMeshProUGUI[] OpponentDiceValues;
    [SerializeField] private TextMeshProUGUI opponentDiceResult;
    [SerializeField] private TextMeshProUGUI opponentName;

    [Header("Global")]
    [SerializeField] private GameObject diePopupObject;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI playText;
    [SerializeField] private Animator choiceAnimator;

    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private TextMeshProUGUI hintText;

    [SerializeField] private TextMeshProUGUI mainPlayText;
    [SerializeField] private TextMeshProUGUI hintText2;

    [SerializeField] private Animator[] diceAnimators;

    [SerializeField] private Sprite[] diceSprites;

    [SerializeField] private Image[] diceImages;

    [SerializeField] private GameObject diceContainer;

    [SerializeField] private Animator selectedDiceAnimator;

    [SerializeField] private GameObject AnthonyNPC;

    [SerializeField] private TextMeshProUGUI continueText;
    [SerializeField] private Image continueIcon;

    private int selectedIndex = 0;

    private bool isPlaying = false;

    public bool DiceGameOn = false;
#pragma warning disable CS0414
    private bool wait = false;
#pragma warning restore CS0414

    private int dieIndex = 0;

    private int[] allyValues = new int[3];
    private int[] opponentValues = new int[3];

    private bool opponent = true;

    private bool pressed = false;

    private bool finishing = false;

    void Awake()
    {
        instance = this;
        for (int i = 0; i < OpponentDiceValues.Length; i++)
        {
            OpponentDiceValues[i].enabled = false;
            AllyDiceValues[i].enabled = false;
        }
        InitializeGame();//when bulding game
    }

    void Update()
    {
        if (diePopupObject.activeSelf == false)
        {
            DiceGameOn = false;
            return;
        }
        else
        {
            DiceGameOn = true;
        }
        /* if playtesting in unity
        if (!wait)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
            {
                wait = true;
                InitializeGame();
                return;
            }
        }*/
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && isPlaying && !opponent)
        {
            if (finishing) return;
            StartCoroutine(StopNextDie());
            return;
        }
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && !isPlaying)
        {
            if (!pressed)
            {
                pressed = true;

                if (continueText.enabled)
                {
                    if (continueText.text == "Konec")
                    {
                        Exit();
                    }
                    else if (continueText.text == "Znovu")
                    {
                        InitializeGame();
                        mainPlayText.enabled = true;
                        hintText2.enabled = true;
                        continueText.enabled = false;
                        continueIcon.enabled = false;

                    }
                }
                else
                {
                    if (mainPlayText.enabled)
                    {
                        NewGame();
                        StartCoroutine(PlayInit());
                        return;
                    }

                    switch (selectedIndex)
                    {
                        case 0: mainPlayText.enabled = true; hintText2.enabled = true; break;
                        case 1: Exit(); break;
                        default: return;
                    }
                }
            }

        }/*
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !isPlaying)
        {
            if (selectedIndex != 0)
            {
                selectedIndex--;
                choiceAnimator.Play(selectedIndex.ToString());
                Debug.Log(selectedIndex);
            }
        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !isPlaying)
        {
            if (selectedIndex != 1)
            {
                selectedIndex++;
                choiceAnimator.Play(selectedIndex.ToString());
                Debug.Log(selectedIndex);
            }
        }*/
    }



    IEnumerator StopNextDie()
    {
        var random = new System.Random();
        int randomnum = random.Next(1, 7);
        Debug.Log(randomnum);
        diceAnimators[dieIndex].enabled = false;
        diceImages[dieIndex].sprite = diceSprites[randomnum - 1];
        if (opponent)
        {
            opponentValues[dieIndex] = randomnum;
        }
        if (opponent == false)
        {
            allyValues[dieIndex] = randomnum;
        }
        dieIndex++;
        selectedDiceAnimator.Play(dieIndex.ToString());
        if (dieIndex == 3)
        {
            if (opponent)
            {
                for (int i = 0; i < OpponentDiceValues.Length; i++)
                {
                    OpponentDiceValues[i].enabled = true;
                    OpponentDiceValues[i].text = "<color=#ff637d>" + opponentValues[i].ToString() + "</color>";
                }
                opponentName.enabled = true;
                yield return null;
            }
            else
            {
                for (int i = 0; i < AllyDiceValues.Length; i++)
                {
                    AllyDiceValues[i].enabled = true;
                    AllyDiceValues[i].text = "<color=#47c2ff>" + allyValues[i].ToString() + "</color>";
                }
                allyName.enabled = true;
                finishing = true;
                yield return new WaitForSeconds(1);

                StartCoroutine(Finish());
            }
        }
        else yield return null;
    }

    void NewGame()
    {
        opponent = true;

        playerText.enabled = true;

        allyName.enabled = false;
        opponentName.enabled = false;

        allyDiceResult.enabled = false;
        opponentDiceResult.enabled = false;

        diceContainer.SetActive(true);

        mainPlayText.enabled = false;
        hintText2.enabled = false;

        resultText.enabled = false;

        for (int i = 0; i < OpponentDiceValues.Length; i++)
        {
            OpponentDiceValues[i].enabled = false;
            AllyDiceValues[i].enabled = false;
        }
    }

    void InitializeGame()
    {
        opponent = true;

        pressed = false;

        continueIcon.enabled = false;
        continueText.enabled = false;

        mainPlayText.enabled = true;
        hintText2.enabled = true;

        playerText.enabled = false;
        hintText.enabled = false;

        diceContainer.SetActive(false);

        allyName.enabled = false;
        opponentName.enabled = false;

        allyDiceResult.enabled = false;
        opponentDiceResult.enabled = false;

        resultText.enabled = false;

        for (int i = 0; i < OpponentDiceValues.Length; i++)
        {
            OpponentDiceValues[i].enabled = false;
            AllyDiceValues[i].enabled = false;
        }
    }

    IEnumerator Finish()
    {
        finishing = true;


        playerText.enabled = false;
        hintText.enabled = false;
        diceContainer.SetActive(false);

        yield return new WaitForSeconds(1);

        allyDiceResult.enabled = true;
        opponentDiceResult.enabled = true;

        int _allydiceresult = 0;
        foreach (int val in allyValues)
        {
            _allydiceresult += val;
        }
        allyDiceResult.text = "<color=#47c2ff>" + _allydiceresult.ToString() + "</color>";

        int _opponentdiceresult = 0;
        foreach (int val in opponentValues)
        {
            _opponentdiceresult += val;
        }
        opponentDiceResult.text = "<color=#ff637d>" + _opponentdiceresult.ToString() + "</color>";

        yield return new WaitForSeconds(1);

        resultText.enabled = true;
        if (_allydiceresult > _opponentdiceresult) resultText.text = "<color=#47c2ff>Vyhráváš!</color>";
        else if (_allydiceresult == _opponentdiceresult) resultText.text = "Remíza";
        else resultText.text = "<color=#ff637d>Anthony</color> vyhrál";

        yield return new WaitForSeconds(1);

        allyValues = new int[3];
        opponentValues = new int[3];
        opponent = true;
        isPlaying = false;
        //single button text
        if (GameManager.instance.diceGameCount == 0)
        {
            continueText.enabled = true;
            if (resultText.text == "Remíza") continueText.text = "Znovu";
            else continueText.text = "Konec";
            continueIcon.enabled = true;
        }
        else
        {
            selectedIndex = 0;
            choiceAnimator.Play(selectedIndex.ToString());
        }
        if (resultText.text != "Remíza") GameManager.instance.diceGameCount++;
        pressed = false;
        finishing = false;

    }

    IEnumerator PlayInit()
    {
        if (opponent == true)
        {
            playerText.text = "<color=#ff637d>Anthony</color> je na řadě";
            hintText.enabled = false;
            dieIndex = 0;
            selectedDiceAnimator.Play(dieIndex.ToString());
            choiceAnimator.Play("default");
            foreach (Animator anim in diceAnimators)
            {
                anim.enabled = true;
                anim.Play("0");
            }
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(2f);
                StartCoroutine(StopNextDie());
            }
            yield return new WaitForSeconds(2f);
            opponent = false;
            StartCoroutine(PlayInit());
        }
        else
        {
            playerText.text = "<color=#47c2ff>Jsi</color> na řadě";
            hintText.enabled = true;
            isPlaying = true;
            dieIndex = 0;
            selectedDiceAnimator.Play(dieIndex.ToString());
            choiceAnimator.Play("default");
            foreach (Animator anim in diceAnimators)
            {
                anim.enabled = true;
                anim.Play("0");
            }
            yield return null;
        }
    }
    void Exit()
    {
        playText.text = "Hrát";
        selectedIndex = 0;
        wait = false;
        diePopupObject.SetActive(false);
        DialogueTrigger trigger = AnthonyNPC.GetComponentInChildren<DialogueTrigger>();
        switch (resultText.text)
        {
            case "<color=#47c2ff>Vyhráváš!</color>":
                trigger.EnterDialogue(); trigger.dialogueIndex++; break;
            case "<color=#ff637d>Anthony</color> vyhrál":
                trigger.dialogueIndex++; trigger.EnterDialogue(); break;
            default:
                Debug.Log("How did we get here?");
                return;
        }


    }
}
