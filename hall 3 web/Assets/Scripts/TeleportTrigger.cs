using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    // Трансформ объекта назначения
    public Transform destination;

    // Метод срабатывает, когда объект входит в триггер
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, имеет ли вошедший объект тег "Player"
        if (other.CompareTag("Player"))
        {
            // Перемещаем объект игрока к назначению
            other.transform.position = destination.position;
            // Если нужно, можно сбросить вращение:
            // other.transform.rotation = destination.rotation;
        }
    }
}