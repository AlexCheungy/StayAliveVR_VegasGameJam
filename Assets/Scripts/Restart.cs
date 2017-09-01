using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Awake()
    {
        Player.PlayerDeadEvent += PlayerDead;
        gameObject.SetActive(false);
    }

    void PlayerDead()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {
        if( GvrControllerInput.AppButton)
        {
            SceneManager.LoadScene(0);
        }
    }
}
