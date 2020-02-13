using Trisibo;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public SceneField nextScene;

    public void LoadNextScene() => LoadScene(nextScene);

    public void LoadOptionsScene() => SceneManager.LoadScene("Options");

    public void LoadHighScoresScene() => SceneManager.LoadScene("HighScores");

    public void LoadExperimentalScene() => SceneManager.LoadScene("Experimental");

    public void LoadScene(SceneField scene) => SceneManager.LoadScene(scene.BuildIndex);
}