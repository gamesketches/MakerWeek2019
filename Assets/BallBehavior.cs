using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallBehavior : MonoBehaviour
{
	Vector2 velocity;
	public float movementSpeed;
	public Rect gameBox;
	bool moving;
	public TextMeshPro counter;
	float currentPower;

    // Start is called before the first frame update
    void Start()
    {
		currentPower = 0.5f;
        moving = false;
		gameBox.y = transform.position.y + gameBox.height;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(moving) {
			float nextStepX = transform.position.x + velocity.x;
			float nextStepY = transform.position.y + velocity.y;
			if(nextStepX > gameBox.x + gameBox.width || nextStepX < gameBox.x) {
				velocity.x *= -1;
				nextStepX = transform.position.x + velocity.x;
			} else if( nextStepY > gameBox.y) {
				velocity.y *= -1;
				nextStepY = transform.position.y + velocity.y;
			} else if(nextStepY < gameBox.y - gameBox.height) {
				moving = false;
				transform.position -= new Vector3(0,nextStepY,0);
			}
			transform.position = new Vector3(nextStepX, nextStepY, transform.position.z);
		} else if(Input.GetKeyDown(KeyCode.A)) {
			moving = true;
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 travelVector = new Vector2(worldPoint.x, worldPoint.y) - new Vector2(transform.position.x, transform.position.y);
			velocity = travelVector.normalized * movementSpeed * Time.deltaTime;
		}
    }

	void OnCollisionEnter2D(Collision2D other) {
		velocity.y *= -1;
		SquareBehavior block = other.gameObject.GetComponent<SquareBehavior>();
		if(block.squareValue < currentPower) {
			StartCoroutine(HitStop());
			ScreenShake.shakeTime = 0.3f;
			currentPower += block.squareValue;
			Destroy(block.gameObject);
		}
		counter.text = "x " + currentPower;
	}

	IEnumerator HitStop() {
		float startTime = Time.realtimeSinceStartup;
		Time.timeScale = 0;
		while(Time.realtimeSinceStartup - startTime <0.045f) {
			yield return null;
		}
		Time.timeScale = 1;
	}
}
