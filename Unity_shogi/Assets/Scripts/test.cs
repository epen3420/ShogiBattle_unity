using UnityEngine;
using System.Collections;
using System;

public class test : MonoBehaviour
{
    //enabeldを切り替えればUpdate関数の発動を調整できる
    void Start()
    {
        StartCoroutine(start());
    }
    private IEnumerator start()
    {
        Debug.Log("start");
        yield return StartCoroutine(a());
        enabled = false;
        yield return new WaitForSeconds(1);
        enabled = true;
        yield return StartCoroutine(b());
        Debug.Log("EndStart");
    }

    void Update()
    {
        //Debug.Log("Update");
    }
    private IEnumerator a()
    {
        Debug.Log("r");
        yield return new WaitForSeconds(1);
    }
    private IEnumerator b()
    {
        Debug.Log("q");
        yield return new WaitForSeconds(1);
    }
}
