using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour
{
    float smokeEmissionRate = 0;

    TopDownCarController topDownCarController;
    ParticleSystem particleSystem;
    ParticleSystem.EmissionModule emissionModule;

    void Awake() {
        topDownCarController = GetComponentInParent<TopDownCarController>();
        particleSystem = GetComponent<ParticleSystem>();

        emissionModule = particleSystem.emission;
        emissionModule.rateOverTime = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        smokeEmissionRate = Mathf.Lerp(smokeEmissionRate, 0, Time.deltaTime * 5);
        emissionModule.rateOverTime = smokeEmissionRate;

        if (topDownCarController.TireOnDrift(out float laterVel, out bool isBraking)){
            if (isBraking)
            {
                smokeEmissionRate = 50;
            }

            else { 
                smokeEmissionRate = Mathf.Abs(laterVel) * 2; 
            }
        }

        else
        {
            smokeEmissionRate = 20;
        }
    }
}
