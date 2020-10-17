using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuikMouve : MonoBehaviour
{

    [Header("Attribute")]
    public GameObject destination;
    public Transform nextCameraPosition;

	
    // Start is c alled before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void FixedUpdate()
	{
	
	}
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
			collision.transform.position = destination.transform.position;
			collision.attachedRigidbody.velocity = Vector2.zero;
            Input.ResetInputAxes();
            if(nextCameraPosition != null) {
                Camera.main.transform.position = nextCameraPosition.position;
                Debug.Log(Camera.main.transform.position);
            }
        }
    }
}
