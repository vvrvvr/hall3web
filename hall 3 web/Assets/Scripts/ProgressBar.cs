using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Transform obj; // Объект, за которым мы следим
    public float startRotation; // Начальное значение вращения (в градусах)
    public float endRotation; // Конечное значение вращения (в градусах)
    public float progress; // Прогресс от 0 до 100
    public Slider progressSlider; // Слайдер для отображения прогресса

    void Update()
    {
        // Получаем текущее локальное вращение объекта вокруг оси Y (или любой другой оси, если нужно)
        float currentRotation = obj.localEulerAngles.y;

        // Корректируем вращение, чтобы значение было в диапазоне от 0 до 360 градусов
        currentRotation = NormalizeAngle(currentRotation);
        startRotation = NormalizeAngle(startRotation);
        endRotation = NormalizeAngle(endRotation);

        // Рассчитываем прогресс в процентах
        if (endRotation != startRotation)
        {
            progress = Mathf.InverseLerp(startRotation, endRotation, currentRotation) * 100f;
        }
        else
        {
            progress = 100f; // Если начальное и конечное значения равны, считаем прогресс 100%
        }

        // Ограничиваем значение прогресса в диапазоне от 0 до 100
        progress = Mathf.Clamp(progress, 0f, 100f);

        // Обновляем значение слайдера
        if (progressSlider != null)
        {
            progressSlider.value = progress;
        }
        
    }

    // Метод для нормализации угла в диапазоне от 0 до 360 градусов
    private float NormalizeAngle(float angle)
    {
        angle = angle % 360f;
        if (angle < 0f)
        {
            angle += 360f;
        }
        return angle;
    }
}
