using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CoreGames.GameName
{
    public class GoldRotation : MonoBehaviour
    {
        void Start()
        {
            transform.DORotate(new Vector3(0, 180, 0), 1f).SetLoops(-1, LoopType.Incremental);
        }

        void Update()
        {
            
        }
    }
}
