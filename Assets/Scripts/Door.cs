using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public UnityEvent OnWin;

    private void OnCollisionEnter2D(Collision2D other) {
        //If it collides with the player, win
        if (other.gameObject.CompareTag("Player"))
        {
            OnWin.Invoke();
        }
    }
}
