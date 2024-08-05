using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShogiController : MonoBehaviour
{

    [SerializeField] GameObject prefabOu;
    [SerializeField] GameObject prefabGyoku;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 OnePpos = new Vector3(0, 1, -4);
        GameObject Ou = Instantiate(prefabOu, OnePpos, Quaternion.Euler(0, 180, 0));

        Ou.AddComponent<Rigidbody>();

        Vector3 TwoPpos = new Vector3(0, 1, 4);
        GameObject Gyoku = Instantiate(prefabGyoku, TwoPpos, Quaternion.Euler(0, 0, 0));

        Gyoku.AddComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
