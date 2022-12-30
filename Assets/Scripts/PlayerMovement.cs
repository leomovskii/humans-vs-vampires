using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private Rigidbody2D m_Controller;
	[SerializeField] private Collider2D m_Collider;
	[SerializeField] private float m_MovementSpeed = 5f;
	[SerializeField] private float m_RotationSpeed = 360f;

	private void Start() {
		CameraController.SetFollowing(transform);
	}

	private void Update() {
		var input = InputHandler.input;

		if (input != Vector2.zero) {
			var rotation = Quaternion.LookRotation(Vector3.forward, input);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, m_RotationSpeed);
		}
	}

	private void FixedUpdate() {
		m_Controller.velocity = InputHandler.input * m_MovementSpeed;
	}

	public void SetSleepIn(Transform bed) {
		this.enabled = false;
		m_Collider.enabled = false;
		m_Controller.velocity = Vector3.zero;
		transform.parent = bed;
		transform.position = bed.position;
		transform.eulerAngles = Vector3.zero;
	}
}