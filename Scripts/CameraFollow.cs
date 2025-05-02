using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	Transform target;

	Vector3 velocity = Vector3.zero;
	public float smoothTime = .15f;

	private void Start() {
		target = GameObject.Find("Player").transform;		
	}

	void Update()
	{
		Vector3 targetPos = target.position;

		targetPos.z = transform.position.z;

		transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref velocity, smoothTime);
	}
}
