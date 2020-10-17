using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrintheTile : MonoBehaviour
{
    [Header("Rotation")]
    public bool rotationEnabled = true;
    public float angle = 0.0f;

    [Header("Translation")]
    public bool translationEnabled = false;
    public GameObject otherPosition = null;
    public bool stickyToCharacter = true;

    bool isMoving;
    Vector3 firstPos; // Original Position
    Vector3 secondPos; // Next Position
    Vector3 futureMove; // Future movement
    float endOfMoveTime = 0.0f;
    bool newStatus = false;
    bool blocStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRotation();
        firstPos = transform.position;
        secondPos = otherPosition.transform.position;
        futureMove = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTranslation();
    }

    void FixedUpdate()
    {
        if (isMoving) {
            /*
            Debug.Log(transform.position);
            Debug.Log(firstPos + (futureMove*(1-Mathf.Max(0, endOfMoveTime - Time.time))));
            Debug.Log("-----");
            */
            transform.position = firstPos + (futureMove*(1-Mathf.Max(0, endOfMoveTime - Time.time)));
            if (endOfMoveTime - Time.time <= 0) {
                var temp = firstPos;
                firstPos = secondPos;
                secondPos = temp;
                isMoving = false;
            }
        }
    }

    void UpdateTranslation() {
        if (!isMoving) {
            if (newStatus != blocStatus) {
                /*Debug.Log(firstPos);
                Debug.Log(secondPos);
                Debug.Log(transform.position);*/
                endOfMoveTime = Time.time + 1;
                isMoving = true;
                blocStatus = newStatus;
                futureMove = secondPos - firstPos;
                /*Debug.Log(futureMove);
                Debug.Log(firstPos+futureMove);
                Debug.Log("=====");*/
            }
        }
    }

    void UpdateRotation() {
        //transform.Rotate(0.0f, 0.0f, angle, Space.Self);
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle-180.0f));
        //Debug.Log(transform.rotation);
    }

    void OnMouseDown()
    {
        if (rotationEnabled) {
            angle = (angle+90.0f) % 360.0f;
            //Debug.Log(angle);
            UpdateRotation();
        } else if (translationEnabled) {
            newStatus = !blocStatus;
        }
    }
}
