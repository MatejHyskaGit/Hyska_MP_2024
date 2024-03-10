using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;

public class InkExternalFunctions
{
    public void Bind(Story story, GameObject obj)
    {
        if(obj.name == "DiePopup")
        {
            story.BindExternalFunction("startDice", () =>
            {
                obj.SetActive(true);
            });
        }
        if(obj.name == "StatueLeft")
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
                if(spriteren.sprite.name.Equals(rightsprite.name) || spriteren.sprite.name.Equals(rightSprite_dir.name))
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
        if(obj.name == "NPCAnthony")
        {
            story.BindExternalFunction("getFlowerSack", () =>
            {
                Sprite sprite = Resources.Load<Sprite>("Sprites/sacek"); 
                Item item = new Item { Name = "Sáèek s kvìtinama", Icon = sprite, Description = "Tohle musím donést malíøovi" }; 
                GameManager.instance.AddItem(item);
            });
        }


    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("startDice");
        story.UnbindExternalFunction("turnLeft");
        story.UnbindExternalFunction("turnRight");
    }
}
