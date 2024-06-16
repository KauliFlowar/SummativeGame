using System;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGrabTransformer : OneGrabFreeTransformer, ITransformer
{
    [NonSerialized]
    private Rigidbody _rb;

    private bool _isGrabbed;

    public bool IsGrabbed
    {
        get => _isGrabbed;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void ITransformer.BeginTransform()
    {
        base.BeginTransform();
        _isGrabbed = true;
    }

    void ITransformer.EndTransform() { 
        base.EndTransform();
        _isGrabbed = false;
    }

    void ITransformer.UpdateTransform()
    {
        base.UpdateTransform();
        if (_rb != null)
            Physics.SyncTransforms();
    }
}
