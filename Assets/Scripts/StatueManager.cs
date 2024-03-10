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

    public static bool isLocked { get; private set; }
    // Start is called before the first frame update
    void Start()
    {/*
        leftCanvas.enabled = false;
        midCanvas.enabled = false;
        rightCanvas.enabled = false;*/
        isLocked = true;
        sprites = Resources.LoadAll<Sprite>("Sprites/smrtacci");
        leftSprite = sprites[0];
        midSprite = sprites[1];
        rightSprite = sprites[2];
        sprites_dir = Resources.LoadAll<Sprite>("Sprites/smrtacci_dir");
        leftSprite_dir = sprites_dir[0];
        midSprite_dir = sprites_dir[1];
        rightSprite_dir = sprites_dir[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (leftSpriteR.sprite.name.Equals(rightSprite.name)) leftSpriteR.sprite = rightSprite_dir;

        if (midSpriteR.sprite.name.Equals(leftSprite.name)) midSpriteR.sprite = leftSprite_dir;

        if (rightSpriteR.sprite.name.Equals(midSprite.name)) rightSpriteR.sprite = midSprite_dir;

        if(leftSpriteR.sprite.name.Equals(leftSprite.name) && midSpriteR.sprite.name.Equals(rightSprite.name) && rightSpriteR.sprite.name.Equals(rightSprite.name))
        {
            isLocked = false;
            Destroy(leftTrigger);
            Destroy(midTrigger);
            Destroy(rightTrigger);
        }
    }
}
