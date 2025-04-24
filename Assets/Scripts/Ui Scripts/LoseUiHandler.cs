using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUiHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_StatTable;
    private void Start()
    {
        m_StatTable.text = "STATS\n\nFinal Distance: " + PlayerPrefs.GetFloat("Distance").ToString("F2") + "\n\nCoins Collected: " + PlayerPrefs.GetFloat("Coins").ToString("F0") + "\n\nCoins Point: " + PlayerPrefs.GetFloat("CoinTotal").ToString("F0") + "\n\nFinal Score: " + PlayerPrefs.GetFloat("FinalScore").ToString("F2");
    }
    public void OnPlayAgainClicked()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
