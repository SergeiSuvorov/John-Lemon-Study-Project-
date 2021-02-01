using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float deltaPosition = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraMove = new Vector3 ((_playerTransform.position.x - transform.position.x), (_playerTransform.position.y - transform.position.y +5), (_playerTransform.position.z - transform.position.z - deltaPosition));

        transform.Translate(cameraMove);
    }
}
 