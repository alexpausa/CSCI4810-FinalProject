using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVScript : MonoBehaviour
{
    public Text moneyText;
    public Text goalText;
    public Text bigText;
    public Text timeText;

    public CheckoutScript check;

    private CupScript cup;
    private float goal;
    private float money;
    private int day;
    private float timeRemaining;

    private bool showBigText;
    private float timedPause;

    private bool win;
    private bool lose;

    public AudioClip winNoise;
    public AudioClip moneyNoise;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        win = false;
        lose = false;
        goal = 100.0f;
        money = 0.0f;
        timeRemaining = 240.0f;
        timedPause = 0.0f;
        day = 1;
        showBigText = false;

        DisplayDay();
    }

    // Update is called once per frame
    void Update()
    {
        if (!showBigText) {
            cup = check.cup;

            if (cup != null)
            {
                SellJuice();
            }

            timeRemaining -= Time.deltaTime;

            timeText.text = "Day " + day + "   Time: " + timeRemaining.ToString("F0") + "s";
            moneyText.text = "Money: $" + money.ToString("F2");
            goalText.text = "Goal: $" + goal.ToString("F2");

            CheckWin();
        }
        else
        {
            timedPause += Time.deltaTime;

            if (timedPause > 3.0f && !win)
            {
                timedPause = 0.0f;
                bigText.text = "";
                showBigText = false;
            }
            else if (lose && timedPause > 5.0f)
            {
                timedPause = 0.0f;
                DisplayDay();
                lose = false;
            }
        }
    }

    private void SellJuice()
    {
        // Maybe Add Conditions here and reject the cup if the wrong type of juice (or subtract money)
        money += cup.GetJuice() * 25.0f;

        if(cup.GetJuice() > 0.0f)
        {
            audioSource.clip = moneyNoise;
            audioSource.Play();
        }

        cup.ResetJuice();
    }

    // If won change the day and make the goal harder with same alotted time to make the juice
    // If you win Day 5 say that you finished the game or something
    // Also check if you lost to display you lost and start at goal 100 and reset to day 1.
    private void CheckWin()
    {
        if (timeRemaining <= 0.0f && money < goal)
        {
            YouLose();
        }
        else if (money >= goal)
        {
            audioSource.clip = winNoise;
            audioSource.Play();

            day += 1;
            goal += 10.0f;
            money = 0.0f;
            timeRemaining = 240.0f;

            DisplayDay();

            // You win if you get past day 5 and it displays the winning screen on the TV.
            if (day > 5)
            {
                win = true;

                timeText.text = " ";
                moneyText.text = " ";
                goalText.text = " ";
                bigText.text = "YOU WIN";
            }
        }
    }

    // Shows losing screen and resets Day and 
    private void YouLose()
    {
        showBigText = true;
        lose = true;
        day = 1;
        goal = 100.0f;
        money = 0.0f;

        timeText.text = " ";
        moneyText.text = " ";
        goalText.text = " ";
        bigText.text = "YOU LOSE";
    }

    private void DisplayDay() {
        showBigText = true;

        timeText.text = " ";
        moneyText.text = " ";
        goalText.text = " ";
        bigText.text = "Day " + day;
    }
}
