using UnityEngine;
public class Environment : MonoBehaviour
{
    float endurance = 100;
    public bool noLoot;
    public bool Bush;
    GameObject Aim2;
    float damageItem;
    [SerializeField]
    public float damageItemF = 25;
    float y;
    float x;
    public bool decrease;
    public bool explosive;
    public bool isWindow;
    public bool isWall;
    public GameObject windowsill;
    public bool medications = false;
    private RandomLoots randomLoots;
    public GameObject splinter;
    ParticleSystem effect;
    int num;
 
    void Start()
    { 
         if(Bush)
        {
            int randomLoot = Random.Range(0, randomLoots.LootsBush.Length);
            foreach (GameObject Loot in randomLoots.LootsBush[randomLoot].Loots)
            {
                Instantiate(Loot,gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            }
        }
        if(explosive)
        {
            effect = GetComponent<Transform>().GetChild(0).GetComponent<ParticleSystem>();
        }
        randomLoots = FindObjectOfType<RandomLoots>();
    }

    void RandomLoots()
    {
            if(!noLoot)
            {
                if(medications)
                {
                    int randomLoot = Random.Range(0, randomLoots.LootsMedications.Length);
                    for (int i = 0;i < randomLoots.LootsNotmal.Length;i++)
                    {
                        Instantiate(randomLoots.LootsNotmal[randomLoot].Loots[i],gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                    }
                }
                else {
                num = Random.Range(0, 100);
                if(num >=40 && num <= 50)
                {
                    return;
                }
                else if (num <= 53)
                {
                    int randomLoot = Random.Range(0, randomLoots.LootsNotmal.Length);
                    foreach (GameObject Loot in randomLoots.LootsNotmal[randomLoot].Loots)
                    {
                        Instantiate(Loot, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

                    }
                }
                else if (num >= 54 && num <= 76)
                {
                    int randomLoot = Random.Range(0, randomLoots.LootsRare.Length);
                    foreach (GameObject Loot in randomLoots.LootsRare[randomLoot].Loots)
                    {
                        Instantiate(Loot, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

                    }
                }
                else if (num >= 77 && num <= 90)
                {
                    int randomLoot = Random.Range(0, randomLoots.LootsEpic.Length);
                    foreach (GameObject Loot in randomLoots.LootsEpic[randomLoot].Loots)
                    {
                        Instantiate(Loot, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

                    }
                }
                else if (num >= 91 && num <= 97)
                {
                    int randomLoot = Random.Range(0, randomLoots.LootsMythical.Length);
                    foreach (GameObject Loot in randomLoots.LootsMythical[randomLoot].Loots)
                    {
                        Instantiate(Loot, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

                    }
                }
                else if (num >= 98 && num <= 100)
                {
                    int randomLoot = Random.Range(0, randomLoots.LootsLegendary.Length);
                    foreach (GameObject Loot in randomLoots.LootsLegendary[randomLoot].Loots)
                    {
                        Instantiate(Loot, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

                    }
                }
                Destroy(Instantiate(randomLoots.pusher,transform.position,Quaternion.identity),0.2f);    
        Debug.Log(num);
        }
    }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("hand"))
        {
            endurance -= 35;
            other.GetComponent<AnimationEvent>().rightHand.enabled = false;
        }
        else if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.GetComponent<Rigidbody2D>());
            Destroy(other.gameObject,0.3f);
            endurance -= other.gameObject.GetComponent<Bullet>().damage;
        }
        if(endurance <= 0 & !decrease)
        {
            CheckDestructibility();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Bullet"))
        {
            endurance -= other.gameObject.GetComponent<Bullet>().damage;
        }
        if(endurance <= 0 & !decrease)
        {
           CheckDestructibility();
        }   
    }
    private void CheckDestructibility()
    {
            if(isWindow)
            {
                Instantiate(windowsill,transform.position,transform.rotation);
            }
            // RandomLoots();
            if(explosive)
            {
                for(int i = 0;i < 10;i++)
                {
                    Instantiate(splinter,transform.position,Quaternion.Euler(0,0,Random.Range(0,360)));
                }
                Destroy(gameObject,0.5f);
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(this);
            effect.Play();
            }
            if(isWall)
            {
                transform.parent.GetComponent<Home>().OnDisableWall((transform.parent.position - transform.position).normalized);
                Debug.Log((transform.position - transform.parent.position).normalized);
            }
            Destroy(gameObject); 
    }
}
