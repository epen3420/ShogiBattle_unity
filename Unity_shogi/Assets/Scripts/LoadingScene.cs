using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private Slider slider;


    public void LoadNextScene(string nextScene)
    {
        loadingUI.SetActive(true);
        StartCoroutine(LoadScene(nextScene));
    }

    IEnumerator LoadScene(string scene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        while (!async.isDone)
        {
            slider.value = async.progress;
            yield return null;
        }
    }
}
