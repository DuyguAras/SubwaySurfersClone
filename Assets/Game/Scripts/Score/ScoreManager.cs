using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoreGames.GameName
{
    public class ScoreManager : MonoBehaviour
    {

        private float distance;
        //private float highscore;

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject startPosition;
        [SerializeField] private GameObject scoreTextObject;

        //[SerializeField] private TextMeshProUGUI highscoreText;
        void Start()
        {
            scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        }
        void Update()
        {
            AddScore();
        }

        private void AddScore()
        {
            distance = (startPosition.transform.position.z + this.transform.position.z);
            scoreText.text = distance.ToString("Score: " + "0");
        }
    }
}
