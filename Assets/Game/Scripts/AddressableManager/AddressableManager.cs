using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace CoreGames.GameName.AddressableManager
{
    public class AddressableManager : MonoBehaviour
    {
        private bool clearPreviousScene = false;
        private SceneInstance previousLoadedScene;
      
        public void LoadAddressableLevel(string addressableKey)
        {
            if (clearPreviousScene)
            {
                Addressables.UnloadSceneAsync(previousLoadedScene).Completed += (asyncHandle) =>
                {
                    clearPreviousScene = false;
                    previousLoadedScene = new SceneInstance();
                    //Logger.Instance.LogInfo($"Unloaded scene {addressableKey} successfully ");
                };
            }

            Addressables.LoadSceneAsync(addressableKey, LoadSceneMode.Single).Completed += (asyncHandle) =>
            {
                clearPreviousScene = true;
                previousLoadedScene = asyncHandle.Result;
                //Logger.Instance.LogInfo($"Loaded scene {addressableKey} successfully ");
            };
        }
    }
}
