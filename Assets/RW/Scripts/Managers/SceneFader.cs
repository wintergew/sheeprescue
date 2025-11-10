using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public GameObject FadeOverlay;
    public float fadeSpeed = 0.8f;

    private Image fadeImage;

    private void Awake()
    {
        fadeImage = FadeOverlay.GetComponentInChildren<Image>();
    }

    private void Start()
    {
        StartCoroutine(Fade(FadeDirection.Out));
    }

    public void FadeAndLoad(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(FadeDirection.In, sceneName));
    }

    private IEnumerator Fade(FadeDirection fadeDirection)
    {
        FadeOverlay.SetActive(true);

        float alpha = (fadeDirection == FadeDirection.In) ? 0f : 1f;
        float fadeEndAlpha = (fadeDirection == FadeDirection.In) ? 1f : 0f;

        while (!Mathf.Approximately(alpha, fadeEndAlpha))
        {
            alpha += Time.deltaTime / fadeSpeed * ((fadeDirection == FadeDirection.In) ? 1f : -1f);
            alpha = Mathf.Clamp01(alpha);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        if (fadeDirection == FadeDirection.Out)
            FadeOverlay.SetActive(false);
    }

    private IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad)
    {
        yield return Fade(fadeDirection);
        SceneManager.LoadScene(sceneToLoad);
    }

    public enum FadeDirection { In, Out }
}
