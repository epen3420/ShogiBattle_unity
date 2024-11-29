using UnityEngine;

interface IKomaAction
{
    /// <summary>
    /// 動かしたいベクトルに衝撃を与える関数
    /// </summary>
    /// <param name="moveVector"></param>
    void Move(Vector3 moveVector);
}
