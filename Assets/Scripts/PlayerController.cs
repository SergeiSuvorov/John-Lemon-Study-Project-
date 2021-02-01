using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 1f;
    [SerializeField] private float _speed; // Скорость движения, а в дальнейшем ускорение
    [SerializeField] private GameObject _snowBall;
    [SerializeField] private GameObject _mine;
    [SerializeField] private Transform _ammunitionSpawnPlace; // точка, где создается шар или мина
    [SerializeField] private float _force;
    private bool IsOnGround = false;
    private GameObject _cargo;
    private Vector3 _direction; // Направление движения
    private Vector3 _viewDirection;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;


    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
  
    }
    private void Update()
    {
        
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        bool isWalking = (_direction != Vector3.zero);
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


        if (Input.GetButtonDown("Fire1"))
        {
            if (_cargo == null)
            {
                GameObject _ball = Instantiate(_snowBall, _ammunitionSpawnPlace.position, _ammunitionSpawnPlace.rotation);
                // Создаем _snowBoll в точке _ammunitionSpawnPlace
                Vector3 _ballShootDirection = new Vector3(_ball.transform.position.x - transform.position.x, 0.27f, _ball.transform.position.z - transform.position.z);
                // Задаем направление выстрела
                _ball.GetComponent<SnowBall>().SetImpuls(_ballShootDirection, _force*2);
                // "Стреляем"
            }
            else
                DeleteCargo();
            
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (_cargo == null)
            {
                Instantiate(_mine, _ammunitionSpawnPlace.position, _ammunitionSpawnPlace.rotation);
                // Создаем _mine в точке _ammunitionSpawnPlace
            }
            else
                DeleteCargo();
            
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
            //m_Rigidbody.AddForce(Vector3.up * _force, ForceMode.Impulse);
            m_Rigidbody.velocity = Vector3.up*_force/2;
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
    private void DeleteCargo()
    {
        _cargo.transform.parent = null;
        _cargo = null;
    }
    private void FixedUpdate()
    {

        Move();
    }

    private void Move()
    {
        if (transform == null)
            return;


        if (_direction.magnitude < 0.01f)
            return;

        transform.forward = _direction.normalized;

        var translate = Vector3.forward * (_speed * Time.deltaTime);

        transform.Translate(translate);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
        if (collision.gameObject.CompareTag("Level Object") )
        {
            Debug.Log("Cube Collision");
            _cargo = collision.gameObject;
            _cargo.transform.parent = _ammunitionSpawnPlace;
            _cargo.transform.position = new Vector3(_ammunitionSpawnPlace.position.x, _ammunitionSpawnPlace.position.y + 0.5f, _ammunitionSpawnPlace.position.z);
        }
    }
}
