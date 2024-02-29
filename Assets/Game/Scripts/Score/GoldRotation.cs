using UnityEngine;
using DG.Tweening;
using CoreGames.GameName.Events.States;
using CoreGames.GameName.EventSystem;

namespace CoreGames.GameName
{
    public class GoldRotation : MonoBehaviour
    {
        private void OnEnable()
        {
            EventBus<GameStartEvent>.AddListener(GameStart);
        }
        private void OnDisable()
        {
            EventBus<GameStartEvent>.RemoveListener(GameStart);
        }

        private void GameStart(object sender, GameStartEvent e)
        {
            transform.DORotate(new Vector3(0, 180, 0), 1f).SetLoops(-1, LoopType.Incremental);
        }
    }
}
