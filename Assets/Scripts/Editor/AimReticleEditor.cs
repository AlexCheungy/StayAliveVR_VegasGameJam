using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(AimReticle))]
public class AimReticleEditor : Editor
{
    AimReticle m_aimReticle;

    void OnEnable()
    {
        m_aimReticle = target as AimReticle;
    }

    void OnDisable()
    {
    }

    public override void OnInspectorGUI()
    {
        // calls CheckersGame inspector to view/edit public variables
        base.OnInspectorGUI();

            
        if (GUILayout.Button("Setup Aim Reticule Boxes"))
        {
            SetupAimReticleBoxes();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(m_aimReticle.gameObject);
        }
    }

    void SetupAimReticleBoxes()
    {
        int count = m_aimReticle.AimReticleBoxes.Length;

        Vector3[] newPositions = new Vector3[4];
        newPositions[0] = new Vector3(-m_aimReticle.Size, -m_aimReticle.Size, 0f);
        newPositions[1] = new Vector3(-m_aimReticle.Size, m_aimReticle.Size, 0f);
        newPositions[2] = new Vector3(m_aimReticle.Size, m_aimReticle.Size, 0f);
        newPositions[3] = new Vector3(m_aimReticle.Size, -m_aimReticle.Size, 0f);

        m_aimReticle.PrimaryAimReticle.SetPositions(newPositions);
        // setup spacing
        for ( int i = 0; i < count; ++i)
        {
            m_aimReticle.AimReticleBoxes[i].SetPositions(newPositions);
            m_aimReticle.SpacingFromCamera[i] = EditorGUILayout.FloatField("Distance From Camera", m_aimReticle.SpacingFromCamera[i]);
            m_aimReticle.AimReticleBoxes[i].transform.position = new Vector3(0f, 0f, m_aimReticle.SpacingFromCamera[i]);
        }
    }

    void SetupElements()
    {

    }
}