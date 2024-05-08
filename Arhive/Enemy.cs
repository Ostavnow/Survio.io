using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Arhive {
public class Enemy : MonoBehaviour
{
   public int health;
   void Update() {
        if(health <= 0)
        {
            Destroy(gameObject);
        }    
   }
   public void TakeDamage(int damage)
   {
       health -= damage;
   }
   void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("hand"))
    {
        TakeDamage(1);
    }    
    }
}
}