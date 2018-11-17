using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
	
	public Vector3 asteroidPosition = Vector3.zero;
	public Vector3 direction = new Vector3(1, 0, 0);
	public Vector3 velocity = Vector3.zero;
    public float RateOfAcceleration = 1f;

	// Use this for initialization
	void Start()
	{
		asteroidPosition = new Vector3(Random.Range(-20, 20), Random.Range(-10,10), 0);	
	}

	// Update is called once per frame
	void Update()
	{
		
		Move();
		ScreenWrap();
		setTransform();
		
		
	}

	

	public void ScreenWrap()
	{
	

        if (asteroidPosition.x < -23)
        {
            asteroidPosition.x = 23;
        }
        if (asteroidPosition.x > 23)
        {
            asteroidPosition.x = -23;
        }
        if (asteroidPosition.y > 10)
        {
            asteroidPosition.y = -10;
        }
        if (asteroidPosition.y < -10)
        {
            asteroidPosition.y = 10;
        }
    }

	void Move() {

		Random.InitState(this.gameObject.GetInstanceID());

		velocity = new Vector3(Random.Range(0, 0.05f), Random.Range(0,0.05f), 0);
        velocity *= RateOfAcceleration;
        velocity = Vector3.ClampMagnitude(velocity, 0.1f);

		asteroidPosition += velocity;


	}

	void setTransform() {

		transform.position = asteroidPosition;
	}

}
