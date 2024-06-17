using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger
    : MonoBehaviour
{
    public Transform glassesTrigger;
    public GameObject door;

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
                // warp to end screen
                SceneManager.LoadScene("EndScreen");

            }
            else
            {
                // reset door rotation, warp player back into level if possible
            }
        }
    }
}
