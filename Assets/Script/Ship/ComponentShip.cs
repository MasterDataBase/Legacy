using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComponentShip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textGUI;
    [SerializeField] int health = 100;

    [SerializeField] ComponentType compType;

    Player player;    

    // Start is called before the first frame update
    void Start()
    {
        textGUI.text = health.ToString();
        player = transform.parent.transform.parent.gameObject.GetComponent<Player>();
    }

    public void HitComponent(int input)
    {
        health -= input;
        player.CheckOnHealth(compType, health);
        textGUI.text = health.ToString();
    }
}
