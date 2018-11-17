using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

	public List<GameObject> bullets = new List<GameObject>();
	public GameObject bullet;
    Spaceship ship;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void ShootBullet()
	{
		ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Spaceship>();
		GameObject clonebullet = Instantiate(bullet, ship.vehiclePosition, Quaternion.identity);

		bullets.Add(clonebullet);
		
	}

    
}
