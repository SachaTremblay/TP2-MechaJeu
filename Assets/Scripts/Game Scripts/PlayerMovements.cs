using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] GameUiHandler m_GameUI;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_JumpForce;
    [SerializeField] private AudioClip m_CoinSFX;
    float m_FinalScore;
    float m_Distance;
    float m_CoinValue = 25;
    float m_CoinTotal = 0;
    float m_CollectedCoins = 0;
    bool m_GameStarted = false;
    bool m_IsOnGround;
    bool m_DoubleJump = false;
    Rigidbody2D m_RigidBody2D;
    Animator m_Animation;
    AudioSource m_AudioSource;
    void Start()
    {
        m_Animation = GetComponent<Animator>();
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();
        if (m_GameUI != null)
        {
            m_GameUI.NotifyPlayerCoinPointsChanged(m_CoinTotal);
            m_GameUI.NotifyPlayerCoinAmmountChanged(m_CollectedCoins);
            m_GameUI.NotifyPlayerXChanged(transform.position.x);
        }
    }
    void Update()
    {
        Movements();
        Jumping();
        m_Distance = transform.position.x; //Distance Score
        m_GameUI.NotifyPlayerXChanged(transform.position.x);
    }
    private void Movements()
    {
        if (Input.GetKeyDown(KeyCode.E)) m_GameStarted = true; //Start Game
        if (!m_GameStarted) m_Animation.SetBool("Running", false); //Idle Animation
        //Starts All Running Related
        else if (m_GameStarted)
        {
            Vector3 CalcVelocity = m_RigidBody2D.velocity;
            CalcVelocity.x = m_Speed * (1 + (transform.position.x / 1000));
            print(CalcVelocity.x);
            m_RigidBody2D.velocity = CalcVelocity;
            m_Animation.SetBool("Running", true);
        }
    }
    private void Jumping()
    {
        //To avoid the jump being weaker
        Vector3 YVelocity = m_RigidBody2D.velocity;
        YVelocity.y = 0;
        //First Jump
        if (Input.GetKeyDown(KeyCode.Space) && m_IsOnGround)
        {
            m_RigidBody2D.velocity = YVelocity;
            m_RigidBody2D.AddForce(Vector3.up * m_JumpForce, ForceMode2D.Impulse);
            m_IsOnGround = false;
            m_DoubleJump = true;
        }
        //Second Jump
        if (Input.GetKeyDown(KeyCode.Space) && !m_IsOnGround && m_DoubleJump)
        {
            m_RigidBody2D.velocity = YVelocity;
            m_RigidBody2D.AddForce(Vector3.up * m_JumpForce, ForceMode2D.Impulse);
            m_DoubleJump = false;
        }
        //Dash Down
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_RigidBody2D.velocity = YVelocity;
            m_RigidBody2D.AddForce(Vector3.down * m_JumpForce * 2, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Death and HighScore
        if (collision.gameObject.tag == "Obstacle")
        {
            m_Distance = transform.position.x;
            m_FinalScore = m_Distance + m_CoinTotal;
            if (!PlayerPrefs.HasKey("HighScore")) PlayerPrefs.SetFloat("HighScore", m_FinalScore);
            else if (m_FinalScore > PlayerPrefs.GetFloat("HighScore")) PlayerPrefs.SetFloat("HighScore", m_FinalScore);
            //Everything Below is for Game Over Screen
            PlayerPrefs.SetFloat("Distance", transform.position.x);
            PlayerPrefs.SetFloat("CoinAmmount", m_CollectedCoins);
            PlayerPrefs.SetFloat("CoinPoint", m_CoinTotal);
            PlayerPrefs.SetFloat("FinalScore", m_FinalScore);
            SceneManager.LoadScene("LosingScreen");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Coin Points
        if (collision.gameObject.tag == "Coin")
        {
            m_CoinTotal += m_CoinValue;
            m_CollectedCoins++;
            if (m_GameUI != null)
            {
                m_GameUI.NotifyPlayerCoinPointsChanged(m_CoinTotal);
                m_GameUI.NotifyPlayerCoinAmmountChanged(m_CollectedCoins);
            }
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            m_AudioSource.clip = m_CoinSFX;
            m_AudioSource.Play();
            m_IsOnGround = true;
            m_DoubleJump = true;
        }
        else m_IsOnGround = false; //Avoid pseudo infinite jumps
    }
}