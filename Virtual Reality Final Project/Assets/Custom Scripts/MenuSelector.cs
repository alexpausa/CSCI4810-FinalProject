using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour
{
    public LineRenderer laserLine;
    public float laserWidth = 0.1f;
    public Transform rightHand;
    public GameObject player;

    private GameObject selection;
    private GameObject prevSelection;

    private float hold;

    // Start is called before the first frame update
    void Start()
    {
        laserLine.SetWidth(laserWidth, laserWidth);
        hold = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(rightHand.position, rightHand.forward);
        RaycastHit hit;

        Vector3 endPosition = rightHand.position + (rightHand.forward * 20);

        if (Physics.Raycast( ray, out hit))
        {
            selection = hit.collider.gameObject;
        }

        CheckCollision();

        prevSelection = selection;

        laserLine.SetPosition(0, rightHand.position);
        laserLine.SetPosition(1, endPosition);
    }

    private void CheckCollision()
    {
        if (selection == null || prevSelection == null) { return; }

        if (prevSelection.name == selection.name && selection.tag == "Menu")
        {
            hold += Time.deltaTime;
            if (selection.name == "Play" && hold >= 3.0f)
            {
                PlayGame();
            }
        }
        else
        {
            hold = 0.0f;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        Destroy(player);
    }
}
