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

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
        UpdateRotation();
        if (translationEnabled) {
            for (int i=0;i<otherPositions.Length-1;i++) {
                tilePositions.Add(otherPositions[i+1].transform.position - otherPositions[i].transform.position);
            }
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
                previousState = nextState;
                futureMove = new Vector3(0.0f, 0.0f, 0.0f);
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
                isMoving = true;
                futureMove = tilePositions[previousState];
                /*Debug.Log(futureMove);
                Debug.Log(otherPositions[previousState].transform.position+futureMove);
                Debug.Log("=====");*/
            }
        }
    }

    void UpdateRotation() {
        //transform.Rotate(0.0f, 0.0f, angle, Space.Self);
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle-180.0f));
        //Debug.Log(transform.rotation);
    }

    void OnMouseUpAsButton()
    {
        /*if (Input.GetButton("Fire1")) {*/
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
        //}
    }
}
