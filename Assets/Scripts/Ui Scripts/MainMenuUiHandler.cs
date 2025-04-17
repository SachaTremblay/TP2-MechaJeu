using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUiHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_HighScore;
    public void Start()
    {
        if (PlayerPrefs.HasKey("HighScore")) m_HighScore.text = "Highscore: " + PlayerPrefs.GetFloat("HighScore").ToString();
        else m_HighScore.text = "Highscore: No highcore has been found.. Please play at least once to set one";
    }
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
