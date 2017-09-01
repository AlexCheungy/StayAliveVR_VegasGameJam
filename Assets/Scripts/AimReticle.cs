using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimReticle : MonoBehaviour
{
    public LineRenderer m_primaryAimReticle;
    public LineRenderer[] m_aimReticleBoxes;
    public float m_size = 1;
    public float[] m_spacingFromCamera;
    Vector3 m_lastRotation;

    public float m_angularVelocity;
    const float s_maxVelocity = 400f;

    public float m_percent;
    public float m_catchUpVelocity = 10f;

    public LineRenderer[] AimReticleBoxes
    {
        get { return m_aimReticleBoxes; }
        set { m_aimReticleBoxes = value; }
    }

    public float Size
    {
        get { return m_size; }
        set { m_size = value; }
    }

    public float[] SpacingFromCamera
    {
        get { return m_spacingFromCamera; }
        set { m_spacingFromCamera = value; }
    }

    public LineRenderer PrimaryAimReticle
    {
        get { return m_primaryAimReticle; }
        set { m_primaryAimReticle = value; }
    }

    void Awake()
    {
    }

    void Start()
    {
    }

    void Update()
    {
        Vector3 rotationDifference = transform.rotation.eulerAngles - m_lastRotation;
        m_angularVelocity = rotationDifference.magnitude;

        m_percent = Mathf.Clamp01(1 - (m_angularVelocity / s_maxVelocity));
        m_percent = Mathf.Clamp01(m_percent + Time.smoothDeltaTime * m_catchUpVelocity);

        // setup spacing
        Vector3 newPos = transform.position;
        float[] testSpeeds = new float[3];
        testSpeeds[0] = .15f;
        testSpeeds[1] = .125f;
        testSpeeds[2] = .1f;
        for (int i = 0; i < m_aimReticleBoxes.Length; ++i)
        {
            newPos = transform.position;
            newPos += (transform.forward * m_spacingFromCamera[i]);
            m_aimReticleBoxes[i].transform.localPosition = Vector3.Lerp(m_aimReticleBoxes[i].transform.localPosition, newPos, testSpeeds[i]);
            //m_aimReticleBoxes[i].transform.position = newPos;
        }

        m_lastRotation = transform.rotation.eulerAngles;
    }

}
