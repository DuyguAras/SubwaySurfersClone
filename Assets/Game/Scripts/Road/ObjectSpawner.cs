using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreGames.GameName
{
    public class ObjectSpawner : MonoBehaviour
    {
        private bool spawningObject = false;

        [SerializeField] private float groundSpawnDistance = 50f;

        public static ObjectSpawner instance;

        private void Awake()
        {
            instance = this;
        }

        public void SpawnGround()
        {
            ObjectPooler.instance.SpawnFromPool("Ground", new Vector3(-6f, 0, 50f), Quaternion.identity);
        }
    }
}
