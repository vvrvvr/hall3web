using StarterAssets;
using UnityEngine;
using TMPro;
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
    
    [Header("Экран завершения")]
    [Tooltip("Canvas с CanvasFade для экрана завершения игры")]
    public CanvasFade endGameCanvasFade;
    
    [Tooltip("Текстовое поле для отображения времени прохождения")]
    public TextMeshProUGUI completionTimeText;
    
    [Header("Трансформация мира")]
    [Tooltip("Скрипт для управления трансформацией мира после просмотра статистики")]
    public WorldTransformation worldTransformation;
    
    private GameObject _player;
    private bool _gameEnded = false;
    private GameTimer _gameTimer;
    
    private void Update()
    {
        // Если игра завершена и нажат Enter, скрываем экран завершения и возвращаем управление
        if (_gameEnded && Input.GetKeyDown(KeyCode.Return))
        {
            ResumeGame();
        }
    }
    
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
        
        // Получаем или создаём GameTimer
        _gameTimer = GetComponent<GameTimer>();
        if (_gameTimer == null)
        {
            _gameTimer = gameObject.AddComponent<GameTimer>();
        }
        
        // Запускаем таймер при старте игры
        _gameTimer.StartTimer();
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
        
        // Останавливаем таймер и выводим время
        if (_gameTimer != null)
        {
            _gameTimer.StopTimer();
            
            // Получаем отформатированное время и устанавливаем в текстовое поле
            if (completionTimeText != null)
            {
                string timeString = _gameTimer.GetFormattedTime();
                completionTimeText.text = $"Completion time: {timeString}";
            }
        }
        
        // Показываем экран завершения с фейдом
        if (endGameCanvasFade != null)
        {
            endGameCanvasFade.FadeIn();
        }
    }
    
    /// <summary>
    /// Отключение управления игроком
    /// </summary>
    private void DisablePlayerInput()
    {
        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = false;
        }
        if (playerAnimator != null)
        {
            playerAnimator.enabled = false;
        }
    }
    
    /// <summary>
    /// Возврат управления игроку
    /// </summary>
    private void EnablePlayerInput()
    {
        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = true;
        }
        if (playerAnimator != null)
        {
            playerAnimator.enabled = true;
        }
    }
    
    /// <summary>
    /// Возобновление игры - скрывает экран завершения и возвращает управление
    /// </summary>
    private void ResumeGame()
    {
        // Скрываем экран завершения с фейдом
        if (endGameCanvasFade != null)
        {
            endGameCanvasFade.FadeOut();
        }
        
        // Трансформируем мир
        if (worldTransformation != null)
        {
            worldTransformation.TransformWorld();
        }
        
        // Возвращаем управление игроку
        EnablePlayerInput();
        
        // Сбрасываем флаг завершения игры (чтобы можно было продолжить играть)
        _gameEnded = false;
    }
}

