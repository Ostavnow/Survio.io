using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Arhive {
public class Map2dGrid : MonoBehaviour
{
    [Header("Количество линий")]
    public float CellSize = 10;
    [Header("Ширина линий")]
    public float widht = 0.5f;
    [Header("Карта")]
    public GameObject Map;
    [Header("Спрайт полосок")]
    public Sprite sprite;
    [Header("Цвет")]
    public Color color;
    
    

    public void GridGen(){
            GameObject Grid = new GameObject("Grid");
            Grid.transform.position = transform.position;
            Grid.transform.SetParent(transform);




            GameObject Line = new GameObject("Line");
            SpriteRenderer spriteRenderer = Line.AddComponent<SpriteRenderer>();
            spriteRenderer.color = color;
            spriteRenderer.sprite = sprite;
            Line.layer = 0;
            Line.transform.parent = Grid.transform;
            Line.transform.localScale = new Vector3(widht, transform.localScale.y, -0.1f);
            Line.transform.position = new Vector3(0, transform.localScale.y /2, 0.1f);
            Line.transform.rotation = Quaternion.Euler(0,0,90);
            for(float i = 0; i < transform.localScale.x / CellSize; i ++){
                GameObject LineX = Instantiate(Line,new Vector3(i * CellSize - Map.transform.localScale.x / 2 ,transform.position.y,transform.position.z - 1),Quaternion.identity,Grid.transform);
                GameObject LineY = Instantiate(Line,new Vector3(transform.position.x,(i * CellSize - Map.transform.localScale.y / 2 ) - -10,transform.position.z - 1),Quaternion.Euler(0,0,90),Grid.transform);
                LineX.transform.localScale = new Vector3(widht, transform.localScale.y, -0.1f);
                LineY.transform.localScale = new Vector3(widht, transform.localScale.x, -0.1f);
            }
    }






    void Start() {
        GridGen();
    }
}
}