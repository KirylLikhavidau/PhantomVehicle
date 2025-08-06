using Ashsvp;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public abstract class Car : MonoBehaviour
{
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
        StartButton.onClick.AddListener( () =>
        {
            RigidBody.linearVelocity = Vector3.zero;
            RigidBody.transform.position = DefaultPosition;
            RigidBody.transform.rotation = Quaternion.identity;
        });
    }
    protected virtual void OnDisable()
    {
        StartButton.onClick.RemoveListener( () =>
        {
            RigidBody.linearVelocity = Vector3.zero;
            RigidBody.transform.position = DefaultPosition;
            RigidBody.transform.rotation = Quaternion.identity;
        });
    }
}
