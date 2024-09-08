using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    // Ссылки на две камеры Cinemachine
    public CinemachineVirtualCamera cinemachineCamera1;
    public CinemachineVirtualCamera cinemachineCamera2;

    void Update()
    {
        // Переключение на камеру 1 по нажатию клавиши "O"
        if (Input.GetKeyDown(KeyCode.O))
        {
            SwitchCamera(1);
        }
        // Переключение на камеру 2 по нажатию клавиши "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchCamera(2);
        }
    }

    // Метод для переключения камер
    public void SwitchCamera(int cameraIndex)
    {
        if (cameraIndex == 1)
        {
            // Устанавливаем приоритет для камеры 1 выше
            cinemachineCamera1.Priority = 10;
            cinemachineCamera2.Priority = 5;
        }
        else if (cameraIndex == 2)
        {
            // Устанавливаем приоритет для камеры 2 выше
            cinemachineCamera1.Priority = 5;
            cinemachineCamera2.Priority = 10;
        }
    }
}