using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flatformlgnore : MonoBehaviour
{
    public BoxCollider2D platformCollider;
    
    private void Start()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.X))
            {
                Physics2D.IgnoreCollision(collision.GetComponent<CapsuleCollider2D>(), platformCollider, true);
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<CapsuleCollider2D>(), platformCollider, false);
        }
    }
}
