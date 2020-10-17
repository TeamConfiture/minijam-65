using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Attributes")]
    public float moveMultiplier = 7f;

    GameManager manager = null;

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
    }
}
