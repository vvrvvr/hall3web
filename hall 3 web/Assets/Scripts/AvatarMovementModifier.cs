using UnityEngine;


public class AvatarMovementModifier : MonoBehaviour
{
    public bool isRunComplete = false;

    

    void Start()
    {
       
    }

    private void OnAvatarLoadComplete()
    {
        if(isRunComplete)
            ModifyAvatarValues();
    }

    public void ModifyAvatarValues()
    {
       
    }

    
    
}