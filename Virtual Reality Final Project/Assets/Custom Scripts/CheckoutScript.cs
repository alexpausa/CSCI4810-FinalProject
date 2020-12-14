using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutScript : MonoBehaviour
{
    [System.NonSerialized]
    public CupScript cup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cup")
        {
            Debug.Log("we got the cup");
            cup = other.GetComponent<CupScript>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Cup")
        {
            cup = null;
        }
    }
}
