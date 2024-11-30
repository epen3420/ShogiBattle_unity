using UnityEngine;

public class KomaHP : MonoBehaviour, IDamageable
{
    public PlayerDatas datas;
    public PlayerDatas Datas
    {
        set { datas = value; }
    }


    public void Death()
    {
        datas.isDead = true;
        Destroy(this.gameObject);
    }
}
