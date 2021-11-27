using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeFader : MonoBehaviour
{
    public Animator animator;
    private string sceneNameToLoad;

    public void FadeToScene(string sceneName)
    {
        sceneNameToLoad = sceneName;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
