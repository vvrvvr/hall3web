using UnityEngine;

/// <summary>
/// Таймер игры - отслеживает время с момента запуска до завершения
/// </summary>
public class GameTimer : MonoBehaviour
{
    [Header("Debug Info")]
    [Tooltip("Текущее время игры (отображается в Inspector во время игры)")]
    [SerializeField] private string currentTimeDisplay = "00.00.000";
    
    private float _elapsedTime = 0f;
    private bool _isRunning = false;
    private bool _isStopped = false;
    
    private void Update()
    {
        if (_isRunning && !_isStopped)
        {
            _elapsedTime += Time.deltaTime;
            // Обновляем отображаемое время в Inspector
            currentTimeDisplay = FormatTime(_elapsedTime);
        }
    }
    
    /// <summary>
    /// Запустить таймер
    /// </summary>
    public void StartTimer()
    {
        _isRunning = true;
        _isStopped = false;
        _elapsedTime = 0f;
    }
    
    /// <summary>
    /// Остановить таймер
    /// </summary>
    public void StopTimer()
    {
        if (_isStopped) return;
        
        _isStopped = true;
        _isRunning = false;
        
        // Выводим время в Debug в формате MM.SS.MMM
        string timeString = FormatTime(_elapsedTime);
        Debug.Log($"Время игры: {timeString}");
    }
    
    /// <summary>
    /// Получить текущее время в секундах
    /// </summary>
    public float GetElapsedTime()
    {
        return _elapsedTime;
    }
    
    /// <summary>
    /// Форматирование времени в формат MM.SS.MMM
    /// </summary>
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        int milliseconds = Mathf.FloorToInt((timeInSeconds % 1f) * 1000f);
        
        return $"{minutes:D2}.{seconds:D2}.{milliseconds:D3}";
    }
    
    /// <summary>
    /// Проверка, запущен ли таймер
    /// </summary>
    public bool IsRunning => _isRunning && !_isStopped;
}

