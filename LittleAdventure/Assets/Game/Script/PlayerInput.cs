using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float HorizontalInput;
    public float VerticalInput;
    public bool MouseButtonDown;

    void Update(){

        if( !MouseButtonDown && Time.deltaTime != 0){

            MouseButtonDown = Input.GetMouseButtonDown(0);
        }

        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }

    private void OnDisable() {
        
        MouseButtonDown = false;
        HorizontalInput = 0;
        VerticalInput = 0;
    }
}
