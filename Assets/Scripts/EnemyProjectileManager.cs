using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    static EnemyProjectileManager s_instance;

    public EnemyProjectile m_projectilePrefab;
    Dictionary<GameObject, EnemyProjectile> m_projectilesByGameObject;
    EnemyProjectile[] m_projectiles;
    const int s_numProjectiles = 64;
    public float m_projectileVelocity = 10f;
    int m_currentProjectile = 0;
    private float projectileDamage = 5f;

    public static EnemyProjectileManager Instance
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
        EnemyProjectile.Velocity = m_projectileVelocity;
        CreateProjectiles();
    }

    void CreateProjectiles()
    {
        m_projectilesByGameObject = new Dictionary<GameObject, EnemyProjectile>(s_numProjectiles);
        m_projectiles = new EnemyProjectile[s_numProjectiles];
        EnemyProjectile projectile;
        for (int i = 0; i < s_numProjectiles; ++i)
        {
            projectile = Instantiate(m_projectilePrefab) as EnemyProjectile;
            projectile.gameObject.SetActive(false);
            m_projectilesByGameObject.Add(projectile.gameObject, projectile);
            m_projectiles[i] = projectile;
        }
    }

    public void Fire(ref Vector3 _position, ref Vector3 _forward)
    {
        m_projectiles[m_currentProjectile].transform.localPosition = _position + Vector3.up * 1.7f;
        m_projectiles[m_currentProjectile].transform.forward = _forward;
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
}
