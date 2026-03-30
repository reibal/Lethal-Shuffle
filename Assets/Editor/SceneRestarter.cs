using UnityEditor;
using UnityEngine.SceneManagement;

public class SceneRestarter
{
    [MenuItem("Helpers/Restart Scene ^R")]
    private static void RestartScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
