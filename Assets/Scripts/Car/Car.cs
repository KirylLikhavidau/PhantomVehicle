using Ashsvp;
using UnityEngine;
using UnityEngine.UI;
using Triggers;

namespace Cars
{
    public abstract class Car : MonoBehaviour
    {
        //Юазовый класс для PhantomCar и PlayerCar, ставит на дефолтное место машину после нажатия
        //кнопки старт, а также хранит поля, которые используются в дочерних классах
        [SerializeField] protected FinishZone Trigger;
        [SerializeField] protected SimcadeVehicleController Controller;
        [SerializeField] protected Button StartButton;
        [SerializeField] protected Rigidbody RigidBody;

        protected Vector3 DefaultPosition;

        private void Awake()
        {
            DefaultPosition = RigidBody.transform.position;
        }

        protected virtual void OnEnable()
        {
            StartButton.onClick.AddListener(() =>
            {
                RigidBody.linearVelocity = Vector3.zero;
                RigidBody.transform.position = DefaultPosition;
                RigidBody.transform.rotation = Quaternion.identity;
            });
        }
        protected virtual void OnDisable()
        {
            StartButton.onClick.RemoveListener(() =>
            {
                RigidBody.linearVelocity = Vector3.zero;
                RigidBody.transform.position = DefaultPosition;
                RigidBody.transform.rotation = Quaternion.identity;
            });
        }
    }
}