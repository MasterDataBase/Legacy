using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandlerChild : MonoBehaviour
{
    CollisionHandler colH;

    private void Start()
    {
        colH = GetComponentInParent<CollisionHandler>();
    }

    /// <summary>
    /// Questo collision rivela le altri componenti della nave
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Non ho capito, Mr: " + other.gameObject.name);
        if (colH.isTransitioning || colH.collisionDisabled)
        { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                //Debug.Log("Ciao");
                break;

            case "Finish":
                //LoadNextLevel();
                colH.StartSuccessSequence();
                break;

            case "Fuel":
                //Debug.Log("Ok");
                break;

            case "ShipComponent":
                Debug.Log("Pezz: " + other.gameObject.name + " colpito");
                break;

            default:
                Debug.Log("Ok");
                //StartReloadSequence();
                colH.StartCrashSequence();
                break;
        }

        
    }

    private void OnParticleTrigger()
    {
        Debug.Log(name + " triggered by OnparticleTrigger");
    }

    /// <summary>
    /// Da usare per recepire le collisioni con il sistema particellare
    /// Per funzionare ogni componente che ha questo script deve anche avere forzatamente 
    /// l'oggetto RigidBody associato. 
    /// </summary>
    /// <param name="other"></param>
    private void OnParticleCollision(GameObject other)
    {
        //StartCrashSequence();
        Debug.Log(name + " triggered by OnParticleCollision");
    }
}
