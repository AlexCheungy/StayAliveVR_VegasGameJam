using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    static float s_speed = 5f;

    void Update()
    {
        transform.position += Vector3.forward * -s_speed * Time.smoothDeltaTime;
    }

    void TurnOff()
    {
        gameObject.SetActive(false);
    }
}