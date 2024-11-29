using System;
using UnityEngine;

public class KomaHP : MonoBehaviour, IDamageable
{
    public static Action<GameObject> OnKomaDeath;

    public void Death()
    {
        OnKomaDeath?.Invoke(gameObject);
        Destroy(this.gameObject);
    }
}
