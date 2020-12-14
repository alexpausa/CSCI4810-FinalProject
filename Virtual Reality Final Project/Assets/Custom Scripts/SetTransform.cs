using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransform : MonoBehaviour
{
    public float yCoordinate;
    public Transform rigTransform;

    // Update is called once per frame
    void Update()
    {
        float y = yCoordinate + rigTransform.position.y;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
