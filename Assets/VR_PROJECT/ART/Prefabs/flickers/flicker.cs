using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using TMPro;

public class flicker : MonoBehaviour
{
    public Light noor;
    public float mintime;
    public float maxtime;
    private float timer;
    private float thresholdMax;
    private float thresholdMin;

    public float amount = 1;
    public float timeBetween = 1;

    public bool isOn;
    void Start()
    {
        setValue();
    }

    // Update is called once per frame
    void Update()
    {
        flickerLight();
    }

    void flickerLight()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            StartCoroutine(LerpLight());
        }    
    }

    IEnumerator LerpLight()
    {
        if (isOn == true)
        {
            while (noor.intensity > thresholdMin)
            {
                noor.intensity -= amount;
                yield return new WaitForSeconds(timeBetween);
            }
            isOn = false;
        }
        else if (isOn == false)
        {
            while (noor.intensity < thresholdMax)
            {
                noor.intensity += amount;
                yield return new WaitForSeconds(timeBetween);
            }
            isOn = true;
        }
        setValue();
    }

    void setValue()
    {
        timer = Random.Range(mintime, maxtime);
        thresholdMin = Random.Range(0, 10);
        thresholdMax = Random.Range(11, 30);  
        
        
        
    }
}
