using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.InputSystem;

public class HandPresence : MonoBehaviour
{
    [Header("Interactors")]
    public XRBaseInteractor leftXRBaseInteractor;
    public XRBaseInteractor rightXRBaseInteractor;
    
    public static GameObject leftHandHolden;
    public static GameObject rightHandHolden;     
    [Header("Hand Animations")]
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    [Header("Action Maps")] 
    public InputActionReference rightHandTrigger;
    public InputActionReference leftHandTrigger;
    public InputActionReference rightHandGrip;
    public InputActionReference leftHandGrip;

    private void Start()
    {
        Debug.Log("1");
        rightHandTrigger.action.started += SetrightHandTrigger;
        rightHandTrigger.action.canceled += SetrightHandTrigger;
        
        leftHandTrigger.action.started += SetLeftHandTrigger;
        leftHandTrigger.action.canceled += SetLeftHandTrigger;
        
        rightHandGrip.action.started += SetrightHandGrip;
        rightHandGrip.action.canceled += SetrightHandGrip;
        
        leftHandGrip.action.started += SetLeftHandGrip;
        leftHandGrip.action.canceled += SetLeftHandGrip;
        Debug.Log("2");
    }

    private void SetrightHandTrigger(InputAction.CallbackContext context)
    {
        rightHandAnimator.SetFloat("Trigger", context.ReadValue<float>());
        Debug.Log("3");
    }    
    private void SetLeftHandTrigger(InputAction.CallbackContext context)
    {
        leftHandAnimator.SetFloat("Trigger", context.ReadValue<float>());
    }    
    private void SetrightHandGrip(InputAction.CallbackContext context)
    {
        rightHandAnimator.SetFloat("Grip", context.ReadValue<float>());
    }    
    private void SetLeftHandGrip(InputAction.CallbackContext context)
    {
        leftHandAnimator.SetFloat("Grip", context.ReadValue<float>());
    }

    private void handAnimation(){
       //
       // leftHandAnimator.SetFloat("Trigger", getLeftTrigger());
       // leftHandAnimator.SetFloat("Grip", getLeftGrip());
       //
       // rightHandAnimator.SetFloat("Grip", getRightGrip());
   }
}
