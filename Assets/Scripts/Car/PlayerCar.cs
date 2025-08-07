using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Cars
{
    public class PlayerCar : Car
    {
        //Хранит в себе состояния контроллера машины игрока, аудио-слушателя, оповещает о начале/конце заезда записыватель инпутов.
        //[SerializeField] private AudioListener _audioListener;

        public event Action RaceStarted;
        public event Action RaceFinished;

        private async void Start()
        {
            await UniTask.Delay(300);
            Controller.enabled = false;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            StartButton.onClick.AddListener(async () =>
            {
                await UniTask.Delay(1000);
                Controller.enabled = true;
                //_audioListener.enabled = true;
                RaceStarted?.Invoke();
            });

            Trigger.CarFinished += (obj) =>
            {
                Controller.enabled = false;
                //_audioListener.enabled = false;
                RaceFinished?.Invoke();

                RigidBody.linearVelocity = Vector3.zero;
                RigidBody.transform.position = DefaultPosition;
                RigidBody.transform.rotation = Quaternion.identity;
            };
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            StartButton.onClick.RemoveListener(async () =>
            {
                await UniTask.Delay(1000);
                Controller.enabled = true;
                //_audioListener.enabled = true;
                RaceStarted?.Invoke();
            });

            Trigger.CarFinished -= (obj) =>
            {
                Controller.enabled = false;
                //_audioListener.enabled = false;
                RaceFinished?.Invoke();

                RigidBody.linearVelocity = Vector3.zero;
                RigidBody.transform.position = DefaultPosition;
                RigidBody.transform.rotation = Quaternion.identity;
            };
        }
    }
}
