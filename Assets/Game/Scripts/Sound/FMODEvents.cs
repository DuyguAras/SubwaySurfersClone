using FMODUnity;
using UnityEngine;

namespace CoreGames.GameName.Sound
{
    public class FMODEvents : MonoBehaviour
    {
        // Specify the category of the sound.
        //[field: Header("ENV")]
        
        // Define the sound in a variable.
        [field: SerializeField] public EventReference testSound { get; private set; }

        #region MyRegion
        public static FMODEvents instance { get; private set; }
        private void Awake()
        {
            if (instance != null)
                Debug.LogError("Found more than one FMOD Events instance in the scene");

            instance = this;
        }
        #endregion
    }
}