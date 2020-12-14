using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupScript : MonoBehaviour
{
    private float juice;
    private Color juiceColor;
    public MeshRenderer juiceSolid;
    public Transform anchor;

    // Start is called before the first frame update
    void Start()
    {
        juiceColor = new Color(0.0f, 0.0f, 0.0f);
        juice = 0.0f;
        juiceSolid.enabled = false;
    }

    public void AddJuice(float percent, Color color)
    {
        float cjPercent = juice / (juice + percent);
        float pPercent = percent / (juice + percent);

        // ADD SOME SORT OF SOUND EFFECT

        juice += percent;

        if (juice > 1.0f)
        {
            juice = 1.0f;
        }

        juiceColor = new Color((juiceColor.r * cjPercent) + (color.r * pPercent), (juiceColor.g * cjPercent) + (color.g * pPercent), (juiceColor.b * cjPercent) + (color.b * pPercent));

        // NEED TO CHANGE TO VECTOR3 AND CONVERT INTO COLOR!!!!
        juiceSolid.material.color = juiceColor;
        anchor.localScale = new Vector3(anchor.localScale.x, juice, anchor.localScale.z);

        if (juice > 0.0f)
        {
            juiceSolid.enabled = true;
        }
    }

    public void RemoveJuice(float percent)
    {
        juice -= percent;

        if (juice <= 0.0f)
        {
            juiceSolid.enabled = false;
            juice = 0.0f;
        }

        anchor.localScale = new Vector3(anchor.localScale.x, juice, anchor.localScale.z);
    }

    public float GetJuice()
    {
        return juice;
    }

    public void ResetJuice() {
        juice = 0.0f;
        juiceSolid.enabled = false;
    }
}
