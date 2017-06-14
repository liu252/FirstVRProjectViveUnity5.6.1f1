using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickUpParent : MonoBehaviour {

    SteamVR_Controller.Device device;
    SteamVR_TrackedObject trackedObj;

    [Header("Haptics")]
    [Space(5)]
    public int pulseCount = 0;
    [Range(0.0f, 5.0f)]
    public float pulseLength = 0;
    [Range(0.0f, 5.0f)]
    public float pulsePause = 0;
    [Space(10)]
    public GameObject prefabSphere;

    private bool hold = false;
    void Awake ()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	void FixedUpdate ()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if(device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            GameObject.Instantiate(prefabSphere);
            //sphere.transform.position = Vector3.zero;
            //sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

	}

    void OnTriggerStay(Collider col)
    {
        
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && !hold)
        {
            if ("Interactable".Equals(col.gameObject.tag))
            {
                col.attachedRigidbody.isKinematic = true;
                col.gameObject.transform.SetParent(this.gameObject.transform);
            }
            

        }
        else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) && !hold)
        {
            if ("Interactable".Equals(col.gameObject.tag))
            {
                hold = true;
            }
        }
        else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) && hold)
        {
            col.gameObject.transform.SetParent(null);
            col.attachedRigidbody.isKinematic = false;

            tossObject(col.attachedRigidbody);
            
            hold = false;

        }
    }

    void OnTriggerEnter(Collider col)
    {
        StartCoroutine(HapticPatternPulse(pulseCount, pulseLength, pulsePause));
    }

    IEnumerator HapticSinglePulse(float length)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            device.TriggerHapticPulse();
            yield return null;
        }
    }

    IEnumerator HapticPatternPulse(int pulseCount, float pulseLength, float pauselength)
    {
        for (int i = 0; i < pulseCount; i++)
        {
            yield return StartCoroutine(HapticSinglePulse(pulseLength));
            yield return new WaitForSeconds(pauselength);
        }
    }

    void tossObject(Rigidbody rigidbody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
        {
            rigidbody.velocity = origin.TransformVector(device.velocity);
            rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
        }
        else
        {
            rigidbody.velocity = device.velocity;
            rigidbody.angularVelocity = device.angularVelocity;
        }
    }
}
