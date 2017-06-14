using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public Transform reticleImageTransform;
    public Transform trackedPointer;

    [SerializeField]
    private float defaultDistance = 1f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = reticleImageTransform.localScale;
    }

    public void SetPosition()
    {
        reticleImageTransform.position = trackedPointer.position + trackedPointer.forward * defaultDistance;
        reticleImageTransform.localScale = originalScale;
    }
    
    public void SetPosition(RaycastHit hit)
    {
        reticleImageTransform.position = hit.point;
        reticleImageTransform.localScale = originalScale * hit.distance;
    }
}
