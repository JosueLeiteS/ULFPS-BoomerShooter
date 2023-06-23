using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Slider = UnityEngine.UI.Slider;

public class WaveConfiguration : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameManager gameManager;

    public Slider sliderMin;
    public TextMeshProUGUI textMeshProMin;

    public Slider sliderMax;
    public TextMeshProUGUI textMeshProMax;

    public WaveController waveController;

    public GameObject button;

    void Update()
    {
        textMeshProMin.text = sliderMin.value.ToString();
        textMeshProMax.text = sliderMax.value.ToString();
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameManager.isPaused && sliderMin.value <= sliderMax.value)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (sliderMin.value <= sliderMax.value)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameManager.PauseGame();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameManager.ResumeGame();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        waveController.enemiesPerRoundMin = (int)sliderMin.value;
        waveController.enemiesPerRoundMax = (int)sliderMax.value;
    }
}
