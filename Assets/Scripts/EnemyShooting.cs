using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    [SerializeField] private string _enemyTag;
    [SerializeField] private GameObject _snowBall;
    [SerializeField] private CubePatrolZone _patrolZone;
    [SerializeField] private float _reloadTime = 3;
    [SerializeField] private int _maxAmmoClipSize = 3;
    
    private int _ammoClipSize = 3;
    private GameObject _enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_enemyTag))
        {
            Vector3 direction = other.transform.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

           

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.gameObject.CompareTag(_enemyTag))
                {
                    SetEnemy(other.gameObject, direction);
                    
                }
            }
            else _patrolZone.ReadyToGo();
        }
        else _patrolZone.ReadyToGo();
    }

   
    private void SetEnemy(GameObject Enemy, Vector3 enemyDirection)
    {
       if (_enemy == null ||_enemy==Enemy)
        {
            _enemy = Enemy;
            _patrolZone.SetGoalPoint(_enemy.transform.position);
            _patrolZone.Shooting();
            Shoot(enemyDirection);
            Debug.Log(Enemy);
        }

        Debug.Log(_enemy);

    }
    private void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_enemyTag))
        {
            _patrolZone.SetGoalPoint(other.transform.position);
            _patrolZone.ReadyToGo();           
        }
    }

    private void Shoot(Vector3 shootDirection)
    {
        if (_ammoClipSize > 0)
        {
            Vector3 _snowBallSpawnPlace = transform.position;
            GameObject _ball = Instantiate(_snowBall, _snowBallSpawnPlace, Quaternion.identity);
            _ball.GetComponent<SnowBall>().SetImpuls(shootDirection, 5);

            _ammoClipSize--;
            if (_ammoClipSize==0)
                StartCoroutine(Reload());
        }

        
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadTime);
        _ammoClipSize = _maxAmmoClipSize;
        yield break;

    }
}
