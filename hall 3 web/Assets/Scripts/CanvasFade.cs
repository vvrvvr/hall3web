using UnityEngine;
using System.Collections;

/// <summary>
/// Скрипт для управления фейдом Canvas через CanvasGroup
/// </summary>
public class CanvasFade : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Время фейда в секундах")]
    public float fadeDuration = 1f;
    
    private CanvasGroup _canvasGroup;
    
    private void Awake()
    {
        // Получаем или создаём CanvasGroup
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        
        // Устанавливаем начальную прозрачность в 0
        _canvasGroup.alpha = 0f;
    }
    
    /// <summary>
    /// Запустить фейд от текущего значения до 1 (полностью видимый)
    /// </summary>
    public void FadeIn()
    {
        StartCoroutine(FadeCoroutine(1f, fadeDuration));
    }
    
    /// <summary>
    /// Запустить фейд от текущего значения до 0 (полностью прозрачный)
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(FadeCoroutine(0f, fadeDuration));
    }
    
    /// <summary>
    /// Запустить фейд до указанного значения alpha
    /// </summary>
    public void FadeTo(float targetAlpha, float duration)
    {
        StartCoroutine(FadeCoroutine(targetAlpha, duration));
    }
    
    /// <summary>
    /// Корутина для плавного фейда
    /// </summary>
    private IEnumerator FadeCoroutine(float targetAlpha, float duration)
    {
        float startAlpha = _canvasGroup.alpha;
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            _canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, progress);
            yield return null;
        }
        
        // Убеждаемся, что достигли целевого значения
        _canvasGroup.alpha = targetAlpha;
    }
}


