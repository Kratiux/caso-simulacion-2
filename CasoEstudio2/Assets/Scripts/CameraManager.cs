using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Transform target;
 
    [SerializeField]
    float smoothTime = 0.05F;
 
	Vector3 _offset;
	Vector3 _currentVelocity;
 
	void Awake()
	{
		_offset = transform.position - target.position;
	}
 
	void LateUpdate()
	{
		Vector3 targetPosition = target.position + _offset;
		transform.position =
			Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
	}


}

