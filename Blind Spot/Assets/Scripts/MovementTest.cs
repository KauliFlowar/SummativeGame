using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTest : MonoBehaviour
{
    public InputActionAsset actions;

    private InputAction _blah;

    // Start is called before the first frame update
    void Start()
    {
        InputActionMap actionMap = actions.FindActionMap("Gameplay", true);
        actionMap.Enable();

        _blah = actionMap["Move"];
    }

    // Update is called once per frame
    void Update() {
        Vector2 move = _blah.ReadValue<Vector2>();
        transform.Translate(move);
    }
}
