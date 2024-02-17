using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]

public enum Side {Left, Mid, Right}
namespace CoreGames.GameName
{
    public class Controller : MonoBehaviour
    {
        private Side side = Side.Mid;

        private bool isSwipeLeft, isSwipeRight, isSwipeUp, isSwipeDown;

        private CharacterController characterController;
        private Animator animator;

        private float newPosition = 0f;
        private float xValue = 4;
        private float yValue;
        private float moveSpeed;
        private float jumpPower = 4f;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            transform.position = Vector3.zero;
        }

        void Update()
        {
            Movement();
        }

        private void Movement()
        {
            isSwipeLeft = Input.GetKeyDown(KeyCode.A);
            isSwipeRight = Input.GetKeyDown(KeyCode.D);
            isSwipeUp = Input.GetKeyDown(KeyCode.W);

            if (isSwipeLeft)
            {
                if (side == Side.Mid)
                {
                    newPosition = -xValue;
                    side = Side.Left;
                }
                else if (side == Side.Right)
                {
                    newPosition = 0f;
                    side = Side.Mid;
                }
            }
            else if (isSwipeRight)
            {
                if (side == Side.Mid)
                {
                    newPosition = xValue;
                    side = Side.Right;
                }
                else if (side == Side.Left)
                {
                    newPosition = 0f;
                    side = Side.Mid;
                }
            }

            Vector3 moveVector = new Vector3(moveSpeed - transform.position.x, yValue * Time.deltaTime, 0);
            moveSpeed = Mathf.Lerp(moveSpeed, newPosition, Time.deltaTime * 10f);
            characterController.Move(moveVector);

            Jumping();
        }

        private void Jumping()
        {
            

            if (characterController.isGrounded)
            {
                if (isSwipeUp)
                {
                    yValue = jumpPower;
                    animator.CrossFadeInFixedTime("Jump", 0.1f);
                }
            }
            else
            {
                yValue -= jumpPower * 2 * Time.deltaTime;
            }
        }
    }
}
