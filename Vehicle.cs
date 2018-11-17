using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

	public Vector3 vehiclePosition = Vector3.zero;
	public Vector3 direction = new Vector3(1, 0, 0);
	public Vector3 velocity = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;
	public Vector3 deceleration = Vector3.zero;
	//public float speed;
	public float rotationSpeed;
	public float angleOfRotation;
	public float rateOfAcceleration = 0.05f;
	public float maxSpeed = 1f;
	public float rateOfDeceleration = 0.05f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		RotateVehicle();
		Drive();
		SetTransform();
		ScreenWrap();

		
	}
	/// <summary>
	/// 
	/// </summary>
	public void RotateVehicle() {

		// Rotate the vehicle by 1 degree
		//left arrow = 1 degree to left
		//right arrow = 1 degree to right
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			direction = Quaternion.Euler(0, 0, rotationSpeed) * direction;
			angleOfRotation += rotationSpeed;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			direction = Quaternion.Euler(0, 0, -rotationSpeed) * direction;
			angleOfRotation -= rotationSpeed;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public void Drive() {

		if (Input.GetKey(KeyCode.UpArrow))
		{
			//increase speed for acceleration
			//speed += rateOfAcceleration;
			acceleration = rateOfAcceleration * direction;

			//Move vehicle along velocity
			velocity += acceleration;

			//limit vel vector with ClamMagnitude
			velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

			//set vel vector to object
			vehiclePosition += velocity;

		}
		else
		{
			if (velocity.x > 0)
			{
				deceleration = rateOfDeceleration * direction;

				if ((velocity.x - deceleration.x < 0) || (velocity.y - deceleration.y < 0))
				{
					velocity = Vector3.zero;
				}
				else
				{
					velocity -= deceleration;
				}

				//set vel vector to object
				vehiclePosition += velocity;
			}
			
		}

		//Rotate vehicle to face the correct direction
		transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);


	}


	/// <summary>
	/// 
	/// </summary>
	public void SetTransform(){ 

		//draws vehicle at set position
		transform.position = vehiclePosition;

	}

	public void ScreenWrap() {
		Camera cam = Camera.main;
		float height = cam.orthographicSize * 2f;
		float width = height * cam.aspect;

		if (vehiclePosition.x < (cam.transform.position.x - width/2))
		{
			vehiclePosition += new Vector3(cam.transform.position.x + width , vehiclePosition.y,vehiclePosition.z);
		}
		if (vehiclePosition.x > (cam.transform.position.x + width/2 + 1f))
		{
			vehiclePosition += new Vector3(cam.transform.position.x - width, vehiclePosition.y, vehiclePosition.z);
		}
		if (vehiclePosition.y > (cam.transform.position.y + height/2))
		{
			vehiclePosition += new Vector3(vehiclePosition.x, cam.transform.position.y - height, vehiclePosition.z);
		}
		if (vehiclePosition.y < (cam.transform.position.y - height/2))
		{
			vehiclePosition += new Vector3(vehiclePosition.x, cam.transform.position.y + height, vehiclePosition.z);
		}
	}
}
