using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    [Header("LayerMasks:")]
    [SerializeField] private LayerMask _whatIsFloor;

    public float maxTouchTime;

    public float minTouchTime;

    public Action onTouched { get; set; }

    public Vector3 touchPosition { get; set;}

    public float touchTime { get; set; }

    private void Update()
    {
        GetTouchPosition();
    }

    private void GetTouchPosition()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _whatIsFloor))
        {
            touchPosition = raycastHit.point;

            if (Input.GetMouseButton(0))
            {
                touchTime += Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0) || raycastHit.collider == null)
            {
                onTouched.Invoke();
                touchTime = 0;
            }
        }
    }

}
