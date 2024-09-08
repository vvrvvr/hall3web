using UnityEngine;

public class AxisRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;
     public bool isRotate = true;

    private Vector3 rotationDirection = Vector3.zero;
    private bool isRotating = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartRotation(Vector3.down);
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartRotation(Vector3.up);
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StopRotation();
           
        }
    }

    void FixedUpdate()
    {
        if (isRotating)
        {
            Vector3 newRotation = GetNewRotation();
            SetNewRotation(newRotation);
        }
    }

    public void RotateForward()
    {
        StartRotation(Vector3.down);
    }

    public void RotateBackward()
    {
        StartRotation(Vector3.up);
    }
    void StartRotation(Vector3 direction)
    {
        if (rotationDirection != direction)
        {
            rotationDirection = direction;
            isRotating = true;
        }
    }

    public void StopRotation()
    {
        isRotating = false;
        rotationDirection = Vector3.zero;
    }

    Vector3 GetNewRotation()
    {
        Vector3 rotation = Vector3.zero;

        if (isRotate)
            rotation.y = rotationDirection.y * rotationSpeed * Time.fixedDeltaTime;

        return rotation;
    }

    void SetNewRotation(Vector3 newRotation)
    {
        transform.Rotate(newRotation);
    }
}