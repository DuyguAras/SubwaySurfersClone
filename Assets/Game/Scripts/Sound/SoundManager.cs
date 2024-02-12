using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace CoreGames.GameName.Sound
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance { get; private set; }

        private EventInstance testEventInstance;
        
        private void Awake()
        {
            if (instance != null)
                Debug.LogError("Found more than one Audio Manager in the scene");
            
            instance = this;
        }
        
        /// <summary>
        /// The instance definition for runtime is done here.
        /// For the parametric sound to work.
        /// DO NOT TOUCH THIS!
        /// </summary>
        /// <param name="eventReference"></param>
        /// <returns></returns>
        public EventInstance CreateInstance(EventReference eventReference)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            return eventInstance;
        }
        
        /////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// It is used when only one sound will be played.
        /// Usage (for other class):
        /// SoundManager.instance.PlayOneShot(FMODEvents.instance.<soundName>, transform.position);
        /// </summary>
        /// <param name="sound"></param>
        /// <param name="worldPos"></param>
        public void PlayOneShot(EventReference sound, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(sound, worldPos);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// Each sound needs to be initialized separately.
        /// </summary>
        /// <param name="testEventReference"></param>
        /// <returns></returns>
        public void InitializeTest(EventReference testEventReference)
        {
            testEventInstance = CreateInstance(testEventReference);
            testEventInstance.start();
        }
        
        /// <summary>
        /// First Phase:
        /// It is done by assigning the sound to a variable with initialization.
        /// InitializeTest(FMODEvents.instance.testSound);
        /// Second Phase:
        /// Usage with parameters:
        /// SoundManager.instance.SetTestParameter("parameterName", paramterValue);
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="parameterValue"></param>
        public void SetTestParameter(string paramterName, float parameterValue)
        {
            testEventInstance.setParameterByName(paramterName, parameterValue);
            testEventInstance.start();
        }
        
    }
}
