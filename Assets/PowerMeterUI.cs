using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerMeterUI : MonoBehaviour
{
    [SerializeField] Image needle;

    [SerializeField] float MAX_SPEED_ANGLE;
    [SerializeField] float ZERO_SPEED_ANGLE;

    [SerializeField] float speedMax;
    [SerializeField] public float speed;

    [SerializeField] int OFFSET_POWER_METER = 1000;

    private void Update()
    {
        speedMax = 900;

        needle.transform.eulerAngles = new Vector3(0, 0, SetPower());
    }

    public float SetPower()
    {
        float totalAngleSize = ZERO_SPEED_ANGLE / MAX_SPEED_ANGLE;

        float speedNormalized = ((speed*-1)/ speedMax) * OFFSET_POWER_METER;

        return ZERO_SPEED_ANGLE - speedNormalized * totalAngleSize;
    }

}
