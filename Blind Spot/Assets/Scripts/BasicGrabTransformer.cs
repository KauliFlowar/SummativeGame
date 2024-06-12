using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGrabTransformer : OneGrabFreeTransformer, ITransformer
{
    private IGrabbable _grabbable;
    private Collider[] _colliders;

    private bool _isGrabbed;

    public bool IsGrabbed
    {
        get => _isGrabbed;
    }

    private void Start()
    {
        _colliders = gameObject.GetComponentsInChildren<Collider>();
    }

    void ITransformer.BeginTransform()
    {
        base.BeginTransform();
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
        _isGrabbed = true;
    }

    void ITransformer.EndTransform() { 
        base.EndTransform(); 
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
        _isGrabbed = false;
    }
}
