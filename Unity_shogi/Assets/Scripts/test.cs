using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Update()
    {
        if (this.GetComponent<Rigidbody>().IsSleeping())
        {
            Debug.Log("a");
        }
    }
}
