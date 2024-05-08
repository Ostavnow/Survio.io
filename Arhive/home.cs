using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Arhive {
public class home : MonoBehaviour
{
    GameObject homeC;
    GameObject Roof;
    GameObject Wall1;
    GameObject Wall2;
    GameObject Wall3;
    GameObject Wall4;
    Transform childObject;
    public bool withoutMalls;
    public float width;
    public float height;
    float Cwidth;
    float Cheight;
    bool check = true;
    int count1;
    int count2;
    int count3;
    int count4;
    int count5;
    public int countWall;
    void Start() {
        width += 1;
        height += 1;
        Cwidth = width / 2;
        Cheight = height / 2;
    childObject = GetComponentInChildren<Transform>().Find("Крыша"); 
    Roof = childObject.gameObject;   
    if(!withoutMalls){
    childObject = GetComponentInChildren<Transform>().GetChild(1);
    Wall1 = childObject.gameObject;
    childObject = GetComponentInChildren<Transform>().GetChild(2);
    Wall2 = childObject.gameObject;
    childObject = GetComponentInChildren<Transform>().GetChild(3);
    Wall3 = childObject.gameObject;
    childObject = GetComponentInChildren<Transform>().GetChild(4);
    Wall4 = childObject.gameObject;
    }
    }
    void Update() {  
        if(!withoutMalls){
            if(Wall1 == null && Wall2 == null && Wall3 == null  && count5 == 0)
            {
                count5++;
                Destroy(Roof);
            }
        BoxCollider2D collider2D = gameObject.GetComponent<Transform>().GetComponent<BoxCollider2D>();
        if(Wall1 == null && count1 == 0)
        {
            if(gameObject.GetComponent<EdgeCollider2D>())
            {
            check = false;
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
            check = true;
            }
            count1++;
            collider2D.size = new Vector2(collider2D.size.x + width,collider2D.size.y);
            collider2D.offset = new Vector2(collider2D.offset.x + Cwidth,collider2D.offset.y);
        }  
        if(Wall2 == null && count2 == 0)
        {
            if(gameObject.GetComponent<EdgeCollider2D>())
            {
            check = false;
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
            check = true;
            }
            count2++;
             collider2D.size = new Vector2(collider2D.size.x + width,collider2D.size.y);
             collider2D.offset = new Vector2(collider2D.offset.x - Cwidth,collider2D.offset.y);
        } 
        if(Wall3 == null && count3 == 0)
        {
           if(gameObject.GetComponent<EdgeCollider2D>())
            {
            check = false;
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
            check = true;
            }
            count3++;
            collider2D.size = new Vector2(collider2D.size.x,collider2D.size.y + height);
             collider2D.offset = new Vector2(collider2D.offset.x,collider2D.offset.y + Cheight);
        } 
         if(Wall4 == null && count4 == 0 && countWall <= 4)
         {
            if(gameObject.GetComponent<EdgeCollider2D>())
            {
            check = false;
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
            check = true;
            }
             count4++;
              collider2D.size = new Vector2(collider2D.size.x,collider2D.size.y + height);
             collider2D.offset = new Vector2(collider2D.offset.x,collider2D.offset.y - Cheight);
         }
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !(Roof == null))
        {
            Debug.Log("Вход");
            ChangeColor();
        
        }
   }
   void OnTriggerExit2D(Collider2D other) {
       if(other.CompareTag("Player") && !(Roof == null))
        {
            Debug.Log("Выход");
        ChangeColorExit();
        }
   }
  public void ChangeColor()
   {
       Color color = Roof.GetComponent<SpriteRenderer>().color;
       for(float c = 255;c > -255; c-=1)
       Roof.GetComponent<SpriteRenderer>().color = new Vector4(color.r,color.g,color.b,c);
   }
  public void ChangeColorExit()
   {
       if(check)
       {
       Color color = Roof.GetComponent<SpriteRenderer>().color;
       for(float c = 255;c > 0; c-=1)
       Roof.GetComponent<SpriteRenderer>().color = new Vector4(color.r,color.g,color.b,c);
       }
   }
}
}