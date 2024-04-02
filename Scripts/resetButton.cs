using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class resetButton : MonoBehaviour
{
    private HandGrabInteractable _interactable;
    private bool grabbed = false;
    public GameObject pingroup;

    void Start()
    {
        _interactable = gameObject.GetComponent<HandGrabInteractable>();
    }


    // Update is called once per frame
    void Update()
    {
        var hand = _interactable.Interactors.FirstOrDefault<HandGrabInteractor>();
        if (hand != null && !grabbed)
        {
            Debug.Log("RST");
            grabbed = true;
            var pins = GameObject.FindGameObjectsWithTag("pingroup");
            var spheres = GameObject.FindGameObjectsWithTag("sphere");
            foreach (var pin in pins)
            {
                Destroy(pin);
            }

            float offset = 0.0f;
            foreach (var sphere in spheres)
            {
                offset += 0.5f;
                sphere.transform.position = new Vector3(0.0f + offset, 0.345f, 0.0f);
            }


            Instantiate(pingroup);
  
        }
        else if (hand == null)
        {
            grabbed = false;
        }
    }
}
