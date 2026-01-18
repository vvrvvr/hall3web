using UnityEngine;
using DG.Tweening;

/// <summary>
/// Скрипт для управления трансформацией мира после просмотра статистики
/// </summary>
public class WorldTransformation : MonoBehaviour
{
    [Header("Настройки трансформации")]
    [Tooltip("Включить трансформацию при старте (для тестирования)")]
    public bool transformOnStart = false;
    
    [Header("Освещение")]
    [Tooltip("Длительность анимации изменения освещения в секундах")]
    public float lightingChangeDuration = 2f;
    
    [Tooltip("Основной источник света сцены")]
    public Light mainLight;
    
    [Tooltip("Целевой цвет основного освещения")]
    public Color mainLightTargetColor = Color.white;
    
    [Tooltip("Целевая интенсивность основного освещения")]
    public float mainLightTargetIntensity = 1f;
    
    [Tooltip("Дополнительный источник света сцены")]
    public Light additionalLight;
    
    [Tooltip("Целевой цвет дополнительного освещения")]
    public Color additionalLightTargetColor = Color.white;
    
    [Tooltip("Целевая интенсивность дополнительного освещения")]
    public float additionalLightTargetIntensity = 1f;
    
    [Header("Туман")]
    [Tooltip("Длительность анимации изменения тумана в секундах")]
    public float fogChangeDuration = 2f;
    
    [Tooltip("Целевой цвет тумана")]
    public Color fogTargetColor = Color.white;
    
    [Tooltip("Целевая плотность тумана (для режима Exponential Squared)")]
    public float fogTargetDensity = 0.01f;
    
    [Header("Анимации")]
    [Tooltip("Список объектов с компонентом RotationScript для отключения")]
    public RotationScript[] rotationScriptObjects;
    
    [Header("Аудио")]
    [Tooltip("Длительность анимации изменения громкости в секундах")]
    public float audioChangeDuration = 2f;
    
    [Tooltip("Первый AudioSource для изменения громкости")]
    public AudioSource audioSource1;
    
    [Tooltip("Целевая громкость первого AudioSource (от 0 до 1)")]
    [Range(0f, 1f)]
    public float audio1TargetVolume = 1f;
    
    [Tooltip("Второй AudioSource для изменения громкости")]
    public AudioSource audioSource2;
    
    [Tooltip("Целевая громкость второго AudioSource (от 0 до 1)")]
    [Range(0f, 1f)]
    public float audio2TargetVolume = 1f;
    
    private void Start()
    {
        // Для тестирования можно включить трансформацию сразу
        if (transformOnStart)
        {
            TransformWorld();
        }
    }
    
    /// <summary>
    /// Метод трансформации мира - вызывается когда игрок нажимает Enter после просмотра статистики
    /// Здесь будут добавляться изменения освещения, анимаций, величин и других параметров
    /// </summary>
    public void TransformWorld()
    {
        Debug.Log("WorldTransformation: Начало трансформации мира");
        
        // Изменение освещения
        ChangeLighting();
        
        // Изменение тумана
        ChangeFog();
        
        // Отключение анимаций
        DisableRotationScripts();
        
        // Изменение громкости аудио
        ChangeAudioVolume();
        
        // Здесь будут добавляться другие методы трансформации:
        // - Изменение величин и параметров
        // и т.д.
        
        // Пример структуры (закомментировано):
        // ModifyValues();
    }
    
    /// <summary>
    /// Изменение освещения сцены - анимирует цвет и интенсивность источников света
    /// </summary>
    private void ChangeLighting()
    {
        // Анимация основного источника света
        if (mainLight != null)
        {
            // Анимация интенсивности от текущего значения к целевому
            DOTween.To(() => mainLight.intensity, x => mainLight.intensity = x, 
                mainLightTargetIntensity, lightingChangeDuration);
            
            // Анимация цвета от текущего значения к целевому
            DOTween.To(() => mainLight.color, x => mainLight.color = x, 
                mainLightTargetColor, lightingChangeDuration);
        }
        
        // Анимация дополнительного источника света
        if (additionalLight != null)
        {
            // Анимация интенсивности от текущего значения к целевому
            DOTween.To(() => additionalLight.intensity, x => additionalLight.intensity = x, 
                additionalLightTargetIntensity, lightingChangeDuration);
            
            // Анимация цвета от текущего значения к целевому
            DOTween.To(() => additionalLight.color, x => additionalLight.color = x, 
                additionalLightTargetColor, lightingChangeDuration);
        }
    }
    
    /// <summary>
    /// Изменение тумана сцены - анимирует цвет и плотность тумана
    /// </summary>
    private void ChangeFog()
    {
        // Убеждаемся, что туман включен
        if (!RenderSettings.fog)
        {
            RenderSettings.fog = true;
        }
        
        // Анимация цвета тумана от текущего значения к целевому
        DOTween.To(() => RenderSettings.fogColor, x => RenderSettings.fogColor = x, 
            fogTargetColor, fogChangeDuration);
        
        // Анимация плотности тумана от текущего значения к целевому
        DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, 
            fogTargetDensity, fogChangeDuration);
    }
    
    /// <summary>
    /// Отключение RotationScript у всех объектов из списка
    /// </summary>
    private void DisableRotationScripts()
    {
        if (rotationScriptObjects == null || rotationScriptObjects.Length == 0)
        {
            return;
        }
        
        foreach (RotationScript rotationScript in rotationScriptObjects)
        {
            if (rotationScript != null)
            {
                rotationScript.enabled = false;
            }
        }
        
       // Debug.Log($"WorldTransformation: Отключено RotationScript у {rotationScriptObjects.Length} объектов");
    }
    
    /// <summary>
    /// Изменение громкости AudioSource - анимирует громкость от текущего значения к целевому
    /// </summary>
    private void ChangeAudioVolume()
    {
        // Анимация громкости первого AudioSource
        if (audioSource1 != null)
        {
            // Анимация громкости от текущего значения к целевому
            DOTween.To(() => audioSource1.volume, x => audioSource1.volume = x, 
                audio1TargetVolume, audioChangeDuration);
        }
        
        // Анимация громкости второго AudioSource
        if (audioSource2 != null)
        {
            // Анимация громкости от текущего значения к целевому
            DOTween.To(() => audioSource2.volume, x => audioSource2.volume = x, 
                audio2TargetVolume, audioChangeDuration);
        }
    }
    
    // Здесь будут добавляться другие методы для конкретных трансформаций:
    // Например:
    // private void DisableAnimations() { ... }
    // private void ModifyValues() { ... }
}

