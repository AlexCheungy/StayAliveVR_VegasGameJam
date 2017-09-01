using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    static float s_velocity;

    ParticleSystem[] m_particleSystems;

    public static float Velocity
    {
        get { return s_velocity; }
        set { s_velocity = value; }
    }

    void Awake()
    {
        m_particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        transform.localPosition += (transform.forward * s_velocity * Time.smoothDeltaTime);
    }

    void OnDisable()
    {
        for( int i = 0; i < m_particleSystems.Length; ++i)
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
}
