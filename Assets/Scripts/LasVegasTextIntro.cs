using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasVegasTextIntro : MonoBehaviour
{
    public float m_speed = 20f;

    void Start()
    {
        Invoke("TurnOff", 10f);
    }

    void Update ()
    {
        transform.position += Vector3.forward * -m_speed * Time.smoothDeltaTime;		
	}

    void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
