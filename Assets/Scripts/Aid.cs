using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aid : MonoBehaviour
{
    [SerializeField] private int healthAid = 30;
    private float _startYPosition;
    private bool _moveUp = true;
    // Start is called before the first frame update
    private void Start()
    {
        _startYPosition = transform.position.y;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Rotation();
    }

    private void Move()
    {
        if (transform.position.y > (_startYPosition + 1) || transform.position.y < _startYPosition)
            _moveUp = !_moveUp;

        int moveMultiplay = _moveUp ? 1 : -1;
        Vector3 move = new Vector3(0,moveMultiplay*0.005f,0);
        transform.Translate(move);
    }

    private void Rotation()
    {
        float yRotation = transform.rotation.eulerAngles.y + 0.5f;
        
        transform.rotation  = Quaternion.Euler(0, yRotation, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponents<Health>()!=null)
        other.GetComponent<Health>().GetAid(healthAid);
        Destroy(gameObject);
    
    }
}
