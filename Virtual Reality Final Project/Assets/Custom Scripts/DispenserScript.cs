using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserScript : MonoBehaviour
{
    public JuiceScript blenderJuice;
    private CupScript cupJuice;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cup")
        {
            Debug.Log("we got the cup");
            cupJuice = other.GetComponent<CupScript>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Cup")
        {
            cupJuice = null;
        }
    }

    public void MoveJuice()
    {
        if (cupJuice != null && blenderJuice.GetJuice() > 0.0f && cupJuice.GetJuice() < 1.0f)
        {
            cupJuice.AddJuice(0.004f, blenderJuice.GetJuiceColor());
            blenderJuice.RemoveJuice(0.003f);
        }
    }
}
