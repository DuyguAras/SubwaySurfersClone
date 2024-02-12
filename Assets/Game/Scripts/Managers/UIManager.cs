using CoreGames.GameName.Events.States;
using CoreGames.GameName.EventSystem;
using UnityEngine;

namespace CoreGames.GameName.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject preparePanel;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject overPanel;
        [SerializeField] private GameObject endPanel;
        
        private void OnEnable()
        {
            EventBus<GameStartEvent>.AddListener(GameStart);
            EventBus<GameNextLevelEvent>.AddListener(GameNextLevel);
            EventBus<GameRestartLevelEvent>.AddListener(GameRestartLevel);
            EventBus<GameOverEvent>.AddListener(GameOver);
            EventBus<GameEndEvent>.AddListener(GameEnd);
            EventBus<GamePrepareEvent>.AddListener(GamePrepare);
        }

        private void OnDisable()
        {
            EventBus<GameStartEvent>.RemoveListener(GameStart);
            EventBus<GameNextLevelEvent>.RemoveListener(GameNextLevel);
            EventBus<GameRestartLevelEvent>.RemoveListener(GameRestartLevel);
            EventBus<GameOverEvent>.RemoveListener(GameOver);
            EventBus<GameEndEvent>.RemoveListener(GameEnd);
            EventBus<GamePrepareEvent>.RemoveListener(GamePrepare);
        }

        private void GamePrepare(object sender, GamePrepareEvent e)
        {
            preparePanel.SetActive(true);
            gamePanel.SetActive(false);
            overPanel.SetActive(false);
            endPanel.SetActive(false);
        }

        private void GameStart(object sender, GameStartEvent e)
        {
            preparePanel.SetActive(false);
            gamePanel.SetActive(true);
        }
        
        private void GameNextLevel(object sender, GameNextLevelEvent e)
        {
            
        }
        
        private void GameRestartLevel(object sender, GameRestartLevelEvent e)
        {
            
        }
        
        private void GameOver(object sender, GameOverEvent e)
        {
            gamePanel.SetActive(false);
            overPanel.SetActive(true);
        }
        
        private void GameEnd(object sender, GameEndEvent e)
        {
            gamePanel.SetActive(false);
            endPanel.SetActive(true);
        }
    }
}
