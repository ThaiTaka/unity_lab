using System;
using System.Collections;
using System.Collections.Generic;
using PolyverseSkiesAsset;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Bed : Buildings, IInteractable
{

    public float wakupTime;
    public float startSleepTime;
    public float endSleepTime;
    public float sleepToGive;
    public RawImage FadeScreen2;

    public string GetInteractPrompt()
    {
        return CanSleep() ? "Sleep" : "It's too early to sleep";
    }
    
    private void Start()
    {
        // Tìm RawImage có tên "FadeScreen" hoặc tag cụ thể, KHÔNG PHẢI VideoDisplay!
        GameObject fadeObj = GameObject.Find("FadeScreen");
        if (fadeObj != null)
        {
            FadeScreen2 = fadeObj.GetComponent<RawImage>();
        }
        
        if (FadeScreen2 == null)
        {
            Debug.LogWarning("⚠️ Bed: FadeScreen not found! Sleep animation won't work.");
        }
    }

   

    public void OnInteract()    
    {
        if (CanSleep())
        {
            if (FadeScreen2 != null)
            {
                Animation anim = FadeScreen2.GetComponent<Animation>();
                if (anim != null)
                {
                    anim.Play("sleep_anim");
                }
                else
                {
                    Debug.LogWarning("⚠️ FadeScreen doesn't have Animation component!");
                }
            }

            PolyverseSkies.instance.gameObject.SetActive(false);
            
            PolyverseSkies.instance.gameObject.SetActive(true);
            
            
            DayNight.instance.time = wakupTime;
            PolyverseSkies.instance.timeOfDay = 0.0f;
           
            PlayerNeeds.instance.Sleep(sleepToGive);
            
        }
    }
    

    bool CanSleep()
    {
        return DayNight.instance.time >= startSleepTime || DayNight.instance.time < endSleepTime;
        
    }
    
}
    