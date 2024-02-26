using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoreGames.GameName
{
    public class ScoreManager : MonoBehaviour
    {

        private float distance;
        private float highscore;

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highscoreText;
        [SerializeField] private GameObject startPosition;
        [SerializeField] private GameObject scoreTextObject;
        [SerializeField] private GameObject highscoreTextObject;

        
        void Start()
        {
            scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
            highscoreText = highscoreTextObject.GetComponent <TextMeshProUGUI>();
            highscore = PlayerPrefs.GetInt("Highscore: ", 0);
            
        }
        void Update()
        {
            AddScore();
        }

        private void AddScore()
        {
            distance = (startPosition.transform.position.z + this.transform.position.z);
            scoreText.text = distance.ToString("Score: " + "0");

            AddHighscore((int)distance);
        }

        private void AddHighscore(int score)
        {
            if (score > highscore)
            {
                highscore = score;
                highscoreText.text = "Highscore: " + highscore.ToString();
                PlayerPrefs.GetInt("Highscore", 0);
            }
        }
    }
}
