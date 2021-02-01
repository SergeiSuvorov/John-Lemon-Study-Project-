 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float turnSpeed = 20f;
    [SerializeField] public float _playerSpeed = 2f;
    [SerializeField] private GameObject _snowBall;
    [SerializeField] private Transform _snowBallSpawnPlace; // точка, где создается шар
    [SerializeField] private GameObject _mine;
    [SerializeField] private Transform _mineSpawnPlace; // точка, где создается мина
    [SerializeField] private float _force;
    private bool IsOnGround=false;
    private Vector3 _direction;


    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
    }

    void FixedUpdate ()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    private void Update()
    {
       
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject _ball = Instantiate(_snowBall, _snowBallSpawnPlace.position, _snowBallSpawnPlace.rotation);
            Vector3 _ballShootDirection = new Vector3(_ball.transform.position.x - transform.position.x, 0.27f, _ball.transform.position.z - transform.position.z);
            _ball.GetComponent<SnowBall>().SetImpuls(_ballShootDirection, _force);
            // Создаем _snowBoll в точке _snowBollSpawnPlace
        }

        if (Input.GetButtonDown("Fire2"))
        {
             Instantiate(_mine, _mineSpawnPlace.position, _mineSpawnPlace.rotation);
            // Создаем _mine в точке _mineBollSpawnPlace
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    } 

    private void Jump()
    {
        if (IsOnGround)
        {
            m_Rigidbody.AddForce(Vector3.up * _force, ForceMode.Impulse);
            
            IsOnGround = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
            
        }
    }
    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * _playerSpeed * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}