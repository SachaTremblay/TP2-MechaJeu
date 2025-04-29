using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameUiHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_PlayerControls;
    [SerializeField] private TextMeshProUGUI m_Distance;
    [SerializeField] private TextMeshProUGUI m_CoinCollected;
    [SerializeField] private TextMeshProUGUI m_CoinPoints;
    PlayerMovements m_Coins;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Destroy(m_PlayerControls);

    }
    public void NotifyPlayerXChanged(float PlayerPosX = 0)
    {
        m_Distance.text = "Distance: " + PlayerPosX.ToString("F2");
    }
    public void NotifyPlayerCoinAmmountChanged(float CoinAmmount = 0)
    {
        m_CoinCollected.text = "Collected " + CoinAmmount.ToString("F0") + " coins";
    }
    public void NotifyPlayerCoinPointsChanged(float CoinPoints = 0)
    {
        m_CoinPoints.text = "Coin Points: " + CoinPoints.ToString("F0");
    }
}
