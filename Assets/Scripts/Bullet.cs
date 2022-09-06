using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTimeAfterHit;
    [SerializeField] private float _lifeTime;

    [Header("LayerMasks:")]
    [SerializeField] private LayerMask _whatCanDestroy;   

    private Vector3 _startPosition;
    public Vector3 direction { get; set; }

    private void Start()
    {
        _startPosition = transform.position;
        Destroy(gameObject, _lifeTime);
    }

    private void FixedUpdate()
    {
        MoveInDirection();
    }

    private void MoveInDirection()
    {
        transform.Translate((direction - _startPosition).normalized.x * _speed, 0, (direction - _startPosition).normalized.z * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_whatCanDestroy.Contains(other.gameObject.layer))
        {
            Destroy(gameObject, _lifeTimeAfterHit * transform.localScale.x);
        }
    }
}
