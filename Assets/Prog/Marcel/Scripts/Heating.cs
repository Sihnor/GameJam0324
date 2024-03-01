using System.Collections;
using System.Collections.Generic;
using Prog.Marcel.Scripts;
using UnityEngine;

public class Heating : MonoBehaviour, IHeatingUp
{
    public void HeatingUp(float distance, float heatValue = 1)
    {
        Debug.Log("Heating up: " + distance + " " + heatValue);
    }
}
