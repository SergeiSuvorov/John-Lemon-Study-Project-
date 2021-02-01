using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public AudioSource m_AudioSource;
    
   
    private void OnDisable()
    {
        m_AudioSource.Play();
    }

  
}
