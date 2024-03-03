using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float bullet_speed = 10f;
    PlayerScript player;
    float xSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        player = FindAnyObjectByType<PlayerScript>();
        xSpeed = player.transform.localScale.x * bullet_speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(xSpeed, 0f);
        
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Enemies") 
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); 
    }
}
