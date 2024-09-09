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

    void Start()
    {
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
}