using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    // Valores que deseas compartir entre escenas
    public int valorCompartido;

    public void Awake()
    {
        // Hacer que este objeto persista entre escenas
        DontDestroyOnLoad(gameObject);
    }
}
