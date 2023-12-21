using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaShot : MonoBehaviour
{
    public Transform spawnPoint; // Punto de Spawn de los proyectiles
    public GameObject bullet; // Objeto proyectil
    public ParticleSystem impactParticles; // Sistema de partículas para impacto
    public float shotForce = 1500f; // Fuerza de disparo
    public float shotRate = 0.5f; // Tiempo de espera en cada disparo
    public float liftForce = 500f; // Fuerza para levantar objetos
    public string interactableTag = "Interactable"; // Tag del objeto interactuable

    private float shotRateTime = 0f;

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
                GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                Rigidbody newBulletRb = newBullet.GetComponent<Rigidbody>();
                
                // Aplicar una fuerza al proyectil en la dirección hacia adelante
                newBulletRb.AddForce(spawnPoint.forward * shotForce);

                // Iniciar la corrutina para interactuar con objetos después de un tiempo
                StartCoroutine(InteractWithObjects(newBullet));

                // Actualizar el tiempo del último disparo
                shotRateTime = Time.time + shotRate;
            }
        }
    }

    // Corrutina para interactuar con objetos después de un tiempo
    IEnumerator InteractWithObjects(GameObject newBullet)
    {
        // Esperar 0.5 segundos
        yield return new WaitForSeconds(0.5f);

        // Encontrar objetos cercanos al proyectil
        Collider[] colliders = Physics.OverlapSphere(newBullet.transform.position, 5f);

        // Iterar sobre los objetos cercanos
        foreach (Collider collider in colliders)
        {
            // Verificar si el objeto tiene la etiqueta "Interactable"
            if (collider.CompareTag(interactableTag))
            {
                // Obtener el Rigidbody del objeto
                Rigidbody rb = collider.GetComponent<Rigidbody>();

                // Verificar si el objeto tiene un Rigidbody
                if (rb != null)
                {
                    // Aplicar una fuerza hacia arriba al objeto
                    rb.AddForce(Vector3.up * liftForce);

                    // Desactivar la gravedad del objeto para que se mantenga levitando
                    rb.useGravity = false;

                    // Desactivar la detección de colisiones para evitar problemas con otros objetos
                    rb.detectCollisions = false;

                    // Iniciar la corrutina para reactivar la gravedad y detección de colisiones después de 5 segundos
                    StartCoroutine(ReactivateGravityAndCollisions(rb, 5f));
                }

                // Activar el sistema de partículas en la posición del impacto
                if (impactParticles != null)
                {
                    // Instanciar el sistema de partículas
                    ParticleSystem instantiatedParticles = Instantiate(impactParticles, newBullet.transform.position, Quaternion.identity);

                    // Destruir las partículas después de un tiempo determinado (ajusta según sea necesario)
                    Destroy(instantiatedParticles.gameObject, 5f);
                    Destroy(newBullet);
                }
            }
        }
    }

    // Corrutina para reactivar la gravedad y detección de colisiones después de un tiempo
    IEnumerator ReactivateGravityAndCollisions(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reactivar la gravedad y detección de colisiones del objeto
        rb.useGravity = true;
        rb.detectCollisions = true;
    }
}
