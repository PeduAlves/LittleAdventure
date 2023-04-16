using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    
    public VisualEffect FootStep;
    public ParticleSystem Blade01;
    private Character _character;
    public float SecondsToWait = 0.9f;

    private void Awake(){

        _character = GetComponent<Character>();
    }

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
        AttackAnimationEnds();
    }
    public void AttackAnimationEnds(){

        print("entrando na AttackAnimationEnds");
        
        _character.SwtichStateTo( Character.CharacterState.Normal);
        print("saindo na AttackAnimationEnds");
    }

}

    