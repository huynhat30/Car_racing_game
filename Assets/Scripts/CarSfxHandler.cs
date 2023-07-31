using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
 
public class CarSfxHandler : MonoBehaviour
{

    [Header("Audio")]
    public AudioSource engineAudioSource;

    TopDownCarController topDownCarController;

    float desiredEnginePitch = 0.5f;
  

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        topDownCarController = GetComponentInParent<TopDownCarController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();
        
    }

    void UpdateEngineSFX()
    {
        //Handle engine SFX
        float velocityMagnitude = topDownCarController.GetVelocityMagnitude();

        //Increase the engine volume as the car goes faster
        float desiredEngineVolume = velocityMagnitude * 0.05f;

     
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

        
        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

}

