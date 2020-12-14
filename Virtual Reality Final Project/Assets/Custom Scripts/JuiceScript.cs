using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceScript : MonoBehaviour
{

    private Color juiceColor;
    private MeshRenderer juice;
    private float currentJuice;

    // Start is called before the first frame update
    void Start()
    {
        juice = gameObject.GetComponentInChildren<MeshRenderer>();
        juiceColor = new Color(0.0f, 0.0f, 0.0f);
        currentJuice = 0.0f;
    }

    public void AddJuice(float percent, Color color)
    {
        float cjPercent = currentJuice / (currentJuice + percent);
        float pPercent = percent / (currentJuice + percent);

        // ADD SOME SORT OF SOUND EFFECT

        currentJuice += percent;

        if (currentJuice > 1.0f)
        {
            currentJuice = 1.0f;
        }

        juiceColor = new Color((juiceColor.r * cjPercent) + (color.r * pPercent), (juiceColor.g * cjPercent) + (color.g * pPercent), (juiceColor.b * cjPercent) + (color.b * pPercent));

        // NEED TO CHANGE TO VECTOR3 AND CONVERT INTO COLOR!!!!
        juice.material.color = juiceColor;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, currentJuice);

        if (currentJuice > 0.0f)
        {
            juice.enabled = true;
        }
    }

    public void RemoveJuice(float percent)
    {
        currentJuice -= percent;

        if (currentJuice <= 0.0f)
        {
            juice.enabled = false;
            currentJuice = 0.0f;
        }

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, currentJuice);
    }

    public float GetJuice()
    {
        return currentJuice;
    }

    public Color GetJuiceColor()
    {
        return juiceColor;
    }
}
