using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FinishZone : MonoBehaviour
{
    public event Action<Car> CarFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCar car))
        {
            CarFinished?.Invoke(car);
        }
        else if (other.TryGetComponent(out PhantomCar phantom))
        {
            CarFinished?.Invoke(phantom);
        }
    }
}