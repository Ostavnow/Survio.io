using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent1 : MonoBehaviour
{
    public bool isChanget;
    private Collider2D rightHand;
    void Start()
    {
        rightHand = GetComponent<Transform>().GetChild(0).GetComponent<Collider2D>();
    }

    public void Hit()
    {
        if(!isChanget)
        {
                isChanget = true;
                rightHand.enabled = false;
        }
        else
        {
                isChanget = false;
                rightHand.enabled = true;
        }
    }
}
