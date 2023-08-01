using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
    public static class SceneTransition
    {
        public static void Initiate(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public static int GetCurrentSceneIndex() => SceneManager.GetActiveScene().buildIndex;
    }
}
