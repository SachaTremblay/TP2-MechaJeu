using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUiHandler : MonoBehaviour
{ 
    [SerializeField] TextMeshProUGUI m_StatTable;
    [SerializeField] TextMeshProUGUI m_Distance;
    [SerializeField] TextMeshProUGUI m_CollectedCoins;
    [SerializeField] TextMeshProUGUI m_CoinPoints;
    [SerializeField] TextMeshProUGUI m_FinalScore;
    [SerializeField] TextMeshProUGUI m_HighScore;
    [SerializeField] AudioClip m_DeathSFX;
    AudioSource m_SoundsSource;
    private float m_SoundTimer;
    private void Start()
    {
        m_SoundsSource = GetComponent<AudioSource>();
        m_StatTable.text = "STATS";
        m_Distance.text = "Distance: " + PlayerPrefs.GetFloat("Distance").ToString("F2");
        m_CollectedCoins.text = "Collected coins: " + PlayerPrefs.GetFloat("CoinAmmount").ToString("F0");
        m_CoinPoints.text = "Coin points: " + PlayerPrefs.GetFloat("CoinPoint").ToString("F0");
        m_FinalScore.text = "Final score: " + PlayerPrefs.GetFloat("FinalScore").ToString("F2");
        m_HighScore.text = "HighScore: " + PlayerPrefs.GetFloat("HighScore").ToString("F2");
        m_HighScore.text = "HighScore: " + PlayerPrefs.GetFloat("HighScore").ToString("F2");
        m_SoundsSource.clip = m_DeathSFX;
        m_SoundsSource.Play();
    }
    private void Update()
    {
        if(m_SoundsSource.isPlaying)
        {
            m_SoundTimer += Time.deltaTime;
        }
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
