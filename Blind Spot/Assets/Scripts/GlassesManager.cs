using System;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;

[RequireComponent(typeof(PhysicsGrabbable))]
public class GlassesManager : MonoBehaviour
{
    [SerializeField]
    private PhysicsGrabbable _physicsGrabbable;
    [SerializeField]
    private PostProcessVolume _postProcessVolume;
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Collider _target;

    private DepthOfField _nearsightedness;
    private MeshRenderer[] _meshRenderers;
    
    private bool _started;
    private bool _isTouchingTarget;

    private bool _isVisible = true;

    private bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.enabled = value;
            }
        }
    }

    private void Reset()
    {
        _physicsGrabbable = GetComponent<PhysicsGrabbable>();
        _postProcessVolume = GetComponent<PostProcessVolume>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        this.BeginStart(ref _started);
        this.AssertField(_physicsGrabbable, nameof(_physicsGrabbable));
        this.AssertField(_postProcessVolume, nameof(_postProcessVolume));
        this.AssertField(_target, nameof(_target));

        if (!_postProcessVolume.profile.TryGetSettings(out _nearsightedness))
            throw new ApplicationException("This class requires a depth-of-field override");

        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        
        this.EndStart(ref _started);
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
        // make glasses visible
        IsVisible = true;
        // unbind glasses from target
        transform.parent = null;
        // glasses are off, we can't see
        _nearsightedness.enabled.value = true;
    }
    
    private void OnAfterPhysicsEnable()
    {
        if (_isTouchingTarget)
        {
            // make sure glasses will stick to target
            _rigidbody.interpolation = RigidbodyInterpolation.None;
            _rigidbody.isKinematic = true;
            // stick glasses to target
            transform.parent = _target.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            // make glasses invisible
            IsVisible = false;
            // we can see now
            _nearsightedness.enabled.value = false;
        }
        else
        {
            // make physics apply to glasses
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            _rigidbody.isKinematic = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _target.gameObject)
        {
            _isTouchingTarget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _target.gameObject)
        {
            _isTouchingTarget = false;
        }
    }
}