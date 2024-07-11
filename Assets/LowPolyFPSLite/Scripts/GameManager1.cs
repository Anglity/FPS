using System.Collections;  // Asegúrate de incluir este espacio de nombres
using UnityEngine;
using UnityEngine.SceneManagement;  // Para cargar escenas
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Image fillBar; // Referencia a la imagen de relleno

    private int score = 0;
    private int totalDummies; // El total de puntos necesarios para llenar la barra

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Configura el total de puntos necesarios basado en la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "nivel2")
        {
            totalDummies = 9;
        }
        else
        {
            totalDummies = 8; // Valor por defecto
        }

        fillBar.fillAmount = 0; // Inicia la barra vacía
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateFillBar();

        if (score >= totalDummies)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == "nivel1")
            {
                StartCoroutine(LoadSceneAfterDelay(5, "nivel2")); // Carga la escena "level2" después de 5 segundos
            }
            else if (currentSceneName == "nivel2")
            {
                StartCoroutine(LoadSceneAfterDelay(5, "menuinicio")); // Carga la escena "menuinicio" después de 5 segundos
            }
        }
    }

    private void UpdateFillBar()
    {
        fillBar.fillAmount = (float)score / totalDummies; // Actualiza el porcentaje de la barra
    }

    private IEnumerator LoadSceneAfterDelay(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
