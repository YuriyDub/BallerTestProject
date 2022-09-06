using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowToPlayer : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float _acceleration;

    private Transform _playerTransform;

    private Vector3 _cameraOffset;

    private void Start()
    {
        _cameraOffset = transform.position;

        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        FollowTo();
    }

    private void FollowTo()
    {
        transform.position = Vector3.Lerp(transform.position, _playerTransform.position + _cameraOffset, _acceleration);
    }
}
