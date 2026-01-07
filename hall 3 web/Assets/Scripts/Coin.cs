using UnityEngine;

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
    
    private bool _isCollected = false;
    
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
        
        // Уничтожаем монетку
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Проверка, собрана ли монетка
    /// </summary>
    public bool IsCollected => _isCollected;
}

