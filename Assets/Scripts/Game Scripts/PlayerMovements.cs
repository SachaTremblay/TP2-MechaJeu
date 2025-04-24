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
    public float m_CoinTotal = 0;
    public float m_CollectedCoins = 0;
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
    void Update()
    {
        Movements();
        Jumping();
        m_Distance = transform.position.x; //Distance Score
    }
    private void Movements()
    { 
        if (Input.GetKeyDown(KeyCode.E)) m_GameStarted = true; //Start Game
        if (!m_GameStarted) m_Animation.SetBool("Running", false); //Idle Animation
        //Starts All Running Related
        else if (m_GameStarted)
        {
            transform.Translate(Vector3.right * m_Speed * Time.deltaTime);
            m_Animation.SetBool("Running", true);
        }
    }
    private void Jumping()
    {
        //First Jump
        if (Input.GetKeyDown(KeyCode.Space) && m_IsOnGround)
        { 
            m_RigidBody2D.AddForce(Vector3.up * m_JumpForce, ForceMode2D.Impulse);
            m_IsOnGround = false;
            m_DoubleJump = true;
        }
        //Second Jump
        if(Input.GetKeyDown(KeyCode.Space) && !m_IsOnGround && m_DoubleJump)
        {
            Vector3 YVelocity = m_RigidBody2D.velocity;
            YVelocity.y = 0;
            m_RigidBody2D.velocity = YVelocity;
            m_RigidBody2D.AddForce(Vector3.up * m_JumpForce, ForceMode2D.Impulse);
            m_DoubleJump = false;
        }
        //Dash Down
        if (Input.GetKeyDown(KeyCode.S)) m_RigidBody2D.AddForce(Vector3.down * m_JumpForce * 2, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") m_IsOnGround = true; //Jump
        else m_IsOnGround = false;
        //Coin Points
        if(collision.gameObject.tag == "Coin")
        {
            m_CoinTotal += m_CoinValue;
            PlayerPrefs.SetFloat("CoinTotal", m_CoinTotal);
            m_CollectedCoins++;
            PlayerPrefs.SetFloat("Coins", m_CollectedCoins);

        }
        //Death and HighScore
        if(collision.gameObject.tag == "Obstacle") 
        {
            m_Distance = transform.position.x;
            m_FinalScore = m_Distance + m_CoinTotal;
            if(!PlayerPrefs.HasKey("HighScore")) PlayerPrefs.SetFloat("HighScore", m_FinalScore);
            else if (m_FinalScore > PlayerPrefs.GetFloat("HighScore"))  PlayerPrefs.SetFloat("HighScore", m_FinalScore);
            //Everything Below is for Game Over Screen
            PlayerPrefs.SetFloat("Distance", transform.position.x);
            PlayerPrefs.SetFloat("Coins", m_CollectedCoins);
            PlayerPrefs.SetFloat("CoinTotal", m_CoinTotal);
            PlayerPrefs.SetFloat("FinalScore", m_FinalScore);
            SceneManager.LoadScene("LosingScreen");
        }
    }
}
