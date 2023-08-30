using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://answers.unity.com/questions/1197119/how-to-detect-when-and-where-a-particle-hits-a-sur.html
public class ParticleCollider : MonoBehaviour { 

public float radius = 5f;
public float power = 100f;
public float liftPower = 50f;
private ParticleSystem PSystem;
private List<ParticleCollisionEvent> CollisionEvents;

    void Start()
    {
        PSystem = GetComponent<ParticleSystem>();
        CollisionEvents = new List<ParticleCollisionEvent>(8);
    }

    public void OnParticleCollision(GameObject other)
    {
        int collCount = PSystem.GetSafeCollisionEventSize();

            //Debug.Log("number CollisionEvents: " + CollisionEvents.Count);

        if (collCount > CollisionEvents.Count)
        {
            CollisionEvents = new List<ParticleCollisionEvent>(collCount);
        }

        int eventCount = PSystem.GetCollisionEvents(other, CollisionEvents);

        for (int i = 0; i < eventCount; i++)
        {
            //Debug.Log(CollisionEvents[i].colliderComponent.gameObject.name);
            CollisionEvents[i].colliderComponent.GetComponent<ComponentShip>().HitComponent(5);
        }
    }
}