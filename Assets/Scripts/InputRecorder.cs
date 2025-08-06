using Ashsvp;
using System.Collections.Generic;
using UnityEngine;

public class InputRecorder : MonoBehaviour
{
    public readonly string Acceleration = nameof(Acceleration);
    public readonly string Steer = nameof(Steer);
    public readonly string Brake = nameof(Brake);

    [SerializeField] private PlayerCar _playerCar;
    [SerializeField] private SimcadeVehicleController _playerController;

    public Dictionary<string, Queue<float>> RecordedInputs => _recordedInputs;

    private bool _isRecording = false;
    private Dictionary<string, Queue<float>> _recordedInputs;
    private Dictionary<string, Queue<float>> _recordingInputs;

    private void Awake()
    {
        CreateNewDictionary();
    }

    private void OnEnable()
    {
        _playerCar.RaceStarted += () =>
        {
            CreateNewDictionary();
            _isRecording = true;
        };

        _playerCar.RaceFinished += () =>
        {
            _isRecording = false;
            MoveInputsToRecorded();
        };
    }

    private void OnDisable()
    {
        _playerCar.RaceStarted -= () =>
        {
            CreateNewDictionary();
            _isRecording = true;
        };

        _playerCar.RaceFinished -= () =>
        {
            _isRecording = false;
            MoveInputsToRecorded();
        };
    }

    private void Update()
    {
        if (_isRecording)
        {
            if (_recordingInputs.TryGetValue(Acceleration, out var accelerationInputs) &&
                _recordingInputs.TryGetValue(Steer, out var steerInputs) &&
                _recordingInputs.TryGetValue(Brake, out var brakeInputs))
            {
                accelerationInputs.Enqueue(_playerController.accelerationInput);
                steerInputs.Enqueue(_playerController.steerInput);
                brakeInputs.Enqueue(_playerController.brakeInput);
            }
        }
    }

    private void MoveInputsToRecorded()
    {
        _recordedInputs = new Dictionary<string, Queue<float>>
            {
                { Acceleration, new Queue<float>() },
                { Steer, new Queue<float>() },
                { Brake, new Queue<float>() }
            };

        if (_recordingInputs.TryGetValue(Acceleration, out var accelerationInputs) &&
            _recordingInputs.TryGetValue(Steer, out var steerInputs) &&
            _recordingInputs.TryGetValue(Brake, out var brakeInputs))
        {
            if (_recordedInputs.TryGetValue(Acceleration, out var recordedAccelerationInputs) &&
            _recordedInputs.TryGetValue(Steer, out var recordedSteerInputs) &&
            _recordedInputs.TryGetValue(Brake, out var recordedBrakeInputs))
            {
                while (accelerationInputs.Count > 0)
                    recordedAccelerationInputs.Enqueue(accelerationInputs.Dequeue());
                while (steerInputs.Count > 0)
                    recordedSteerInputs.Enqueue(steerInputs.Dequeue());
                while (brakeInputs.Count > 0)
                    recordedBrakeInputs.Enqueue(brakeInputs.Dequeue());
            }
        }
    }
    private void CreateNewDictionary()
    {
        _recordingInputs = new Dictionary<string, Queue<float>>
        {
            { Acceleration, new Queue<float>() },
            { Steer, new Queue<float>() },
            { Brake, new Queue<float>() }
        };
    }
}
