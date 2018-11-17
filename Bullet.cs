using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Vector3 bulletPosition = Vector3.zero;
	public Vector3 direction = new Vector3(1, 0, 0);
	public Vector3 velocity = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;
    Spaceship ship;

    

	// Use this for initialization
	void Start() {

        

    }

	// Update is called once per frame
	void Update() {
      
        Fire();
        SetTransform();
        
	}

	void Fire(){
		ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Spaceship>();

		bulletPosition = this.transform.position;
		acceleration = 0.03f * ship.direction;

		velocity += acceleration;

		bulletPosition += velocity;
	}

	void SetTransform() {
		transform.position = bulletPosition;
		transform.rotation = Quaternion.Euler(0, 0, ship.angleOfRotation);
	}

    
}
