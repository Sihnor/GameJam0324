using System;
using System.Collections;
using System.Collections.Generic;
using Prog.Marcel.Scripts;
using UnityEngine;

public class HeatScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IHeatingUp>().HeatingUp((transform.position - other.transform.position).magnitude);
        }
    }
}
