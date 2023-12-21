using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateArms : MonoBehaviour
{
    private TakeWeapon takeArming;
    public int numberArms;
    // Start is called before the first frame update
    void Start()
    {
        takeArming = GameObject.FindGameObjectWithTag("Player").GetComponent<TakeWeapon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            takeArming.ActivateWeapons(numberArms);
        }
    }
}
