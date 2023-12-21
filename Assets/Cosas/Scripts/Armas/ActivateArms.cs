using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateArms : MonoBehaviour
{
    private TakeWeapon takeArming;
    public int numberArms;
    private bool canInteract = false;

    // Start is called before the first frame update
    void Start()
    {
        takeArming = GameObject.FindGameObjectWithTag("Player").GetComponent<TakeWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        // Verificar si el jugador puede interactuar y presiona la tecla "E"
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            takeArming.ActivateWeapons(numberArms);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Permitir al jugador interactuar
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            // Desactivar la capacidad de interactuar al salir del área
            canInteract = false;
        }
    }



    /*
    public TakeWeapon takeArming;
    public int numberArms;
    // Start is called before the first frame update
    void Start()
    {
        takeArming = GameObject.FindGameObjectWithTag("Player").GetComponent<takeArming>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            takeArming.ActivateWeapons(numberArms);
        }
    }*/
}
