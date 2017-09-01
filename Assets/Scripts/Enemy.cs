using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    Vector3 m_destinationPos;

    float m_speed;
    float m_health;
    MeshRenderer m_meshRenderer;

    float m_attackRate;
    float m_attackTime;

    public enum TYPE
    {
        SHOOTER,
        DOG,
    }

    public TYPE m_currentType;

    public Vector3 DestinationPos
    {
        get { return m_destinationPos; }
        set { m_destinationPos = value; }
    }

    public float Speed
    {
        get { return m_speed; }
        set { m_speed = value; }
    }

    public float Health
    {
        get { return m_health; }
        set { m_health = value; }
    }

    public float AttackRate
    {
        get { return m_attackRate; }
        set { m_attackRate = value; }
    }

    void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        m_attackTime = Mathf.Clamp01(m_attackTime + Time.smoothDeltaTime * m_attackRate);
        Vector3 playerDirection = Player.Instance.transform.position - transform.position;

        switch (m_currentType)
        {
            case TYPE.SHOOTER:
                if (Mathf.Approximately(1f, m_attackTime))
                {
                    Vector3 pos = transform.position;
                    EnemyProjectileManager.Instance.Fire(ref pos, ref playerDirection);
                    m_attackTime = 0f;
                }
                transform.forward = m_destinationPos - transform.localPosition;
                transform.localPosition += (transform.forward * m_speed * Time.smoothDeltaTime);
                break;
            case TYPE.DOG:
                playerDirection.y = 0f;
                transform.forward = playerDirection;
                transform.localPosition += (transform.forward * m_speed * Time.smoothDeltaTime);
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (ProjectileManager.Instance.IsValidProjectile(other.gameObject))
        {
            m_meshRenderer.material.color = Color.red;
            m_meshRenderer.material.DOColor(Color.white, 1f);
            // take damage
            m_health = Mathf.Clamp(m_health - ProjectileManager.Instance.ProjectileDamage, 0f, float.MaxValue);
            if (Mathf.Approximately(0f, m_health))
            {
                Player.Instance.IncreaseScore();
                gameObject.SetActive(false);
            }
        }
        else if (other.tag == "Despawn")
        {
            Invoke("TurnOff", .5f);
            m_meshRenderer.material.DOFade(0f, .5f);
        }
        else if (other.tag == "Player" && m_currentType == TYPE.DOG)
        {
            //  if we run into the player, have the dog stop and face the player
            m_speed = 0f;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && m_currentType == TYPE.DOG)
        {
            if (Mathf.Approximately(1f, m_attackTime))
            {
                Debug.Log("Damage Source Name: " + gameObject.name);
                Player.Instance.Damage();
                m_attackTime = 0f;
            }
        }
    }

    void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        // TODO: just spawn a puff a smoke
        Color alphaWhite = Color.white;
        alphaWhite.a = 0f;
        m_meshRenderer.material.color = alphaWhite;
        m_meshRenderer.material.DOFade(1f, .5f);
    }


}