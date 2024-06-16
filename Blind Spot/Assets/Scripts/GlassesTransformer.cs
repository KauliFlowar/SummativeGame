using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;

public class GlassesTransformer : BasicGrabTransformer, ITransformer
{
    public PostProcessVolume postProcessVolume;
    public Collider target;
    
    private DepthOfField _nearsighted;

    private bool _attached;

    private MeshRenderer[] _meshRenderers;
    private Collider[] _colliders;

    private bool _isTouchingTarget;
    private bool _followFace;
    
    private bool _isVisible = true;

    private Rigidbody _rb;
    private bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            foreach (var mesh in _meshRenderers)
                mesh.enabled = value;
        }
    }

    private bool _isTangible = true;

    private bool IsTangible
    {
        get => _isTangible;
        set
        {
            _isTangible = value;
            foreach (var collider in _colliders)
            {
                collider.enabled = value;
            }
        }
    }

    private void Start()
    {
        _meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        _colliders = gameObject.GetComponentsInChildren<Collider>();
        
        if (!postProcessVolume.profile.TryGetSettings(out _nearsighted)) 
            throw new ApplicationException("NOOOOOOOOOOOOOOOO");

        _rb = GetComponent<Rigidbody>();

        _nearsighted.enabled.value = true;
    }

    private void Update()
    {
        bool hasParent = transform.parent != null;
        _nearsighted.enabled.value = !hasParent;
        if (hasParent)
        {
            _rb.position = transform.position = transform.parent.position;
        }
    }

    void ITransformer.BeginTransform()
    {
        _rb.isKinematic = false;
        base.BeginTransform();
    }

    void ITransformer.EndTransform()
    {
        base.EndTransform();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target.gameObject)
        {
            _isTouchingTarget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target.gameObject)
        {
            _isTouchingTarget = false;
        }
    }
}
