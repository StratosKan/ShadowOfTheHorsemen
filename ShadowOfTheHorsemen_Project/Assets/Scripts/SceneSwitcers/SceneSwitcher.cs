using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    //this is nested on transitionTriggers it basicly load the proper scene and enables a black image
    public int sceneToLoad = 0;
    public GameObject blackImage;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            blackImage.SetActive(true);
            SceneManager.LoadScene(sceneToLoad,LoadSceneMode.Single);
        }
    }
}