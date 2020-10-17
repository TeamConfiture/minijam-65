using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Attributes")]
    public float moveMultiplier = 7f;
    public Rigidbody2D bullet;

    
    GameManager manager = null;
    GameObject myPlatform = null;
    Vector3 oldPlatformPos;

    [SerializeField]
    public float bulletSpeed = 500f;
    public float lifespan = 3f;

    void Start()
    {
        manager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && CandyTextScript.candyAmount > 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        Debug.Log("Fire");
        CandyTextScript.candyAmount--;

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        var direction = worldMousePosition - transform.position;
        direction.Normalize();

        Rigidbody2D projectile = Instantiate(bullet, transform.position, transform.rotation);
        projectile.AddForce(direction * bulletSpeed);
        Destroy(projectile, lifespan);
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
