using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // Desbloquear y mostrar el cursor al inicio de la escena
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
