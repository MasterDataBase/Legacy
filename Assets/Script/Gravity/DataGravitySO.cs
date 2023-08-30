using UnityEngine;

[CreateAssetMenu(fileName = "DataGravity", menuName = "DataGravity", order = 1)]
public class DataGravitySO : ScriptableObject
{
    [SerializeField]
    int radius = 100;
    [SerializeField]
    int forceAmount = 100;
    [SerializeField]
    float gravity = 0;

    public int GetRadius() { return radius; }
    public int GetForceAmount() { return forceAmount; }
    public float GetGravity() { return gravity; }
}
