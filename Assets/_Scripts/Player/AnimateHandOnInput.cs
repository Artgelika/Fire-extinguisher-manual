using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    private static readonly int GripHash = Animator.StringToHash("Grip");
    private static readonly int TriggerHash = Animator.StringToHash("Trigger");
    public InputActionProperty triggerValue;
    public InputActionProperty gripValue;

    public Animator handAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        float trigger = triggerValue.action.ReadValue<float>();
        float grip = gripValue.action.ReadValue<float>();

        handAnimator.SetFloat(TriggerHash, trigger);
        handAnimator.SetFloat(GripHash, grip);
    }
}
