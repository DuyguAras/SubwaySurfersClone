using CoreGames.GameName.Events.States;
using CoreGames.GameName.EventSystem;
using TMPro;
using UnityEngine;

namespace CoreGames.GameName
{
    public class ScoreManager : MonoBehaviour
    {
        private float distance;
        private float highscore;

        [SerializeField] private int goldScore;

        private bool isHighscored = false;

        private Color mainHighscoreColor;

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highscoreText;
        [SerializeField] private GameObject startPosition;
        [SerializeField] private GameObject scoreTextObject;
        [SerializeField] private GameObject highscoreTextObject;

        private void OnEnable()
        {
            EventBus<GamePrepareEvent>.AddListener(PrepareGame);
        }
        private void OnDisable()
        {
            EventBus<GamePrepareEvent>.RemoveListener(PrepareGame);
        }

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

                if (!isHighscored)
                {
                    isHighscored = true;
                    highscoreText.color = Color.red;
                }
            }
        }

        private void PrepareGame(object sender, GamePrepareEvent e)
        {
            isHighscored = false;

            mainHighscoreColor = Color.black;
            highscoreText.color = mainHighscoreColor;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.tag == "Collectable")
            {
                distance += goldScore;
                Destroy(hit.gameObject);
                Debug.Log("gold is collected " + goldScore);
            }
        }
    }
}
