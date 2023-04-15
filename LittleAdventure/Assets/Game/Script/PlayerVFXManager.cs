using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    
    public VisualEffect FootStep;
    public ParticleSystem Blade01;

    public void Update_FootStep( bool state ){

        if( state ){
            FootStep.Play();
        }
        else{
            FootStep.Stop();
        }
    }

    public void PlayBlade01(){

        Blade01.Play();
    }
}
