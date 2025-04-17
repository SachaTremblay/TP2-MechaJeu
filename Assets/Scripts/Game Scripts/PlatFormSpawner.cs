using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> Biome1;
    [SerializeField] private List<GameObject> Biome2;
    [SerializeField] private List<GameObject> Biome3;
    [SerializeField] private Transform m_StartingPlatform;
    [SerializeField] private Transform m_Player;
    [SerializeField] private float m_DistanceForPlatformSpawn = 8;
    [SerializeField] private int m_MaxPlatformCount;
    private int m_PlatformChoice;
    private int m_Biome;
    private Queue<Transform> m_PlatformQueue;
    private Transform m_LastCreatedPlatform;
    void Start()
    {
        m_PlatformQueue = new Queue<Transform>();
        m_LastCreatedPlatform = m_StartingPlatform;
        Transform PlatformEndPoint = m_LastCreatedPlatform.Find("End");
        m_LastCreatedPlatform = SpawnNewPlatform(PlatformEndPoint.position);
        m_PlatformQueue.Enqueue(m_LastCreatedPlatform);
    }
    private void Update()
    {
        if (Vector3.Distance(m_Player.position, m_LastCreatedPlatform.position) < m_DistanceForPlatformSpawn)
        {
            Transform PlatformEndPoint = m_LastCreatedPlatform.Find("End");
            m_LastCreatedPlatform = SpawnNewPlatform(PlatformEndPoint.position);
            m_PlatformQueue.Enqueue(m_LastCreatedPlatform);
            if (m_PlatformQueue.Count > m_MaxPlatformCount)
            {
                Transform platformtoDelete = m_PlatformQueue.Dequeue();
                Destroy(platformtoDelete.gameObject);
            }
        }
    }
    private GameObject RandomPlatform()
    {
        if(m_Biome == 1)
        {
            m_PlatformChoice = Random.Range(1, 10);
            return Biome1[m_PlatformChoice];
        }
        if(m_Biome == 2)
        {
            m_PlatformChoice = Random.Range(1, 8);
            return Biome2[m_PlatformChoice];
        }
        if(m_Biome == 3)
        {
            m_PlatformChoice = Random.Range(1, 8);
            return Biome3[m_PlatformChoice];
        }
        return Biome1[Random.Range(1,8)];
    }
    private void BiomeGenerator()
    {
        m_Biome = ((int)m_Player.transform.position.x / 100) % 3;
    }
    private Transform SpawnNewPlatform(Vector3 PlatformPosition)
    {
            GameObject NewPlatform = Instantiate(RandomPlatform(), PlatformPosition, Quaternion.identity);
            return NewPlatform.transform;
    }
}