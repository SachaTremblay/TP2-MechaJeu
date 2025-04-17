using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerToFollow;
    [SerializeField] private Vector3 m_Offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = m_PlayerToFollow.transform.position + m_Offset;
    }
}
