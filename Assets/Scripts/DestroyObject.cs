using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float _lifeDuration; 
    private void Start()
    {
        Destroy(gameObject, _lifeDuration);
    }
}
