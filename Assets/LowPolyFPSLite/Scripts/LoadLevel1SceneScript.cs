using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel1SceneScript : MonoBehaviour
{
    public AudioSource backgroundMusic; // Referencia al AudioSource de la m�sica de fondo
    public GameObject panel; // Referencia al panel que quieres activar/desactivar

    void Start()
    {
        // Asegurarte de que el panel est� desactivado al inicio
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

    // M�todo para activar/desactivar el panel
    public void TogglePanel()
    {
        if (panel != null)
        {
            // Activar el panel si est� desactivado, desactivarlo si est� activado
            panel.SetActive(!panel.activeSelf);
            Debug.Log("Panel toggled: " + panel.activeSelf);
        }
    }

    // M�todo para desactivar el panel
    public void DeactivatePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
            Debug.Log("Panel deactivated.");
        }
    }

    // M�todo para cargar la escena "nivel1"
    public void LoadLevel1Scene()
    {
        ChangeScene("nivel1");
    }

    // M�todo para ganar el nivel 1
    public void WinLevel1()
    {
        Debug.Log("Level 1 won!");
        ChangeScene("nivel1"); // Cambia a la escena de victoria para el nivel 1
    }

    // M�todo para ganar el nivel 2
    public void WinLevel2()
    {
        Debug.Log("Level 2 won!");
        ChangeScene("nivel2"); // Cambia a la escena de victoria para el nivel 2
    }

    // M�todo para regresar al men� de inicio
    public void MenuInicio()
    {
        ChangeScene("menuinicio"); // Cambia a la escena de men� de inicio
    }

    // M�todo general para cambiar de escena
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

    // M�todo para cargar la escena inicial
    public void LoadInitialScene()
    {
        ChangeScene("InitialScene"); // Asumiendo que la escena inicial tiene un nombre espec�fico
    }

    // M�todo para salir del juego
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
