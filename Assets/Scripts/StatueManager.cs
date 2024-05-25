using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueManager : MonoBehaviour
{

    [SerializeField] private SpriteRenderer leftSpriteR;
    [SerializeField] private SpriteRenderer midSpriteR;
    [SerializeField] private SpriteRenderer rightSpriteR;

    [SerializeField] private GameObject leftTrigger;
    [SerializeField] private GameObject midTrigger;
    [SerializeField] private GameObject rightTrigger;

    Sprite[] sprites;
    Sprite leftSprite;
    Sprite midSprite;
    Sprite rightSprite;

    Sprite[] sprites_dir;
    Sprite leftSprite_dir;
    Sprite midSprite_dir;
    Sprite rightSprite_dir;

    Sprite midSprite_broken;

    bool stFixed;

    public static bool isLocked { get; private set; }
    // Start is called before the first frame update
    void Start()
    {/*
        leftCanvas.enabled = false;
        midCanvas.enabled = false;
        rightCanvas.enabled = false;*/

        isLocked = true;
        stFixed = false;
        sprites = Resources.LoadAll<Sprite>("Sprites/smrtacci");
        leftSprite = sprites[0];
        midSprite = sprites[1];
        rightSprite = sprites[2];
        sprites_dir = Resources.LoadAll<Sprite>("Sprites/smrtacci_dir");
        leftSprite_dir = sprites_dir[0];
        midSprite_dir = sprites_dir[1];
        rightSprite_dir = sprites_dir[2];
        midSprite_broken = Resources.LoadAll<Sprite>("Sprites/smrtacek_1")[0];

        if (GameManager.instance.statueFixed)
        {
            TextAsset dialogueStatue = Resources.Load<TextAsset>("Dialogues/Statue");
            TextAsset dialogueMid1 = Resources.Load<TextAsset>("Dialogues/StatueMid");
            midTrigger.GetComponent<DialogueTrigger>().inkJSONArray = new TextAsset[2];
            midTrigger.GetComponent<DialogueTrigger>().inkJSONArray[0] = dialogueStatue;
            midTrigger.GetComponent<DialogueTrigger>().inkJSONArray[1] = dialogueMid1;
            stFixed = true;
            midSpriteR.sprite = midSprite;
        }
        else
        {
            midSpriteR.sprite = midSprite_broken;
            TextAsset dialogueMid_broken = Resources.Load<TextAsset>("Dialogues/StatueBroken");
            midTrigger.GetComponent<DialogueTrigger>().inkJSONArray = new TextAsset[1];
            midTrigger.GetComponent<DialogueTrigger>().inkJSONArray[0] = dialogueMid_broken;
        }
        if (GameManager.instance.puzzleOneIsFinished)
        {
            stFixed = true;
            isLocked = false;
            leftSpriteR.sprite = leftSprite;
            midSpriteR.sprite = rightSprite;
            rightSpriteR.sprite = rightSprite;
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.statueFixed && !stFixed)
        {
            TextAsset dialogueStatue = Resources.Load<TextAsset>("Dialogues/Statue");
            TextAsset dialogueMid1 = Resources.Load<TextAsset>("Dialogues/StatueMid");
            midTrigger.GetComponent<DialogueTrigger>().inkJSONArray = new TextAsset[2];
            midTrigger.GetComponent<DialogueTrigger>().inkJSONArray[0] = dialogueStatue;
            midTrigger.GetComponent<DialogueTrigger>().inkJSONArray[1] = dialogueMid1;
            stFixed = true;
            midSpriteR.sprite = midSprite;
        }
        if (leftSpriteR.sprite.name.Equals(rightSprite.name)) leftSpriteR.sprite = rightSprite_dir;

        if (midSpriteR.sprite.name.Equals(leftSprite.name)) midSpriteR.sprite = leftSprite_dir;

        if (rightSpriteR.sprite.name.Equals(midSprite.name)) rightSpriteR.sprite = midSprite_dir;

        if (leftSpriteR.sprite.name.Equals(leftSprite.name) && midSpriteR.sprite.name.Equals(rightSprite.name) && rightSpriteR.sprite.name.Equals(rightSprite.name))
        {
            if (GameManager.instance.puzzleOneIsFinished == false) AudioManager.Instance.PlaySound("puzzleFinished");
            GameManager.instance.puzzleOneIsFinished = true;
            isLocked = false;
            Destroy(leftTrigger);
            Destroy(midTrigger);
            Destroy(rightTrigger);
        }
    }
}
