using UnityEngine;

public class MapRange : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        var iSetKomaGrade = other.GetComponent<ISetKomaGrade>();
        iSetKomaGrade.SetGradeKoma(1, false);
        Destroy(other.gameObject);
    }
}
