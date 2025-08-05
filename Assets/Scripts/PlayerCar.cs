using Ashsvp;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCar : Car
{
    [SerializeField] private SimcadeVehicleController _controller;
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] protected FinishZone _trigger;

    private async void Start()
    {
        await Task.Delay(300);
        _controller.enabled = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartButton.onClick.AddListener(async () =>
        {
            await Task.Delay(1000);
            _controller.enabled = true;
            _audioListener.enabled = true;
        });

        _trigger.CarFinished += (obj) => 
        {
            _controller.enabled = false;
            _audioListener.enabled = false;

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
            await Task.Delay(1000);
            _controller.enabled = true;
            _audioListener.enabled = true;
        });

        _trigger.CarFinished -= (obj) =>
        {
            _controller.enabled = false;
            _audioListener.enabled = false;

            RigidBody.linearVelocity = Vector3.zero;
            RigidBody.transform.position = DefaultPosition;
            RigidBody.transform.rotation = Quaternion.identity;
        };
    }
}
