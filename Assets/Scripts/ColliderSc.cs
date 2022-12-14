using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderSc : MonoBehaviour
{
    //General
    public GameObject lose;
    private GameObject player;
    private GameObject hiddenPlayer;
    private GameObject checkPoint;
    public Vector3 position;

    //FallingCollider
    private Transform fallObstacle;
    private Vector3 fallDir;
    private int fallSpeed;

    //RollingCollider
    private Transform rollObstacle;
    private Vector3 rollDir;
    private int rollSpeed;

    //SpinningCollider
    private Transform spinObstacle;
    private int spinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //General
        player = GameObject.Find("Player");
        hiddenPlayer = GameObject.Find("HiddenPlayer");
        checkPoint = GameObject.Find("CheckPoint");

        //Special Colliders
        if(gameObject.tag == "FallingCollider")
        {
            fallObstacle = gameObject.transform;
            fallDir = Vector3.down;
            fallSpeed = 5;
        }
        
        if(gameObject.tag == "RollingCollider")
        {
            rollObstacle = gameObject.transform;
            rollDir = Vector3.left;
            rollSpeed = 5;
        }

        if(gameObject.tag == "SpinningCollider")
        {
            spinObstacle = GameObject.Find("SpinningPivot").transform;
            spinSpeed = 90;
        }
    }

    void Update()
    {
        if(gameObject.tag == "FallingCollider")
        {
            if (fallObstacle.transform.position.y <= 1.5) {
                fallDir = Vector3.up;
            } 
            else if (fallObstacle.transform.position.y >= 5) {
                fallDir = Vector3.down;
            }
            fallObstacle.transform.Translate(fallDir * Time.deltaTime * fallSpeed);
        }
        if(gameObject.tag == "RollingCollider")
        {
            if (rollObstacle.transform.position.x <= -3.5) {
                rollDir = Vector3.right;
            } else if (rollObstacle.transform.position.x >= 0) {
                rollDir = Vector3.left;
            }
            rollObstacle.transform.Translate(rollDir * Time.deltaTime * rollSpeed);
        }

        if(gameObject.tag == "SpinningCollider")
        {
            spinObstacle.transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player")
        {
            if (player.GetComponent<PlayerMovement>().health > 0)
            {
                player.GetComponent<PlayerMovement>().health --;
                if (checkPoint.GetComponent<CheckPoint>().isTriggered)
                {
                    player.transform.position = checkPoint.GetComponent<Transform>().position;
                }
                else
                {
                    player.transform.position = new Vector3(0, 1, 0);
                }
            }
            else
            {
                lose.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    // Update is called once per frame
}
