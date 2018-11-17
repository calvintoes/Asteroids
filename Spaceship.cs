using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {

    public Vector3 vehiclePosition = Vector3.zero;
    public Vector3 direction = new Vector3(1, 0, 0);
    public Vector3 velocity = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    float rotationSpeed = 2;
    public float angleOfRotation = 1;
    float rateOfAcceleration = 0.3f;
    float maxSpeed = 1f;
    float rateOfDeceleration = 0.9f;

    public AudioClip LaserBeam;
    private AudioSource source;

    // Use this for initialization
    void Awake() {

        source = GetComponent<AudioSource>();
       
    }



	
	// Update is called once per frame
	void Update () {
        GameObject bManager = GameObject.Find("BulletManager");
        BulletManager bM = bManager.GetComponent<BulletManager>();

        RotateVehicle();
		Drive();
		SetTransform();
		ScreenWrap();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bM.ShootBullet();
            source.PlayOneShot(LaserBeam);
        }
		CleanUpBullet();
	}

	/// <summary>
	/// 
	/// </summary>
	public void RotateVehicle()
	{

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
	public void Drive()
	{

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

			velocity *= rateOfDeceleration;

			if (Mathf.Abs(velocity.x) < 0.0001f || Mathf.Abs(velocity.y) < 0.0001f)
			{
				velocity = Vector3.zero;
			}

			//set vel vector to object
			vehiclePosition += velocity;

			//multiply velocity by rateOfDec .90
			//check when velocity is too small and then make it 0

			

		}
	}


	/// <summary>
	/// 
	/// </summary>
	public void SetTransform()
	{

		//draws vehicle at set position
		transform.position = vehiclePosition;

		//Rotate vehicle to face the correct direction
		transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
	}

	public void ScreenWrap()
	{
		Camera cam = Camera.main;
		float height = cam.orthographicSize * 2f;
		float width = height * cam.aspect;

		if (vehiclePosition.x < -23)
		{
            vehiclePosition.x = 23;
		}
		if (vehiclePosition.x > 23)
		{
            vehiclePosition.x = -23;
		}
		if (vehiclePosition.y > 10)
		{
            vehiclePosition.y = -10;
		}
		if (vehiclePosition.y < -10)
		{
            vehiclePosition.y = 10;
		}
	}

    public void CleanUpBullet()
    {
      
        GameObject bManager = GameObject.Find("BulletManager");
        BulletManager bM = bManager.GetComponent<BulletManager>();

        
        GameObject el = null;
        for (int i = 0; i < bM.bullets.Count; i++)
        {
            el = bM.bullets[i];
            //Debug.Log(i);
            if (bM.bullets[i].transform.position.x < -23 ||
                bM.bullets[i].transform.position.x > 23 ||
                bM.bullets[i].transform.position.y > 10 ||
                bM.bullets[i].transform.position.y < -10)
            {
                
                bM.bullets.Remove(bM.bullets[i]);
                Destroy(el);

            }
			
			
        }

    }
}
