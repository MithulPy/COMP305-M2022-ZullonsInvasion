using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static bool playGame = true;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void Update()
    {
        scoreText.text = "Score:" + score.ToString();
    }
}
