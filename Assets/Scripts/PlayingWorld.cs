using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingWorld : MonoBehaviour
{
    static PlayingWorld s_instance;

    public static PlayingWorld Instance
    {
        get { return s_instance; }
    }

    void Awake()
    {
        s_instance = this;
    }

}
