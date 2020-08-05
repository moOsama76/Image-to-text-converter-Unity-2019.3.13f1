using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MyEnumeratedType 
{
    firstIsBased, Centered
}

public class SrtingSprite : MonoBehaviour
{

    // public
    public MyEnumeratedType formate;
    public string txt;
    public int layer;
    public float lettersDistancing = 0.5f;

    // private
    Transform mainSprite;
    float posX;
    int lastIndex = 0;
    public char [] character;
    public Sprite [] sprite;
    int maxTyped = 0;

    
    void Start()
    {
        posX = transform.position.x; 
        mainSprite = GameObject.Find("New Sprite").transform;
    }

    void Update()
    {
        if(txt.Length > maxTyped){
            maxTyped = txt.Length;
        }
        
        for(int i = lastIndex; i < txt.Length; i++){
            Transform newSprite = Instantiate(mainSprite, new Vector2(0, 0), Quaternion.identity);
            newSprite.name = i.ToString();
            lastIndex = i + 1;
        }



        for(int i = 0; i < txt.Length; i++){
            for(int j = 0; j < character.Length; j++){
                if(txt[i] == character[j]){
                    SpriteRenderer newSprite = GameObject.Find((i).ToString()).GetComponent<SpriteRenderer>();
                    newSprite.sprite = sprite[j];
                    newSprite.sortingOrder = layer;
                }
            }
        }

        for(int i = maxTyped-1; i >= txt.Length; i--){
            SpriteRenderer newSprite = GameObject.Find((i).ToString()).GetComponent<SpriteRenderer>();
            newSprite.sprite = null;
        }

        if(txt.Length > 0){
            if(formate == MyEnumeratedType.Centered){
                GameObject midChar = GameObject.Find(((txt.Length-1) / 2).ToString());
                if(txt.Length %  2 == 0){
                    midChar.transform.position = new Vector2(-lettersDistancing / 2 + transform.position.x, transform.position.y);
                } else {
                    midChar.transform.position = new Vector2 (transform.position.x, transform.position.y);
                }
                for(int i = (txt.Length-1) / 2 + 1; i < txt.Length; i++){
                    GameObject.Find((i).ToString()).transform.position = new Vector2(GameObject.Find((i-1).ToString()).transform.position.x + lettersDistancing, transform.position.y);
                }
                for(int i = (txt.Length-1) / 2 - 1; i >= 0; i--){
                    GameObject.Find((i).ToString()).transform.position = new Vector2(GameObject.Find((i+1).ToString()).transform.position.x - lettersDistancing, transform.position.y);
                }
            } else {

                GameObject.Find("0").transform.position = new Vector2(transform.position.x, transform.position.y);
                for(int i = 1; i < txt.Length; i++){
                    GameObject.Find((i).ToString()).transform.position = new Vector2(GameObject.Find((i-1).ToString()).transform.position.x + lettersDistancing, transform.position.y);
                }
                
            }
        }
    }
}
