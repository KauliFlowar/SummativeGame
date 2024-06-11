using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDisablesCollision : MonoBehaviour, ITransformer
{
    private IGrabbable _grabbable;
    private Collider[] _colliders;

    private void Start()
    {
        _colliders = gameObject.GetComponentsInChildren<Collider>();
    }

    public void BeginTransform()
    {
        foreach (Collider collider in _colliders)
        {
            collider.enabled = false;
        }
    }

    public void EndTransform()
    {
        foreach (Collider collider in _colliders)
        {
            collider.enabled = true;
        }
    }

    public void Initialize(IGrabbable grabbable)
    {
        _grabbable = grabbable;
    }

    public void UpdateTransform()
    {
        transform.position = _grabbable.Transform.position;
        transform.rotation = _grabbable.Transform.rotation;
    }
}
