using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dance_Selectionator : MonoBehaviour
{
    private Animator animate;
    private Game_Manager gameManage;
    private int valorRecuperado;
    private string[] animationNames = { "House_Dance", "Macarena_Dance", "Hip Hop_Dance" };

    void Start()
    {
        // Obtener el componente Animator del GameObject actual
        animate = gameObject.GetComponent<Animator>();

        // Buscar el GameManager existente en la escena
        Game_Manager gameManager = FindObjectOfType<Game_Manager>();

        if (gameManager != null)
        {
            // Obtener el valor compartido del GameManager
            valorRecuperado = gameManager.valorCompartido;
            // Iniciar la animación con el valor recuperado
            animation_starter(valorRecuperado);
            Debug.Log("Valor recuperado: " + valorRecuperado);
        }
        else
        {
            Debug.LogError("No se encontró el GameManager.");
        }
    }

    public void animation_starter(int anime)
    {
        if (anime >= 0 && anime < animationNames.Length)
        {
            // Establecer el trigger de la animación usando el nombre del array
            animate.SetTrigger(animationNames[anime]);

            // Comenzar la repetición de la animación mediante un método invocado repetidamente
            InvokeRepeating("RestartAnimation", 0f, animate.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            Debug.LogError("Valor inexistente");
        }
    }

    // Método para reiniciar la animación
    private void RestartAnimation()
    {
        // Establecer el trigger de la animación usando el valor recuperado
        animate.SetTrigger(animationNames[valorRecuperado]);
    }
}
