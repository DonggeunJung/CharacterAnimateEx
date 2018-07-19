using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour {

	CharacterController controller;
	Animator animator;
	Vector3 moveDirection = Vector3.zero;

	public float gravity = 20f;
	public float speedJump = 8f;
	public float speedZ = 3f;

	// Use this for initialization
	void Start () {
		// Auto geting components
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if( controller.isGrounded ) {
			if (Input.GetButton ("Jump")) {
				moveDirection.y = speedJump;
				animator.SetTrigger ("jump");
			}
			if (Input.GetAxis ("Vertical") < 0)
				animator.SetTrigger ("damage");
			else if (Input.GetAxis ("Vertical") > 0) {
				moveDirection.z = speedZ;
			} else {
				moveDirection.z = 0;
			}

			transform.Rotate (0, Input.GetAxis ("Horizontal") * 3, 0);
		}
		animator.SetBool ("run", moveDirection.z > 0);

		moveDirection.y -= gravity * Time.deltaTime;

		Vector3 globalDirection = transform.TransformDirection (moveDirection);
		controller.Move (globalDirection * Time.deltaTime);
	}
}
