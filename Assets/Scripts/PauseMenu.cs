using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;

    public static bool isGamePaused = false;

    private static float _prevTimeScale;

    private void Start() {
        _prevTimeScale = Time.timeScale;
    }

    public void Pause() {
        isGamePaused = true;
        StopTime();
        pauseMenu.SetActive(true);
    }

    public void Resume() {
        isGamePaused = false;
        RestoreTime();
        pauseMenu.SetActive(false);
    }

    public void MainMenu() {
        RestoreTime();
        // TODO: remove hardcode
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit() {
        Application.Quit();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isGamePaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    private void StopTime() {
        _prevTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    private void RestoreTime() {
        Time.timeScale = _prevTimeScale;
    }
}