using System;
using System.Collections;
using UnityEngine;

public class KomaHP : MonoBehaviour, IDamageable
{
    private PlayerDatas datas;
    private GameObject komaObj;
    public static Action<GameObject> OnKomaDeath;


    public IEnumerator InstantiateKoma(GameObject obj, Transform transform)
    {
        komaObj = Instantiate(obj, transform);
        yield return komaObj;
    }

    public void Death()
    {
        OnKomaDeath?.Invoke(gameObject);
        Destroy(this.gameObject);
    }
}
