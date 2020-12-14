using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMenu : MonoBehaviour
{
    public GameObject player;

    public void PlayGame()
    {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        //Destroy(player);
    }
}
