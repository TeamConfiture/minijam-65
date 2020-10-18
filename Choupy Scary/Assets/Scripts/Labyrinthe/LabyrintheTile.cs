using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrintheTile : MonoBehaviour
{
    [Header("Rotation")]
    public bool rotationEnabled = true;
    public float angle = 0.0f;
    public bool PlayerOnTile = false;

    [Header("Translation")]
    public bool translationEnabled = false;
    public GameObject[] otherPositions;
    
    bool isMoving;
    List<Vector3> tilePositions = new List<Vector3>();

    GameManager manager = null;
    Vector3 futureMove; // Future movement
    float endOfMoveTime = 0.0f;
    int previousState;
    int nextState;

    private bool hasEntity;

    [Header("Cursor texture")]
    public Texture2D cursorTextureRotate;
    public Texture2D cursorTextureTranslate;

    [Header("Audio")]
    public AudioClip translate;
    public AudioClip rotate;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
        hasEntity = false;
        audio = gameObject.GetComponent<AudioSource>();
        angle += 180.0f;
        if (transform.rotation.eulerAngles.z != 0) {
            /*Debug.Log(transform.rotation.eulerAngles.z);
            Debug.Log(angle);*/
            angle = (transform.rotation.eulerAngles.z+angle);
            //Debug.Log(angle);
        }
        angle = angle % 360.0f;
        //UpdateRotation();
        if (translationEnabled) {
            for (int i=0;i<otherPositions.Length-1;i++) {
                tilePositions.Add(otherPositions[i+1].transform.position - otherPositions[i].transform.position);
				otherPositions[i].AddComponent<BoxCollider2D>();
				otherPositions[i].GetComponent<BoxCollider2D>().size = new Vector2(6,6);
            }
			otherPositions[0].GetComponent<BoxCollider2D>().enabled = false;
			
			otherPositions[otherPositions.Length-1].AddComponent<BoxCollider2D>();
			otherPositions[otherPositions.Length-1].GetComponent<BoxCollider2D>().size = new Vector2(6,6);
            tilePositions.Add(otherPositions[0].transform.position - otherPositions[otherPositions.Length-1].transform.position);
            previousState = 0;
            nextState = 0;
            futureMove = new Vector3(0.0f, 0.0f, 0.0f);
            //Debug.Log(transform.position);
            transform.position = otherPositions[0].transform.position;
            /*Debug.Log(transform.position);
            Debug.Log(otherPositions[0].transform.position);
            Debug.Log("===");*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTranslation();/*
        if (Input.GetButton("Fire1")) {

            if (manager.PouvoirBetonniere) {
                if (rotationEnabled) {
                    Debug.Log(GetComponent<BoxCollider2D>().GetInstanceID());
                    Debug.Log(GetInstanceID()+10);
                    Debug.Log(manager.PlayerOnTile);
                    Debug.Log("===");
                    if (manager.PlayerOnTile != GetInstanceID()+10) {
                        angle = (angle+90.0f) % 360.0f;
                        //Debug.Log(angle);
                        UpdateRotation();
                    }
                } else if (translationEnabled) {
                    if (!isMoving) {
                        nextState = (previousState+1)%otherPositions.Length;
                    }
                }
            }
        }*/
    }

    void FixedUpdate()
    {
        if (isMoving) {
            /*
            Debug.Log(transform.position);
            Debug.Log(firstPos + (futureMove*(1-Mathf.Max(0, endOfMoveTime - Time.time))));
            Debug.Log("-----");
            */
            transform.position = otherPositions[previousState].transform.position + 
                                 (futureMove*(1-Mathf.Max(0, endOfMoveTime - Time.time)));
            if (endOfMoveTime - Time.time <= 0) {
				for(int i =0; i < otherPositions.Length;i++)
				{
					if(otherPositions[i].transform.position == otherPositions[previousState].transform.position)
						otherPositions[i].GetComponent<BoxCollider2D>().enabled = true;
				}
				
                previousState = nextState;
                futureMove = Vector2.zero;
                isMoving = false;
            }
        }
    }

    void UpdateTranslation() {
        if (!isMoving) {
            if (previousState != nextState) {
                /*Debug.Log(previousState);
                Debug.Log(nextState);
                Debug.Log(otherPositions[previousState].transform.position);
                Debug.Log(otherPositions[nextState].transform.position);
                Debug.Log(transform.position);
                Debug.Log(otherPositions[0].transform.position);*/
                endOfMoveTime = Time.time + 1;
				
				for(int i =0; i < otherPositions.Length;i++)
				{
					if(otherPositions[i].transform.position == otherPositions[nextState].transform.position)
						otherPositions[i].GetComponent<BoxCollider2D>().enabled = false;
				}
                
				isMoving = true;
				
                futureMove = tilePositions[previousState];
                audio.PlayOneShot(translate);
                /*Debug.Log(futureMove);
                Debug.Log(otherPositions[previousState].transform.position+futureMove);
                Debug.Log("=====");*/
            }
        }
    }

    void UpdateRotation() {
        //transform.Rotate(0.0f, 0.0f, angle, Space.Self);
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle-180.0f));
        audio.PlayOneShot(rotate);
        //Debug.Log(transform.rotation);
    }

    void OnMouseUpAsButton()
    {
        /*if (Input.GetButton("Fire1")) {*/
            if (manager.PouvoirBetonniere) {
                if (rotationEnabled) {
                    /*Debug.Log(GetComponent<BoxCollider2D>().GetInstanceID());
                    Debug.Log(GetInstanceID()+10);
                    Debug.Log("===");*/
                    if (!hasEntity) {
                        angle = (angle+90.0f) % 360.0f;
                        //Debug.Log(angle);
                        UpdateRotation();
                    }
                } else if (translationEnabled) {
                    if (!isMoving) {
                        nextState = (previousState+1)%otherPositions.Length;
						
                    }
                }
            }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("DevilDoudou"))
        {
            //Debug.Log("on tile " + transform.name);
            hasEntity = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("DevilDoudou"))
        {
            //Debug.Log("exit tile " + transform.name);
            hasEntity = false;
        }
    }

    private void OnMouseEnter()
    {
        if (rotationEnabled)
        {
            Cursor.SetCursor(cursorTextureRotate, Vector2.zero, CursorMode.Auto);
        } else if (translationEnabled)
        {
            Cursor.SetCursor(cursorTextureTranslate, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseOver()
    {
        if (rotationEnabled && !hasEntity)
        {
            Cursor.SetCursor(cursorTextureRotate, Vector2.zero, CursorMode.Auto);
        } else if (translationEnabled)
        {
            Cursor.SetCursor(cursorTextureTranslate, Vector2.zero, CursorMode.Auto);
        } else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
