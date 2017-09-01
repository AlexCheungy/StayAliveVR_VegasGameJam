using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy m_enemyPrefab;
    public Enemy m_dogPrefab;

    BoxCollider m_spawnBox;
    public BoxCollider m_destinationBox;

    Dictionary<GameObject, Enemy> m_enemiesByGameObject;
    Enemy[] m_enemies;
    const int s_numEnemies = 64;
    int m_currentEnemy;

    float m_spawnTime;
    public float m_spawnRate = 4;

    public float m_minHealth = 10f;
    public float m_maxHealth = 20f;

    public float m_minSpeed = 20f;
    public float m_maxSpeed = 40f;

    public float m_dogDamage = 3f;
    public float m_dogAttackRate = .5f;
    public float m_shooterAttackRate = .3f;

    void Awake()
    {
        m_spawnBox = GetComponent<BoxCollider>();
        Player.PlayerDeadEvent += PlayerDead;
        CreateEnemies();
    }

    void CreateEnemies()
    {
        m_enemiesByGameObject = new Dictionary<GameObject, Enemy>(s_numEnemies);
        m_enemies = new Enemy[s_numEnemies];
        Enemy enemy;
        for (int i = 0; i < s_numEnemies; ++i)
        {
            if( i % 2 == 0 )
            {
                enemy = Instantiate(m_enemyPrefab) as Enemy;
                enemy.m_currentType = Enemy.TYPE.SHOOTER;
                enemy.AttackRate = m_shooterAttackRate;
            }
            else
            {
                enemy = Instantiate(m_dogPrefab) as Enemy;
                enemy.m_currentType = Enemy.TYPE.DOG;
                enemy.AttackRate = m_dogAttackRate;
            }
            m_enemiesByGameObject.Add(enemy.gameObject, enemy);
            m_enemies[i] = enemy;
            enemy.transform.SetParent(PlayingWorld.Instance.transform);
            enemy.transform.position = transform.position;
            enemy.gameObject.SetActive(false);
        }
    }

    void Spawn()
    {
        // find random point in spawning box
        Vector3 spawnPos = new Vector3(
            Random.Range(m_spawnBox.bounds.min.x, m_spawnBox.bounds.max.x),
            Random.Range(m_spawnBox.bounds.min.y, m_spawnBox.bounds.max.y),
            Random.Range(m_spawnBox.bounds.min.z, m_spawnBox.bounds.max.z) );

        // find random point in destination box
        Vector3 destinationPos = new Vector3(
            Random.Range(m_destinationBox.bounds.min.x, m_destinationBox.bounds.max.x),
            Random.Range(m_destinationBox.bounds.min.y, m_destinationBox.bounds.max.y),
            Random.Range(m_destinationBox.bounds.min.z, m_destinationBox.bounds.max.z) );

        m_enemies[m_currentEnemy].transform.localPosition = spawnPos;
        m_enemies[m_currentEnemy].DestinationPos = destinationPos;
        m_enemies[m_currentEnemy].gameObject.SetActive(true);

        // set the speed
        m_enemies[m_currentEnemy].Speed = Random.Range(m_minSpeed, m_maxSpeed);
        // set the health
        m_enemies[m_currentEnemy].Health = Random.Range(m_minHealth, m_maxHealth);
        // fade in
        m_enemies[m_currentEnemy].Spawn();

        ++m_currentEnemy;
        if (m_currentEnemy >= s_numEnemies)
        {
            m_currentEnemy = 0;
        }
    }

    void Update()
    {
        m_spawnTime = Mathf.Clamp01(m_spawnTime + Time.smoothDeltaTime * m_spawnRate);

        if ( Mathf.Approximately(1f, m_spawnTime))
        {
            Spawn();
            m_spawnTime = 0;
        }
    }

    void PlayerDead()
    {
        for (int i = 0; i < s_numEnemies; ++i)
        {
            m_enemies[i].gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }

}
