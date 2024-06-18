using System;
using System.Linq;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    
    [SerializeField]
    private PhysicsGrabbable _physicsGrabbable;

    [SerializeField]
    private Grabbable _grabbable;

    [SerializeField]
    private GrabInteractable _grabInteractable;

    [SerializeField] private Transform _glassesTrigger;

    private void Awake()
    {
    }

    private void Reset()
    {
        _physicsGrabbable = GetComponent<PhysicsGrabbable>();
        _grabbable = GetComponent<Grabbable>();
        _grabInteractable = GetComponent<GrabInteractable>();
    }

    private void Update()
    {
        bool shouldEnable = _glassesTrigger.childCount > 0;
        _grabInteractable.enabled = shouldEnable;
        _grabbable.enabled = shouldEnable;
    }
    
    private void OnEnable()
    {
        _physicsGrabbable.BeforePhysicsDisable += OnBeforePhysicsDisable;
        _physicsGrabbable.AfterPhysicsEnable += OnAfterPhysicsEnable;
    }


    private void OnDisable()
    {
        
        _physicsGrabbable.BeforePhysicsDisable += OnBeforePhysicsDisable;
        _physicsGrabbable.AfterPhysicsEnable += OnAfterPhysicsEnable;
    }

    private void OnBeforePhysicsDisable()
    {
    }
    
    private void OnAfterPhysicsEnable()
    {
        if (_glassesTrigger.childCount > 0)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }
}