using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Attributes")]
    public float moveMultiplier = 7f;

    GameManager manager = null;
    GameObject myPlatform = null;
    Vector3 oldPlatformPos;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        bool canMove = true;
        if (myPlatform != null) {
            Vector3 delta = (myPlatform.transform.position - oldPlatformPos);
            if (delta != new Vector3(0.0f,0.0f,0.0f)) {
                transform.position += delta;
                canMove = false;
            }
            oldPlatformPos = myPlatform.transform.position;
        }
        if (canMove) {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            //Debug.Log(movement);
            transform.position += movement * Time.deltaTime * moveMultiplier;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Platform") {
            myPlatform = collision.gameObject;
            oldPlatformPos = myPlatform.transform.position;
            Debug.Log("Enter " + collision.gameObject);
            Debug.Log("Sticking to a Tile");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (myPlatform != null) {
            if (myPlatform == collision.gameObject) {
                myPlatform = null;
                Debug.Log("Leaving my Tile");
            }
        }
    }
}
