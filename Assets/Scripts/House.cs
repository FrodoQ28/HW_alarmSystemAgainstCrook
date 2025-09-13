using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class House : MonoBehaviour
{
    public event Action HousebreakingDetekted;
    public event Action HousebreakingUndetekted;

    private void Start()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Mover mover))
            HousebreakingDetekted?.Invoke();

    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Mover mover))
            HousebreakingUndetekted?.Invoke();
    }
}