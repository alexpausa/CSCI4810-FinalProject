using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFruit : MonoBehaviour
{
    public GameObject fruit;
    public Transform spawnerPoint;

    private bool canBePressed;

    private void Start()
    {
        canBePressed = true;
    }

    public void CreateFruit()
    {
        if (canBePressed) {
            Instantiate(fruit, spawnerPoint.position, Quaternion.identity);
            GetComponent<AudioSource>().Play();
            canBePressed = false;
            Invoke("ResetButton", 2);
        }
    }

    private void ResetButton()
    {
        canBePressed = true;
    }
}
