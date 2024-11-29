using UnityEngine;

public class MapRange : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        var iDamageable = other.GetComponent<IDamageable>();
        if (iDamageable == null) return;
        iDamageable.Death();
    }
}
