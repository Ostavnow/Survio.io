using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public static bool isHit;
    [HideInInspector]
    public Collider2D rightHand;
    void Start()
    {
        rightHand = GetComponent<Collider2D>();
        rightHand.enabled = false;
    }

    public void Hit()
    {
        if(!isHit)
        {
                isHit = true;
                rightHand.enabled = true;
        }
        else
        {
                isHit = false;
                rightHand.enabled = false;
        }
    }
}
