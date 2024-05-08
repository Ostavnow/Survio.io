using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public bool isOpenDoor;
    public bool isVertical;
    public bool inverting;
    Transform parent;
    Animator animator;
    private void Start() {
        parent = transform.parent;
        animator = GetComponent<Transform>().parent.GetComponent<Animator>();
    }
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!isOpenDoor & PlayerController.isMoving)
            {
                OpenDoor((transform.position - other.transform.position).normalized);
            }
        }
    }
    public void OpenDoor(Vector2 vector2)
    {
        isOpenDoor = true;
        if(isVertical)
        {
         if(vector2.y > 0)
        {
            if(!inverting)
            {
                animator.SetTrigger("OpenInside");
            }
            else
            {
                animator.SetTrigger("OpenOutside");  
            }

        }
        if(vector2.y < 0)
        {
            if(!inverting)
            {
                animator.SetTrigger("OpenOutside");
            }
            else
            {
                animator.SetTrigger("OpenInside");

            }
        }   
        }
        else
        {
        if(vector2.x > 0)
        {
            if(!inverting)
            {
                animator.SetTrigger("OpenInside");
            }
            else
            {
                animator.SetTrigger("OpenOutside");  
            }
        }
        if(vector2.x < 0)
        {
           if(!inverting)
            {
                animator.SetTrigger("OpenOutside");
            }
            else
            {
                animator.SetTrigger("OpenInside");

            }     
        }   
        }
    }
    public void ExitDoor(Vector2 vector2)
    {
        isOpenDoor = false;
        if(isVertical)
        {
         if(vector2.y > 0)
        {
            if(!inverting)
            {
                animator.SetTrigger("ClosureInside");
            }
            else
            {
                animator.SetTrigger("ClosureOutside");  
            }

        }
        if(vector2.y < 0)
        {
            if(!inverting)
            {
                
                animator.SetTrigger("ClosureOutside");
            }
            else
            {
                animator.SetTrigger("ClosureInside");
            }
        }   
        }
        else
        {
        if(vector2.x > 0)
        {
            if(!inverting)
            {
                animator.SetTrigger("ClosureInside");
            }
            else
            {
                animator.SetTrigger("ClosureOutside");  
            }
        }
        if(vector2.x < 0)
        {
           if(!inverting)
            {
                animator.SetTrigger("ClosureOutside");
            }
            else
            {
                animator.SetTrigger("ClosureInside");
            }   
        }   
        }
    }
}
