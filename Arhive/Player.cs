using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Arhive {
public class Player : MonoBehaviour
{
    [Header("Здоровье")]
    public float health;
    public GameManager gameManager;
    [Header("Размер камеры")]

    
    [Header("Камера")]
    public Camera cam;

    public enum CameraSizeE{x1,x2,x4,x8,x15};

    public CameraSizeE CurrentCamSize;
    CameraSizeE currentCamSize;
    public int DamageHand;
    public Loot loot;
    public GameObject lo;
    Color _Color;
    public Image image; 
    private float particleHealht;
    private float damageLines = 0;
    public Text textRed;
    public Text textBlue;
    public Text textGreen;
    public Text textYellow;
    public int QuantityRed;
    public int QuantityBlue;
    public int QuantityGreen;
    public int QuantityYellow;
    public GameObject [] inventory = new GameObject[2];
    public int currentWeapon;
    public GameObject createWeapon;
    private bool check1 = true;
    private bool check2 = true;
    private bool check3 = true;
    int count = 1;
    int count1 = 1;
    int countt = 1;
    public Image backgroundBullet;
    public Image backgroundBulletQuantity;
    GameObject objectWeapon1;
    GameObject objectWeapon2;
    Weapon Weapon1;
    Weapon Weapon2;
    Transform currentTransform;
    int isRecharge;
    int counter;
    int isSlot;
    public Joystick joystick1;
    public Button BX1;
    public Button BX2;
    public Button BX4;

    public Button BX8;

    public Button BX15;
    public Canvas canvas;
    int countCamButton = 1;
    TextAsset  textLoots;
    public float x1 = 5;
    public float x2 = 7;

    public float x4 = 10;

    public float x8 = 12;

    public float x15 = 15;
    void Start()
    {
      currentCamSize = CurrentCamSize;
        if(counter == 0)
        particleHealht = health / 98;
    }

    // Update is called once per frame
    void Update()
    {
      
      Transform currentTransforms = gameObject.GetComponentInChildren<Transform>().GetChild(0);
        GameObject RightHand = currentTransforms.gameObject;
        currentTransforms = gameObject.GetComponentInChildren<Transform>().GetChild(1);
        GameObject LeftHand = currentTransforms.gameObject;
        currentTransforms = gameObject.GetComponentInChildren<Transform>().GetChild(2);
        GameObject RightWeapon = currentTransforms.gameObject;
        currentTransforms = gameObject.GetComponentInChildren<Transform>().GetChild(3);
        GameObject LeftWeapon = currentTransforms.gameObject;
        PlayerController con = gameObject.GetComponent<PlayerController>();
      if(inventory[1] != null)
      {
      currentTransform = createWeapon.GetComponentInChildren<Transform>().GetChild(0);
       Weapon2 = currentTransform.gameObject.GetComponent<Weapon>();
      }
        if(counter == 0)
        {
        con.textClip.enabled = false;
        backgroundBulletQuantity.enabled = false;
          con.quantitiTextClip.enabled = false;
          backgroundBullet.enabled = false;
          counter++;
        }
        // создание оружия на 1 слот
      if(inventory[0] != null && count > 0)
      {
        objectWeapon1 = Instantiate(inventory[0],new Vector3(createWeapon.transform.position.x,createWeapon.transform.position.y,-1.8f),Quaternion.Euler(0,0,transform.eulerAngles.z + -90f));
        objectWeapon1.transform.SetParent(createWeapon.transform);
                currentTransform = createWeapon.GetComponentInChildren<Transform>().GetChild(0);
      Weapon1 = currentTransform.gameObject.GetComponent<Weapon>();
        Transform shot = objectWeapon1.GetComponentInChildren<Transform>().GetChild(0);
        isSlot = 1;
        backgroundBullet.enabled = true;
      int QuantiytBUllet = int.Parse(con.quantitiTextClip.text);
       con.textClip.enabled = true;
        backgroundBulletQuantity.enabled = true;
        if(Weapon1.clip == 0)
        {
          con.textClip.color = Color.red;
        }
       if(Weapon1.controlType == Weapon.ControlType.BulletGren)
        if(QuantityGreen == 0)
        {
          con.quantitiTextClip.enabled = false;
          backgroundBulletQuantity.enabled = false;
          con.textClip.color = Color.red;
        }
        else
        {
          con.quantitiTextClip.enabled = true;
          backgroundBulletQuantity.enabled = true;
        }
        count=-1;
              if(check1)
              {
                  con.currentHandPosition = Weapon1.positionHand;
              check1 = false;
              check2 = true;
              }
        Debug.Log("Weapon создан");
      }
        // создание оружия на 2 слот
      if(inventory[1] != null && count1 > 0)
      { 
       objectWeapon2 = Instantiate(inventory[1],new Vector3(createWeapon.transform.position.x,createWeapon.transform.position.y,-1.8f),Quaternion.Euler(0,0,transform.eulerAngles.z + -90f));
        objectWeapon2.transform.SetParent(createWeapon.transform);
        Transform shot = objectWeapon2.GetComponentInChildren<Transform>().GetChild(0);
        backgroundBullet.enabled = true;
      int QuantiytBUllet = int.Parse(con.quantitiTextClip.text);
        isSlot = 2;
       con.textClip.enabled = true;
        backgroundBulletQuantity.enabled = true;
        if(Weapon2.clip == 0)
        {
          con.textClip.color = Color.red;
        }
       if(Weapon2.controlType == Weapon.ControlType.BulletGren)
        if(QuantityGreen == 0)
        {
          con.quantitiTextClip.enabled = false;
          backgroundBulletQuantity.enabled = false;
          con.textClip.color = Color.red;
        }
        else
        {
          con.quantitiTextClip.enabled = true;
          backgroundBulletQuantity.enabled = true;
        }
        count1=-1;
              if(check3)
              {
                  con.currentHandPosition =  Weapon2.positionHand;
              check1 = false;
              check2 = true;
              }
        Debug.Log("Weapon создан");
      }
      // Переключение на 1 оружие
      if(Input.GetKeyDown("1") || isRecharge > 0)
      {
        if(inventory[0] != null)
      {
      objectWeapon1.SetActive(true);
        if(inventory[1] != null)
      {
        objectWeapon2.SetActive(false);
      }
      backgroundBullet.enabled = true;
        isSlot = 1;
      con.textClip.enabled = true;
        backgroundBulletQuantity.enabled = true;
        if(Weapon1.clip == 0)
        {
          con.textClip.color = Color.red;
        }
       if(Weapon1.controlType == Weapon.ControlType.BulletGren)
        if(QuantityGreen == 0)
        {
          con.quantitiTextClip.enabled = false;
          backgroundBulletQuantity.enabled = false;
          con.textClip.color = Color.red;
        }
        else
        {
          con.quantitiTextClip.enabled = true;
          backgroundBulletQuantity.enabled = true;
        }
      Debug.Log(objectWeapon1);
      objectWeapon1.GetComponent<Weapon>().enabled = true;
      
        countt=1;
              if(check1)
              {
              con.currentHandPosition = Weapon1.positionHand;
              check1 = false;
              check2 = true;
              }
      }
      }
      // Переключение на 2 оружие
      if(Input.GetKeyDown("2")  || isRecharge < 0)
      {
        if(inventory[1] != null)
      {
      objectWeapon2.SetActive(true);
      objectWeapon1.SetActive(false);
        countt=1;
        gameObject.GetComponent<Weapon>().enabled = true;
        backgroundBullet.enabled = true;
        isSlot = 2;
        con.textClip.enabled = true;
        backgroundBulletQuantity.enabled = true;
        if(Weapon2.clip == 0)
        {
          con.textClip.color = Color.red;
        }
        if(Weapon2.controlType == Weapon.ControlType.BulletGren)
        if(QuantityGreen == 0)
        {
          con.quantitiTextClip.enabled = false;
          backgroundBulletQuantity.enabled = false;
          con.textClip.color = Color.red;
        }
        else
        {
          con.quantitiTextClip.enabled = true;
          backgroundBulletQuantity.enabled = true;
        }
              if(check1)
              {
              con.currentHandPosition = Weapon2.positionHand;
              check1 = false;
              check2 = true;
              }
      }
      }
      if(Input.GetKeyDown("3"))
      {
        Debug.Log("Кнопка 3 нажата");
        if(inventory[0] != null)
        {
        Debug.Log(objectWeapon1);
       objectWeapon1.SetActive(false);
        }
        if(inventory[1] != null)
        {
         objectWeapon2.SetActive(false);
        }
        backgroundBullet.enabled = false;
        backgroundBulletQuantity.enabled = false;
        con.textClip.enabled = false;
        con.quantitiTextClip.enabled = false;
        if(check2)
              {
              con.currentHandPosition = PlayerController.HandPosition.hand;
              check1 = true;
              check2 = false;
              }
        
      }
      if(health <= 0)
        {
            gameManager.EndGame();
            Debug.Log("Конец игры!");
        }
       textYellow.text = QuantityYellow.ToString();
       textRed.text = QuantityRed.ToString();
       textBlue.text = QuantityBlue.ToString();
       textGreen.text = QuantityGreen.ToString();
       CamSize();
    }
    //Метод начинает работать когда игрок входит в триггер
    private void OnTriggerStay2D(Collider2D col)
    {  
        GameObject currentObject = col.gameObject;
        Loot currentLoot = currentObject.GetComponent<Loot>();
         Weapon currentTransform = gameObject.GetComponent<Weapon>();
         PlayerController c = gameObject.GetComponent<PlayerController>();
         CameraSize cam = gameObject.GetComponentInChildren<Transform>().GetChild(5).GetComponent<CameraSize>();
         RectTransform RX1 = BX1.GetComponent<RectTransform>();
         RectTransform RX2 = BX2.GetComponent<RectTransform>();
                RectTransform RX4 = BX4.GetComponent<RectTransform>();
                RectTransform RX8 = BX8.GetComponent<RectTransform>();
                RectTransform RX15 = BX15.GetComponent<RectTransform>();
                 GameObject Objx15 = BX15.gameObject;
                GameObject Objx8 = BX8.gameObject;
                GameObject Objx4 = BX4.gameObject;
                GameObject Objx2 = BX2.gameObject;
                //Работает когда игрок нажал на f(Подобрал лут)
        if(Input.GetKeyDown("f")){
          //Проверяется подобрал ли игрок именно лут
        if (col.CompareTag("Loot")) 
          {
            
              if(currentLoot.currentType  == Loot.ControlType.BulletYellow)
              {
                QuantityYellow += currentLoot.Quantity;
              }

              if(currentLoot.currentType  == Loot.ControlType.BulletRed)
              {
                QuantityRed += currentLoot.Quantity;
                if(isSlot == 1)
                isRecharge++;
                else
                isRecharge--;
              }
              if(currentLoot.currentType  == Loot.ControlType.BulletBlue)
              {
                QuantityBlue += currentLoot.Quantity;
              }
              if(currentLoot.currentType  == Loot.ControlType.BulletGren)
              {
                QuantityGreen += currentLoot.Quantity;
                if(!(c.currentHandPosition == PlayerController.HandPosition.hand))
                {
          c.quantitiTextClip.enabled = true;
          backgroundBulletQuantity.enabled = true;
                }
              }
              //Проверяет взяли мы именно прицел х2 и проверяет брали мы его раньше
              if(currentLoot.currentType == Loot.ControlType.x2 && !Objx2.activeSelf)
              {
                RX1.anchoredPosition = new Vector2(-30,RX1.anchoredPosition.y); 
                //Проверка ставит true на checkMax если камера должна становится шире или на checkMin если камера становится меньше,
                //и еще проверка смотрит нет ли у нас прицела больше 
                if(c.currentCamSize < x2 && !Objx15.activeSelf && !Objx8.activeSelf && !Objx4.activeSelf)
                {
                  c.checkMin = false;
                  c.checkMax = true;
                }
                c.SizeCam = x2;
                RX1 = BX1.GetComponent<RectTransform>();
                
                if(countCamButton == 1)
                {
                  if(!Objx15.activeSelf)
                  {
                ButtonSize(2);
                  }
                RX15.anchoredPosition = new Vector2(80,RX1.anchoredPosition.y);
                RX2.anchoredPosition = new Vector2(25,RX1.anchoredPosition.y);
                countCamButton++;
                Objx2.SetActive(true);
                }
                if(countCamButton == 2 && !Objx2.activeSelf)
                {
                  RX1.anchoredPosition = new Vector2(-90,RX1.anchoredPosition.y);
                RX15.anchoredPosition = new Vector2(90,RX1.anchoredPosition.y);
                RX4.anchoredPosition = new Vector2(30,RX1.anchoredPosition.y);
                RX8.anchoredPosition = new Vector2(30,RX1.anchoredPosition.y);
                RX2.anchoredPosition = new Vector2(-30,RX1.anchoredPosition.y);

                countCamButton++;
                Objx2.SetActive(true);
                }
                
                if(countCamButton == 3 && Objx4.activeSelf && Objx8.activeSelf)
                {
                  RX1.anchoredPosition = new Vector2(-85,RX1.anchoredPosition.y);
                  RX2.anchoredPosition = new Vector2(-28,RX1.anchoredPosition.y);
                   RX4.anchoredPosition = new Vector2(28,RX1.anchoredPosition.y);
                   RX8.anchoredPosition = new Vector2(85,RX1.anchoredPosition.y);
                RX15.anchoredPosition = new Vector2(145,RX1.anchoredPosition.y);
                 countCamButton++;
                Objx2.SetActive(true);

                }
                if(countCamButton == 4 && Objx2.activeSelf && Objx4.activeSelf && Objx8.activeSelf && Objx15.activeSelf)
                {
                  RX1.anchoredPosition = new Vector2(-85,RX1.anchoredPosition.y);
                  RX2.anchoredPosition = new Vector2(-28,RX1.anchoredPosition.y);
                   RX4.anchoredPosition = new Vector2(28,RX1.anchoredPosition.y);
                   RX8.anchoredPosition = new Vector2(85,RX1.anchoredPosition.y);
                   RX15.anchoredPosition = new Vector2(145,RX1.anchoredPosition.y);
                 countCamButton++;
                Objx2.SetActive(true);

                }
              }
              if(currentLoot.currentType == Loot.ControlType.x4 && !Objx4.activeSelf)
              {
                RX1.anchoredPosition = new Vector2(RX1.anchoredPosition.x,RX1.anchoredPosition.y);
                if(c.currentCamSize < x4 && !Objx15.activeSelf && !Objx8.activeSelf)
                {
                  c.checkMin = false;
                  c.checkMax = true;
                }
                c.SizeCam = x4;
                RX1 = BX1.GetComponent<RectTransform>();
                
                 if(!Objx8.activeSelf && !Objx15.activeSelf && !Objx8.activeSelf)
                 {
                   ButtonSize(4);
                 }
                if(countCamButton == 1)
                {
                  RX1.anchoredPosition = new Vector2(-30,RX1.anchoredPosition.y);
                  RX4.anchoredPosition = new Vector2(30,RX1.anchoredPosition.y);
                  RX15.anchoredPosition = new Vector2(90,RX1.anchoredPosition.y);
                countCamButton++;
                Objx4.SetActive(true);
                }
                if (countCamButton >= 2 && Objx2.activeSelf && !Objx4.activeSelf && !Objx8.activeSelf)
                {
                RX1.anchoredPosition = new Vector2(-65 ,RX1.anchoredPosition.y);
                RX2.anchoredPosition = new Vector2(-7,RX1.anchoredPosition.y);
                RX4.anchoredPosition = new Vector2(51 ,RX1.anchoredPosition.y);
                RX15.anchoredPosition = new Vector2(110,RX1.anchoredPosition.y);
                countCamButton++;
                Objx4.SetActive(true);
                }
                if (countCamButton >= 2 && !Objx2.activeSelf && !Objx4.activeSelf && Objx8.activeSelf)
                {
                RX1.anchoredPosition = new Vector2(-55,RX1.anchoredPosition.y);
                RX8.anchoredPosition = new Vector2(55,RX1.anchoredPosition.y);
                RX4.anchoredPosition = new Vector2(0,RX1.anchoredPosition.y);
                RX15.anchoredPosition = new Vector2(110,RX1.anchoredPosition.y);
                countCamButton++;
                Objx4.SetActive(true);
                }
                if (countCamButton >= 2 && Objx2.activeSelf && !Objx4.activeSelf && Objx8.activeSelf)
                {
                  RX1.anchoredPosition = new Vector2(-85,RX1.anchoredPosition.y);
                  RX2.anchoredPosition = new Vector2(-28,RX1.anchoredPosition.y);
                   RX4.anchoredPosition = new Vector2(28,RX1.anchoredPosition.y);
                   RX8.anchoredPosition = new Vector2(85,RX1.anchoredPosition.y);
                   RX15.anchoredPosition = new Vector2(145,RX1.anchoredPosition.y);
                   countCamButton++;
                   Objx4.SetActive(true);
                }
              }
              if(currentLoot.currentType == Loot.ControlType.x8 && !Objx8.activeSelf)
              {
                RX1 = BX1.GetComponent<RectTransform>();
                RX1.anchoredPosition = new Vector2(RX1.anchoredPosition.x,RX1.anchoredPosition.y);
                Debug.Log("Взят 8x");
                if(c.currentCamSize < x8 && !Objx15.activeSelf)
                {
                  c.checkMin = false;
                  c.checkMax = true;
                  Debug.Log("Переключенно на 'уменьшение'");
                }
                c.SizeCam = x8;
                
                if(!Objx15.activeSelf)
                 {
                   ButtonSize(8);
                 }
                if(countCamButton == 1)
                {
                RX1.anchoredPosition = new Vector2(-30,RX1.anchoredPosition.y);
                RX8.anchoredPosition = new Vector2(27,RX1.anchoredPosition.y);
                RX15.anchoredPosition = new Vector2(85,RX1.anchoredPosition.y);
                countCamButton++;
                Objx8.SetActive(true);
                }
                if(countCamButton == 2 && Objx4.activeSelf ^ Objx2.activeSelf && !Objx8.activeSelf)
                {
                  RX1.anchoredPosition = new Vector2(-55,RX1.anchoredPosition.y);
                RX4.anchoredPosition = new Vector2(0 ,RX1.anchoredPosition.y);
                RX2.anchoredPosition = new Vector2(0 ,RX1.anchoredPosition.y);
                RX8.anchoredPosition = new Vector2(55,RX1.anchoredPosition.y);
                RX15.anchoredPosition = new Vector2(110,RX1.anchoredPosition.y);
                Objx8.SetActive(true);
                countCamButton++;
                }
                if (countCamButton >= 2 && Objx2.activeSelf && Objx4.activeSelf && !Objx8.activeSelf)
                {
                  Debug.Log("Сработало");
                  RX1.anchoredPosition = new Vector2(-85,RX1.anchoredPosition.y);
                  RX2.anchoredPosition = new Vector2(-28,RX1.anchoredPosition.y);
                   RX4.anchoredPosition = new Vector2(28,RX1.anchoredPosition.y);
                   RX8.anchoredPosition = new Vector2(85,RX8.anchoredPosition.y);
                   RX15.anchoredPosition = new Vector2(140,RX15.anchoredPosition.y);
                   Objx8.SetActive(true);
                   countCamButton++;
                }
              }

              if(currentLoot.currentType == Loot.ControlType.x15 && !Objx15.activeSelf)
              {
                if(c.currentCamSize < x15)
                {
                  Debug.Log("x15 больше");
                  c.checkMin = false;
                  c.checkMax = true;
                }else
                {
                c.checkMin = true;
                c.checkMax = false;
                }
                c.SizeCam = x15;
                RX1 = BX1.GetComponent<RectTransform>(); 
                 ButtonSize(15);
                if(countCamButton == 1 && !Objx15.activeSelf)
                {
                RX1.anchoredPosition = new Vector2(-30 + RX1.anchoredPosition.x,RX1.anchoredPosition.y);
                RX15.anchoredPosition = new Vector2(30,RX1.anchoredPosition.y);
                Objx15.SetActive(true);
                }
                if(countCamButton >= 2 && !Objx15.activeSelf)
                {
                Objx15.SetActive(true); 
                }
              }
              }
              if(currentLoot.currentType == Loot.ControlType.Weapon)
              {
                if(inventory[0] == null)
                {
                  inventory[0] = currentLoot.ObjectParent;
                }
                else if(inventory[1] == null)
                {
                  inventory[1] = currentLoot.ObjectParent;
                }
                else
                {
                  inventory[currentWeapon] = currentLoot.ObjectParent;
                }
              }
              Destroy(currentObject.transform.parent.gameObject);
          }
        }


        private void OnTriggerEnter2D(Collider2D col) {
          if(col.CompareTag("Dander"))
        {
            healths(50);
        }
        }


    public void healths(float damage){
        damageLines +=damage / particleHealht;
        health -= damage;
        RectTransform rt = image.GetComponent<RectTransform >();
    rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left,2,98 - damageLines);
    }


   public void ButtonSize(int i)
    {
       CameraSize cX1 = BX1.GetComponent<CameraSize>(); 
                CameraSize cX2 = BX2.GetComponent<CameraSize>(); 
                CameraSize cX4 = BX4.GetComponent<CameraSize>(); 
                CameraSize cX8 = BX8.GetComponent<CameraSize>(); 
                CameraSize cX15 = BX15.GetComponent<CameraSize>();
      switch (i)
      {
        case 1:
      cX4.acceptedButton = false;
      cX8.acceptedButton = false;
      cX15.acceptedButton = false;
      cX2.acceptedButton = false;
      cX1.acceptedButton = true;
      cX1.ButtonSize();
      cX2.ButtonSize();
      cX4.ButtonSize();
      cX8.ButtonSize();
      cX15.ButtonSize();

      break;
        case 2:
                cX4.acceptedButton = false;
                cX8.acceptedButton = false;
                cX15.acceptedButton = false;
                cX1.acceptedButton = false;
                cX2.acceptedButton = true;
                cX2.ButtonSize();
                cX1.ButtonSize();
                cX4.ButtonSize();
                cX8.ButtonSize();
                cX15.ButtonSize();
                break;
                case 4:
                cX2.acceptedButton = false;
                cX8.acceptedButton = false;
                cX15.acceptedButton = false;
                cX1.acceptedButton = false;
                cX4.acceptedButton = true;
                cX4.ButtonSize();
                cX2.ButtonSize();
                cX1.ButtonSize();
                cX8.ButtonSize();
                cX15.ButtonSize();
                break;
                case 8:
                cX2.acceptedButton = false;
                cX4.acceptedButton = false;
                cX15.acceptedButton = false;
                cX1.acceptedButton = false;
                cX8.acceptedButton = true;
                cX8.ButtonSize();
                cX2.ButtonSize();
                cX4.ButtonSize();
                cX1.ButtonSize();
                cX15.ButtonSize();
                break;
                case 15:
                cX4.acceptedButton = false;
                cX8.acceptedButton = false;
                cX2.acceptedButton = false;
                cX1.acceptedButton = false;
                cX15.acceptedButton = true;
                cX15.ButtonSize();
                cX2.ButtonSize();
                cX4.ButtonSize();
                cX8.ButtonSize();
                cX1.ButtonSize();
                break;
                
      }
    }


    void CamSize()
    {
      PlayerController c = gameObject.GetComponent<PlayerController>();
      if(CurrentCamSize != currentCamSize){
      switch(CurrentCamSize)
      {
        case CameraSizeE.x1:
                if(c.currentCamSize > x1)
                {
                c.checkMin = true;
                c.checkMax = false;
                }
                c.SizeCam = x1;
        currentCamSize = CurrentCamSize;
        break;
        case CameraSizeE.x2:
        if(c.currentCamSize < x2)
                {
                  c.checkMin = false;
                  c.checkMax = true;
                }
                else if(c.currentCamSize > x2)
                {
                c.checkMin = true;
                c.checkMax = false;
                }
                c.SizeCam = x2;
        currentCamSize = CurrentCamSize;
        break;
        case CameraSizeE.x4:
        if(c.currentCamSize < x4)
                {
                  c.checkMin = false;
                  c.checkMax = true;
                }
                else if(c.currentCamSize > x4)
                {
                c.checkMin = true;
                c.checkMax = false;
                }
                c.SizeCam = x4;
        currentCamSize = CurrentCamSize;
        break;
        case CameraSizeE.x8:
        if(c.currentCamSize < x8)
                {
                  c.checkMin = false;
                  c.checkMax = true;
                }
                else if(c.currentCamSize > x8)
                {
                c.checkMin = true;
                c.checkMax = false;
                }
                c.SizeCam = x8;
       currentCamSize = CurrentCamSize;
        break;
        case CameraSizeE.x15:
        if(c.currentCamSize < x15)
                {
                  c.checkMin = false;
                  c.checkMax = true;
                }
                else if(c.currentCamSize > x15)
                {
                c.checkMin = true;
                c.checkMax = false;
                }
                c.SizeCam = x15;
        currentCamSize = CurrentCamSize;
        break;
      }
      }
    }
}
}