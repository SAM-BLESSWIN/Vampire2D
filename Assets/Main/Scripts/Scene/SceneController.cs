using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController 
{
    public static void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int loadSceneIndex = nextSceneIndex < SceneManager.sceneCountInBuildSettings ? nextSceneIndex : 0;

        SceneManager.LoadScene(loadSceneIndex);
    }

    public static void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public static string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
