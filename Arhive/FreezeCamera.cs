using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Arhive {
public class FreezeCamera : MonoBehaviour
{
    public GameObject Camera;
        void Update()
    {
        Camera.transform.rotation = Quaternion.Euler(0,0,0);
    }
}
}