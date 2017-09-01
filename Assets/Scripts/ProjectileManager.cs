using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    static ProjectileManager s_instance;

    public Projectile m_projectilePrefab;
    Dictionary<GameObject, Projectile> m_projectilesByGameObject;
    Projectile[] m_projectiles;
    const int s_numProjectiles = 64;
    public float m_projectileVelocity = 10f;
    int m_currentProjectile = 0;
    public float m_fireRate = 4f;
    float m_fireTime;
    private float projectileDamage = 5f;
    public static ProjectileManager Instance
    {
        get { return s_instance; }
        set { s_instance = value; }
    }

    public float ProjectileDamage
    {
        get { return projectileDamage; }
        set { projectileDamage = value; }
    }

    void Awake()
    {
        s_instance = this;
        Projectile.Velocity = m_projectileVelocity;
        Player.PlayerDeadEvent += PlayerDead;
        CreateProjectiles();
    }

    void CreateProjectiles()
    {
        m_projectilesByGameObject = new Dictionary<GameObject, Projectile>(s_numProjectiles);
        m_projectiles = new Projectile[s_numProjectiles];
        Projectile projectile;
        for (int i = 0; i < s_numProjectiles; ++i)
        {
            projectile = Instantiate(m_projectilePrefab) as Projectile;
            projectile.gameObject.SetActive(false);
            m_projectilesByGameObject.Add(projectile.gameObject, projectile);
            m_projectiles[i] = projectile;
        }
    }
    void Fire(ref Vector3 _forward)
    {
        m_projectiles[m_currentProjectile].transform.localPosition = transform.localPosition;
        m_projectiles[m_currentProjectile].transform.forward = transform.forward;
        m_projectiles[m_currentProjectile].gameObject.SetActive(true);
        ++m_currentProjectile;
        if (m_currentProjectile >= s_numProjectiles)
        {
            m_currentProjectile = 0;
        }
    }

    public bool IsValidProjectile(GameObject _gameObject)
    {
        if (m_projectilesByGameObject.ContainsKey(_gameObject))
        {
            _gameObject.SetActive(false);
            return true;
        }

        return false;
    }

    void Update()
    {
        m_fireTime = Mathf.Clamp01(m_fireTime + Time.smoothDeltaTime * m_fireRate);

        if (GvrControllerInput.ClickButton && Mathf.Approximately(1f, m_fireTime))
        {
            Vector3 forward = transform.forward;
            Fire(ref forward);
            m_fireTime = 0;
        }
    }

    void PlayerDead()
    {
        gameObject.SetActive(false);
    }
}
