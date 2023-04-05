using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxCharacterControllerInput : MonoBehaviour
{
	private FoxCharacterController __foxCharacterController = null;
	public FoxCharacterController FoxCharacterController
    {
        get
        {
			if (this.__foxCharacterController == null)
				this.__foxCharacterController = this.GetComponent<FoxCharacterController>();
			return this.__foxCharacterController;
		}
    }

	// Update is called once per frame
	protected virtual void Update()
	{
		// Input for horizontal movement
		this.FoxCharacterController.horizontalInput = Input.GetAxisRaw("Horizontal");

		// Jump
		if (Input.GetKeyDown(KeyCode.Space) == true || Input.GetKeyDown(KeyCode.UpArrow) == true)
			this.FoxCharacterController.jump = true;

		// Crouch
		this.FoxCharacterController.crouch = Input.GetAxisRaw("Vertical") < 0.0f; //Input.GetKey(KeyCode.LeftControl);
	}
}