using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeShot : MonoBehaviour
{
    public GameObject grenadePrefab; // Objeto proyectil
    public Transform spawnPoint; // Punto de Spawn de los proyectiles
    public float shotForce = 1500f; // Fuerza de disparo
    public float shotRate = 0.5f; // Tiempo de espera en cada disparo
    private float shotRateTime = 0f; // Contador
    public ParticleSystem impactParticles; // Sistema de partículas para impacto

    public float timeToExplode = 1.0f;
    public float power = 1000.0f;
    public float radius = 8.0f;
    public float upForce = 2.0f;

    // Update is called once per frame
    void Update()
    {
        // Verificar si se presionó el botón izquierdo del mouse
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Verificar si ha pasado suficiente tiempo desde el último disparo
            if (Time.time > shotRateTime)
            {
                // Instanciar un nuevo proyectil en el spawnPoint
                GameObject newgrenadePrefab = Instantiate(grenadePrefab, spawnPoint.position, spawnPoint.rotation);
                Rigidbody newgrenadePrefabRb = newgrenadePrefab.GetComponent<Rigidbody>();
                
                // Aplicar una fuerza al proyectil en la dirección hacia adelante
                newgrenadePrefabRb.AddForce(spawnPoint.forward * shotForce);

                // Iniciar la corrutina para interactuar con objetos después de un tiempo
                StartCoroutine(DelayExplosion(newgrenadePrefab));

                // Actualizar el tiempo del último disparo
                shotRateTime = Time.time + shotRate;
            }
        }
    }

    IEnumerator DelayExplosion(GameObject newgrenadePrefab)
    {
        yield return new WaitForSeconds(timeToExplode);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(power, transform.position, radius, upForce);
            }
        }
        Destroy(newgrenadePrefab);
    }
}
