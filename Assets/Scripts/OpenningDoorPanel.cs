using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenningDoorPanel : MonoBehaviour
{
    // Start is called before the first frame update

  
    [SerializeField] private DoorOpenController _doorOpenController;
    [SerializeField] float openDoorTime = 3;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //if ( collision.gameObject.CompareTag("Player"))

    //    _doorOpenController.openDoorTime = openDoorTime;
    //    _doorOpenController.OpenDoor();
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //    if(_doorOpenController.openDoorTime < openDoorTime)
    //        _doorOpenController.openDoorTime = openDoorTime;
    //}
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Ground"))
        //    return;
        _doorOpenController.openDoorTime = openDoorTime;
        _doorOpenController.OpenDoor();
    }

    private void OnTriggerStay(Collider other)
    {
        //if ( collision.gameObject.CompareTag("Player"))
        //if (other.gameObject.CompareTag("Ground"))
        //    return;
        _doorOpenController.openDoorTime = openDoorTime;
        _doorOpenController.OpenDoor();
    }


    //private void OnCollisionExit(Collision collision)
    //{
    //       _doorOpenController.CloseDoor();
    //}

}
