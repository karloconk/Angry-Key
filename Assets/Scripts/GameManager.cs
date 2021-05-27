using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager singletonInstance;
    //public int avgFrameRate;
    [SerializeField] int time = 30;
    [SerializeField]static int numberOfEnemiesOnlevel = 10;

    public TMP_Text display_Text;
    Queue<EnemyController> enemiesOnLevel = new Queue<EnemyController>(numberOfEnemiesOnlevel);

    [SerializeField]bool isInGame = true;
    public UnityEvent OnTimeout;

    void Awake()
    {
        //Destroy singleton instance if reinstanciated
        if (singletonInstance == null)
        {
            singletonInstance = this;
        } 
        else 
        {
            Destroy(gameObject);
        }

        if (isInGame)
        {
            display_Text.text = "Time: " + time;
            StartCoroutine(Timer());
        }

        // Empty this on instanciation
        CleanEnemies();
    }

    void Update ()
    {
        /* Saving this fps count for later
        float current     = Time.frameCount / Time.time;
        display_Text.text = current.ToString() + " FPS";
        */
    }

    IEnumerator Timer()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            display_Text.text = "Time: " + time;
        }
        OnTimeout.Invoke();
    }

    public void CycleEnemies(EnemyController newEnemy) 
    {
        // Only numberOfEnemiesOnlevel enemies on level 
        if (enemiesOnLevel.Count == numberOfEnemiesOnlevel)
        {
            Destroy(enemiesOnLevel.Dequeue().gameObject);
        }
        enemiesOnLevel.Enqueue(newEnemy);
    }

    public void CleanEnemies() 
    {
        // destroy objects and clean data structure (queue)
        foreach (var enemy in enemiesOnLevel)
        {
            Destroy(enemy.gameObject);
        }
        enemiesOnLevel = new Queue<EnemyController>(numberOfEnemiesOnlevel);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit() 
    {
        Application.Quit();
    }
}
