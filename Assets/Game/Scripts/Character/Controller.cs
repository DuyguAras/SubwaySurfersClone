using UnityEngine;
using System;
using CoreGames.GameName.Events.States;
using CoreGames.GameName.EventSystem;
using CoreGames.GameName.Managers;

[Serializable]
public enum Side { Left, Mid, Right }
namespace CoreGames.GameName
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private float zForwardSpeed;

        private Side side = Side.Mid;

        private bool isSwipeLeft, isSwipeRight, isSwipeUp, isSwipeDown;

        private CharacterController characterController;
        private Animator animator;

        [SerializeField] private float slideValue = 4f;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float jumpPower = 6f;
        private float newPosition = 0f;
        private float yValue;
        private float moveSpeed;
        private float colliderHeight;
        private float colliderCenter;

        private int healthCounter;
        private float initialColliderSize;

        private void OnEnable()
        {
            EventBus<GameStartEvent>.AddListener(StartGame);
            EventBus<GamePrepareEvent>.AddListener(PrepareGame);
        }

        private void OnDisable()
        {
            EventBus<GameStartEvent>.RemoveListener(StartGame);
            EventBus<GamePrepareEvent>.RemoveListener(PrepareGame);
        }

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            colliderHeight = characterController.height;
            colliderCenter = characterController.center.y;
            animator = GetComponent<Animator>();
            transform.position = Vector3.zero;
            initialColliderSize = characterController.height;
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
                    newPosition = -slideValue;
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
                    newPosition = slideValue;
                    side = Side.Right;
                }
                else if (side == Side.Left)
                {
                    newPosition = 0f;
                    side = Side.Mid;
                }
            }

            if (GameStateManager.Instance.GetGameState() == GameStateManager.GameState.Start)
            {
                Vector3 moveVector = new Vector3(moveSpeed - transform.position.x, yValue * Time.deltaTime, zForwardSpeed * Time.deltaTime);
                moveSpeed = Mathf.Lerp(moveSpeed, newPosition, Time.deltaTime * 10f);
                characterController.Move(moveVector);
            }

            if (zForwardSpeed < maxSpeed)
            {
                zForwardSpeed += 0.05f * Time.deltaTime;
            }

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
                characterController.center = new Vector3(0, colliderCenter / 2f, 0);
                characterController.height = colliderHeight / 2f;
                animator.CrossFadeInFixedTime("Roll", 0.1f);
                Invoke(nameof(SizeOfColliderDelay), 0.75f);
            }
        }

        private void SizeOfColliderDelay()
        {
            characterController.center = new Vector3(0, colliderCenter, 0);
            characterController.height = initialColliderSize;
        }

        private void StartGame(object sender, GameStartEvent e)
        {
            animator.SetBool("isGameStarted", true);
        }

        private void PrepareGame(object sender, GamePrepareEvent e)
        {
            animator.SetBool("isGameStarted", false);
            newPosition = 0f;
            side = Side.Mid;
        }
    }
}