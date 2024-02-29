using CoreGames.GameName.Events.States;
using CoreGames.GameName.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace CoreGames.GameName
{
    public class TakeDamage : MonoBehaviour
    {
        [SerializeField] private Renderer characterMat;
        private int counter;
        private Color originalColor;

        private bool canCrash;

        private void OnEnable()
        {
            EventBus<GamePrepareEvent>.AddListener(ResetCounter);
        }

        private void OnDisable()
        {
            EventBus<GamePrepareEvent>.RemoveListener(ResetCounter);
        }

        private void Start()
        {
            canCrash = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                if (canCrash)
                {
                    counter++;
                    canCrash = false;

                    if (counter == 3)
                    {
                        Debug.Log("Touch the obstacle");
                        EventBus<GamePrepareEvent>.Emit(this, new GamePrepareEvent());
                        transform.position = Vector3.zero;
                    }
                    else
                    {
                        Sequence colorSequence = DOTween.Sequence();

                        colorSequence.Append(characterMat.material.DOColor(Color.red, 0.5f).SetEase(Ease.InQuad));
                        colorSequence.Append(characterMat.material.DOColor(Color.white, 0.5f).SetEase(Ease.InQuad));
                        colorSequence.Append(characterMat.material.DOColor(Color.red, 0.5f).SetEase(Ease.InQuad));
                        colorSequence.Append(characterMat.material.DOColor(Color.white, 0.5f).SetEase(Ease.InQuad).OnComplete(
                            () =>
                            {
                                canCrash = true;
                            }));

                        Debug.Log("Color change");
                    }
                }
            }
        }

        private void ResetCounter(object sender, GamePrepareEvent e)
        {
            counter = 0;
            characterMat.material.color = Color.white;
            canCrash = true;
        }
    }
}

