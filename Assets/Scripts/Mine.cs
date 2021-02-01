using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private int _damage=50;
    [SerializeField] private int _damageForce = 5;
    [SerializeField] private bool isExplosionMode;
    [SerializeField] private Collider _explosionRadious;
    //[SerializeField] private GameObject _explosionRadious;
    private bool isActive = false;
    AudioSource m_AudioSource;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            return;

      
            _explosionRadious.enabled = true;
            Debug.Log(collision.gameObject.name);
      
    }
    private void OnTriggerEnter(Collider other)
    {
        //_explosionRadious.SetActive(true);
        if (other.gameObject.CompareTag("Ground"))
            return;

            
        SetDamage(other.gameObject);
        SetImpuls(other.gameObject);
        Debug.Log(other.name);
        
    }

    private void SetDamage(GameObject Enemy)
    {
        if (Enemy.GetComponent<Health>()!= null)
        {
            var enemy = Enemy.GetComponent<Health>();
            enemy.GetDamage(_damage);
            gameObject.GetComponent<Collider>().enabled = false;
            m_AudioSource.Play();
            Destroy(gameObject, m_AudioSource.clip.length);
        }
    }

    private void SetImpuls(GameObject Enemy)
    {
        Vector3 impulsDirection = Enemy.transform.position - transform.position;
        Debug.Log(" impuls "+impulsDirection.magnitude);
        if (Enemy.GetComponent<Rigidbody>() !=null)
        {
            Rigidbody enemyRigibody = Enemy.GetComponent<Rigidbody>();
            enemyRigibody.AddForce(impulsDirection * (_damageForce/ impulsDirection.magnitude), ForceMode.Impulse);
        }
    }
}
