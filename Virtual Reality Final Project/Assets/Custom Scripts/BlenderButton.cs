using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderButton : MonoBehaviour
{
    // Need to add BoxCollider variable to the blender script so you can swap blenders between bases - gets from baseCollider.collision
    public BlenderScript blender;

    public void setBlender(BlenderScript blend)
    {
        blender = blend;
    }

    public void resetBlender()
    {
        // blender = null;
    }

    public void ButtonPress()
    {
        if (blender != null)
        {
            blender.TurnOnBlender();
            GetComponent<AudioSource>().Play();
        }
    }
}
