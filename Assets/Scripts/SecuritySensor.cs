using System;
using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class SecuritySensor : MonoBehaviour
{
    private Vector3 _scaleCollider = new Vector3(93f, 20f, 71f);
    private Vector3 _positionCollider = new Vector3(116f, 7f, -40f);

    public event Action HousebreakingDetekted;
    public event Action HousebreakingUndetekted;

    private void Start()
    {
        BoxCollider collider = GetComponent<BoxCollider> ();

        collider.transform.position = _positionCollider;
        collider.size = _scaleCollider;
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