using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Arhive {
public class Loot : MonoBehaviour
{
    public int Quantity = 8;
    Transform c;
    public GameObject ObjectParent;
    public enum ControlType{BulletYellow,BulletRed,BulletBlue,BulletGren,Weapon,dafault,x2,x4,x8,x15};
    public ControlType currentType;
    void Start()
    {
    }

    void Update()
    {

    }
    
}
}