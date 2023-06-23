using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;

    void Update()
    {
        // Aqu� puedes agregar cualquier l�gica adicional que necesites para el juego
    }

    public void PauseGame()
    {
        Time.timeScale = 0; // Esto pausa el tiempo en el juego
        isPaused = true;

        // Aqu� puedes realizar otras acciones cuando el juego est� en pausa
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Esto reanuda el tiempo en el juego
        isPaused = false;

        // Aqu� puedes realizar otras acciones cuando el juego se reanude
    }
}
