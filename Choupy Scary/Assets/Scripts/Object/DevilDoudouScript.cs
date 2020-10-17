using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilDoudouScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite newSprite;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            // Remove devil doudou and replace by cute doudou
            spriteRenderer.sprite = newSprite;
        }
    }
}
