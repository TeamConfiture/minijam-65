using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Attributes")]
    public float moveMultiplier = 7f;
    public Rigidbody2D bullet;
    public Animator anim;
    
    GameManager manager = null;
    GameObject myPlatform = null;
    Vector3 oldPlatformPos;

    [SerializeField]
    public float bulletSpeed = 500f;
    public float lifespan = 3f;

    void Start()
    {
        manager = GameManager.Instance;
        manager.doudouNb = 0;
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
        direction.z = 0;


        //on restreint le tir à 4 directions en enlevant la coordonée (x ou y) la plus petite (en valeur absolue) pour ne garder que la direction "principale"
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction.y = 0;
        else
            direction.x = 0;
            

        direction.Normalize();

        Debug.Log(direction);

        Rigidbody2D projectile = Instantiate(bullet, transform.position, transform.rotation);
        projectile.AddForce(direction * bulletSpeed);
        projectile.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Destroy(projectile.transform.gameObject, lifespan);
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

            if (movement.x > 0)
            {
                anim.SetBool("IsWalking", true);
                anim.SetInteger("Direction", 1);
            } else if (movement.x < 0)
            {
                anim.SetBool("IsWalking", true);
                anim.SetInteger("Direction", 3);
            } else if (movement.y > 0)
            {
                anim.SetBool("IsWalking", true);
                anim.SetInteger("Direction", 0);
            } else if (movement.y < 0)
            {
                anim.SetBool("IsWalking", true);
                anim.SetInteger("Direction", 2);
            } else
            {
                anim.SetBool("IsWalking", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Platform") {
            myPlatform = collision.gameObject;
            oldPlatformPos = myPlatform.transform.position;
            /*Debug.Log("Enter " + collision.gameObject);
            Debug.Log("Sticking to a Tile");*/
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (myPlatform != null) {
            if (myPlatform == collision.gameObject) {
                myPlatform = null;
                //Debug.Log("Leaving my Tile");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("DevilDoudou"))
        {
            Debug.Log("MORT");
        }
    }
}
