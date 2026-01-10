using UnityEngine;

/// <summary>
/// Скрипт для автоматического уничтожения трейла после того, как он полностью исчез
/// Должен быть прикреплен к префабу Trail
/// </summary>
public class TrailAutoDestroy : MonoBehaviour
{
    private TrailRenderer _trailRenderer;
    private bool _wasEmitting = true;
    private float _timeSinceEmissionStopped = 0f;
    
    private void Awake()
    {
        // Получаем компонент TrailRenderer
        _trailRenderer = GetComponent<TrailRenderer>();
        if (_trailRenderer == null)
        {
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
        }
        
        if (_trailRenderer == null)
        {
            Debug.LogWarning("TrailAutoDestroy: TrailRenderer не найден на объекте " + gameObject.name);
            // Если нет TrailRenderer, уничтожаем объект сразу
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        if (_trailRenderer == null)
        {
            Destroy(gameObject);
            return;
        }
        
        // Проверяем, отключена ли эмиссия
        if (!_trailRenderer.emitting && _wasEmitting)
        {
            // Эмиссия только что отключилась
            _wasEmitting = false;
            _timeSinceEmissionStopped = 0f;
        }
        
        // Если эмиссия отключена, отслеживаем время
        if (!_trailRenderer.emitting)
        {
            _timeSinceEmissionStopped += Time.deltaTime;
            
            // Проверяем, исчез ли трейл полностью
            // Трейл считается исчезнувшим, если:
            // 1. Нет точек в трейле (positionCount == 0)
            // 2. Или прошло время жизни трейла + небольшой запас
            bool isTrailGone = _trailRenderer.positionCount == 0;
            bool timeExceeded = _timeSinceEmissionStopped >= (_trailRenderer.time + 0.1f); // +0.1f запас
            
            if (isTrailGone || timeExceeded)
            {
                // Трейл полностью исчез, уничтожаем объект
                Destroy(gameObject);
            }
        }
    }
}






