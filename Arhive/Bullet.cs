using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Arhive {
public class Bullet : MonoBehaviour
{
    public float speed;
    public float distanse;
    public int damage;
    public int lifetime;
    public LayerMask whatInSolid;
    void Update()
    { 
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,transform.up,distanse,whatInSolid);
        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            if(hitInfo.collider.CompareTag("LootBox"))
            {
                hitInfo.collider.GetComponent<Environment>();
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
}
