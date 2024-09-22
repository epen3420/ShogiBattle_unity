using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public void GotoMainScene()
    {
        Invoke("SceneChange", 2.0f);
    }

    private void SceneChange()
    {
        SceneManager.LoadScene("MainScene");
    }
}
