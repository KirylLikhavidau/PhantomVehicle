using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Inputs;

namespace Cars
{
    public class PhantomCar : Car
    {
        //Берет инпуты из записывателя(рекордера), кэширует их в поля методом
        //CacheCollections, изменяет инпуты в контроллере(в Update), обновляя кэшированные
        //инпуты из рекордера. Также как и PlayerCar отвечает за состояние контроллера. 
        [SerializeField] private InputRecorder _recorder;

        private bool _isRaceStarted = false;
        private Queue<float> _accelerationInputs;
        private Queue<float> _steerInputs;
        private Queue<float> _brakeInputs;

        protected override void OnEnable()
        {
            base.OnEnable();
            StartButton.onClick.AddListener(async () =>
            {
                _isRaceStarted = true;
                CacheCollections();
                await UniTask.Delay(100);
                Controller.enabled = true;
            });

            Trigger.CarFinished += (obj) =>
            {
                Controller.enabled = false;
                _isRaceStarted = false;
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
                _isRaceStarted = true;
                CacheCollections();
                await UniTask.Delay(1000);
                Controller.enabled = true;
            });

            Trigger.CarFinished -= (obj) =>
            {
                Controller.enabled = false;
                _isRaceStarted = false;
                RigidBody.linearVelocity = Vector3.zero;
                RigidBody.transform.position = DefaultPosition;
                RigidBody.transform.rotation = Quaternion.identity;
            };
        }

        private void Update()
        {
            if (_isRaceStarted && _accelerationInputs != null &&
                _steerInputs != null && _brakeInputs != null)
            {
                if (_accelerationInputs.TryDequeue(out var accelerationInput))
                    Controller.accelerationInput = accelerationInput;
                if (_steerInputs.TryDequeue(out var steerInput))
                    Controller.steerInput = steerInput;
                if (_brakeInputs.TryDequeue(out var brakeInput))
                    Controller.brakeInput = brakeInput;
            }
        }

        private void CacheCollections()
        {
            if (_recorder.RecordedInputs != null)
            {
                if (_recorder.RecordedInputs.TryGetValue(_recorder.Acceleration, out var accelerationInputs) &&
                _recorder.RecordedInputs.TryGetValue(_recorder.Steer, out var steerInputs) &&
                _recorder.RecordedInputs.TryGetValue(_recorder.Brake, out var brakeInputs))
                {
                    _accelerationInputs = accelerationInputs;
                    _steerInputs = steerInputs;
                    _brakeInputs = brakeInputs;
                }
            }
        }
    }
}