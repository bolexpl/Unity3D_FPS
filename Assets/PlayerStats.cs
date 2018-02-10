using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerStats : MonoBehaviour {

	private float maxHealth = 100;
	private float currentHealth = 100;
	private float maxArmour = 100;
	private float currentArmour = 100;
	private float maxStamina = 100;
	private float currentStamina = 100;

	public Texture2D healthTexture;
	public Texture2D armourTexture;
	public Texture2D staminaTexture;

	private float barWidth;
	private float barHeight;

	public float walkSpeed = 10.0f;
	public float runSpeed = 20.0f;

	private CharacterController chCont;
	private FirstPersonController fpsC;

	private Vector3 lastPosition;

	void Awake() {
		barHeight = Screen.height * 0.02f;
		barWidth = barHeight  * 10.0f;

		chCont = GetComponent<CharacterController> ();
		fpsC = gameObject.GetComponent<FirstPersonController> ();
	}

	void OnGUI() {
		GUI.DrawTexture (new Rect(Screen.width - barWidth - 10, 
			Screen.height - barHeight - 10, 
			currentHealth * barWidth / maxHealth,
			barHeight),
			healthTexture);
		GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
			Screen.height - barHeight * 2 - 20,
			currentArmour * barWidth / maxArmour,
			barHeight),
			armourTexture);
		GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
			Screen.height - barHeight * 3 - 30,
			currentStamina * barWidth / maxStamina,
			barHeight),
			staminaTexture);
	}

	void takeHit(float damage) {
		if (currentArmour > 0) {
			currentArmour -= damage;
			if (currentArmour < 0) {
				currentHealth += currentArmour;
			}
		} else {
			currentHealth -= damage;
		}

		currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth);
		currentArmour = Mathf.Clamp (currentArmour, 0, maxArmour);
	}

	void FixedUpdate () {
		float speed = walkSpeed;

		if (chCont.isGrounded 
			&& Input.GetKey (KeyCode.LeftShift)
			&& lastPosition != transform.position 
			&& currentStamina > 0) {

			lastPosition = transform.position;
			speed = runSpeed;
			currentStamina -= 1;
			currentStamina = Mathf.Clamp (currentStamina, 0, maxStamina);
		}

		if (currentStamina > 0) {
			fpsC.CanRun = true;
		} else {
			fpsC.CanRun = false;
		}
	}
}
