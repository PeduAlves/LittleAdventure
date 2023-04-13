using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    
    public VisualEffect FootStep;

    public void Update_FootStep( bool state ){

        if( state ){
            FootStep.Play();
        }
        else{
            FootStep.Stop();
        }
    }
}
