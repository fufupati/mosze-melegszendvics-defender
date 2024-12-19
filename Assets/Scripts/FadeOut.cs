using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float fadeDuration = 5f;  

    void Start()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();  
        
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

   private IEnumerator FadeOutCoroutine()
{
    float fadeSpeed = 1f / fadeDuration;  
    
 
    while (canvasGroup.alpha < 1)
    {
        canvasGroup.alpha += Time.deltaTime * fadeSpeed;
        yield return null;
    }

    
    LoadGameOverScene();
}
    private void LoadGameOverScene()
    {
       
        SceneManager.LoadScene("GameOverScene");
    }
}
