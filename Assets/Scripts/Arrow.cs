using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] [Range(5, 20)] float speed = 5.0f;
    [SerializeField] [Range(5, 20)] float lifetime = 5.0f;    
    [SerializeField] [Range(1, 3)] int damage = 1;   
    
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
 
     void Update()
    {
        transform.position += -(transform.right) * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        // Destroy the arrow when it collides with walls or player
        if(other.gameObject.CompareTag("Environment")) 
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.updateHealth(damage);
            Destroy(gameObject);
        }
    }
}
