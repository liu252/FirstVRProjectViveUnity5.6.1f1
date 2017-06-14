using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {


    private Color startColor;
    private Material material;

    public bool targetted = false;

	// Use this for initialization
	void Start ()
    {
        material = GetComponent<Renderer>().material;
        startColor = material.color;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(targetted)
        {
            Targeted();
            targetted = false;
        }
        else
        {
            Untargetted();
        }
	}

    public void Targeted()
    {
        material.color = Color.cyan;
    }

    public void Untargetted()
    {
        material.color = startColor;
    }  
}
