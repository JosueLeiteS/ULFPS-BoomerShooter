using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;

    void Update()
    {
        // Aquí puedes agregar cualquier lógica adicional que necesites para el juego
    }

    public void PauseGame()
    {
        Time.timeScale = 0; // Esto pausa el tiempo en el juego
        isPaused = true;

        // Aquí puedes realizar otras acciones cuando el juego esté en pausa
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Esto reanuda el tiempo en el juego
        isPaused = false;

        // Aquí puedes realizar otras acciones cuando el juego se reanude
    }
}
