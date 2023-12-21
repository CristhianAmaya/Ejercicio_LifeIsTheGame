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
    public float attractionForce = 200f; // Fuerza de atracción hacia el centro
    public string interactableTag = "Interactable"; // Tag del objeto interactuable

    private float shotRateTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Time.time > shotRateTime)
            {
                GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                Rigidbody newBulletRb = newBullet.GetComponent<Rigidbody>();
                newBulletRb.AddForce(spawnPoint.forward * shotForce);

                InteractWithObjects(newBullet);

                shotRateTime = Time.time + shotRate;
            }
        }
    }

    void InteractWithObjects(GameObject newBullet)
    {
        float waitTime = 0.5f;
        StartCoroutine(AttractObjects(newBullet, waitTime));

        // Resto del código...
    }

    IEnumerator AttractObjects(GameObject newBullet, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Collider[] colliders = Physics.OverlapSphere(newBullet.transform.position, 5f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(interactableTag))
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Calcular la dirección hacia el centro de la bala
                    Vector3 directionToBullet = (newBullet.transform.position - collider.transform.position).normalized;

                    // Desactivar la gravedad del objeto para que se mantenga levitando
                    rb.useGravity = false;

                    // Aplicar una fuerza de atracción hacia el centro
                    rb.AddForce(directionToBullet * attractionForce);

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
