using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePatrolZone : MonoBehaviour
{
    [SerializeField] float PatrolZoneRadius;
    [SerializeField] float turnSpeed;
    [SerializeField] float _cubeSpeed;
    [SerializeField] Transform _pointOfView;
    [SerializeField] bool IsTurel = false;
    private Vector3 _centreZone;
    private Vector3 _goalPoint;
    private Vector3 _direction;
    private bool isShooting = false;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Random random = new Random();
     
    void Start()
    {

        m_Rigidbody = GetComponent<Rigidbody>();

        _centreZone = transform.parent.transform.position;

        SetGoalPoint();

    }

    void Update()
    {
        if (CheckPosition())
            SetGoalPoint();
        else if (!IsTurel)
            Move();
        else
            Turn();

    }

    private bool CheckCoordinate()
    {
        float XRadius = Mathf.Abs(transform.position.x - _centreZone.x);
        float ZRadius = Mathf.Abs(transform.position.z - _centreZone.z);
        float maxCoordinate = XRadius > ZRadius ? XRadius : ZRadius;
        if (maxCoordinate - PatrolZoneRadius < 0)
            return true;
        else
            return false;
    }
    private void Move()
    {    
        if (GetDirectionAngle() < 1 && !isShooting)
        {
            _direction = new Vector3(_goalPoint.x - transform.position.x, 0, _goalPoint.z - transform.position.z);
            Vector3 viewDirection = new Vector3(_pointOfView.position.x - transform.position.x, 0, _pointOfView.position.z - transform.position.z);
            var curAngle = Vector3.Angle(_direction, viewDirection);
            _direction.Normalize();
            var speed = _direction * _cubeSpeed * Time.deltaTime;
            transform.Translate(speed);
        }
        else 
        {
            Turn();
        }
    }

    private void Turn()
    {

        _direction = new Vector3(_goalPoint.x - transform.position.x, 0, _goalPoint.z - transform.position.z);
        Vector3 viewDirection = new Vector3(_pointOfView.position.x - transform.position.x, 0, _pointOfView.position.z - transform.position.z);
        Vector3 desiredForward = Vector3.RotateTowards(viewDirection, _direction, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
    private void SetGoalPoint()
    {

        _goalPoint.x = Random.Range(_centreZone.x - PatrolZoneRadius, _centreZone.x + PatrolZoneRadius);
        _goalPoint.z = Random.Range(_centreZone.z - PatrolZoneRadius, _centreZone.z + PatrolZoneRadius);
        _goalPoint.y = 0;
    }

    private bool CheckPosition()
    {
        if (!IsTurel)
            return (transform.position.x > _goalPoint.x - 0.5 && transform.position.x < _goalPoint.x + 0.5);
        else
            return (GetDirectionAngle() < 1);
        
    }

    private float GetDirectionAngle()
    {
        _direction = new Vector3(_goalPoint.x - transform.position.x, 0, _goalPoint.z - transform.position.z);
        Vector3 viewDirection = new Vector3(_pointOfView.position.x - transform.position.x, 0, _pointOfView.position.z - transform.position.z);
        var curAngle = Vector3.Angle(_direction, viewDirection);
        return curAngle;
    }

    public void SetGoalPoint(Vector3 position)
    {
        _goalPoint = position;
        _goalPoint.y = 0;
    }
    public void Shooting()
    {
        isShooting = true;
    }

    public void ReadyToGo()
    {
        isShooting = false;
    }

    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
