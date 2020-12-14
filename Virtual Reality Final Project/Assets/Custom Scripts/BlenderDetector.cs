using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderDetector : MonoBehaviour
{
    private BlenderScript blender;
    public BlenderButton button;

    private void OnTriggerEnter(Collider other)
    {
        blender = other.GetComponentInParent<BlenderScript>();

        if (blender != null)
        {
            Debug.Log(blender);
            button.blender = blender;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        button.resetBlender();
        Debug.Log("Exit");
    }
}
