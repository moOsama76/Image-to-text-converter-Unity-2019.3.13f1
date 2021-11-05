using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MyEnumeratedType
{
    firstIsBased, Centered
}

public class StringSprite : MonoBehaviour
{
    // public
    public MyEnumeratedType formate;
    public string txt;
    public int layer;
    public float lettersDistancing = 0.5f;
    public Transform mainSprite;
    public char[] character;
    public Sprite[] sprite;

    // private
    float posX;
    int lastIndex = 0;
    int maxTyped = 0;


    void Start()
    {
        posX = transform.position.x;
        if (txt.Length > maxTyped)
        {
            maxTyped = txt.Length;
        }

        for (int i = lastIndex; i < txt.Length; i++)
        {
            Transform newSprite = Instantiate(mainSprite, new Vector2(0, 0), Quaternion.identity);
            newSprite.SetParent(transform);
            newSprite.name = i.ToString();
            lastIndex = i + 1;
        }



        for (int i = 0; i < txt.Length; i++)
        {
            for (int j = 0; j < character.Length; j++)
            {
                if (txt[i] == character[j])
                {
                    SpriteRenderer newSprite = transform.GetChild((i)).GetComponent<SpriteRenderer>();
                    newSprite.sprite = sprite[j];
                    newSprite.sortingOrder = layer;
                }
            }
        }

        for (int i = maxTyped - 1; i >= txt.Length; i--)
        {
            SpriteRenderer newSprite = transform.GetChild((i)).GetComponent<SpriteRenderer>();
            newSprite.sprite = null;
        }

        if (txt.Length > 0)
        {
            if (formate == MyEnumeratedType.Centered)
            {
                Transform midChar = transform.GetChild(((txt.Length - 1) / 2));
                if (txt.Length % 2 == 0)
                {
                    midChar.transform.position = new Vector2(-lettersDistancing / 2 + transform.position.x, transform.position.y);
                }
                else
                {
                    midChar.transform.position = new Vector2(transform.position.x, transform.position.y);
                }
                for (int i = (txt.Length - 1) / 2 + 1; i < txt.Length; i++)
                {
                    transform.GetChild((i)).transform.position = new Vector2(transform.GetChild((i - 1)).transform.position.x + lettersDistancing, transform.position.y);
                }
                for (int i = (txt.Length - 1) / 2 - 1; i >= 0; i--)
                {
                    transform.GetChild((i)).transform.position = new Vector2(transform.GetChild((i + 1)).transform.position.x - lettersDistancing, transform.position.y);
                }
            }
            else
            {

                transform.GetChild(0).transform.position = new Vector2(transform.position.x, transform.position.y);
                for (int i = 1; i < txt.Length; i++)
                {
                    transform.GetChild((i)).transform.position = new Vector2(transform.GetChild((i - 1)).transform.position.x + lettersDistancing, transform.position.y);
                }

            }
        }
    }




}
