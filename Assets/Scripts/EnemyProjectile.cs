using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyProjectile : MonoBehaviour
{
    static float s_velocity;
    static int s_maxHealthCount = 3;
    int m_currentHealthCount = 3;
    //ParticleSystem[] m_particleSystems;
    MeshRenderer m_meshRenderer;

    public static float Velocity
    {
        get { return s_velocity; }
        set { s_velocity = value; }
    }

    void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        //m_particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        transform.localPosition += (transform.forward * s_velocity * Time.smoothDeltaTime);
    }

    /*
    void OnDisable()
    {
        for (int i = 0; i < m_particleSystems.Length; ++i)
        {
            m_particleSystems[i].Stop();
        }
    }

    void OnEnable()
    {
        for (int i = 0; i < m_particleSystems.Length; ++i)
        {
            m_particleSystems[i].Play();
        }
    }
    */

    void OnTriggerEnter(Collider other)
    {
        if(ProjectileManager.Instance.IsValidProjectile(other.gameObject) )
        {
            m_meshRenderer.material.color = Color.red;
            m_meshRenderer.material.DOColor(Color.white, 1f);

            m_currentHealthCount = Mathf.Clamp( m_currentHealthCount - 1, 0, s_maxHealthCount);
            if( m_currentHealthCount == 0)
            {
                Player.Instance.IncreaseScore();
                gameObject.SetActive(false);
                m_currentHealthCount = s_maxHealthCount;
            }
        }
    }

}
