using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameUiHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerXPos;
    [SerializeField] private TextMeshProUGUI m_PlayerControls;
    [SerializeField] private TextMeshProUGUI m_Distance;
    [SerializeField] private TextMeshProUGUI m_CoinCollected;
    [SerializeField] private TextMeshProUGUI m_CoinPoints;
    PlayerMovements m_Coins;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Destroy(m_PlayerControls);
        m_Distance.text = "Distance: " + m_PlayerXPos.transform.position.x;
        m_CoinCollected.text = "Collected " + m_Coins.m_CollectedCoins + " coins";
        m_CoinPoints.text = "Coin Points: " + m_Coins.m_CoinTotal;
    }
}
