using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        ChangeBoxAlpha(false);
        //StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        System.Func<KeyCode, bool> inputFunction;
        inputFunction = Input.GetKeyDown;
        if (inputFunction(KeyCode.Space))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }

    }
    public void ChangeBoxAlpha(bool on)
    {
        Image image = gameObject.GetComponent<Image>();
        //turn previously selected button white
        if (on)
        {
            image.color = new Color(1f, 1f, 1f, 1f);
            return;
        }

        image.color = new Color(1f, 1f, 1f, 0f);
        return;

        // SelectedButton.colors.normalColor = new Color(1f, 1f, 1f, 1f);
        // SelectedButton = newButton;
        // SelectedButton.colors.normalColor = new Color(1f, 0.92f, 0.016f, 1f);

    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            ChangeBoxAlpha(false);
            textComponent.text = string.Empty;
        }
    }

    public void CreateAndStartDialogue(string[] text, int length)
    {
        lines = new string[length];
        ChangeBoxAlpha(true);
        lines = text;
        StartDialogue();
    }
}
