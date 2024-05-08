using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Home : MonoBehaviour
{
    private int currentFiddingTrigger;
    public bool withoutMalls;
    public float width;
    public float height;
    bool check = true;
    public int countWall;
    BoxCollider2D colliderView;
    SpriteRenderer roof;
    void Start() {
        width += 1;
        height += 1;
        colliderView = GetComponent<BoxCollider2D>();
        roof = GetComponentInChildren<Transform>().GetChild(0).GetComponent<SpriteRenderer>();   
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            currentFiddingTrigger++;
            ChangeColor();
        }
   }
   void OnTriggerExit2D(Collider2D other) {
       if(other.CompareTag("Player"))
        {
            currentFiddingTrigger--;
            if(currentFiddingTrigger == 0)
            {
                ChangeColorExit();
            }
        
        }
   }
   public void OnDisableWall(Vector2  vector){
       countWall--;
       if(vector.x > 0.5 && vector.x > 0 && vector.y < 0.5 && vector.y > -0.5)
       {
           colliderView.size += new Vector2(width,0);
           colliderView.offset += new Vector2(width/2,0);
       }
       else if(vector.x < -0.5 && vector.x < 0 && vector.y < 0.5 && vector.y > -0.5)
       {
           colliderView.offset += new Vector2(width,0);
           colliderView.size += new Vector2(width/2,0);
       }
       else if(vector.y < -0.5 && vector.y < 0 && vector.x < 0.5 && vector.x > -0.5)
       {
           colliderView.offset += new Vector2(height,width);
           colliderView.size += new Vector2(height/2,0);
       }
       else if(vector.y > 0.5 && vector.y > 0 &&  vector.x < 0.5 && vector.x > -0.5)
       {
           colliderView.offset += new Vector2(height,0);
           colliderView.size += new Vector2(height/2,0);
       }
   }
  public void ChangeColor()
   {
        Debug.Log("Игрок вошёл в дом");
        roof.color = new Vector4(roof.color.r,roof.color.g,roof.color.b,-1);
   }
  public void ChangeColorExit()
   {
        Debug.Log("Игрок вошёл из дома");
        if(check & countWall != 0)
        {
            roof.color = new Vector4(roof.color.r,roof.color.g,roof.color.b,1);
        }
   }
}
