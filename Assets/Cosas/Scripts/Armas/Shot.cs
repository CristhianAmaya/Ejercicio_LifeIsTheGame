using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform spawnPoint; // Punto de Spawn de los proyectiles
    public GameObject bullet; // Objeto proyectil
    public float shotForce = 1500f; // Fuerza de disparo
    public float shotRate = 0.5f; // Tiempo de espera en cada disparo
    private float shotRateTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Time.time > shotRateTime)
            {
                GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

                shotRateTime = Time.time + shotRate;

                Destroy(newBullet, 5);
            }
        }
    }
}
