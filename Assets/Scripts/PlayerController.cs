using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float acceleration = 20.0f;
    [SerializeField] public float jumpThrust = 20f;
    [SerializeField] private Transform top;
    [SerializeField] public int health = 3;
    [SerializeField] float extraDistance = 50;
    [SerializeField] int maxTime = 1;
    [SerializeField] int time = 5;
    [SerializeField] bool spawns = true;
    [SerializeField] EnemyController enemyPrefab;
    public UnityEvent OnDeath;

    Vector3 moveDirection;
    public TMP_Text display_Text;
    protected Rigidbody2D _rigidbody;
    GameManager gameManager;


    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        updateHealth(0);
    }

    void Start()
    {
        gameManager = GameManager.singletonInstance; 
        gameManager.CleanEnemies();
        StartCoroutine(EnemySpawnCorroutine());
    }

    void FixedUpdate()
    {
        // Movement controller
        moveDirection.x = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(Vector3.MoveTowards(transform.position, top.position, 2000) * jumpThrust);
        }
        _rigidbody.velocity = new Vector2( acceleration * moveDirection.x * Time.deltaTime, _rigidbody.velocity.y);
    }

    public void updateHealth(int amount)
    {
        // spawns is only true if is in-Game
        if (spawns)
        {
            this.health = this.health - (int) Mathf.Max(0.0f,  amount);
            display_Text.text = "    X " + health;
            if (health == 0)
            {
                OnDeath.Invoke();
                gameManager.CleanEnemies();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator EnemySpawnCorroutine()
    {
        while (true) {
            while (time > 0)
            {
                yield return new WaitForSeconds(1.0f);
                time--;
            }
            time = maxTime;
            Vector3 newposition = new Vector3(transform.position.x + extraDistance, transform.position.y, transform.position.z);
            if(spawns)
            {
                gameManager.CycleEnemies(Instantiate(enemyPrefab, newposition, enemyPrefab.transform.rotation));
            }
        }
    }

}
