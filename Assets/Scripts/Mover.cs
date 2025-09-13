using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;
    [SerializeField] private Transform _pointList;
    [SerializeField] private Transform[] _targetPoints;
    [SerializeField] private float _speed;

    private int _currentPoint = 0;
    private float _runSpeed;

    private void Start()
    {
        if (_targetPoints == null)
            throw new NullReferenceException("Список точек пуст");

        _runSpeed = _speed * 4;
    }

    private void OnEnable()
    {
        _alarm.alarmEnabled += Run;
    }

    private void OnDisable()
    {
        _alarm.alarmEnabled -= Run;
    }

    private void Update()
    {
        if (transform.position == _targetPoints[_currentPoint].position)
        {
            _currentPoint = ++_currentPoint % _targetPoints.Length;

            RotateToTarget();
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPoints[_currentPoint].position, _speed);
    }

    private void RotateToTarget()
    {
        transform.LookAt(_targetPoints[_currentPoint].position);
    }

    private void Run()
    {
        _speed = _runSpeed;
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = _pointList.childCount;
        _targetPoints = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            _targetPoints[i] = _pointList.GetChild(i);
        }
    }
#endif
}