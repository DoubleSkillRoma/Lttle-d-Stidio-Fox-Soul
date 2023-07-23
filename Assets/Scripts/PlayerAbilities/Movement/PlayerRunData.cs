using UnityEngine;

[CreateAssetMenu(menuName = "Player Run Data")] 
public class PlayerRunData : ScriptableObject
{
    [Header("Run")]
    public float runMaxSpeed; 
    public float runAcceleration;
    public float jumpForce;
    [HideInInspector] public float runAccelAmount; 
    public float runDecceleration; 
    [HideInInspector] public float runDeccelAmount; 
    [Space(10)]
    [Range(0.01f, 1)] public float accelInAir; 
    [Range(0.01f, 1)] public float deccelInAir;
    public bool doConserveMomentum;


    private void OnValidate()
    {

        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
    }
}