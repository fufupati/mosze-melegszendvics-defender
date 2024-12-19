using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroFade : MonoBehaviour
{
    [SerializeField] private float introDuration = 4f; 
    [SerializeField] private float fadeDuration = 2f; 
    [SerializeField] private string nextSceneName = "Scene"; 
    [SerializeField] private CanvasGroup canvasGroup; 

    private void Start()
    {
        
        Invoke("StartFadeOut", introDuration);
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOutAndLoad());
    }

    private System.Collections.IEnumerator FadeOutAndLoad()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

   
        SceneManager.LoadScene(nextSceneName);
    }
}
