using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    /// <summary>
    /// Количество врагов, после которого EnemySpawnPoint будет уничтожен
    /// </summary>
    [SerializeField] private int _enemyMaxCount;
    /// <summary>
    /// Количество врагов, относящихся к данному EnemySpawnPoint может быть на сцене
    /// </summary>
    [SerializeField] private int _enemyInSceneMaxCount;
    private int _enemyCount=0;


   
    // Start is called before the first frame update

    void Start()
    {
        
        while (_enemyCount < _enemyInSceneMaxCount)
        {
            CreateEnemy();
            _enemyCount = transform.childCount;
         
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemyCount();
    }

    private void CheckEnemyCount()
    {
        _enemyCount = transform.childCount;
        if (_enemyCount < _enemyInSceneMaxCount && _enemyMaxCount > 0)
        {
            CreateEnemy();
            _enemyMaxCount--;
        }
        else if (_enemyMaxCount <= 0 && _enemyCount<=0)
            Destroy(gameObject);
    }

    private void CreateEnemy()
    {

        Vector3 InstantiatePosition = new Vector3(transform.position.x + transform.childCount, transform.position.y, transform.position.z + transform.childCount);
        Instantiate(_enemy, InstantiatePosition, Quaternion.identity).transform.SetParent(transform);
       
       // Instantiate(_enemy, transform);
    }
}
