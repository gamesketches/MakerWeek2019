using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

	public float shakeMagnitude;
	public static float shakeTime;
	public float shakeDecay;
	Vector3 startPosition;

    // Start is called before the first frame update
    void Awake()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(shakeTime > 0) {
			transform.position = startPosition + Random.insideUnitSphere *shakeMagnitude;
			shakeTime -= shakeDecay;
		}
		else {
			shakeTime = 0;
			transform.position = startPosition;
		}
    }
}
