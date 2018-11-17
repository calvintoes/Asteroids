using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipManager : MonoBehaviour {
    public GameObject ship;
    // Use this for initialization
    void Start () {
        
        Instantiate(ship, new Vector3(0, 0, 0), Quaternion.identity);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
