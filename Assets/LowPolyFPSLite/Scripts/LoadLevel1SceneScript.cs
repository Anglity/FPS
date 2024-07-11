using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel1SceneScript : MonoBehaviour
{
    public AudioSource backgroundMusic; // Referencia al AudioSource de la música de fondo
    public GameObject panel; // Referencia al panel que quieres activar/desactivar

    void Start()
    {
        // Asegurarte de que el panel está desactivado al inicio
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc key was pressed.");
            LoadInitialScene();
        }
    }

    // Método para activar/desactivar el panel
    public void TogglePanel()
    {
        if (panel != null)
        {
            // Activar el panel si está desactivado, desactivarlo si está activado
            panel.SetActive(!panel.activeSelf);
            Debug.Log("Panel toggled: " + panel.activeSelf);
        }
    }

    // Método para desactivar el panel
    public void DeactivatePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
            Debug.Log("Panel deactivated.");
        }
    }

    // Método para cargar la escena "nivel1"
    public void LoadLevel1Scene()
    {
        ChangeScene("nivel1");
    }

    // Método para ganar el nivel 1
    public void WinLevel1()
    {
        Debug.Log("Level 1 won!");
        ChangeScene("nivel1"); // Cambia a la escena de victoria para el nivel 1
    }

    // Método para ganar el nivel 2
    public void WinLevel2()
    {
        Debug.Log("Level 2 won!");
        ChangeScene("nivel2"); // Cambia a la escena de victoria para el nivel 2
    }

    // Método para regresar al menú de inicio
    public void MenuInicio()
    {
        ChangeScene("menuinicio"); // Cambia a la escena de menú de inicio
    }

    // Método general para cambiar de escena
    private void ChangeScene(string sceneName)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(sceneName);
    }

    // Método para cargar la escena inicial
    public void LoadInitialScene()
    {
        ChangeScene("InitialScene"); // Asumiendo que la escena inicial tiene un nombre específico
    }

    // Método para salir del juego
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
