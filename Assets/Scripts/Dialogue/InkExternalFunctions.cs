using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;

public class InkExternalFunctions
{
    public void Bind(Story story, GameObject obj)
    {
        if (obj.name == "DiePopup")
        {
            story.BindExternalFunction("startDice", () =>
            {
                obj.SetActive(true);
            });
        }
        if (obj.name == "StatueLeft")
        {
            story.BindExternalFunction("turnLeftLeft", () =>
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/smrtacci");
                Sprite leftsprite = sprites[0];
                Sprite midsprite = sprites[1];
                Sprite rightsprite = sprites[2];
                SpriteRenderer spriteren = obj.GetComponentInChildren<SpriteRenderer>();
                Sprite[] sprites_dir = Resources.LoadAll<Sprite>("Sprites/smrtacci_dir");
                Sprite leftSprite_dir = sprites_dir[0];
                Sprite midSprite_dir = sprites_dir[1];
                Sprite rightSprite_dir = sprites_dir[2];
                if (spriteren.sprite.name.Equals(midsprite.name))
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = leftsprite;
                }
                if (spriteren.sprite.name.Equals(rightsprite.name) || spriteren.sprite.name.Equals(rightSprite_dir.name))
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = midsprite;
                }
            });
            story.BindExternalFunction("turnLeftRight", () =>
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/smrtacci");
                Sprite leftsprite = sprites[0];
                Sprite midsprite = sprites[1];
                Sprite rightsprite = sprites[2];
                SpriteRenderer spriteren = obj.GetComponentInChildren<SpriteRenderer>();
                Sprite[] sprites_dir = Resources.LoadAll<Sprite>("Sprites/smrtacci_dir");
                Sprite leftSprite_dir = sprites_dir[0];
                Sprite midSprite_dir = sprites_dir[1];
                Sprite rightSprite_dir = sprites_dir[2];
                if (spriteren.sprite.name.Equals(midsprite.name))
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = rightsprite;
                }
                else if (spriteren.sprite.name.Equals(leftsprite.name))
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = midsprite;
                }
            });
        }
        if (obj.name == "StatueMid")
        {
            story.BindExternalFunction("turnMidLeft", () =>
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/smrtacci");
                Sprite leftsprite = sprites[0];
                Sprite midsprite = sprites[1];
                Sprite rightsprite = sprites[2];
                SpriteRenderer spriteren = obj.GetComponentInChildren<SpriteRenderer>();
                Sprite[] sprites_dir = Resources.LoadAll<Sprite>("Sprites/smrtacci_dir");
                Sprite leftSprite_dir = sprites_dir[0];
                Sprite midSprite_dir = sprites_dir[1];
                Sprite rightSprite_dir = sprites_dir[2];
                if (spriteren.sprite.name.Equals(midsprite.name))
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = leftsprite;
                }
                if (spriteren.sprite.name.Equals(rightsprite.name))
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = midsprite;
                }
            });
            story.BindExternalFunction("turnMidRight", () =>
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/smrtacci");
                Sprite leftsprite = sprites[0];
                Sprite midsprite = sprites[1];
                Sprite rightsprite = sprites[2];
                SpriteRenderer spriteren = obj.GetComponentInChildren<SpriteRenderer>();
                Sprite[] sprites_dir = Resources.LoadAll<Sprite>("Sprites/smrtacci_dir");
                Sprite leftSprite_dir = sprites_dir[0];
                Sprite midSprite_dir = sprites_dir[1];
                Sprite rightSprite_dir = sprites_dir[2];
                if (spriteren.sprite.name.Equals(midsprite.name))
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = rightsprite;
                }
                else if (spriteren.sprite.name.Equals(leftsprite.name) || spriteren.sprite.name.Equals(leftSprite_dir.name))
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = midsprite;
                }
            });
        }
        if (obj.name == "StatueRight")
        {
            story.BindExternalFunction("turnRightLeft", () =>
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/smrtacci");
                Sprite leftsprite = sprites[0];
                Sprite midsprite = sprites[1];
                Sprite rightsprite = sprites[2];
                SpriteRenderer spriteren = obj.GetComponentInChildren<SpriteRenderer>();
                Sprite[] sprites_dir = Resources.LoadAll<Sprite>("Sprites/smrtacci_dir");
                Sprite leftSprite_dir = sprites_dir[0];
                Sprite midSprite_dir = sprites_dir[1];
                Sprite rightSprite_dir = sprites_dir[2];
                if (spriteren.sprite.name.Equals(midsprite.name) || spriteren.sprite.name.Equals(midSprite_dir.name))
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = leftsprite;
                }
                if (spriteren.sprite.name.Equals(rightsprite.name))
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = midsprite;
                }
            });
            story.BindExternalFunction("turnRightRight", () =>
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/smrtacci");
                Sprite leftsprite = sprites[0];
                Sprite midsprite = sprites[1];
                Sprite rightsprite = sprites[2];
                SpriteRenderer spriteren = obj.GetComponentInChildren<SpriteRenderer>();
                Sprite[] sprites_dir = Resources.LoadAll<Sprite>("Sprites/smrtacci_dir");
                Sprite leftSprite_dir = sprites_dir[0];
                Sprite midSprite_dir = sprites_dir[1];
                Sprite rightSprite_dir = sprites_dir[2];
                if (spriteren.sprite.name.Equals(midsprite.name) || spriteren.sprite.name.Equals(midSprite_dir.name))
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = rightsprite;
                }
                else if (spriteren.sprite.name.Equals(leftsprite.name))
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = midsprite;
                }
            });
        }
        if (obj.name == "NPCAnthony")
        {
            story.BindExternalFunction("getFlowerSack", () =>
            {
                Time.timeScale = 0f;
                Sprite sprite = Resources.Load<Sprite>("Sprites/sacek");
                Item item = new Item { Name = "Sáček s květinami", Icon = sprite, Description = "Tohle musím donést malíři" };
                GameManager.instance.AddItem(item);
            });
            story.BindExternalFunction("getHeart", () =>
            {
                Time.timeScale = 0f;
                GameManager.instance.GetHeart();
            });
            story.BindExternalFunction("loseHeart", () =>
            {
                Time.timeScale = 0f;
                GameManager.instance.LoseHeart();
            });
        }
        if (obj.name == "BoxWing")
        {
            story.BindExternalFunction("getWing", () =>
            {
                Sprite[] sprite = Resources.LoadAll<Sprite>("Sprites/smrtacek_1");
                Item item = new Item { Name = "Opracovaný kámen", Icon = sprite[1], Description = "Opracovaný kus kamene, trochu připomíná křídlo" };
                GameManager.instance.malirroom1item = true;
                GameManager.instance.AddItem(item);
            });
            story.BindExternalFunction("decrementWingIndex", () =>
            {
                DialogueTrigger dt = obj.GetComponentInChildren<DialogueTrigger>();
                if (dt != null) dt.dialogueIndex--;
            });
        }
        if (obj.name == "BoxRat")
        {
            story.BindExternalFunction("takeRatDamage", () =>
            {
                Time.timeScale = 0f;
                GameManager.instance.LoseHeart();
            });
            story.BindExternalFunction("decrementRatIndex", () =>
            {
                DialogueTrigger dt = obj.GetComponentInChildren<DialogueTrigger>();
                if (dt != null) dt.dialogueIndex--;
            });
        }
        if (obj.name == "BoxPaper")
        {
            story.BindExternalFunction("getPaper", () =>
            {
                Sprite sprite = Resources.Load<Sprite>("Sprites/papir");
                Item item = new Item { Name = "Papír", Icon = sprite, Description = "Zvláštně pomalovaný papír" };
                GameManager.instance.AddItem(item);
            });
            story.BindExternalFunction("decrementPaperIndex", () =>
            {
                DialogueTrigger dt = obj.GetComponentInChildren<DialogueTrigger>();
                if (dt != null) dt.dialogueIndex--;
            });
        }
        if (obj.name == "FlowerPot")
        {
            story.BindExternalFunction("decrementFlowerIndex", () =>
            {
                DialogueTrigger dt = obj.GetComponentInChildren<DialogueTrigger>();
                if (dt != null) dt.dialogueIndex--;
            });
        }
        if (obj.name == "Nathaniel")
        {
            story.BindExternalFunction("tavernPlayNathanielAnim1", () =>
            {
                Animator nathAnimator = obj.GetComponent<Animator>();
                nathAnimator.Play("TavernNathanielAnimation1");
            });
            story.BindExternalFunction("tavernPlayNathanielAnim2", () =>
            {
                Animator nathAnimator = obj.GetComponent<Animator>();
                nathAnimator.Play("TavernNathanielAnimation2");
            });
        }
        if (obj.name == "NPC")
        {
            story.BindExternalFunction("goToEnd", () =>
            {
                GameManager.instance.LoadScene("Ending");
            });
        }
        //bidnout a nastavit co dělá funknce pro start animace
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("startDice");
        story.UnbindExternalFunction("turnLeft");
        story.UnbindExternalFunction("turnRight");
    }
}
