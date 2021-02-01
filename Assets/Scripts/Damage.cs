using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private bool isGroundIgnore;
    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<Health>()!=null)
        {
            var enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth == null)
                Destroy(collision.gameObject);
            else
                enemyHealth.GetDamage(_damage);
            m_AudioSource.Play();
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject, m_AudioSource.clip.length);
            //Debug.Log("collision with enemy " + collision.gameObject.name);
        }
        else if (!((collision.gameObject.CompareTag("Ground") && isGroundIgnore)))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            m_AudioSource.Play();
            Destroy(gameObject, m_AudioSource.clip.length);
        }


    }
   
   
}
