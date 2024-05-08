using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test5 : MonoBehaviour
{
    float value;
    void Start()
    {
        for(int i = 0;i < 1000000;i++)
        {
            float a = Random.Range(2,100);
            if( a > 50f && a != 50 )
            {
                value++;
            }
            else
            {
                value--;
            }
        }
        Debug.Log("Значение " + value);
    }
}
