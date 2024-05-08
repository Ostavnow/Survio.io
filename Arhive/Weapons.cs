using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Arhive {
public class Weapons : MonoBehaviour
{
    public string Name;
    public Transform shotPoint;
    public float lifetime;
    public float timeBtwShots;
    public float starimeBtwShots;
    public float Scatter;

    public Joystick joystick1;
    public PlayerController player;
    private Vector3 diffirence;
    public float speed;
    public float distanse;
    public int damage;
    public GameObject bullet;
    public LayerMask whatInSolid;
    public int isClip = 30;
    public int clip = 0;
    public int QuantityBullet = 1;
    public enum ControlType{BulletYellow,BulletRed,BulletBlue,BulletGren};
    public ControlType controlType;
    public PlayerController.HandPosition positionHand;
    
    void Start() {    
    }
    void Update()
    { 
       Bullet bullets = bullet.GetComponent<Bullet>();
       bullets.speed = speed;
       bullets.distanse = distanse;
       bullets.damage = damage;
       bullets.whatInSolid = whatInSolid;
       Player players = gameObject.GetComponent<Player>();
    }
}
}