using StarterAssets;
using UnityEngine;


public class AvatarMovementModifier : MonoBehaviour
{
    public bool isRunComplete = false;
    public ThirdPersonController thirdPersonController;
    public float moveSpeed = 20f;
    public float sprintSpeed = 40f ;
    public float jumpHeight = 15f;
    public float gravity = -15f;
    
    


    private void OnAvatarLoadComplete()
    {
        if(isRunComplete)
            ModifyAvatarValues();
    }

    public void ModifyAvatarValues()
    {
       Debug.Log("модифицировал");
       thirdPersonController.canFly = true;
       thirdPersonController.MoveSpeed = moveSpeed;
       thirdPersonController.SprintSpeed = sprintSpeed;
       thirdPersonController.JumpHeight = jumpHeight;
       thirdPersonController.Gravity = gravity;
    }

    
    
}