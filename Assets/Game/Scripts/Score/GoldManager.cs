using System.Collections.Generic;
using CoreGames.GameName.Events.States;
using CoreGames.GameName.EventSystem;
using UnityEngine;

namespace CoreGames.GameName
{
    public class GoldManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> goldList;
        
        private void OnEnable()
        {
            EventBus<GamePrepareEvent>.AddListener(GameStart);
        }
        private void OnDisable()
        {
            EventBus<GamePrepareEvent>.RemoveListener(GameStart);
        }

        private void GameStart(object sender, GamePrepareEvent e)
        {
            ResetGolds();
        }

        public void ResetGolds()
        {
            foreach (GameObject obj in goldList)
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}
