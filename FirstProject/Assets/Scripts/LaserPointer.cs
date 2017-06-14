using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {

    GameObject hitObject;
    RaycastHit hit;
    public Reticle reticle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            reticle.SetPosition(hit);
            hitObject = hit.transform.gameObject;
            Interactable interactable = hitObject.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.targetted = true;
            }
        }
        else
        {
            reticle.SetPosition();
        }
    }
}
