using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject m_treePrefab;

    BoxCollider m_spawnBox;

    GameObject[] m_trees;
    const int s_numTrees = 128;
    int m_currentTree = 0;

    float m_spawnTime;
    public float m_spawnRate = 4;

    void Awake()
    {
        m_spawnBox = GetComponent<BoxCollider>();
        CreateEnemies();
    }

    void CreateEnemies()
    {
        m_trees = new GameObject[s_numTrees];
        GameObject gameObject;
        for (int i = 0; i < s_numTrees; ++i)
        {
            gameObject = Instantiate(m_treePrefab) as GameObject;
            gameObject.SetActive(false);
            m_trees[i] = gameObject;
            gameObject.transform.SetParent(PlayingWorld.Instance.transform);
        }
    }

    void Spawn()
    {
        // find random point in spawning box
        Vector3 spawnPos = new Vector3(
            Random.Range(m_spawnBox.bounds.min.x, m_spawnBox.bounds.max.x),
            Random.Range(m_spawnBox.bounds.min.y, m_spawnBox.bounds.max.y),
            Random.Range(m_spawnBox.bounds.min.z, m_spawnBox.bounds.max.z));

        m_trees[m_currentTree].transform.localPosition = spawnPos;
        m_trees[m_currentTree].gameObject.SetActive(true);

        // fade in
        //m_trees[m_currentTree].Spawn();

        ++m_currentTree;
        if (m_currentTree >= s_numTrees)
        {
            m_currentTree = 0;
        }
    }

    void Update()
    {
        m_spawnTime = Mathf.Clamp01(m_spawnTime + Time.smoothDeltaTime * m_spawnRate);

        if (Mathf.Approximately(1f, m_spawnTime))
        {
            Spawn();
            m_spawnTime = 0;
        }
    }

}
