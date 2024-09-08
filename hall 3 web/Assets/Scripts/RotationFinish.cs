using UnityEngine;
using System.Collections; // Required for Coroutines

public class RotationFinish : MonoBehaviour
{
    private bool isOnce = true;
    public AvatarMovementModifier _avatarMovementModifier;
    public AxisRotation axisRotation;

    // New public array to hold Rigidbody references
    public Rigidbody[] rigidbodies;
    public GameObject[] objects;
    public GameObject[] objects2;

    // Time to wait before calling AfterWaiting()
    public float waitSeconds = 2f;
    public float waitSeconds2 = 10f;// You can set this value in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOnce)
        {
            isOnce = false;
            _avatarMovementModifier.isRunComplete = true;
            _avatarMovementModifier.ModifyAvatarValues();
            axisRotation.isRotate = false;
            axisRotation.StopRotation();
            Debug.Log("run complete");

            // Disable kinematic on all rigidbodies in the array
            foreach (Rigidbody rb in rigidbodies)
            {
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }

            // Start the coroutine to wait and then call AfterWaiting()
            StartCoroutine(WaitAndExecute());
            StartCoroutine(WaitAndExecute2());
        }
    }

    private void TurnOffObjects()
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
    // Coroutine to wait for specified seconds before calling AfterWaiting()
    private IEnumerator WaitAndExecute()
    {
        yield return new WaitForSeconds(waitSeconds);
        AfterWaiting();
    }

    // Method to be called after waiting
    private void AfterWaiting()
    {
        TurnOffObjects();
    }
    private IEnumerator WaitAndExecute2()
    {
        yield return new WaitForSeconds(10);
        foreach (GameObject obj in objects2)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}