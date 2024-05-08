using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Weapon : MonoBehaviour
{
    public float damage;
    public float rateOfFire;
    public float spread;
    public float lifetimeSecond;
    public float speedBullet;
    public float reloadTime;
    public int maxBulletClip;
    public int quantitiBulletClip;
    public int numberBulletsCartridge = 1;
    public float speedPlayer;
    public bool isMainWeapon;
    public Player.TypeCartridges typeCartridges;
    public bool isShoot;
    public bool isShotgun;
    private Transform shotPoint;
    public GameObject prefabBullet;
    public GameObject prefabLoot;
    public Sprite sprite;
    private PlayerController playerController;
    private GameObject panelReloadTime;
    private TMP_Text remainingReloadTime;
    private Image loadingLineReload;
    [HideInInspector]
    public SpriteRenderer [] allSpriteRendererPrefab;
    private Bullet bullet;
    private Player player;
    public void Start() {
        bullet = prefabBullet.GetComponent<Bullet>();
        shotPoint = transform.GetChild(0);
        allSpriteRendererPrefab = GetComponentsInChildren<SpriteRenderer>();
        playerController = FindObjectOfType<PlayerController>();
        remainingReloadTime = GameObject.Find("Canvas").transform.GetChild(12).GetChild(2).GetComponent<TMP_Text>();
        loadingLineReload = GameObject.Find("Canvas").transform.GetChild(12).GetChild(1).GetComponent<Image>();
        panelReloadTime = remainingReloadTime.transform.parent.gameObject;
        player = FindObjectOfType<Player>();
    }
    public IEnumerator Shoot(Transform player)
    {
        isShoot = true;
        bullet.speed = speedBullet;
        bullet.lifetime = lifetimeSecond;
        bullet.damage = damage;
        for(int i = 0; i < numberBulletsCartridge;i++)
        {
            Instantiate(prefabBullet,shotPoint.position,Quaternion.Euler(0,0,player.eulerAngles.z - 90 + Random.Range(-spread,spread)));
        }
        Debug.Log(player.up);
        quantitiBulletClip--;
        yield return new WaitForSeconds(rateOfFire);
        isShoot = false;
    }
    private IEnumerator RechargeCorutine()
    {
        GameObject.Find("Canvas").transform.GetChild(12).GetChild(4).GetComponent<TMP_Text>().text = "Перезарядка";
        remainingReloadTime.transform.parent.GetComponent<TextFilterSize>().UpdateWidthBackground();
        playerController.isRecharge = true;
        if(!isShotgun)
        {
            if(player.currentCartridges > 0 && quantitiBulletClip < maxBulletClip)
            {
                float timer = reloadTime;
                panelReloadTime.SetActive(true);
                while(timer > 0)
                {
                    if(!playerController.isRecharge)
                    {
                        yield break;
                    }
                    timer -= Time.deltaTime;
                    remainingReloadTime.text = ((float)((int)(timer * 10))/10).ToString();
                    loadingLineReload.fillAmount = timer / reloadTime;
                    yield return null;
                }
                panelReloadTime.SetActive(false);
                if(!(maxBulletClip - quantitiBulletClip > player.currentCartridges))
                {
                player.currentCartridges -= maxBulletClip - quantitiBulletClip;
                quantitiBulletClip += maxBulletClip - quantitiBulletClip;
                playerController.UpdatePanelInfoCartridges();
                }
                else
                {
                quantitiBulletClip += player.currentCartridges;  
                player.currentCartridges = 0;
                playerController.UpdatePanelInfoCartridges();
                }
            }  
    }
    else
    {
        panelReloadTime.SetActive(true);
        while(player.currentCartridges > 0 && quantitiBulletClip < maxBulletClip)
        {
            float timer = reloadTime;
            while(timer > 0)
            {
                if(!playerController.isRecharge)
                {
                    yield break;
                }
                timer -= Time.deltaTime;
                remainingReloadTime.text = ((float)((int)(timer * 10))/10).ToString();
                loadingLineReload.fillAmount = timer / reloadTime;
                yield return null;
            } 
            quantitiBulletClip++;
            player.currentCartridges--;
            playerController.UpdatePanelInfoCartridges(); 
        }  
        panelReloadTime.SetActive(false);
    }
    playerController.isRecharge = false;
}
    public void Recharge()
    {
        StartCoroutine(RechargeCorutine());
    }
}