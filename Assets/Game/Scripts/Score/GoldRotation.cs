using System;
using UnityEngine;
using DG.Tweening;
using CoreGames.GameName.Events.States;
using CoreGames.GameName.EventSystem;

namespace CoreGames.GameName
{
    public class GoldRotation : MonoBehaviour
    {
        private void Start()
        {
            transform.DORotate(new Vector3(0, 180, 0), 1f).SetLoops(-1, LoopType.Incremental);
        }

    }
}
