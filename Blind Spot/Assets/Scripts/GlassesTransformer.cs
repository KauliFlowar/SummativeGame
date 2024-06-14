using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GlassesTransformer : OneGrabFreeTransformer, ITransformer
{
    private PostProcessVolume _ppVolume;
    private DepthOfField _dof;

    private bool _attached;

    private MeshRenderer _meshRenderer;
    private Collider[] _colliders;

    private GameObject _potentialParent;


    private bool _isGrabbed;

    public bool IsGrabbed
    {
        get => _isGrabbed;
    }

    private void Start()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _colliders = gameObject.GetComponentsInChildren<Collider>();

        _ppVolume = GetComponent<PostProcessVolume>();
        if (!_ppVolume.profile.TryGetSettings<DepthOfField>(out _dof)) 
            throw new ApplicationException("NOOOOOOOOOOOOOOOO");
    }

    private void Update()
    {
        _dof.enabled.value = (transform.parent == null);
    }

    void ITransformer.BeginTransform()
    {
        transform.parent = null;

        base.BeginTransform();
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
        _isGrabbed = true;
    }

    void ITransformer.EndTransform()
    {
        base.EndTransform();
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }

        _isGrabbed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherObject = other.gameObject;
        if (otherObject.name == "CenterEyeAnchor")
        {
            transform.parent = otherObject.transform.parent;
        }
    }
}
