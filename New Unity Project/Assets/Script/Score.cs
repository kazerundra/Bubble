using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int currentScore = 0;
    public void AddScore(int Score)
    {
        currentScore += Score;
        scoreText.text = currentScore.ToString();
    }

}
