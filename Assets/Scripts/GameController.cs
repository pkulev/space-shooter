using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public int hazardCount;
    public Vector3 spawnValues;

    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text wavesText;
    public Text restartText;
    public GameObject restartButton;
    public Text gameOverText;

    private bool gameOver = false;
    private int score = 0;
    private int wave = 0;

    void Start() {
        restartText.text = "";
        restartText.gameObject.SetActive(false);
        restartButton.SetActive(false);
        gameOverText.text = "";
        UpdateScore();
        UpdateWaves();
        StartCoroutine(SpawnWaves());
    }

    //void Update() {
    //    if (gameOver && Input.GetKeyDown(KeyCode.R)) {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    }
    //}

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);

        while (true) {
            for (int i = 0; i < hazardCount; i++) {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x),
                                                    spawnValues.y,
                                                    spawnValues.z);

                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver) {
                //restartText.text = "Press any key to restart";
                //restartButton.SetActive(true);
                break;
            }

            wave++;
            UpdateWaves();
        }
    }

    public void AddScore(int value) {
        score += value;
        UpdateScore();
    }

    public void GameOver() {
        gameOverText.text = "Game Over!";
        gameOver = true;
        restartButton.SetActive(true);
    }
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateScore() {
        scoreText.text = string.Format("Score: {0}", score);
    }

    void UpdateWaves() {
        wavesText.text = string.Format("Waves: {0}", wave);
    }
}
