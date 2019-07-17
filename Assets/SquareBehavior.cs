using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBehavior : MonoBehaviour
{
	SpriteRenderer spriteRenderer;
	public float squareValue;
	public static Color emptyColor = new Color(0,0,0);
	public static Color fullColor = new Color(241f/255f,84f/255f,64f/255f);

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
		ChangeColor(1f);
    }

    // Update is called once per frame
    void Update() {
        
    }

	public void ChangeColor(float input) {
		squareValue = input;
		spriteRenderer.color = Color.Lerp(emptyColor, fullColor, input);
	}
		
}
