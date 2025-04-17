using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_JumpForce;
    float m_FinalScore;
    float m_Distance;
    float m_CoinValue = 25;
    float m_CoinTotal = 0;
    float m_CollectedCoins;
    bool m_GameStarted = false;
    bool m_IsOnGround = false;
    bool m_DoubleJump = false;
    Rigidbody2D m_RigidBody2D;
    Animator m_Animation;
    void Start()
    {
        m_Animation = GetComponent<Animator>();
        m_RigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Jumping();
        m_Distance = transform.position.x;
    }
    private void Moving()
    { 
        if (Input.GetKeyDown(KeyCode.E)) m_GameStarted = true;
        if (!m_GameStarted) m_Animation.SetBool("Running", false);
        else if (m_GameStarted)
        {
            transform.Translate(Vector3.right * m_Speed * Time.deltaTime);
            m_Animation.SetBool("Running", true);
        }
    }
    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_IsOnGround) //Premier Saut
        { 
            m_RigidBody2D.AddForce(Vector3.up * m_JumpForce, ForceMode2D.Impulse);
            m_IsOnGround = false;
            m_DoubleJump = true;
        }
        if(Input.GetKeyDown(KeyCode.Space) && !m_IsOnGround && m_DoubleJump)
        {
            Vector3 YVelocity = m_RigidBody2D.velocity;
            YVelocity.y = 0;
            m_RigidBody2D.velocity = YVelocity;
            m_RigidBody2D.AddForce(Vector3.up * m_JumpForce, ForceMode2D.Impulse);
            m_DoubleJump = false;
        }
        if (Input.GetKeyDown(KeyCode.S)) m_RigidBody2D.AddForce(Vector3.down * m_JumpForce * 2, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") m_IsOnGround = true;
        else m_IsOnGround = false;
        if(collision.gameObject.tag == "Obstacle")
        {
            m_Distance = transform.position.x;
            m_FinalScore = m_Distance + m_CoinTotal;
            if(!PlayerPrefs.HasKey("HighScore")) PlayerPrefs.SetFloat("HighScore", m_FinalScore);
            else if (m_FinalScore > PlayerPrefs.GetFloat("HighScore"))  PlayerPrefs.SetFloat("HighScore", m_FinalScore);
            SceneManager.LoadScene("LosingScreen");
        }
        if(collision.gameObject.tag == "Coin")
        {
            m_CoinTotal += m_CoinValue;
            m_CollectedCoins = m_CoinTotal / m_CoinValue;
        }
    }
}
