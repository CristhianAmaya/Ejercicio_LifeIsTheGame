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

                // Verificar la interacción con objetos después de un tiempo
                StartCoroutine(DelayExplosion(newgrenadePrefab));

                // Actualizar el tiempo del último disparo
                shotRateTime = Time.time + shotRate;
            }
        }
    }

    IEnumerator DelayExplosion(GameObject newgrenadePrefab)
    {
        yield return new WaitForSeconds(timeToExplode);

        // Obtener objetos cercanos al proyectil
        Collider[] colliders = Physics.OverlapSphere(newgrenadePrefab.transform.position, radius);

        // Variable para determinar si hubo interacción con un objeto "Interactable"
        bool interacted = false;

        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("Interactable"))
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Aplicar fuerza de explosión solo si es un objeto "Interactable"
                    rb.AddExplosionForce(power, newgrenadePrefab.transform.position, radius, upForce);
                    interacted = true;
                }
            }
        }

        // Si hubo interacción, realizar la explosión
        if (interacted)
        {
            // Activar el sistema de partículas en la posición del impacto
            if (impactParticles != null)
            {
                // Instanciar el sistema de partículas
                ParticleSystem instantiatedParticles = Instantiate(impactParticles, newgrenadePrefab.transform.position, Quaternion.identity);

                // Destruir las partículas después de un tiempo determinado (ajusta según sea necesario)
                Destroy(instantiatedParticles.gameObject, 2f);
            }

            // Destruir el objeto de la granada
            Destroy(newgrenadePrefab);
        }
        else
        {
            // Si no hubo interacción, simplemente destruir la granada sin causar explosión
            Destroy(newgrenadePrefab);
        }
    }
}