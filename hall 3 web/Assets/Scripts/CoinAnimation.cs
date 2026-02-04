using UnityEngine;

/// <summary>
/// Скрипт анимации монетки: вращение вокруг оси и движение вверх-вниз
/// </summary>
public class CoinAnimation : MonoBehaviour
{
    [Header("Вращение")]
    [Tooltip("Ось вращения (по умолчанию Y - вертикальная ось)")]
    public Vector3 rotationAxis = Vector3.up;
    
    [Tooltip("Скорость вращения в градусах в секунду")]
    public float rotationSpeed = 90f;
    
    [Header("Движение вверх-вниз")]
    [Tooltip("Амплитуда движения вверх-вниз (в единицах Unity)")]
    public float verticalAmplitude = 0.5f;
    
    [Tooltip("Скорость движения вверх-вниз (циклов в секунду)")]
    public float verticalSpeed = 1f;
    
    [Tooltip("Начальная позиция по Y (если 0, используется текущая позиция)")]
    public float baseYPosition = 0f;
    
    private Vector3 _startPosition;
    private bool _useBaseY = false;
    
    private void Start()
    {
        // Сохраняем начальную позицию
        _startPosition = transform.position;
        
        // Если baseYPosition не задан (равен 0), используем текущую Y позицию
        if (baseYPosition == 0f)
        {
            baseYPosition = _startPosition.y;
        }
        else
        {
            _useBaseY = true;
        }
    }
    
    private void Update()
    {
        // Вращение вокруг заданной оси
        transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime, Space.Self);
        
        // Движение вверх-вниз с использованием синуса
        float verticalOffset = Mathf.Sin(Time.time * verticalSpeed * 2f * Mathf.PI) * verticalAmplitude;
        float targetY = _useBaseY ? baseYPosition : _startPosition.y;
        
        transform.position = new Vector3(
            _startPosition.x,
            targetY + verticalOffset,
            _startPosition.z
        );
    }
    
    /// <summary>
    /// Сброс начальной позиции (полезно если монетка перемещается)
    /// </summary>
    public void ResetStartPosition()
    {
        _startPosition = transform.position;
        if (!_useBaseY)
        {
            baseYPosition = _startPosition.y;
        }
    }
}










