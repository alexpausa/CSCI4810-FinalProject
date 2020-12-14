using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private float velocity;
    private Vector3 prevPos;

    // Update is called once per framdde
    void LateUpdate()
    {
        velocity = ((transform.position - prevPos).magnitude) / Time.deltaTime;
        prevPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Checks to see if the weapon hit the fruit
        Debug.Log(velocity);

        if (collision.gameObject.tag == "Fruit")
        {
            FruitScript fruit = collision.gameObject.GetComponentInParent(typeof(FruitScript)) as FruitScript;

            // Add velocity requirement later
            if (fruit != null) {
                if (!fruit.IsDead())
                {
                    GetComponent<AudioSource>().Play();
                }
                fruit.killFruit();                
            }
        }
    }
}
