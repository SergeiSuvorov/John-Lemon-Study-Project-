using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenController : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] bool openByPanel = false;
    public float openDoorTime = 0;

   
    public void OpenDoor()
    {
        if (_door.activeInHierarchy)
        {
            _door.gameObject.SetActive(false);
            StartCoroutine(CloseDoorCount());
        }
    }
    public void CloseDoor()
    {
        if (!_door.activeInHierarchy)
        {
            StartCoroutine(CloseDoorCount());
        }
           
    }

    private IEnumerator CloseDoorCount()
    {
        while (openDoorTime > 0)
        {

            openDoorTime -= Time.deltaTime;
            yield return null;
        }
        _door.SetActive(true);

        yield break;

    }
}
