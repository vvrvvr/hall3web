using StarterAssets;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// Менеджер игры - отслеживает завершение уровня и управляет состоянием игры
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public ThirdPersonController thirdPersonController;
    public Animator playerAnimator;
    
    /// <summary>
    /// Единственный экземпляр GameManager (синглтон)
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("GameManager");
                    _instance = managerObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    
    [Header("Настройки")]
    [Tooltip("Тег игрока (по умолчанию 'Player')")]
    public string playerTag = "Player";
    
    private GameObject _player;
    private bool _gameEnded = false;
    
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
        // Находим игрока по тегу
        _player = GameObject.FindGameObjectWithTag(playerTag);
        
        if (_player == null)
        {
            Debug.LogWarning("GameManager: Игрок с тегом '" + playerTag + "' не найден!");
        }
    }
    
    /// <summary>
    /// Метод завершения игры - отключает управление игроком
    /// </summary>
    public void EndGame()
    {
        if (_gameEnded) return;
        
        _gameEnded = true;
        
        Debug.Log("Игра завершена! Все монетки собраны!");
        
        // Отключаем управление у игрока
        DisablePlayerInput();
    }
    
    /// <summary>
    /// Отключение управления игроком
    /// </summary>
    private void DisablePlayerInput()
    {
        thirdPersonController.enabled = false;
        playerAnimator.enabled = false;
    }
}

