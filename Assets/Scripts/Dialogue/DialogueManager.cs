using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;


    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;


    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    [SerializeField] private GameObject choiceIndicator;

    private Animator buttonAnimator;

    [SerializeField] GameObject continueIcon;

    private Animator layoutAnimator;

    private TextMeshProUGUI[] choicesText;

    private Button[] choicesButtons;

    private Story currentStory;

    private int selectedIndex;

    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;

    public bool submitSkip = false;

    private Coroutine displayLineCoroutine;

    public static DialogueManager instance { get; private set; }

    private const string SPEAKER_TAG = "speaker";

    private const string PORTRAIT_TAG = "portrait";

    private const string LAYOUT_TAG = "layout";

    private InkExternalFunctions inkExternalFunctions;

    [Header("Tavern")]
    [SerializeField] private GameObject diePopup;
    [SerializeField] private GameObject anthonyNPC;
    [SerializeField] private GameObject Nathaniel;

    [Header("MalirRoom1")]
    [SerializeField] private GameObject boxWing;
    [SerializeField] private GameObject boxRat;

    [Header("MalirRoom3")]
    [SerializeField] private GameObject statueLeft;
    [SerializeField] private GameObject statueMid;
    [SerializeField] private GameObject statueRight;
    [SerializeField] private GameObject boxPaper;









    void Awake()
    {
        if (instance != null) Debug.LogWarning("More than one Dialogue Manager in the scene");
        instance = this;

        inkExternalFunctions = new InkExternalFunctions();
    }

    void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        choicesButtons = new Button[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            choicesButtons[index] = choice.GetComponent<Button>();
            index++;
        }
        layoutAnimator = dialoguePanel.GetComponent<Animator>();
        buttonAnimator = choiceIndicator.GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (DiceGameManager.instance != null) if (DiceGameManager.instance.DiceGameOn) return;
            submitSkip = true;
        }

        if (!dialogueIsPlaying)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (DiceGameManager.instance != null) if (DiceGameManager.instance.DiceGameOn) return;
            HandleButtonMove(KeyCode.RightArrow);
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (DiceGameManager.instance != null) if (DiceGameManager.instance.DiceGameOn) return;
            HandleButtonMove(KeyCode.LeftArrow);
            return;
        }
        if (submitSkip && currentStory.currentChoices.Count != 0 && canContinueToNextLine)
        {
            submitSkip = false;
            MakeChoice();
        }
        else if (submitSkip && currentStory.currentChoices.Count == 0 && canContinueToNextLine)
        {
            submitSkip = false;
            ContinueStory();
            Debug.Log("You pressed space to continue");
        }
    }


    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        if (SceneManager.GetActiveScene().name == "Tavern")
        {
            inkExternalFunctions.Bind(currentStory, diePopup);
            inkExternalFunctions.Bind(currentStory, anthonyNPC);
            inkExternalFunctions.Bind(currentStory, Nathaniel);
        }
        if (SceneManager.GetActiveScene().name == "MalirRoom1")
        {
            inkExternalFunctions.Bind(currentStory, boxWing);
            inkExternalFunctions.Bind(currentStory, boxRat);
        }

        if (SceneManager.GetActiveScene().name == "MalirRoom3")
        {
            inkExternalFunctions.Bind(currentStory, statueLeft);
            inkExternalFunctions.Bind(currentStory, statueMid);
            inkExternalFunctions.Bind(currentStory, statueRight);
            inkExternalFunctions.Bind(currentStory, boxPaper);
        }




        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");

        ContinueStory();
    }

    public IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.1f);

        //inkExternalFunctions.Unbind(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (GameManager.instance.gettingItem) return;
            submitSkip = false;
            //StartCoroutine(Wait());
            //Debug.Log("Can Continue");
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            string nextLine = currentStory.Continue();

            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                StartCoroutine(ExitDialogueMode());
            }
            else
            {
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        continueIcon.SetActive(false);
        HideChoices();

        submitSkip = false;

        canContinueToNextLine = false;

        //StartCoroutine(CanSkip());

        bool isAddingRichTextTag = false;
        //StartCoroutine(Wait());
        //yield return new WaitForEndOfFrame();
        foreach (char letter in line.ToCharArray())
        {

            if (submitSkip)
            {
                submitSkip = false;
                dialogueText.maxVisibleCharacters = line.Length;
                //yield return new WaitForSeconds(0.1f);
                break;
            }

            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        choiceIndicator.SetActive(false);
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Could not parse tag: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not handled properly: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.Log("More choices than UI can support");
        }

        if (currentChoices.Count == 0)
        {
            continueIcon.SetActive(true);
        }
        else
        {
            continueIcon.SetActive(false);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        selectedIndex = 0;
        if (choicesButtons[selectedIndex].IsActive())
        {
            choiceIndicator.SetActive(true);
            buttonAnimator.Play(selectedIndex.ToString());
        }
        else
        {
            choiceIndicator.SetActive(false);
        }
    }
    private void HandleButtonMove(KeyCode pressed)
    {
        if (pressed == KeyCode.RightArrow)
        {
            if (choicesButtons[selectedIndex + 1].IsActive())
            {
                selectedIndex++;
                buttonAnimator.Play(selectedIndex.ToString());
            }
        }
        if (pressed == KeyCode.LeftArrow)
        {
            if (selectedIndex != 0)
            {
                selectedIndex--;
                buttonAnimator.Play(selectedIndex.ToString());
            }
        }
    }

    public void MakeChoice()
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(selectedIndex);
            Debug.Log("You made a choice, continuing");
            submitSkip = false;
            ContinueStory();
        }
    }
}
