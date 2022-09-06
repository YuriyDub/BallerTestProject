using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [Header("Parameters:")]
    [SerializeField] private float _openDistance;

    [Header("Components:")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private ActionManager _actionManager;

    [Header("LayerMasks:")]
    [SerializeField] private LayerMask _whatCanFinish;

    private Animator _animator;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        OpenDoors();
    }

    private void OpenDoors()
    {
        if (Vector3.Distance(transform.position, _playerTransform.position) <= _openDistance)
        {           
            _animator.SetTrigger("isOpening");            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_whatCanFinish.Contains(other.gameObject.layer))    
        {
            _audioSource.Play();
            _actionManager.win.Invoke();           
        }     
    }
}
