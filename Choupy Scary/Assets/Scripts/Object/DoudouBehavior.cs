using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoudouBehavior : MonoBehaviour
{
    private SpriteRenderer sprd;
    private Rigidbody2D rb2d;
    private Collider2D coll2d;
    public Sprite niceDoudou;

    public GameObject player;
    public float distance;
    public float speedEvil;
    public float speedNice;

    private bool isEvil;
    private bool isPickedUp;
    private int followerNb;

    GameManager manager = null;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;

        sprd = gameObject.GetComponent<SpriteRenderer>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        coll2d = gameObject.GetComponent<Collider2D>();

        isEvil = true;
        isPickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Evil Doudou follow if the player is too close
        if (isEvil)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speedEvil * Time.deltaTime);
            }
        } 

        //Nice Doudou follow in a trail
        if (isPickedUp)
        {
            float currentDistance = Vector3.Distance(player.transform.position, transform.position);
            if (currentDistance > followerNb * 1.5)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speedNice * currentDistance * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Bullet turn evil doudou into nice ones <3
        if (collision.tag == "Bullet")
        {
            // Remove devil doudou and replace by cute doudou
            sprd.sprite = niceDoudou;
            isEvil = false;
            transform.gameObject.tag = "NiceDoudou";
            coll2d.isTrigger = true;
            Destroy(rb2d);
        }
        //Handle when a player pick up a nice doudou
        if (!isEvil && collision.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            followerNb = manager.RegisterNewDoudou();
        }
    }
}
