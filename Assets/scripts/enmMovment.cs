using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enmMovment : MonoBehaviour
{
    [SerializeField] float enmSpeed = 5f;
    Rigidbody2D rb;

    void Start()
    {
     rb = GetComponent<Rigidbody2D>();   
       
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(enmSpeed, 0f);
        
    }
   void Filpenm()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(rb.velocity.x)), 1f);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        enmSpeed = -enmSpeed;
        Filpenm();
    }
}

