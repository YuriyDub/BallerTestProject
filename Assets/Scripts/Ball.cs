using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    [Header("Parameters:")]
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minScale;
    [SerializeField] private float _bulletSize;
    [SerializeField] private float _shootCostMultiplicator;

    [Header("Components:")]
    [SerializeField] private TouchHandler _touchHandler;
    [SerializeField] private ActionManager _actionManager;

    [SerializeField] private GameObject _bulletPrefab;

    [Header("Effects:")]
    [SerializeField] ParticleSystem _resizeEffect;

    [Header("LayerMasks:")]
    [SerializeField] private LayerMask _whatCanResize;

    private float _startSpeed;

    private float _distanceToFinish;
    private float _currentProgress;

    private Vector3 _startPosition;
    private Vector3 _finishPosition;

    private bool _hasStoped = false;

    public float _scaleMultiplicator { get; set; }

    private void Start()
    {
        _startSpeed = _speed;       

        _finishPosition = GameObject.FindGameObjectWithTag("Finish").GetComponent<Transform>().position;
        _startPosition = transform.position;

        _distanceToFinish = Mathf.Abs(_finishPosition.z - transform.position.z);
    }

    private void FixedUpdate()
    {
        CalculateScaleMultiplicator();
        IncreaseSpeed();
        MoveToFinish();
        CheckState();
    }

    private void CalculateScaleMultiplicator()
    {
        if (_touchHandler.touchTime < _touchHandler.maxTouchTime && _touchHandler.touchTime > _touchHandler.minTouchTime)
        {
            _scaleMultiplicator = _touchHandler.touchTime / _touchHandler.maxTouchTime;
        }
        else if (_touchHandler.touchTime > _touchHandler.maxTouchTime)
        {
            _scaleMultiplicator = 1;
        }
        else if (_touchHandler.touchTime < _touchHandler.minTouchTime)
        {
            _scaleMultiplicator = 0;
        }
        else return;
    }

    private void Shoot()
    {
        if (!_hasStoped && _touchHandler.touchTime > _touchHandler.minTouchTime)
        {
            GameObject bullet;          

            _resizeEffect.Play();

            bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.localScale = transform.localScale * _scaleMultiplicator * _bulletSize;
            transform.localScale *= (1 - _scaleMultiplicator * _shootCostMultiplicator);

            bullet.GetComponent<Bullet>().direction = _touchHandler.touchPosition;
        }
    }

    private void CheckState()
    {
        if (transform.localScale.x < _minScale)
        {
            _hasStoped = true;
            _actionManager.lose.Invoke();
        }
    }

    private void MoveToFinish()
    {
        if (!_hasStoped)
        {
            transform.Translate(Vector3.forward * _speed);
        }
    }

    private void IncreaseSpeed()
    {
        if (!_hasStoped)
        {
            _currentProgress = Mathf.Abs(transform.position.z - _startPosition.z) / _distanceToFinish;
            _speed = Mathf.Lerp(_startSpeed, _maxSpeed, _currentProgress);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_hasStoped)
        {
            if (_whatCanResize.Contains(other.gameObject.layer))
            {
                float resizeDamage = other.gameObject.GetComponent<Obstacle>().resizeDamage;
                transform.localScale -= new Vector3(resizeDamage, resizeDamage, resizeDamage);
                _resizeEffect.Play();
            }
        }
    }

    private void StopBall()
    {
        _hasStoped = true;
    }

    private void OnEnable()
    {
        _touchHandler.onTouched += Shoot;
        _actionManager.win += StopBall;
    }

    private void OnDisable()
    {
        _touchHandler.onTouched -= Shoot;
        _actionManager.win -= StopBall;
    }
}
