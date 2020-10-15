using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    private float turnSpeed = 180;
    private float thrust = 0.000015f;
    private Vector3 shipDirection = new Vector3(0, 1, 0);
    private Rigidbody2D rb;
    private float maxX = 9.2f;
    private float maxY = 5.2f;
    private float maxSpeed = 2.0f;
    private float bulletSpeed = 18f;
    private GameController GameController;


    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(Random.Range(-maxX, maxX),Random.Range(-maxY, maxY),0);
    }

    // public void setGameController(GameController _gameController){
    //     gameController = _gameController;
    // }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float turnAngle;

        if(Input.GetKey("j")){
            turnAngle = turnSpeed * Time.deltaTime;
            transform.Rotate(0,0,turnAngle);
            shipDirection = Quaternion.Euler(0,0,turnAngle) * shipDirection;
        }
        if(Input.GetKey("l")){
            turnAngle = -turnSpeed * Time.deltaTime;
            transform.Rotate(0,0,turnAngle);
            shipDirection = Quaternion.Euler(0,0,turnAngle) * shipDirection;

        }
        if(Input.GetKey("k")){
            rb.AddForce(shipDirection * thrust);
        }

        // fire 
        if(Input.GetKeyDown("space")){
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = gameObject.transform.position;
            bullet.transform.rotation = gameObject.transform.rotation;
            bullet.GetComponent<Rigidbody2D>().velocity = shipDirection * bulletSpeed;
        }

        if(rb.velocity.magnitude > maxSpeed){
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // border
        if (transform.position.x < -maxX){
            transform.position = new Vector2(maxX,transform.position.y);
        }        
        else if (transform.position.x > maxX){
            transform.position = new Vector2(-maxX,transform.position.y);
        }

        if (transform.position.y < -maxY){
            transform.position = new Vector2(transform.position.x,maxY);
        }        
        else if (transform.position.y > maxY){
            transform.position = new Vector2(transform.position.x,-maxY);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Asteroid"){
            GameObject explosion = Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    
    }
}
