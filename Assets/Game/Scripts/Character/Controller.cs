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
        private float xValue = 5;
        private float yValue;
        private float moveSpeed;
        private float forwardMove = 5f;
        private float jumpPower = 4f;
        private float colliderHeight;
        private float colliderCenter;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            colliderHeight = characterController.height;
            colliderCenter = characterController.center.y;
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
            isSwipeDown = Input.GetKeyDown(KeyCode.S);

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
            Sliding();
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

        private void Sliding()
        {
            if (isSwipeDown)
            {
                yValue -= 10f;
                characterController.center = new Vector3(0, colliderCenter/2f, 0);
                characterController.height = colliderHeight / 2f;
                animator.CrossFadeInFixedTime("Slide", 0.1f);
            }
        }
    }

}
