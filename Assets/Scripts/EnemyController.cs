using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int maxTime = 3;
    [SerializeField] Arrow arrowPrefab;
    Animator animatorController;
    AudioSource audioSource;

    int time;

    void Start()
    {
        animatorController = GetComponent<Animator>();
        audioSource        = GetComponent<AudioSource>();
        time = 1;
        StartCoroutine(CountDownToShoot());
    }
    
    IEnumerator CountDownToShoot()
    {
        while (true) {
            while (time > 0)
            {
                yield return new WaitForSeconds(1);
                time--;
                if (time == 1)
                {
                    // Animate the sprite
                    animatorController.SetBool("isShooting", true);
                }
            }
            time = maxTime;
            Shoot();
        }
    }

    void Shoot()
    {
        // Play the bow sound, shoot and reset animator
        audioSource.Play(0);
        Instantiate(arrowPrefab,transform.position, arrowPrefab.transform.rotation);
        animatorController.SetBool("isShooting", false);
    }

}
