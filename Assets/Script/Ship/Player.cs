using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Script that has to be used during the ship phase
/// </summary>
public class Player : MonoBehaviour
{
    Movement movement;


    [SerializeField] float fuel;
    [SerializeField] float tankFuel;
    [SerializeField] Image image;

    private void Start()
    {
        fuel = tankFuel;
        Physics.gravity = Vector3.zero;
        movement = GetComponent<Movement>();

        image.fillAmount = fuel;
    }

    public void CheckOnHealth(ComponentType compType, float health)
    {
        switch (compType)
        {
            case ComponentType.WingSX:
                break;

            case ComponentType.WingDX:
                break;

            case ComponentType.Body:
                if (health < 25f) { movement.SetMainThrust(250f); return; }
                if (health < 50f) { movement.SetMainThrust(500f); return; }
                if (health < 75f) { movement.SetMainThrust(750f); return; }
                break;

            default:
                break;
        }
    }

    public float GetCurrentFuel()
    {
        return fuel;
    }

    public void DecriseFuel(float input)
    {
        fuel -= input;
        image.fillAmount = fuel / 100;
    }
}

public enum ComponentType{
    WingSX,
    WingDX,
    Body
}
