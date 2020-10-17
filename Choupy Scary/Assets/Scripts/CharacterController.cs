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
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        //Debug.Log(movement);
        transform.position += movement * Time.deltaTime * moveMultiplier;
        if (myPlatform != null) {
            transform.position += (myPlatform.transform.position - oldPlatformPos);
            oldPlatformPos = myPlatform.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Platform") {
            myPlatform = collision.gameObject;
            oldPlatformPos = myPlatform.transform.position;
            Debug.Log(collision.gameObject);
            manager.PlayerOnTile = collision.gameObject.GetInstanceID();
            Debug.Log("Sticking to a Tile");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (myPlatform != null) {
            if (myPlatform == collision.gameObject) {
                myPlatform = null;
                manager.PlayerOnTile = -1;
                Debug.Log("Leaving my Tile");
            }
        }
    }
}
