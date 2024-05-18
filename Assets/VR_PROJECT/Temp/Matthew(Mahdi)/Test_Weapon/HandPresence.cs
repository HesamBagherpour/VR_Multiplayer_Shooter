using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandPresence : MonoBehaviour
{
    [Header("Interactors")]
    public XRBaseInteractor leftXRBaseInteractor;
    public XRBaseInteractor rightXRBaseInteractor;
    
    public static GameObject leftHandHolden;
    public static GameObject rightHandHolden;
   
}
