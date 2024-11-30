using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDatas", menuName = "Scriptable Objects/PlayerDatas")]
public class PlayerDatas : ScriptableObject
{
    public int komaSets;
    public int currentKomaInKomaSets;
    public bool isDead = false;
}
