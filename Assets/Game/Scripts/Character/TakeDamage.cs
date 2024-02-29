using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreGames.GameName
{
    public class TakeDamage : MonoBehaviour
    {
        private int counter;
        private Color origionalColor;
        void Start()
        {
           origionalColor = GetComponent<Renderer>().material.color;
        }

       
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                counter++;

            }
        }
    }
}
