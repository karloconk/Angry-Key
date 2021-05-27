using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other) {
        //If it collides with the player, destroy it
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.updateHealth(player.health);
        }
    }

}
