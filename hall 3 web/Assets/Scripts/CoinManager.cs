using UnityEngine;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// Менеджер монеток - синглтон, отслеживает все монетки на уровне и их подбор
/// </summary>
public class CoinManager : MonoBehaviour
{
    private static CoinManager _instance;
    
    /// <summary>
    /// Единственный экземпляр CoinManager (синглтон)
    /// </summary>
    public static CoinManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CoinManager>();
                
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("CoinManager");
                    _instance = managerObject.AddComponent<CoinManager>();
                }
            }
            return _instance;
        }
    }
    
    [Header("UI")]
    [Tooltip("Текстовое поле для отображения счётчика монеток")]
    public TextMeshProUGUI coinText;
    
    private List<Coin> _allCoins = new List<Coin>();
    private int _collectedCount = 0;
    
    private void Awake()
    {
        // Убеждаемся, что есть только один экземпляр
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    
    private void Start()
    {
        // Автоматически находим все монетки на сцене при старте
        RegisterAllCoins();
        // Обновляем текст при старте
        UpdateCoinText();
    }
    
    /// <summary>
    /// Регистрация монетки в менеджере (вызывается автоматически или вручную)
    /// </summary>
    public void RegisterCoin(Coin coin)
    {
        if (coin != null && !_allCoins.Contains(coin))
        {
            _allCoins.Add(coin);
        }
    }
    
    /// <summary>
    /// Автоматический поиск и регистрация всех монеток на сцене
    /// </summary>
    private void RegisterAllCoins()
    {
        Coin[] coins = FindObjectsOfType<Coin>();
        _allCoins.Clear();
        _allCoins.AddRange(coins);
        _collectedCount = 0;
        
        Debug.Log($"CoinManager: Зарегистрировано монеток на уровне: {_allCoins.Count}");
    }
    
    /// <summary>
    /// Обработка подбора монетки (вызывается из Coin)
    /// </summary>
    public void OnCoinCollected(Coin coin)
    {
        if (coin == null) return;
        
        _collectedCount++;
        
        Debug.Log($"CoinManager: Монетка собрана! Всего: {_collectedCount} / {_allCoins.Count}");
        
        // Обновляем текст UI
        UpdateCoinText();
        
        // Проверяем, все ли монетки собраны, и вызываем завершение игры
        if (AreAllCoinsCollected() && GameManager.Instance != null)
        {
            GameManager.Instance.EndGame();
        }
    }
    
    /// <summary>
    /// Обновление текста счётчика монеток
    /// </summary>
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = $"Монеты: {GetCollectedCount()}/{GetTotalCount()}";
        }
    }
    
    /// <summary>
    /// Получить количество собранных монеток
    /// </summary>
    public int GetCollectedCount()
    {
        return _collectedCount;
    }
    
    /// <summary>
    /// Получить общее количество монеток на уровне
    /// </summary>
    public int GetTotalCount()
    {
        return _allCoins.Count;
    }
    
    /// <summary>
    /// Проверка, все ли монетки собраны
    /// </summary>
    public bool AreAllCoinsCollected()
    {
        return _collectedCount >= _allCoins.Count && _allCoins.Count > 0;
    }
}



