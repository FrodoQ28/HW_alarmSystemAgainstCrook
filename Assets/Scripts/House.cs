using System;
using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class House : MonoBehaviour
{
    public event Action housebreakingDetekted;
    public event Action housebreakingUndetekted;

    private void Start()
    {
        BoxCollider collider = GetComponent<BoxCollider> ();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        housebreakingDetekted?.Invoke();
    }

    private void OnTriggerExit(Collider collider)
    {
        housebreakingUndetekted?.Invoke();
    }
}