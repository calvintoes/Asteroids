using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

	public List<GameObject> gameObjects = new List<GameObject>();
	private List<GameObject> smallerAsteroids = new List<GameObject>();
	public List<GameObject> clones = new List<GameObject>();
    public BulletManager bulletManager;
	GameObject shipInstance;
    
	GUIManager GUIManager;
    Asteroid smallAsteroidInstance;

    public AudioClip Crash;
    public AudioClip Explosion;
    public AudioClip BGMusic;
    private AudioSource source;

    // Use this for initialization
    void Start() {

		SpawnAsteroids();
		
		bulletManager = FindObjectOfType<BulletManager>();
		GUIManager = FindObjectOfType<GUIManager>();

        source = GetComponent<AudioSource>();

        if (!GUIManager.gameOver)
        {
            source.PlayOneShot(BGMusic);
        }
        

	}

	//Update is called once per frame
	void Update() {

        DeadAndGone();

	}

	void SpawnAsteroids()
	{

		for (int i = 0; i < gameObjects.Count; i++)
		{
			float x_value = Random.Range(-24f, 24f);
			float y_value = Random.Range(-10f, 10f);

			clones.Add(Instantiate(gameObjects[i], new Vector3(x_value, y_value, 0), Quaternion.identity));

			

		}
	}
	/// <summary>
	/// Handles dying and breaking into smaller components of itself
	/// </summary>
	void DeadAndGone(){
		shipInstance = GameObject.Find("ship(Clone)");
		List<GameObject> bullets = bulletManager.bullets;

       
        //checks if Collisions are enabled
        if (GUIManager.GetComponent<GUIManager>().Collisions)
        {


            //Checks list of smaller asteroids
            for (int i = 0; i < smallerAsteroids.Count; i++)
            {

                if (CollisionsWithShip(smallerAsteroids[i], shipInstance))
                {
                    //Debug.Log("Ship HIT lose a life");
                    GUIManager.UpdateHealth();
                    source.PlayOneShot(Crash);
                }
                for (int j = 0; j < bullets.Count; j++)
                {
                    //If smaller asteroid gets shot
                    if (CollisionsWithBullet(smallerAsteroids[i], bullets[j]))
                    {
                        //die
                        //Debug.Log("small dead");
                        source.PlayOneShot(Explosion);
                        GUIManager.Score += 50;
                        Destroy(smallerAsteroids[i]);
                        smallerAsteroids.RemoveAt(i);

                        Destroy(bullets[j]);
                        bullets.RemoveAt(j);
                    }
                }

            }

            //Checks list of big asteroids that haven't been shot at yet
            for (int i = 0; i < clones.Count; i++)
            {

                if (CollisionsWithShip(clones[i], shipInstance))
                {
                    //Debug.Log("HIT. Lose a life");
                    GUIManager.UpdateHealth();
                    source.PlayOneShot(Crash);

                }

                for (int j = 0; j < bullets.Count; j++)
                {
                    //If asteroid gets shot down
                    if (CollisionsWithBullet(clones[i], bullets[j]))
                    {

                        Asteroid temp = clones[i].GetComponent<Asteroid>();
                        Vector3 tempLocation = temp.asteroidPosition;


                        //Split into 2 smaller ones and add into smallerAsteroids list
                        //Debug.Log("Hit. Split into 2");

                        Destroy(clones[i]);
                        clones.RemoveAt(i);
                        source.PlayOneShot(Explosion);
                        GUIManager.Score += 25;

                        Destroy(bullets[j]);
                        bullets.RemoveAt(j);

                        GameObject TempAssOne = Instantiate(clones[i], tempLocation, Quaternion.identity);
                        GameObject TempAssTwo = Instantiate(clones[i], tempLocation, Quaternion.identity);
                        TempAssOne.transform.localScale = new Vector3(0.5f, 0.5f, 0);
                        TempAssTwo.transform.localScale = new Vector3(0.5f, 0.5f, 0);
                        smallerAsteroids.Add(TempAssOne);
                        smallerAsteroids.Add(TempAssTwo);
                        Asteroid a = TempAssOne.GetComponent<Asteroid>();
                        Asteroid b = TempAssTwo.GetComponent<Asteroid>();
                        //Debug.Log("smallA: " + a);
                        a.RateOfAcceleration = 30f;
                        b.RateOfAcceleration = 30f;

                        //continue;


                    }


                }
            }
        }
        //become invincible for a bit
        else
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GUIManager.Invincibility();
               
            }
        }
		
	}


	/// <summary>
	/// Handles collisions with itself and ship
	/// </summary>
	/// <param name="asteroid"></param>
	/// <param name="ship"></param>
	public bool CollisionsWithShip( GameObject asteroid, GameObject ship) {
			
			float asteroidMinX = asteroid.GetComponent<SpriteRenderer>().bounds.min.x;
			float asteroidMinY = asteroid.GetComponent<SpriteRenderer>().bounds.min.y;
			float asteroidMaxX = asteroid.GetComponent<SpriteRenderer>().bounds.max.x;
			float asteroidMaxY = asteroid.GetComponent<SpriteRenderer>().bounds.max.y;

			float shipMinX = ship.GetComponent<SpriteRenderer>().bounds.min.x;
			float shipMinY = ship.GetComponent<SpriteRenderer>().bounds.min.y;
			float shipMaxX = ship.GetComponent<SpriteRenderer>().bounds.max.x;
			float shipMaxY = ship.GetComponent<SpriteRenderer>().bounds.max.y;

		


		return (shipMinX < asteroidMaxX && shipMaxX > asteroidMinX && shipMinY < asteroidMaxY && shipMaxY > asteroidMinY);
	}


	/// <summary>
	/// Handles collisions with itself and all bullets
	/// </summary>
	/// <param name="asteroid"></param>
	/// <param name="bullet"></param>
	public bool CollisionsWithBullet(GameObject asteroid, GameObject bullet) {

		float asteroidMinX = asteroid.GetComponent<SpriteRenderer>().bounds.min.x;
		float asteroidMinY = asteroid.GetComponent<SpriteRenderer>().bounds.min.y;
		float asteroidMaxX = asteroid.GetComponent<SpriteRenderer>().bounds.max.x;
		float asteroidMaxY = asteroid.GetComponent<SpriteRenderer>().bounds.max.y;

		float bulletMinX = bullet.GetComponent<SpriteRenderer>().bounds.min.x;
		float bulletMinY = bullet.GetComponent<SpriteRenderer>().bounds.min.y;
		float bulletMaxX = bullet.GetComponent<SpriteRenderer>().bounds.max.x;
		float bulletMaxY = bullet.GetComponent<SpriteRenderer>().bounds.max.y;

		


		return (bulletMinX < asteroidMaxX && bulletMaxX > asteroidMinX && bulletMinY < asteroidMaxY && bulletMaxY > asteroidMinY);
	}
	
}
