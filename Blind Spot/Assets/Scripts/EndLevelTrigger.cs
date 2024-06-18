using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger
    : MonoBehaviour
{
    public Transform glassesTrigger;
    public GameObject door;
    public GrabInteractor grabSource;

    private (Vector3, Quaternion) _initialTransform;

    private void Awake()
    {
        SceneManager.LoadScene("EndScreen");
        _initialTransform = (door.transform.position, door.transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == door)
        {
            if (glassesTrigger.childCount > 0)
            {
                Debug.Log("Joever");
                // warp to end screen
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("EndScreen"));
            }
            else
            {
                // reset door rotation, warp player back into level if possible
                door.transform.position = _initialTransform.Item1;
                door.transform.rotation = _initialTransform.Item2;
                if (grabSource != null)
                    grabSource.ForceRelease();
            }
        }
    }
}
