using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreGames.GameName
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float objectDistance = -40f;
        [SerializeField] private float despawnDistance = -110f;

        private bool canSpawnGround = true;
        private Rigidbody rigidbody;
        void Start()
        {
           rigidbody = GetComponent<Rigidbody>();
        }

        
        void Update()
        {
            transform.position += -transform.forward * speed * Time.deltaTime;

            if (transform.position.z <= objectDistance && transform.CompareTag("Ground") && canSpawnGround)
            {
                ObjectSpawner.instance.SpawnGround();
                canSpawnGround = false;
            }

            if (transform.position.z <= despawnDistance)
            {
                canSpawnGround = true;
                gameObject.SetActive(false);
            }
        }
    }
}
