using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Arhive {
public class PlayerController1 : MonoBehaviour
{
    public int health;
    public ControlType controlType;
    public Joystick joystick;
    public Joystick joystick1;

    public enum ControlType{PC, Android}
    public float speed;
    private Rigidbody2D rb;
    Vector2 moveVelocity;
    Vector2 moveInput;
    private Animator anim;
    public float rotZ;
    private Player player;
    private Vector3 difference;
    public Button CamSize;
    
    
    public Camera cam;
    [Header("X - обзор персонажа")]
    public float CameraSize = 5;
    public float offset; 
    public GameObject RightHand;
    public GameObject LeftHand;
    public enum HandPosition{Avtomat,pistolet,hand};
    public HandPosition currentHandPosition;
    private float time;
    bool check1 = true;
    bool check2 = true;
    public GameObject createWeapon;
    
    public int currentIndexWeapon;
    Weapon Weapon1;
    Weapon Weapon2;
    Weapon  weaponDefault;
    Weapon currentWeapon;
    public Text textClip;
    public Text quantitiTextClip;
    public float currentCamSize = 5;
    public float size = 5;
    public bool checkC;
  bool check;

 public float SizeCam = 5;
 public bool checkMin;
 public bool checkMax;
 private int count;
 private int count1;

    void Start()
    {
     
    
        GetComponent<FreezeCamera>().enabled = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(controlType == ControlType.PC)
        {
            joystick.gameObject.SetActive(false);
        }
    }

    void Update()
    { 
             currentCamSize = cam.orthographicSize;
   if(currentCamSize < SizeCam && checkMax)
   {
     currentCamSize += 3f * Time.deltaTime;
     checkC = false;
     count1 =1;
   }
     else if(currentCamSize > SizeCam && checkMin)
     {
       currentCamSize -= 3f * Time.deltaTime;
     checkC = false;
     count1 =1;
     }
     else if(count1 == 0)
     checkC = true;
     else
     {
         count1 = 0;
     }
      cam.orthographicSize = currentCamSize;
        Transform currentTransform = gameObject.GetComponentInChildren<Transform>().GetChild(0);
        GameObject RightHand = currentTransform.gameObject;
        currentTransform = gameObject.GetComponentInChildren<Transform>().GetChild(1);
        GameObject LeftHand = currentTransform.gameObject;
        currentTransform = gameObject.GetComponentInChildren<Transform>().GetChild(2);
        GameObject RightWeapon = currentTransform.gameObject;
        currentTransform = gameObject.GetComponentInChildren<Transform>().GetChild(3);
        GameObject LeftWeapon = currentTransform.gameObject;
        weaponDefault = gameObject.GetComponent<Weapon>();
        Player player = gameObject.GetComponent<Player>();

        if(player.inventory[0] != null)
        {
        Weapon1 = player.inventory[0].GetComponent<Weapon>();
        currentIndexWeapon = 1;
        }
        else if(player.inventory[1] != null)
        {
        Weapon2 = player.inventory[1].GetComponent<Weapon>();
        currentIndexWeapon = 2;
        }
        else
        {
        currentIndexWeapon = 0;
        }
        if(currentIndexWeapon == 1)
        {
            Debug.Log("Weapon 1 есть");
            currentWeapon = Weapon1;
        }
        else if(currentIndexWeapon == 2)
        {
            Debug.Log("Weapon 2 есть");
            currentWeapon = Weapon2;
        }
        else
        {
            currentWeapon = weaponDefault;
        }
        currentWeapon = weaponDefault;
       Bullet bullets = currentWeapon.bullet.GetComponent<Bullet>();
       bullets.speed = currentWeapon.speed;
       bullets.distanse = currentWeapon.distanse;
       bullets.damage = currentWeapon.damage;
       bullets.whatInSolid = currentWeapon.whatInSolid;
       Player players = gameObject.GetComponent<Player>();
       textClip.text = currentWeapon.clip.ToString();
       if(Input.GetKey("r"))
       {
           Recharge(players,currentWeapon);
       }
       if(currentWeapon.controlType == Weapon.ControlType.BulletGren)
       {
           quantitiTextClip.text = players.QuantityGreen.ToString();
       }
        if(currentWeapon.timeBtwShots <= 0)
        {
            if(Input.GetMouseButton(0) && controlType == ControlType.PC)
            {
                if(currentWeapon.clip > 0)
                for(int i = 0; i < currentWeapon.QuantityBullet; i++)
                {
                      GameObject bulletCurrent = Instantiate(currentWeapon.bullet,new Vector3(currentWeapon.shotPoint.transform.position.x,currentWeapon.shotPoint.transform.position.y + 0.1f * i,currentWeapon.shotPoint.transform.position.z),Quaternion.Euler(currentWeapon.shotPoint.transform.eulerAngles.x,currentWeapon.shotPoint.transform.eulerAngles.y,currentWeapon.shotPoint.transform.eulerAngles.z + Random.Range(currentWeapon.Scatter,-currentWeapon.Scatter)));
        currentWeapon.timeBtwShots = currentWeapon.starimeBtwShots;
                 Destroy(bulletCurrent,currentWeapon.lifetime);
                 --currentWeapon.clip;
                }
            }   
            else if(controlType == PlayerController.ControlType.Android && Mathf.Abs(joystick1.Horizontal) > 0.7f || Mathf.Abs(joystick1.Vertical) > 0.7f) 
            {
                if(joystick1.Horizontal != 0 || joystick1.Vertical != 0)
                {
                    for(int i = currentWeapon.QuantityBullet; i < currentWeapon.QuantityBullet; i++)
                {
                  GameObject bulletCurrent = Instantiate(currentWeapon.bullet,new Vector3(currentWeapon.shotPoint.transform.position.x,currentWeapon.shotPoint.transform.position.y + 0.1f * i,currentWeapon.shotPoint.transform.position.z),Quaternion.Euler(currentWeapon.shotPoint.transform.eulerAngles.x,currentWeapon.shotPoint.transform.eulerAngles.y,currentWeapon.shotPoint.transform.eulerAngles.z + Random.Range(currentWeapon.Scatter,-currentWeapon.Scatter)));
        currentWeapon.timeBtwShots = currentWeapon.starimeBtwShots;
                 Destroy(bulletCurrent,currentWeapon.lifetime);
                 --currentWeapon.clip;
                }
                }
            }
     else
            {
              currentWeapon.timeBtwShots -= Time.deltaTime;  
            }
        if(controlType == ControlType.PC)
        {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,rotZ + offset);
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
            
        }else if(controlType == ControlType.Android && Mathf.Abs(joystick1.Horizontal) > 0.0f || Mathf.Abs(joystick1.Vertical) > 0.0f)
        {
           rotZ = Mathf.Atan2(joystick1.Vertical, joystick1.Horizontal) * Mathf.Rad2Deg;
           transform.rotation = Quaternion.Euler(0f,0f,rotZ + offset);
        }
          if(currentHandPosition == HandPosition.Avtomat){ 
              if(check1)
              {
                  RightWeapon.SetActive(true);
                  LeftWeapon.SetActive(true);
                  RightHand.SetActive(false);
                  LeftHand.SetActive(false);
              check1 = false;
              check2 = true;
              }
          }
              else if(currentHandPosition == HandPosition.hand)
          {   
              if(check2)
              {
                  RightWeapon.SetActive(false);
                  LeftWeapon.SetActive(false);
                  RightHand.SetActive(true);
                  LeftHand.SetActive(true); 
              check1 = true;
              check2 = false;
              }
          }
          time += 1 * Time.deltaTime;
          if(currentHandPosition == HandPosition.hand && time > 0.35f){
        if(Input.GetMouseButton(0))
		{
            time = 0;
            
            if(count % 2 == 0)
            {
            Invoke("onCircleColliderLeft", 0.15f);
            Invoke("offCircleColliderLeft",0.2f); 
            anim.SetBool("isHit",true);
            }
            if(count % 2 == 1)
            {
            Invoke("onCircleColliderRight", 0.15f);
            Invoke("offCircleColliderRight",0.2f); 
            anim.SetBool("isHitRight",true);
            }
            count++;
		}
          }
        else 
        {   
            anim.SetBool("isHitRight",false);
            anim.SetBool("isHit",false);
        }
          
          
       if(controlType == ControlType.Android)
       {
           if(currentHandPosition == HandPosition.hand){
           if(joystick1.Horizontal != 0 || joystick1.Vertical != 0)
           {
            Invoke("onCircleCollider", 0.515f);
            Invoke("offCircleCollider",0.53f);
           anim.SetBool("isHit",true);
           }
        else
        {
            anim.SetBool("isHit",false);
        }
           }
           
            moveInput = new Vector2(joystick.Horizontal,joystick.Vertical);
       }
       moveVelocity = speed * moveInput.normalized;
    }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
           }
           
           public void ChangeHealth(int healthValue)
           {
               health += healthValue;
           }
           void offCircleColliderLeft()
           {
            LeftHand.GetComponent<CircleCollider2D> ().enabled = false;
           }
               void onCircleColliderLeft()
           {
            LeftHand.GetComponent<CircleCollider2D> ().enabled = true;
           }
           void offCircleColliderRight()
           {
               
            RightHand.GetComponent<CircleCollider2D> ().enabled = false;
           }
               void onCircleColliderRight()
           {
            RightHand.GetComponent<CircleCollider2D> ().enabled = true;
           }
           void Recharge(Player player,Weapon weapon)
    {
        Weapon currentTransform = gameObject.GetComponent<Weapon>();
        if(weapon.clip < weapon.isClip)
        if(weapon.controlType == Weapon.ControlType.BulletGren)
        {
            if(player.QuantityGreen > 0)
            {
                textClip.color = Color.white;
                if(player.QuantityGreen >= weapon.isClip)
                {
                    weapon.clip = weapon.isClip;
                    player.QuantityGreen -= weapon.isClip;
                }
                else if(player.QuantityGreen < weapon.isClip)
                {
                    weapon.clip = player.QuantityGreen;
                    player.QuantityGreen -= player.QuantityGreen;
                }
            }
        }
    }
}
}