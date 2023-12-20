using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dance_animations : MonoBehaviour
{
    // Variables para guardar los menus canvas
    public GameObject menu1, menu2, menu3;
    // Referencia al componente Animator del GameObject
    private Animator animate;

    // Define los nombres de las animaciones en un array
    private string[] animationNames = { "House_Dance", "Macarena_Dance", "Hip Hop_Dance" };

    void Start()
    {
        // Obtener el componente Animator del GameObject actual
        animate = gameObject.GetComponent<Animator>();
    }

    // Método para iniciar animaciones según el índice proporcionado por los botones
    public void animation_start(int anime)
    {
        // Verifica que el índice proporcionado esté dentro del rango del array
        if (anime >= 0 && anime < animationNames.Length) // Rango entre 0 y 2
        {
            // Establece el trigger de la animación usando el nombre del array
            animate.SetTrigger(animationNames[anime]);
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
        // Se le envía el valor a la primera función animation_Start
        animation_start(valor);
        // Se activa el menú 3
        menu3.SetActive(true);
    }

    // Método para abrir el segundo menú canvas del 4to botón
    public void dance_select()
    {
        // Activa y desactiva el menú 1 y 2 
        menu1.SetActive(false);
        menu2.SetActive(true);
    }

    // Método para cambiar de escena
    public void change_Scenes()
    {
        SceneManager.LoadScene("Juego");
    }
}