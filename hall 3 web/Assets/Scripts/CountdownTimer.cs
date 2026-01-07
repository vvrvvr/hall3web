using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float timeToStart = 10f; // Время отсчета
    public TextMeshProUGUI timeText; // Поле для отображения времени
    public AudioSource beepSound; // Источник звука
    private float currentTime; // Текущие секунды отсчета
    private bool isCountingDown = true; // Флаг отсчета
    private int previousSecond; // Предыдущая секунда для отслеживания изменения
    public string screenText;
    public AxisRotation axisRotation;
    public GameObject screenTextObj;
    
    [Header("UI Fade")]
    [Tooltip("Скрипт CanvasFade для фейда UI после интро")]
    public CanvasFade canvasFade;
    
    [Header("Debug")]
    public bool isSkipIntro = false; // Пропустить интро-последовательность
    
    public RotationFinish rotationFinish; // Ссылка на RotationFinish для пропуска

    void Start()
    {
        // Если включен пропуск, сразу выполняем все действия
        if (isSkipIntro)
        {
            SkipIntroSequence();
            return;
        }
        
        // Инициализация таймера
        currentTime = timeToStart;
        timeText.text = screenText + " " + currentTime;
        previousSecond = Mathf.CeilToInt(currentTime);
        UpdateTimeText();
    }

    void Update()
    {
        // Если время еще не закончилось
        if (isCountingDown)
        {
            // Отнимаем время, прошедшее за кадр
            currentTime -= Time.deltaTime;

            // Если таймер достиг нуля
            if (currentTime <= 0)
            {
                currentTime = 0;
                isCountingDown = false;
                OnCountdownEnd();
            }

            // Отслеживаем последние три секунды
            int currentSecond = Mathf.CeilToInt(currentTime);
            if (currentSecond <= 3 && currentSecond != previousSecond)
            {
                PlayBeepSound();
            }

            previousSecond = currentSecond;
            UpdateTimeText();
        }
    }

    // Обновляем текст времени
    void UpdateTimeText()
    {
        timeText.text = timeText.text = screenText + " " + Mathf.Ceil(currentTime).ToString();
    }

    // Метод, вызываемый после окончания таймера
    void OnCountdownEnd()
    {
        axisRotation.RotateForward();
        screenTextObj.SetActive(false);
        
        // Запускаем фейд UI
        if (canvasFade != null)
        {
            canvasFade.FadeIn();
        }
        
        Destroy(gameObject);
    }

    // Метод для воспроизведения звука
    void PlayBeepSound()
    {
        if (beepSound != null)
        {
            beepSound.Play();
        }
        else
        {
            Debug.LogWarning("Не назначен источник звука!");
        }
    }
    
    // Метод для пропуска интро-последовательности
    void SkipIntroSequence()
    {
        Debug.Log("Пропуск интро-последовательности");
        
        // Скрываем текст отсчёта
        if (screenTextObj != null)
        {
            screenTextObj.SetActive(false);
        }
        
        // Останавливаем вращение комнаты
        if (axisRotation != null)
        {
            axisRotation.isRotate = false;
            axisRotation.StopRotation();
        }
        
        // Принудительно запускаем разрушение комнаты и активацию полёта
        if (rotationFinish != null)
        {
            rotationFinish.ForceCompleteSequence();
        }
        
        // Запускаем фейд UI
        if (canvasFade != null)
        {
            canvasFade.FadeIn();
        }
        
        // Уничтожаем этот объект
        Destroy(gameObject);
    }
}