using UnityEngine;
using TMPro;
using System.Collections;

public class TextFadeScript : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float delay = 2f;
    public float fadeTime = 1f;

    private void Start()
    {
        StartCoroutine(StartFade());
    }

    private IEnumerator StartFade()
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(FadeText());
        DisableScript();
    }

    private IEnumerator FadeText()
    {
        Color startColor = textMesh.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            textMesh.color = Color.Lerp(startColor, endColor, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMesh.color = endColor;
    }

    private void DisableScript()
    {
        enabled = false;
    }
}