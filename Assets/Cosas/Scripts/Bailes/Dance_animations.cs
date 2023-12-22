using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dance_animations : MonoBehaviour
{
    // Variables para guardar los menus canvas
    public GameObject menu1, menu2;
    // Referencia al componente Animator del GameObject
    private Animator animate;

    // Define los nombres de las animaciones en un array
    private string[] animationNames = { "House_Dance", "Macarena_Dance", "Hip Hop_Dance" };

    public AudioClip[] clipsAudio;
    private AudioSource audioSource;

    // GameManager para compartir valores entre escenas
    private Game_Manager gameManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Obtener el componente Animator del GameObject actual
        animate = gameObject.GetComponent<Animator>();

        // Obtener el GameManager donde se podrán compartir los valores a un script de otra escena
        gameManager = FindObjectOfType<Game_Manager>();
        if (gameManager == null)
        {
            GameObject gameManagerObject = new GameObject("Game_Manager");
            gameManager = gameManagerObject.AddComponent<Game_Manager>();
        }
    }

    // Método para iniciar animaciones según el índice proporcionado por los botones
    public void animation_start(int anime)
    {
        // Verifica que el índice proporcionado esté dentro del rango del array
        if (anime >= 0 && anime < animationNames.Length) // Rango entre 0 y 2
        {
            // Establece el trigger de la animación usando el nombre del array
            animate.SetTrigger(animationNames[anime]);

            // Asigna el clip al AudioSource y reproduce
            if (anime < clipsAudio.Length)
            {
                audioSource.clip = clipsAudio[anime];
                audioSource.Play();
            }
            else
            {
                Debug.LogError("Índice de audio fuera de rango");
            }
        }
        else
        {
            Debug.LogError("Valor inexistente");
        }
        menu2.SetActive(false);
    }

    // Método funcional para el segundo menú
    public void animation_start2(int valor)
    {
        // Establecer el valor compartido
        if (gameManager != null)
        {
            SceneManager.LoadScene("Juego");
            gameManager.valorCompartido = valor;
        }
        else
        {
            Debug.LogError("Game_Manager no encontrado.");
        }
    }

    // Método para abrir el segundo menú canvas del 4to botón
    public void dance_select()
    {
        // Activa y desactiva el menú 1 y 2 
        menu1.SetActive(false);
        menu2.SetActive(true);
    }
}