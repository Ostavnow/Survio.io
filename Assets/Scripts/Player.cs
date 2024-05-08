using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float health = 100;
    public float adrenaline = 0;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            panelHPForeground.fillAmount = health / 100;
            Debug.Log(health);
            if(health >= 100)
            {
                panelHPForeground.color = new Color(179f/255f,179f/255f,179f/255f,1);
            }
            else
            {
                panelHPForeground.color = gradientHpForeground.Evaluate(health/100);
            }
        }
    }
    public int backpacksLevel;
    public enum TypeCartridges{yellowCartrigdes, redCartrigdes, blueCartrigdes, greenCartrigdes}
    public TypeCartridges typeCartridges;
    public int yellowCartridges;
    public int redCartridges;
    public int blueCartridges;
    public int greenCartridges;
    public int currentCartridges
    {
        get
        {
            if(typeCartridges == TypeCartridges.yellowCartrigdes)
            {
                return yellowCartridges;
            }
            else if(typeCartridges == TypeCartridges.redCartrigdes)
            {
                return redCartridges;
            }
            else if(typeCartridges == TypeCartridges.blueCartrigdes)
            {
                return blueCartridges;
            }
            else if(typeCartridges == TypeCartridges.greenCartrigdes)
            {
                return yellowCartridges;
            }
            else
            {
                return yellowCartridges;
            }
        }
        set
        {   
            if(typeCartridges == TypeCartridges.yellowCartrigdes)
            {
                yellowCartridges = value;
            }
            else if(typeCartridges == TypeCartridges.redCartrigdes)
            {
                redCartridges = value;
            }
            else if(typeCartridges == TypeCartridges.blueCartrigdes)
            {
                blueCartridges = value;
            }
            else if(typeCartridges == TypeCartridges.greenCartrigdes)
            {
                yellowCartridges = value;
            }
            else
            {
                yellowCartridges = value;
            }
        }
    }
    public int countBandages;
    public int countMedKid;
    public int countSoda;
    public int coutnPills;
    private float startTimer;
    private float timer;
    private bool isUse;
    private int num;
    private GameObject panelBandage;
    private GameObject panelMedKid;
    private GameObject panelSoda;
    private GameObject panelPills;
    private GameObject panelReloadTime;
    private Transform panelInteraction;
    private Image panelHPForeground;
    private TMP_Text remainingReloadTime;
    private Image loadingLineReload;
    private PlayerController playerController;
    private Image[] adrenalineStripe = new Image[4];
    public Gradient gradientHpForeground;
    private void Start()
    {
        panelBandage = GameObject.Find("Canvas").transform.GetChild(14).gameObject;
        panelMedKid = GameObject.Find("Canvas").transform.GetChild(15).gameObject;
        panelSoda = GameObject.Find("Canvas").transform.GetChild(16).gameObject;
        panelPills = GameObject.Find("Canvas").transform.GetChild(17).gameObject;
        panelInteraction = GameObject.Find("Canvas").transform.GetChild(6);
        adrenalineStripe[0] = GameObject.Find("Canvas").transform.GetChild(18).GetChild(0).GetChild(0).GetComponent<Image>();
        adrenalineStripe[1] = GameObject.Find("Canvas").transform.GetChild(18).GetChild(1).GetChild(0).GetComponent<Image>();
        adrenalineStripe[2] = GameObject.Find("Canvas").transform.GetChild(18).GetChild(2).GetChild(0).GetComponent<Image>();
        adrenalineStripe[3] = GameObject.Find("Canvas").transform.GetChild(18).GetChild(3).GetChild(0).GetComponent<Image>();
        remainingReloadTime = GameObject.Find("Canvas").transform.GetChild(12).GetChild(2).GetComponent<TMP_Text>();
        loadingLineReload = GameObject.Find("Canvas").transform.GetChild(12).GetChild(1).GetComponent<Image>(); 
        panelHPForeground = GameObject.Find("Canvas").transform.GetChild(13).GetChild(1).GetComponent<Image>();
        panelReloadTime = remainingReloadTime.transform.parent.gameObject;
        playerController = FindObjectOfType<PlayerController>();      
    }
    public void UseBandage()
    {
        if(!isUse)
        {
        if(countBandages > 0 & Health < 100)
        {
        timer = startTimer = 3;
        num = 0;
        isUse = true;
        GameObject.Find("Canvas").transform.GetChild(14).GetChild(1).GetComponent<TMP_Text>().text = countBandages.ToString();
        StartCoroutine(Use("Бинт",15,false));
        }
        }
    }
    public void UseMedKid()
    {
        if(!isUse)
        {
        if(countMedKid > 0 & Health < 100)
        {
        timer = startTimer = 6;
        num = 1;
        isUse = true;
         GameObject.Find("Canvas").transform.GetChild(15).GetChild(1).GetComponent<TMP_Text>().text = countMedKid.ToString();
        StartCoroutine(Use("Аптечка",100,false));
        }
        }
    }
    public void UseSoda()
    {
        if(!isUse)
        {
        if(countSoda > 0)
        {
        timer = startTimer = 3;
        num = 2;
        isUse = true;
        GameObject.Find("Canvas").transform.GetChild(16).GetChild(1).GetComponent<TMP_Text>().text = countSoda.ToString();
        StartCoroutine(Use("Сода",25,true));
        }
        }
    }
    public void UsePils()
    {
        if(!isUse)
        {
        if(coutnPills > 0)
        {
        timer = startTimer = 5;
        num = 3;
        isUse = true;
        GameObject.Find("Canvas").transform.GetChild(17).GetChild(1).GetComponent<TMP_Text>().text = coutnPills.ToString();
        StartCoroutine(Use("Пилюля",50,true));
        }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy"))
            {
                Health -= 10;
            }
        if(other.GetComponent<Loot>())
        {
            if(other.GetComponent<Loot>().type == Loot.LootType.Bandage)
            {
                countBandages++;
                panelBandage.transform.GetChild(1).GetComponent<TMP_Text>().text = countBandages.ToString();
                Destroy(other.gameObject);
            }
            
            else if(other.GetComponent<Loot>().type == Loot.LootType.MedKid)
            {
                countMedKid++;
                panelMedKid.transform.GetChild(1).GetComponent<TMP_Text>().text = countMedKid.ToString();
                Destroy(other.gameObject);
            }
            else if(other.GetComponent<Loot>().type == Loot.LootType.Soda)
            {
                countSoda++;
                panelSoda.transform.GetChild(1).GetComponent<TMP_Text>().text = countSoda.ToString();
                Destroy(other.gameObject);
            }
            else if(other.GetComponent<Loot>().type == Loot.LootType.Pills)
            {
                coutnPills++;
                panelPills.transform.GetChild(1).GetComponent<TMP_Text>().text = coutnPills.ToString();
                Destroy(other.gameObject);
            }
        }
    }
    public void InteractionButtonStopBeginTreated()
    {
            playerController.isRecharge = false;
            playerController.changerDoor.SetActive(false);
            panelInteraction.gameObject.SetActive(false);
    }
    private IEnumerator Use(string name,int value,bool isAdrenaline)
    {
        GameObject.Find("Canvas").transform.GetChild(12).GetChild(4).GetComponent<TMP_Text>().text = "Используется " + name;
        panelInteraction.GetChild(2).GetComponent<TMP_Text>().text = "Отмена";
        panelInteraction.gameObject.SetActive(true);
        panelInteraction.GetComponent<TextFilterSize>().UpdateWidthBackground();
        remainingReloadTime.transform.parent.GetComponent<TextFilterSize>().UpdateWidthBackground();
        playerController.speed = 3;
        playerController.changerDoorButton.GetComponent<Button>().onClick.AddListener(delegate { InteractionButtonStopBeginTreated(); });
        playerController.changerDoor.SetActive(true);
        panelReloadTime.SetActive(true);
        playerController.isRecharge = true;
            while(timer > 0)
            {
                if(!playerController.isRecharge)
                {
                    panelReloadTime.SetActive(false);
                    playerController.changerDoor.SetActive(false);
                    playerController.speed = 5;
                    isUse = false;
                    yield break;
                }
                timer -= Time.deltaTime;
                remainingReloadTime.text = ((float)((int)(timer * 10))/10).ToString();
                loadingLineReload.fillAmount = timer / startTimer;
                yield return null;
            }
            if(num == 0)
            {
                countBandages--;
                panelBandage.transform.GetChild(1).GetComponent<TMP_Text>().text = countBandages.ToString();
            }
            else if(num == 1)
            {
                countMedKid--;
                panelMedKid.transform.GetChild(1).GetComponent<TMP_Text>().text = countMedKid.ToString();
            }
            else if(num == 2)
            {
                countSoda--;
                panelSoda.transform.GetChild(1).GetComponent<TMP_Text>().text = countSoda.ToString();
            }
            else if(num == 3)
            {
                coutnPills--;
                panelPills.transform.GetChild(1).GetComponent<TMP_Text>().text = coutnPills.ToString();
            }
            panelReloadTime.SetActive(false);
            playerController.changerDoor.SetActive(false);
            panelInteraction.gameObject.SetActive(false);
            playerController.isRecharge = false;
            isUse = false;
            playerController.speed = 5;
            if(isAdrenaline)
            {
                if(adrenaline + value < 100)
                {
                    adrenaline += value;
                }
                else
                {
                    adrenaline = 100;
                }
                StartCoroutine(AdrenalineActivity());
                if(adrenaline > 25)
                {
                    adrenalineStripe[0].fillAmount = 1;
                }
                if(adrenaline > 47)
                {
                    adrenalineStripe[1].fillAmount = 1;
                }
                if(adrenaline > 84)
                {
                    adrenalineStripe[2].fillAmount = 1;
                }
            }
            else if(Health + value > 100)
            {
                Health = 100;
            }
            else
            {
                Health += value;
            }
    }
    private IEnumerator AdrenalineActivity()
    {
        while(adrenaline > 0)
            {
                if(adrenaline > 84)
                {
                    adrenalineStripe[3].fillAmount = (adrenaline - 85) / 15;
                    if(!(Health >= 100))
                    {
                        Health += 1.5f * Time.deltaTime;
                    }
                    playerController.speed = 6;
                }
                else if(adrenaline > 47)
                {
                    adrenalineStripe[2].fillAmount = (adrenaline - 47) / 37;
                    if(!(Health >= 100))
                    {
                        Health += 1f * Time.deltaTime;
                    }
                    playerController.speed = 6;
                }
                else if(adrenaline > 25)
                {
                    adrenalineStripe[1].fillAmount = (adrenaline - 25) / 23;
                    if(!(Health >= 100))
                    {
                        Health += 0.7f * Time.deltaTime;
                    }
                }
                else
                {
                    adrenalineStripe[0].fillAmount = adrenaline / 25;
                    if(!(Health >= 100))
                    {
                        Health += 0.3f * Time.deltaTime;
                    }
                }
                adrenaline -= 0.37f * Time.deltaTime;
                yield return null;
            }
            playerController.speed = 5;
    }
}
