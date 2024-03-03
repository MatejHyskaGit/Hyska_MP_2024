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

    [SerializeField] private Animator[] diceAnimators;

    [SerializeField] private Sprite[] diceSprites;

    [SerializeField] private Image[] diceImages;

    [SerializeField] private GameObject diceContainer;

    [SerializeField] private Animator selectedDiceAnimator;

    private int selectedIndex = 0;

    private bool isPlaying = false;

    public bool DiceGameOn = false;

    private bool wait = false;

    private int dieIndex = 0;

    private int[] allyValues = new int[3];
    private int[] opponentValues = new int[3];

    private bool opponent = true;

    private bool pressed = false;

    void Awake()
    {
        instance = this;
        for (int i = 0; i < OpponentDiceValues.Length; i++)
        {
            OpponentDiceValues[i].enabled = false;
            AllyDiceValues[i].enabled = false;
        }
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
        if (!wait)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                wait = true;
                InitializeGame();
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && isPlaying && !opponent)
        {
            StartCoroutine(StopNextDie());
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isPlaying)
        {
            if (!pressed)
            {
                pressed = true;

                switch (selectedIndex)
                {
                    case 0: StartCoroutine(PlayInit()); break;
                    case 1: Exit(); break;
                    default: return;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && !isPlaying)
        {
            if (selectedIndex != 0)
            {
                selectedIndex--;
                choiceAnimator.Play(selectedIndex.ToString());
                Debug.Log(selectedIndex);
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && !isPlaying)
        {
            if (selectedIndex != 1)
            {
                selectedIndex++;
                choiceAnimator.Play(selectedIndex.ToString());
                Debug.Log(selectedIndex);
            }
        }
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
                    OpponentDiceValues[i].text = opponentValues[i].ToString();
                }
                yield return null;
            }
            else
            {
                for (int i = 0; i < AllyDiceValues.Length; i++)
                {
                    AllyDiceValues[i].enabled = true;
                    AllyDiceValues[i].text = allyValues[i].ToString();
                }

                StartCoroutine(Finish());
            }
        }
        else yield return null;
    }

    void InitializeGame()
    {
        opponent = true;

        playerText.enabled = true;

        diceContainer.SetActive(true);

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
        playerText.enabled = false;
        hintText.enabled = false;
        diceContainer.SetActive(false);

        yield return new WaitForSeconds(1);

        allyName.enabled = true;
        opponentName.enabled = true;

        yield return new WaitForSeconds(1);

        allyDiceResult.enabled = true;
        opponentDiceResult.enabled = true;

        int _allydiceresult = 0;
        foreach (int val in allyValues)
        {
            _allydiceresult += val;
        }
        allyDiceResult.text = _allydiceresult.ToString();

        int _opponentdiceresult = 0;
        foreach (int val in opponentValues)
        {
            _opponentdiceresult += val;
        }
        opponentDiceResult.text = _opponentdiceresult.ToString();

        yield return new WaitForSeconds(1);

        resultText.enabled = true;
        if (_allydiceresult > _opponentdiceresult) resultText.text = "Vyhráváš!";
        else if (_allydiceresult == _opponentdiceresult) resultText.text = "Remíza";
        else resultText.text = "Protihráč vyhrál";

        yield return new WaitForSeconds(1);

        allyValues = new int[3];
        opponentValues = new int[3];
        opponent = true;
        isPlaying = false;
        pressed = false;
        selectedIndex = 0;
        choiceAnimator.Play(selectedIndex.ToString());

    }

    IEnumerator PlayInit()
    {
        if (opponent == true)
        {
            playerText.text = "Protihráč je na řadě";
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
                yield return new WaitForSeconds(1f);
                StartCoroutine(StopNextDie());
            }
            yield return new WaitForSeconds(1f);
            opponent = false;
            StartCoroutine(PlayInit());
        }
        else
        {
            playerText.text = "Jsi na řadě";
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

        /*
        resultText.text = "";
        for (int i = 0; i < AllyDiceValues.Length; i++)
        {
            AllyDiceValues[i].text = "0";
            OpponentDiceValues[i].text = "0";
        }
        allyDiceResult.text = "0";
        opponentDiceResult.text = "0";
        isPlaying = true;
        int AllyAll = 0;
        int OpponentAll = 0;
        var random = new System.Random();
        for (int i = 0; i < AllyDiceValues.Length; i++)
        {
            yield return new WaitForSeconds(1f);
            int AllyRandom = random.Next(1, 7);
            AllyDiceValues[i].text = AllyRandom.ToString();
            AllyAll += AllyRandom;
            allyDiceResult.text = AllyAll.ToString();

            int OpponentRandom = random.Next(1, 7);
            OpponentDiceValues[i].text = OpponentRandom.ToString();
            OpponentAll += OpponentRandom;
            opponentDiceResult.text = OpponentAll.ToString();
        }

        if (AllyAll > OpponentAll) resultText.text = "Vyhráváš!";
        else if (AllyAll == OpponentAll) resultText.text = "Remíza";
        else resultText.text = "Protihráč vyhrál";
        playText.text = "Znovu";
        isPlaying = false;
        selectedIndex = 0;
        choiceAnimator.Play(selectedIndex.ToString());*/
    }
    void Exit()
    {
        playText.text = "Hrát";
        resultText.text = "";
        selectedIndex = 0;
        wait = false;
        diePopupObject.SetActive(false);
    }
}
