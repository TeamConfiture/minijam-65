using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrintheTile : MonoBehaviour
{
    [Header("LabyrintheStatus")]
    public float angle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateRotation() {
        //transform.Rotate(0.0f, 0.0f, angle, Space.Self);
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle-180.0f));
        //Debug.Log(transform.rotation);
    }

    void OnMouseDown()
    {
        angle = (angle+90.0f) % 360.0f;
        Debug.Log(angle);
        UpdateRotation();
    }
}
