using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Player player;
    private Joystick joystickMove;
    private Joystick joystickAtack;
    private Vector3 direction;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public static bool isMoving;
    private Animator animator;
    private Collider2D colideerDoor;
    [HideInInspector]
    public GameObject changerDoor;
    public Button changerDoorButton;
    public Weapon mainWeapon;
    public Weapon additionalWeapon;
    public enum TypeWeapon{meleeWeapon,additionalWeapon,mainWeapon}
    public TypeWeapon currentWeapon = TypeWeapon.meleeWeapon;

    private GameObject panelInfoCratridges;
    private GameObject panelInteraction;
    private GameObject panelAdditionalWeapon;
    private GameObject panelMeleeWeapon;
    private GameObject panelMainWeapon;
    private GameObject panelCounterYellowCartridges;
    private GameObject panelCounterRedCartridges;
    private GameObject panelCounterBlueCartridges;
    private GameObject panelCounterGreenCartridges;
    private GameObject panelReloadTime;
    private GameObject panelBandage;
    private GameObject panelMedKid;
    private GameObject panelSoda;
    private GameObject panelPills;
    
    private TMP_Text remainingReloadTime;
    private Image loadingLineReload;
    private GameObject loot;
    private Weapon weapon;
    public GameObject prefabYellowCartridger;
    public GameObject prefabRedCartridger;
    public GameObject prefabBlueCartridger;
    public GameObject prefabGreenCartridger;
    private Collider2D meleeCollider;
    public float speed = 5;
    private float rotZ;
    public bool isRecharge;
    public bool isComputerControl;
    private void Start() {
        player = GetComponent<Player>();
        changerDoorButton = GameObject.Find("Canvas/Changer door").transform.GetChild(2).GetComponent<Button>();
        changerDoor = GameObject.Find("Canvas/Changer door");
        remainingReloadTime = GameObject.Find("Canvas").transform.GetChild(12).GetChild(2).GetComponent<TMP_Text>();
        loadingLineReload = GameObject.Find("Canvas").transform.GetChild(12).GetChild(1).GetComponent<Image>();
        panelInfoCratridges = GameObject.Find("Canvas").transform.GetChild(7).gameObject;
        panelMeleeWeapon = GameObject.Find("Canvas").transform.GetChild(5).gameObject;
        panelAdditionalWeapon = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
        panelMainWeapon = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
        panelCounterYellowCartridges = GameObject.Find("Canvas").transform.GetChild(8).gameObject;
        panelCounterRedCartridges = GameObject.Find("Canvas").transform.GetChild(9).gameObject;
        panelCounterBlueCartridges = GameObject.Find("Canvas").transform.GetChild(10).gameObject;
        panelCounterGreenCartridges = GameObject.Find("Canvas").transform.GetChild(11).gameObject;
        panelInteraction = GameObject.Find("Canvas").transform.GetChild(6).gameObject;
        joystickMove = GameObject.Find("Canvas/Move Joystick").GetComponent<Joystick>();
        joystickAtack = GameObject.Find("Canvas/Atack Joystick").GetComponent<Joystick>();
        meleeCollider = transform.GetChild(0).GetComponent<Collider2D>();
        panelReloadTime = remainingReloadTime.transform.parent.gameObject;
        changerDoor.SetActive(false);
        panelReloadTime.SetActive(false);
        panelInfoCratridges.SetActive(false);
        panelInteraction.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Transform>().GetChild(0).GetComponent<Animator>();
    }
    private void Update() {
        if(isComputerControl)
        {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
               isMoving = false; 
            }
            else
            {
                isMoving = true;
            }
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;
            if(Input.GetMouseButton(0))
            {
                Debug.Log("Мы нажали на кнопку");
                Attack();
            }
        }
        else
        {
        moveInput = new Vector2(joystickMove.Horizontal,joystickMove.Vertical);
        if(Mathf.Abs(joystickAtack.Horizontal) != 0f || Mathf.Abs(joystickAtack.Vertical) != 0f)
        {
            rotZ = Mathf.Atan2(joystickAtack.Vertical,joystickAtack.Horizontal) * Mathf.Rad2Deg;
            if(Mathf.Abs(joystickAtack.Horizontal) > 0.7f | Mathf.Abs(joystickAtack.Vertical) > 0.7f && !AnimationEvent.isHit)
            {
                Attack();
            }
        }
        else if(Mathf.Abs(moveInput.x) > 0 | Mathf.Abs(moveInput.y) > 0)
        {
            isMoving = true;
            rotZ = Mathf.Atan2(joystickMove.Vertical,joystickMove.Horizontal) * Mathf.Rad2Deg;
        }
        else
        {
            isMoving = false;
        }
        }  
        transform.rotation = Quaternion.Euler(0,0,rotZ); 
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveInput * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("door"))
        {
            colideerDoor = other;
            changerDoorButton.GetComponent<Button>().onClick.AddListener(delegate { InteractionButtonDoor(); });
            changerDoor.SetActive(true);
        }
        else if(other.CompareTag("Player"))
        {
            Debug.Log("Мы ударили ножом");
            meleeCollider.enabled = false;
        }
        else if(other.CompareTag("Loot"))
        {
            if(other.GetComponent<Loot>().type == Loot.LootType.Weapon)
            {
                Weapon weapon = ((GameObject) other.GetComponent<Loot>().Object).GetComponent<Weapon>();
                if(!CreateWeapon(weapon,other.gameObject))
                {
                    changerDoorButton.GetComponent<Button>().onClick.AddListener(delegate { InteractionButtonWeapon(); });
                    loot = other.gameObject;
                    this.weapon = weapon;
                    panelInteraction.SetActive(true);
                    panelInteraction.transform.GetChild(2).GetComponent<TMP_Text>().text = weapon.name;
                    panelInteraction.GetComponent<TextFilterSize>().UpdateWidthBackground();
                    colideerDoor = other;
                    changerDoor.SetActive(true);
                }
            }   
            else if(other.GetComponent<Loot>().type == Loot.LootType.YellowCartridges && player.yellowCartridges + 30 <= 90)
            {
                player.yellowCartridges += other.GetComponent<Loot>().quantity;
                UpdatePanelInfoCartridges();
                ButtonRecharge();
                Destroy(other.gameObject);
            }
            else if(other.GetComponent<Loot>().type == Loot.LootType.RedCartridges && player.redCartridges + 7 <= 14)
            {
                player.redCartridges += other.GetComponent<Loot>().quantity;
                UpdatePanelInfoCartridges();
                ButtonRecharge();
                Destroy(other.gameObject);
            }
            else if(other.GetComponent<Loot>().type == Loot.LootType.BlueCartridges && player.blueCartridges + 30 <= 90)
            {
                player.blueCartridges += other.GetComponent<Loot>().quantity;
                UpdatePanelInfoCartridges();
                ButtonRecharge();
                Destroy(other.gameObject);
            }
            else if(other.GetComponent<Loot>().type == Loot.LootType.GreenCartridges && player.greenCartridges + 30 <= 90)
            {
                player.greenCartridges += other.GetComponent<Loot>().quantity;
                UpdatePanelInfoCartridges();
                ButtonRecharge();
                Destroy(other.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("door"))
        {
            colideerDoor = other;
            changerDoor.SetActive(false);
        }
        else if(other.CompareTag("Loot"))
        {
            panelInteraction.SetActive(false);
            changerDoor.SetActive(false);
        }
    }
    private void InteractionButtonWeapon()
    {
        if(colideerDoor.GetComponent<Loot>().type == Loot.LootType.Weapon)
        {
            if(weapon.isMainWeapon)
            {
                RemoveWeapon(true,player.typeCartridges);
            }
            else
            {
                RemoveWeapon(false,player.typeCartridges);
            }
            CreateWeapon(weapon,loot);
            panelInteraction.SetActive(false);
        }
    }
    private void InteractionButtonDoor()
    {
        if(colideerDoor.GetComponent<Doors>())
        {
            Debug.Log("Дверь открыта");
        Doors door = colideerDoor.GetComponent<Doors>();
        if(door.isOpenDoor)
        {
            door.ExitDoor((door.transform.position - transform.position).normalized);
        }
        else
        {
            door.OpenDoor((door.transform.position - transform.position).normalized);
        }   
        } 
    }
    public void SwithcingKnife()
    {
        speed = 5;
        if(currentWeapon == TypeWeapon.meleeWeapon)
        {
        }
        else if(currentWeapon == TypeWeapon.additionalWeapon)
        {
            currentWeapon = TypeWeapon.meleeWeapon;
            additionalWeapon.gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
            panelInfoCratridges.SetActive(false);
            panelMeleeWeapon.transform.GetChild(0).GetComponent<Image>().color = new Color(0,0,0,100/255f);
            panelAdditionalWeapon.transform.GetChild(0).GetComponent<Image>().color = new Color(0,0,0,10/255f);
            if(isRecharge)
            {
                panelReloadTime.SetActive(false);
                isRecharge = false;
            }
        }
        else if(currentWeapon == TypeWeapon.mainWeapon)
        {
            currentWeapon = TypeWeapon.meleeWeapon;
            panelInfoCratridges.SetActive(false);
            mainWeapon.gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
            panelMeleeWeapon.transform.GetChild(0).GetComponent<Image>().color = new Color(0,0,0,100/255f);
            panelMainWeapon.transform.GetChild(0).GetComponent<Image>().color = new Color(0,0,0,10/255f);
            if(isRecharge)
            {
                panelReloadTime.SetActive(false);
                isRecharge = false;
            }
        }
    }
    public void SwithcingAdditionalWeapon()
    {
        speed = additionalWeapon.speedPlayer;
        if(currentWeapon == TypeWeapon.additionalWeapon)
        {
        }
        else if(currentWeapon == TypeWeapon.meleeWeapon)
        {
            currentWeapon = TypeWeapon.additionalWeapon;
            panelInfoCratridges.SetActive(true);
            additionalWeapon.gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
            panelMeleeWeapon.transform.GetChild(0).GetComponent<Image>().color = new Vector4(0,0,0,10/255f);
            panelAdditionalWeapon.transform.GetChild(0).GetComponent<Image>().color = new  Vector4(0,0,0,100/255f);
            player.typeCartridges = additionalWeapon.typeCartridges;
            if(additionalWeapon.quantitiBulletClip == 0)
            {
                additionalWeapon.Recharge();
            }
            else
            {
                panelReloadTime.SetActive(false);
            }
            
            UpdatePanelInfoCartridges();
        }
        else if(currentWeapon == TypeWeapon.mainWeapon)
        {
            currentWeapon = TypeWeapon.additionalWeapon;
            panelInfoCratridges.SetActive(true);
            additionalWeapon.gameObject.SetActive(true);
            mainWeapon.gameObject.SetActive(false);
            panelAdditionalWeapon.transform.GetChild(0).GetComponent<Image>().color = new  Vector4(0,0,0,100/255f);
            panelMainWeapon.transform.GetChild(0).GetComponent<Image>().color = new  Vector4(0,0,0,10/255f);
            player.typeCartridges = additionalWeapon.typeCartridges;
            panelReloadTime.SetActive(false);
            UpdatePanelInfoCartridges();
            if(additionalWeapon.quantitiBulletClip == 0)
            {
                additionalWeapon.Recharge();
            }
            else
            {
                panelReloadTime.SetActive(false);
            }
        }
    }
    public void SwithcingMainWeapon()
    {
        speed = mainWeapon.speedPlayer;
        if(currentWeapon == TypeWeapon.mainWeapon)
        {
        }
        else if(currentWeapon == TypeWeapon.meleeWeapon)
        {
            currentWeapon = TypeWeapon.mainWeapon;
            panelInfoCratridges.SetActive(true);
            mainWeapon.gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
            panelMeleeWeapon.transform.GetChild(0).GetComponent<Image>().color = new Vector4(0,0,0,10/255f);
            panelMainWeapon.transform.GetChild(0).GetComponent<Image>().color = new  Vector4(0,0,0,100/255f);
            player.typeCartridges = mainWeapon.typeCartridges;
            panelReloadTime.SetActive(false);
            UpdatePanelInfoCartridges();
            if(mainWeapon.quantitiBulletClip == 0)
            {
                mainWeapon.Recharge();
            }
            else
            {
                panelReloadTime.SetActive(false);
            }
        }
        else if(currentWeapon == TypeWeapon.additionalWeapon)
        {
            currentWeapon = TypeWeapon.mainWeapon;
            panelInfoCratridges.SetActive(true);
            mainWeapon.gameObject.SetActive(true);
            additionalWeapon.gameObject.SetActive(false);
            panelAdditionalWeapon.transform.GetChild(0).GetComponent<Image>().color = new Vector4(0,0,0,10/255f);
            panelMainWeapon.transform.GetChild(0).GetComponent<Image>().color = new Vector4(0,0,0,100/255f);
            player.typeCartridges = mainWeapon.typeCartridges;
            panelReloadTime.SetActive(false);
            UpdatePanelInfoCartridges();
            if(mainWeapon.quantitiBulletClip == 0)
            {
                mainWeapon.Recharge();
            }
            else
            {
                panelReloadTime.SetActive(false);
            }
        }
    }
    public void UpdatePanelInfoCartridges()
    {
        panelCounterYellowCartridges.transform.GetChild(1).GetComponent<TMP_Text>().text = player.yellowCartridges.ToString();
        panelCounterRedCartridges.transform.GetChild(1).GetComponent<TMP_Text>().text = player.redCartridges.ToString();
        panelCounterBlueCartridges.transform.GetChild(1).GetComponent<TMP_Text>().text = player.blueCartridges.ToString();
        panelCounterGreenCartridges.transform.GetChild(1).GetComponent<TMP_Text>().text = player.greenCartridges.ToString();
        panelInfoCratridges.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = player.currentCartridges.ToString();
        if(currentWeapon == TypeWeapon.additionalWeapon)
        {
            panelInfoCratridges.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = additionalWeapon.quantitiBulletClip.ToString();
        }
        else if(currentWeapon == TypeWeapon.mainWeapon)
        {
            panelInfoCratridges.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = mainWeapon.quantitiBulletClip.ToString();

        }
    }
    public void RemoveWeapon(bool isMainWeapon, Player.TypeCartridges typeCartridges)
    {
        SwithcingKnife();
        void CrateCartridges()
            {
             Loot cartidger = new Loot();
            if(typeCartridges == Player.TypeCartridges.yellowCartrigdes)
            {
               cartidger = Instantiate(prefabYellowCartridger,transform.position,Quaternion.identity).GetComponent<Loot>(); 
            }
            else if(typeCartridges == Player.TypeCartridges.redCartrigdes)
            {
               cartidger = Instantiate(prefabRedCartridger,transform.position,Quaternion.identity).GetComponent<Loot>(); 
            }
            else if(typeCartridges == Player.TypeCartridges.blueCartrigdes)
            {
               cartidger = Instantiate(prefabBlueCartridger,transform.position,Quaternion.identity).GetComponent<Loot>(); 
            }
            else if(typeCartridges == Player.TypeCartridges.greenCartrigdes)
            {
               cartidger = Instantiate(prefabGreenCartridger,transform.position,Quaternion.identity).GetComponent<Loot>(); 
            }
            cartidger.tag = "Untagged";
            cartidger.quantity =  isMainWeapon ? mainWeapon.quantitiBulletClip : additionalWeapon.quantitiBulletClip;
            cartidger.MoveStartCorutine();
            } 
            if(isMainWeapon)
        {
            if(mainWeapon.quantitiBulletClip > 0)
            {
            CrateCartridges();
            Debug.Log("wfwefwf");  
            }
        }
        else
        {
            if(additionalWeapon.quantitiBulletClip > 0)
            {
            CrateCartridges();
            Debug.Log("wfwefwf");  
            }
        }  
        Destroy(isMainWeapon ? mainWeapon.gameObject : additionalWeapon.gameObject);
        Loot loot;
        if(isMainWeapon)
        {
        loot = Instantiate(mainWeapon.prefabLoot,transform.position,Quaternion.identity).GetComponent<Loot>();
        mainWeapon = null;
        panelMainWeapon.SetActive(false);
        }
        else if(!isMainWeapon)
        {
        loot = Instantiate(additionalWeapon.prefabLoot,transform.position,Quaternion.identity).GetComponent<Loot>();
        additionalWeapon = null;
        panelAdditionalWeapon.SetActive(false);
        }
        else
        {
            return;
        }  
        loot.tag = "Untagged";
        loot.MoveStartCorutine();
    }
    public bool CreateWeapon(Weapon weapon,GameObject gameObject)
    {
        if(!weapon.isMainWeapon & additionalWeapon == null)
                {
                    Weapon weapon1 = Instantiate(weapon.gameObject,transform.position,Quaternion.identity).GetComponent<Weapon>();
                    weapon1.transform.SetParent(transform);
                    weapon1.Start();
                    additionalWeapon = weapon1;
                    transform.GetChild(0).gameObject.SetActive(false);
                    panelAdditionalWeapon.SetActive(true);
                    weapon1.transform.localPosition += weapon.gameObject.transform.position;
                    weapon1.transform.localEulerAngles = new Vector3(0,0,weapon.transform.eulerAngles.z);
                    panelAdditionalWeapon.transform.GetChild(1).GetComponent<TMP_Text>().text = weapon.name;
                    panelAdditionalWeapon.transform.GetChild(2).GetComponent<Image>().sprite = weapon.sprite;
                    player.typeCartridges = weapon1.typeCartridges;
                    SwithcingKnife();
                    SwithcingAdditionalWeapon();
                    Destroy(gameObject);
                    return true;
                }
                if(weapon.isMainWeapon & mainWeapon == null)
                {
                    Weapon weapon1 = Instantiate(weapon.gameObject,transform.position,Quaternion.identity).GetComponent<Weapon>();
                    weapon1.transform.SetParent(transform);
                    weapon1.Start();
                    mainWeapon = weapon1;
                    transform.GetChild(0).gameObject.SetActive(false);
                    panelMainWeapon.SetActive(true);
                    weapon1.transform.localPosition += weapon.gameObject.transform.position;
                    weapon1.transform.localEulerAngles = new Vector3(0,0,weapon.transform.eulerAngles.z);
                    player.typeCartridges = weapon1.typeCartridges;
                    panelMainWeapon.transform.GetChild(1).GetComponent<TMP_Text>().text = weapon.name;
                    panelMainWeapon.transform.GetChild(2).GetComponent<Image>().sprite = weapon.sprite;
                    SwithcingKnife();
                    SwithcingMainWeapon();
                    Destroy(gameObject);
                    return true;
                }
                    return false;
    }
    public void ButtonRecharge()
    {
        if(!isRecharge)
        {
        if(currentWeapon == TypeWeapon.additionalWeapon)
            {
                additionalWeapon.Recharge();
            }
        else if(currentWeapon == TypeWeapon.mainWeapon)
            {
                mainWeapon.Recharge();
            }
        }
        
    }
    private void Attack()
    {
        if(currentWeapon == TypeWeapon.meleeWeapon && !AnimationEvent.isHit)
                {
                    animator.SetTrigger("HitRight");
                }
                else if(currentWeapon == TypeWeapon.additionalWeapon && !additionalWeapon.isShoot && additionalWeapon.quantitiBulletClip > 0 && !isRecharge)
                {
                    StartCoroutine(additionalWeapon.Shoot(transform));
                    if(additionalWeapon.quantitiBulletClip == 0)
                    {
                        additionalWeapon.Recharge();
                    }
                    UpdatePanelInfoCartridges();
                }
                else if(currentWeapon == TypeWeapon.mainWeapon && !mainWeapon.isShoot && mainWeapon.quantitiBulletClip > 0 && !isRecharge)
                {
                    StartCoroutine(mainWeapon.Shoot(transform));
                    if(mainWeapon.quantitiBulletClip == 0)
                    {
                        mainWeapon.Recharge();
                    }
                    UpdatePanelInfoCartridges();
                }
                else if(currentWeapon == TypeWeapon.mainWeapon && !mainWeapon.isShoot && mainWeapon.isShotgun && mainWeapon.quantitiBulletClip > 0 && isRecharge)
                {
                    StartCoroutine(mainWeapon.Shoot(transform));
                    isRecharge = false;
                    panelReloadTime.SetActive(false);
                    UpdatePanelInfoCartridges();
                }
                else if(currentWeapon == TypeWeapon.mainWeapon && !mainWeapon.isShoot && mainWeapon.isShotgun && mainWeapon.quantitiBulletClip == 0 && !isRecharge)
                {
                    mainWeapon.Recharge();
                }
    }
}
