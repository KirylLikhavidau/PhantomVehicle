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
        [SerializeField] private AudioSource[] _audios;

        protected Vector3 DefaultPosition;

        private void Awake()
        {
            DefaultPosition = RigidBody.transform.position;
        }

        protected virtual void OnEnable()
        {
            StartButton.onClick.AddListener(() =>
            {
                foreach (var audio in _audios)
                    audio.mute = false;
                RigidBody.linearVelocity = Vector3.zero;
                RigidBody.transform.position = DefaultPosition;
                RigidBody.transform.rotation = Quaternion.identity;
            });

            Trigger.CarFinished += (obj) =>
            {
                foreach (var audio in _audios)
                    audio.mute = true;
            };
        }
        protected virtual void OnDisable()
        {
            StartButton.onClick.RemoveListener(() =>
            {
                foreach (var audio in _audios)
                    audio.mute = false;
                RigidBody.linearVelocity = Vector3.zero;
                RigidBody.transform.position = DefaultPosition;
                RigidBody.transform.rotation = Quaternion.identity;
            });

            Trigger.CarFinished -= (obj) =>
            {
                foreach (var audio in _audios)
                    audio.mute = true;
            };
        }
    }
}