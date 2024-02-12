using CoreGames.GameName.Events.States;
using CoreGames.GameName.EventSystem;
using UnityEngine;

namespace CoreGames.GameName.Managers
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            EventBus<GamePrepareEvent>.AddListener(GamePrepare);
            EventBus<GameStartEvent>.AddListener(GameStart);
            EventBus<GameNextLevelEvent>.AddListener(GameNextLevel);
            EventBus<GameRestartLevelEvent>.AddListener(GameRestartLevel);
            EventBus<GameOverEvent>.AddListener(GameOver);
            EventBus<GameEndEvent>.AddListener(GameEnd);
        }

        private void OnDisable()
        {
            EventBus<GamePrepareEvent>.RemoveListener(GamePrepare);
            EventBus<GameStartEvent>.RemoveListener(GameStart);
            EventBus<GameNextLevelEvent>.RemoveListener(GameNextLevel);
            EventBus<GameRestartLevelEvent>.RemoveListener(GameRestartLevel);
            EventBus<GameOverEvent>.RemoveListener(GameOver);
            EventBus<GameEndEvent>.RemoveListener(GameEnd);
        }

        private void Start()
        {
            EventBus<GamePrepareEvent>.Emit(this, new GamePrepareEvent());
        }

        private void GamePrepare(object sender, GamePrepareEvent e)
        {
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.Prepare);
        }

        private void GameStart(object sender, GameStartEvent e)
        {
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.Start);
        }

        private void GameNextLevel(object sender, GameNextLevelEvent e)
        {
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.NextLevel);
            EventBus<GamePrepareEvent>.Emit(this, new GamePrepareEvent());
        }

        private void GameRestartLevel(object sender, GameRestartLevelEvent e)
        {
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.RestartLevel);
            EventBus<GamePrepareEvent>.Emit(this, new GamePrepareEvent());
        }

        private void GameOver(object sender, GameOverEvent e)
        {
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.Over);
        }

        private void GameEnd(object sender, GameEndEvent e)
        {
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.End);
        }

        //Called by button on click event
        public void CallStartEvent()
        {
            EventBus<GameStartEvent>.Emit(this, new GameStartEvent());
        }

        //Called by button on click event
        public void CallRestartEvent()
        {
            //FIRST FOR RESTART CODES
            EventBus<GameRestartLevelEvent>.Emit(this, new GameRestartLevelEvent());
        }

        //Called by button on click event
        public void CallNextLevelEvent()
        {
            //FIRST FOR NEXT LEVEL CODES
            EventBus<GameNextLevelEvent>.Emit(this, new GameNextLevelEvent());
        }
    }
}