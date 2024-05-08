using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basement : MonoBehaviour
{
    private GameObject bodes;
    private GameObject bodesOur;
    private int count;
    private SpriteRenderer sprite;
    private BoxCollider[] colliders;
    void Start()
    {
        colliders = transform.parent.GetComponents<BoxCollider>();
       bodes = GameObject.Find("Mansion/bodes");
       bodesOur = GetComponent<Transform>().GetChild(0).gameObject;
       sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
        PlayerController player = other.GetComponent<PlayerController>();
        other.GetComponent<SpriteRenderer>().sortingOrder = 7;
        other.GetComponent<Transform>().GetChild(0).GetComponent<Transform>().GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 8;
        other.GetComponent<Transform>().GetChild(0).GetComponent<Transform>().GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 8;
        if(player.additionalWeapon != null)
        {
            AllSetLayerWeapon(player.additionalWeapon.allSpriteRendererPrefab,8);
        }
        if(player.mainWeapon != null)
        {
            AllSetLayerWeapon(player.mainWeapon.allSpriteRendererPrefab,8);
        }
        GameObject.Find("Mansion/steps").GetComponent<SpriteRenderer>().sortingOrder = 5;
        count++;
        sprite.color = new Color(1,1,1,1);
        for(int i = 0;i< colliders.Length;i++)
        {
            colliders[i].enabled = false;
        }
        bodesOur.SetActive(true);
        bodes.SetActive(false);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
        PlayerController player = other.GetComponent<PlayerController>();
        count--;
        if(count == 0)
        {
        other.GetComponent<SpriteRenderer>().sortingOrder = 2;
        other.GetComponent<Transform>().GetChild(0).GetComponent<Transform>().GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 3;
        other.GetComponent<Transform>().GetChild(0).GetComponent<Transform>().GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 3;
        GameObject.Find("Mansion/steps").GetComponent<SpriteRenderer>().sortingOrder = 0;
        if(player.additionalWeapon != null)
        {
            AllSetLayerWeapon(player.additionalWeapon.allSpriteRendererPrefab,8);
        }
        if(player.mainWeapon != null)
        {
            AllSetLayerWeapon(player.mainWeapon.allSpriteRendererPrefab,8);
        }
            for(int i = 0;i< colliders.Length;i++)
        {
            colliders[i].enabled = true;
        }
            sprite.color = new Color(1,1,1,0);
        bodesOur.SetActive(false);
        bodes.SetActive(true);
        }
        }
    }
    private void AllSetLayerWeapon(SpriteRenderer [] allSpriteRenderer,int layer)
    {
        for(int i = 0; i < allSpriteRenderer.Length; i++)
        {
            allSpriteRenderer[i].sortingOrder = layer;
        }
    }
}
