using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{

    public float speed = 1;
    
    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        transform.rotation = Quaternion.EulerAngles(time * speed, time * speed, time * speed) * transform.rotation;
    }
}
