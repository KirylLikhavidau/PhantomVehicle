using System;
using UnityEngine;
using Cars;

namespace Triggers
{
    [RequireComponent(typeof(BoxCollider))]
    public class FinishZone : MonoBehaviour
    {
        //Регистрирует - пришел ли игрок или призрак на финиш, отправляет событие о финише.
        [SerializeField] private PhantomCar _car;

        public event Action<Car> CarFinished;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCar car))
            {
                if (!_car.gameObject.activeSelf)
                    _car.gameObject.SetActive(true);
                CarFinished?.Invoke(car);
            }
            else if (other.TryGetComponent(out PhantomCar phantom))
            {
                CarFinished?.Invoke(phantom);
            }
        }
    }
}