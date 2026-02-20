using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed = 1f;
    public IEnumerator FadeOut()
    {
        yield return Fade(0f, 1f); // fade from transparent to opaque
    }
    public IEnumerator FadeIn()
    {
        yield return Fade(1f, 0f); // fade from opaque to transparent
    }
    private IEnumerator Fade(float start, float end)
    {
        float time = 0;
        Color color = fadeImage.color;
        while (time < 1)
        {
            time += Time.deltaTime * fadeSpeed;
            float alpha = Mathf.Lerp(start, end, time);  // smoothly interpolate between start and end alpha values
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);  // update the image's alpha
            yield return null;
        }
    }
}
