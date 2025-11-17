using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    //23:59 , 12:00
    [Range(0.0f,1.0f)]
    public float time ;
    public float fulldayLength;
  
    private float timeRate;
    public Vector3 noon;

    [Header("Sun Setting")] 
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;
    
    [Header("Moon Setting")] 
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Lightning Setting")] 
    public AnimationCurve lightningIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    public static DayNight instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeRate = 1.0f / fulldayLength;
        
        // Force start time to MIDNIGHT for dark atmosphere (survival game)
        time = 0.0f; // Midnight = dark/night time

        // Disable sun at start (night time)
        if (sun != null)
        {
            sun.gameObject.SetActive(false);
            sun.intensity = 0f;
        }
        
        // Enable moon at start
        if (moon != null)
        {
            moon.gameObject.SetActive(true);
        }

        // Set dark ambient lighting for night atmosphere
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.2f, 0.2f, 0.3f); // Dark blue tint
        RenderSettings.ambientIntensity = 0.3f; // Low intensity for night
        
        Debug.Log("DayNight initialized - Starting at MIDNIGHT (time = 0.0) - Dark atmosphere");
    }

    private void Update()
    {
        //zaman akışı
        time += timeRate * Time.deltaTime;
        if (time >= 1.0f)
        {
            time = 0.0f;
        }
        //ışık rotasyonu
        sun.transform.eulerAngles = (time ) * noon * 4.0f;
        moon.transform.eulerAngles = (time -0.5f) * noon * 4.0f;
        
        //light intensity
        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);
        
        //change colors
        sun.color = sunColor.Evaluate(time);
        moon.color = moonColor.Evaluate(time);
        
        //enable and disable the sun
        if (sun.intensity == 0 && sun.gameObject.activeInHierarchy)
        {
            sun.gameObject.SetActive(false);
        }
        else if (sun.intensity > 0 && !sun.gameObject.activeInHierarchy)
        {
            sun.gameObject.SetActive(true);
        }
        
        //enable and disable the moon
        if (moon.intensity == 0 && moon.gameObject.activeInHierarchy)
        {
            moon.gameObject.SetActive(false);
        }
        else if (moon.intensity > 0 && !moon.gameObject.activeInHierarchy)
        {
            moon.gameObject.SetActive(true);
        }
        
        //lightning and reflection intensity
        RenderSettings.ambientIntensity = lightningIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);



    }
    
}
