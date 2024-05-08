using System.Collections;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Object Object;
    public enum LootType{Weapon,ShotgunCartrigdes,PistonCartrigdes,RifleCartridges,MedKid,Pills,Soda,Bandage,backpacksLevel,YellowCartridges,RedCartridges,BlueCartridges,GreenCartridges};
    public LootType type;
    public int quantity;
    public void MoveStartCorutine()
    {
        StartCoroutine(MoveLoot());
    }
    private IEnumerator MoveLoot()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        yield return null;
        transform.eulerAngles = player.transform.eulerAngles +new Vector3(0,0,Random.Range(-30,30));
        Vector2 move = -transform.right;
        transform.eulerAngles = new Vector3(0,0,0);
        float time = 1;
        while(time > 0)
        {
            time -= Time.deltaTime;
            transform.Translate(move * 5 * time * Time.deltaTime);
            yield return null;
        }
        tag = "Loot";
    }
}
