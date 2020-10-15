using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameController : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject spaceshipPrefab;
    private int numAsteroids = 1;
    private float minCollisionRadius = 2.0f;
    private GameObject spaceship;
    private GameObject gameOverSign;


    private GameObject extraLives;
    private GameObject score;
    public int kills;
    private int frame = 0;
    private int difficulty =1;



    private int numLivesLeft;
    private int maxLives = 4;
    public float timeDied;

    public void kill(){
        kills += 1;
    }



    private void Awake(){
        numLivesLeft = maxLives;
        gameOverSign = GameObject.Find("GameOver");
        score = GameObject.Find("Score");
        extraLives = GameObject.Find("ExtraLives");
        InitializeLevel();

    }

    private void InitializeLevel(){
        for (int i = 0; i <numAsteroids; i++){
            spawnAsteroid();
        }

        spawnSpaceship();
        gameOverSign.SetActive(false);
    }

    private bool CheckTooCloseToAsteroid(GameObject testObject){
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        foreach(GameObject asteroid in asteroids){
            if(asteroid != testObject){

                if(Vector3.Distance(asteroid.transform.position, testObject.transform.position) < minCollisionRadius){
                    Destroy(testObject);
                    return false;
                }

            }
        }
        return true;
    }

    private void spawnAsteroid(){

        bool valid;
        GameObject newAsteroid;

        do{
            newAsteroid = Instantiate(asteroidPrefab);
            newAsteroid.gameObject.tag = "Asteroid";

            valid = CheckTooCloseToAsteroid(newAsteroid);

        }while (valid == false);

        return;

    }

    private void spawnSpaceship(){

        bool valid;

        do{
            spaceship = Instantiate(spaceshipPrefab);
            spaceship.gameObject.tag = "Spaceship";

            valid = CheckTooCloseToAsteroid(spaceship);

        }while (valid == false);

        numLivesLeft -= 1;

        return;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score.GetComponent<TMP_Text>().text = "Score: " + kills*10;
        extraLives.GetComponent<TMP_Text>().text = "Extra Lives: " + (numLivesLeft);

        frame += 1;

        if(kills > 1){ difficulty = 2;}
        if(kills > 10){ difficulty = 3;}
        if(kills > 20){ difficulty = 4;}
        if(kills > 30){ difficulty = 5;}
        if(kills > 50){ difficulty = 6;}


        if(frame > (10-difficulty)*60){
            spawnAsteroid();
            frame = 0;
        }
        

        // extraLives.text = "Extra Lives: " + (numLivesLeft-1);

        if (spaceship == null)
        {
            if (numLivesLeft > 0)
            {
                spawnSpaceship();
            }
            else{
                gameOverSign.SetActive(true);
            }
        }
    }
}
