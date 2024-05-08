using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float damage = 30;
    public bool isReflect;
    Rigidbody2D rb;
    private Vector2 prev_vel;

    private void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector2.up) * speed;
        if(isReflect)
        {
        prev_vel = rb.velocity.normalized;
        }
        else
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
        Destroy(this.gameObject,lifetime);  
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.isTrigger)
        {
        Destroy(rb);
        Destroy(gameObject,0.5f);   
        }   
    }

    void ReflectIt(Collision2D other2)
    {
        Vector2 normalvect = other2.contacts[0].normal;
        Vector2 newdirn = Vector2.Reflect(prev_vel, normalvect).normalized;
        // float rotangle =   Vector2.SignedAngle(prev_vel, normalvect);
        // transform.Rotate(new Vector3(0,0,180 + rotangle*2));
        prev_vel = newdirn;
        rb.velocity = newdirn * speed;
    }
}