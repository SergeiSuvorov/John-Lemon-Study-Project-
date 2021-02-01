using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKillForOpen : MonoBehaviour
{
    public GameObject _door;

    void OnDestroy()
    {
        _door.gameObject.SetActive(false);
    }
}
