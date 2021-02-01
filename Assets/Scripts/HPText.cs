using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    private Text _hpText;
    // Start is called before the first frame update
    void Start()
    {

        _hpText = GetComponent<Text>();
        _hpText.text = "HP:" + _playerHealth.HealthCount;
    }

    // Update is called once per frame
    void Update()
    {
        _hpText.text = "HP:" + _playerHealth.HealthCount;
    }
}
