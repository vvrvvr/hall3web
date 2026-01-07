using UnityEngine;
using System.Collections;

/// <summary>
/// Скрипт логики монетки: детекция подбора игроком, вызов менеджера, спавн эффекта
/// </summary>
public class Coin : MonoBehaviour
{
    [Header("Эффект подбора")]
    [Tooltip("Префаб эффекта подбора монетки (спавнится на месте монетки)")]
    public GameObject collectEffectPrefab;
    
    [Header("Настройки")]
    [Tooltip("Тег игрока (по умолчанию 'Player')")]
    public string playerTag = "Player";
    
    [Header("Анимация исчезновения")]
    [Tooltip("Время в секундах, за которое монетка уменьшится до нуля")]
    public float shrinkDuration = 0.5f;
    
    private bool _isCollected = false;
    private Vector3 _initialScale;
    
    private void Start()
    {
        // Сохраняем начальный размер монетки
        _initialScale = transform.localScale;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что это игрок и монетка ещё не собрана
        if (_isCollected) return;
        
        if (other.CompareTag(playerTag))
        {
            CollectCoin();
        }
    }
    
    /// <summary>
    /// Обработка подбора монетки
    /// </summary>
    private void CollectCoin()
    {
        if (_isCollected) return;
        
        _isCollected = true;
        
        // Спавним эффект подбора на месте монетки
        if (collectEffectPrefab != null)
        {
            GameObject effect = Instantiate(collectEffectPrefab, transform.position, transform.rotation);
            // Эффект сам уничтожится по завершении анимации (через скрипт эффекта)
        }
        
        // Уведомляем менеджер о подборе
        CoinManager.Instance?.OnCoinCollected(this);
        
        // Запускаем корутину для плавного уменьшения и уничтожения
        StartCoroutine(ShrinkAndDestroy());
    }
    
    /// <summary>
    /// Корутина для плавного уменьшения монетки до нуля и последующего уничтожения
    /// </summary>
    private IEnumerator ShrinkAndDestroy()
    {
        float elapsedTime = 0f;
        
        // Плавно уменьшаем scale от начального значения до нуля
        while (elapsedTime < shrinkDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / shrinkDuration;
            
            // Используем Lerp для плавного перехода от _initialScale к Vector3.zero
            transform.localScale = Vector3.Lerp(_initialScale, Vector3.zero, progress);
            
            yield return null;
        }
        
        // Убеждаемся, что scale точно равен нулю
        transform.localScale = Vector3.zero;
        
        // Уничтожаем объект после завершения анимации
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Проверка, собрана ли монетка
    /// </summary>
    public bool IsCollected => _isCollected;
}

