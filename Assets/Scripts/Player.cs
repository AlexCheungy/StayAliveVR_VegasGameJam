using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public delegate void voidfunction();
    public static event voidfunction PlayerDeadEvent;

    static Player s_instance;
    public int m_maxHealth = 10;
    public int m_currentHealth = 10;

    public Image m_hurtImage;
    public Text m_gameOverText;

    public int m_score = 0;
    public Text m_scoreText;
    public Image[] m_heartImages;

    public static Player Instance
    {
        get { return s_instance; }
    }

    void Awake()
    {
        s_instance = this;
        m_currentHealth = m_maxHealth;
        PlayerDeadEvent += PlayerDead;
    }

    public void Damage()
    {
        m_currentHealth = Mathf.Clamp(m_currentHealth - 1, 0, m_maxHealth);
        //Debug.Log("Damage, Health = " + m_currentHealth.ToString());
        m_hurtImage.color = Color.red;
        m_hurtImage.DOFade(0f, 1f);
        // remove a heart
        m_heartImages[m_currentHealth].gameObject.SetActive(false);

        if ( 0f == m_currentHealth )
        {
            // TODO: display death text / restart game / turn off all enemies / turn off firing
            RaisePlayerDeadEvent();
            Debug.Log("You are dead");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (EnemyProjectileManager.Instance.IsValidProjectile(other.gameObject))
        {
            other.gameObject.SetActive(false);
            //Debug.Log("Damage Source Name: " + other.name);
            Damage();
        }
    }

    void RaisePlayerDeadEvent()
    {
        if(PlayerDeadEvent != null)
        {
            PlayerDeadEvent();
        }
    }

    void PlayerDead()
    {
        m_gameOverText.gameObject.SetActive(true);
        m_gameOverText.text = "Game Over! Score is " + m_score.ToString() + ". Press App button to restart game";
    }

    void OnDestroy()
    {
        PlayerDeadEvent = null;
    }

    public void IncreaseScore()
    {
        ++m_score;
        m_scoreText.text = m_score.ToString();
    }
}
