using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FinishZone : MonoBehaviour
{
    [SerializeField] private PhantomCar _car;

    public event Action<Car> CarFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCar car))
        {
            if(!_car.gameObject.activeSelf)
                _car.gameObject.SetActive(true);
            CarFinished?.Invoke(car);
        }
        else if (other.TryGetComponent(out PhantomCar phantom))
        {
            CarFinished?.Invoke(phantom);
        }
    }
}