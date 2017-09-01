using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOff : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
