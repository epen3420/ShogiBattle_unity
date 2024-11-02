using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitObjects : MonoBehaviour
{
    public IEnumerator InstantiateObj(List<GameObject> objList, Vector3 pos, Quaternion rotate)
    {
        List<GameObject> tempObj = new List<GameObject>();
        for (int i = 0; i < objList.Count; i++)
        {
            var obj = Instantiate(objList[i], pos, rotate);
            tempObj.Add(obj);
        }
        yield return tempObj;
    }
}
