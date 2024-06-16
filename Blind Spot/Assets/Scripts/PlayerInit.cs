using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerInit : MonoBehaviour
{
    private CharacterController _cc;
    
    private void Start()
    {
        _cc = GetComponent<CharacterController>();
        // _cc.detectCollisions = false;
    }
}
