using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Run : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        
        if(  animator.GetComponent<PlayerVFXManager>() != null ){

        animator.GetComponent<PlayerVFXManager>().Update_FootStep(true);
        }
    }
  
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){  

        if(  animator.GetComponent<PlayerVFXManager>() != null ){
            
        animator.GetComponent<PlayerVFXManager>().Update_FootStep(false);
        }
    }

}
