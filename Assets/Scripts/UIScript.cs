using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public static int score = 0;
    public int pp = 0;
    public static Text scoreText;

    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        score = 0;
    }

    public void UpScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
