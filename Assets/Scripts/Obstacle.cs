using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("LayerMasks:")]
    [SerializeField] private LayerMask _whatCanDetonate;

    [Header("Parameters:")]
    [SerializeField] private float _timeToDestroy;

    [Header("Effects:")]
    [SerializeField] GameObject _boomEffect;

    private AudioSource _audioSource;

    private Animator _animator;

    public float resizeDamage;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_whatCanDetonate.Contains(other.gameObject.layer))
        {
            Instantiate(_boomEffect, transform.position, Quaternion.identity);
            _audioSource.Play();
            _animator.SetTrigger("isDetonate");
            Destroy(gameObject, _timeToDestroy);
        }
    }
}
