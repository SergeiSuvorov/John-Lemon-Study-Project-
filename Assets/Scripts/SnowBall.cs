using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] MeshRenderer mesh;
  
   public void SetImpuls(Vector3 direction, float force)
    {
        rigidbody.AddForce(direction*force, ForceMode.Impulse);
        Destroy(gameObject, 4);
    }
}
