using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GrabStatus : MonoBehaviour
{
    public bool isGrabing = false;
    public GameObject rootGrable;
    private InteractorHandedness currentHandedness;

    [Header("First Hand")]
    public GameObject firstHandRight;
    public GameObject firstHandLeft;


    [Header("Second Hand")]
    public GameObject secondHandRight;
    public GameObject secondHandLeft;

    private void Start()
    {
        GetComponent<XRGrabInteractable>().selectEntered.AddListener(OnGrab);
        GetComponent<XRGrabInteractable>().selectExited.AddListener(OnRelease);
    }

    private void Update()
    {
        
    }

    public void OnGrab(SelectEnterEventArgs args){
        currentHandedness = args.interactorObject.handedness;
        if (currentHandedness == InteractorHandedness.Left) {
            HandPresence.leftHandHolden = args.interactableObject.transform.gameObject;
        }else if (currentHandedness == InteractorHandedness.Right){
            HandPresence.rightHandHolden = args.interactableObject.transform.gameObject;
        }
        if (this.gameObject.activeInHierarchy)
        {        
            StartCoroutine(setTimeOut(() => {
                isGrabing =true;
                if (HandPresence.leftHandHolden != null){
                    if (HandPresence.leftHandHolden.name == rootGrable.name){
                        firstHandLeft.SetActive(true);
                        firstHandRight.SetActive(false);
                    } else if (rootGrable != null){
                        if(rootGrable.name.Contains(HandPresence.leftHandHolden.name)){
                            secondHandLeft.SetActive(true);
                            secondHandRight.SetActive(false);
                        }
                    }
                }

                if (HandPresence.rightHandHolden != null){
                    if (HandPresence.rightHandHolden.name == rootGrable.name){
                        firstHandRight.SetActive(true);
                        firstHandLeft.SetActive(false);
                    }
                    else if (rootGrable != null){
                        if(rootGrable.name.Contains(HandPresence.rightHandHolden.name)){
                            secondHandRight.SetActive(true);
                            secondHandLeft.SetActive(false);
                        }
                    }
                }


            }, 0.01f));
            
        }

    } 

    public void OnRelease(SelectExitEventArgs args)
    {
        var lastHandedness = args.interactorObject.handedness;
        if (lastHandedness == InteractorHandedness.Left) {
            HandPresence.leftHandHolden = null;
        }else if (lastHandedness == InteractorHandedness.Right){
            HandPresence.rightHandHolden = null;
        }
        currentHandedness = InteractorHandedness.None;
        
        
        
        if (this.gameObject.activeInHierarchy)
        {        
            StartCoroutine(setTimeOut(() => {
                isGrabing =false;
                var rhh = HandPresence.rightHandHolden;
                var lhh = HandPresence.leftHandHolden;

                if(rhh == null){
                    firstHandRight.SetActive(false);
                    if(rootGrable != null)
                        secondHandRight.SetActive(false);
                }
                if(lhh == null){
                    firstHandLeft.SetActive(false);
                    if(rootGrable != null)
                        secondHandLeft.SetActive(false);
                }


            }, 0.01f));
        }
    }

    public delegate void Callback();
    public static IEnumerator setTimeOut(Callback callback, float time){
        yield return new WaitForSeconds(time);
        callback();
    }
}
