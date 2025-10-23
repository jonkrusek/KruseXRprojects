using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DoorFeatures : CoreFeatures
{
    [Header("Door Configuration")]
    [SerializeField]
    private Transform doorPivot; //specifically Y rotation 

    [SerializeField]
    private float maxAngle = 90.0f; //probably will need to be <90 degrees but okay starting point

    [SerializeField]
    private bool reverseAngleDirection = false; //Flips direction

    [SerializeField]
    private float doorSpeed = 2.0f;

    [SerializeField]
    private bool open = false;

    [SerializeField]
    private bool MakeKinematicOnOpen = false;

    [Header("Interactions Configurations")]
    [SerializeField]
    private XRSocketInteractor socketInteractor;

    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //When key gets close to the socket add a listener 
        //s = Shorthand, SelectEnterEvents
        socketInteractor?.selectEntered.AddListener((s) => //Abstraction - hiding complexity, simplified 
        {
            //OpenDoor();
            //PlayOnStart();
        });

        socketInteractor?.selectExited.AddListener((s) =>
        {
            PlayOnEnd();
            socketInteractor.socketActive = featureUsage == FeatureUsage.Once ? false : true; // Reusability 
        });
        //Doors with simple interactors may not require a "key". Also good for cabinets, drawers...
        simpleInteractable?.selectEntered.AddListener((s) =>
        {
            //OpenDoor();
        });
    }

    public void OpenDoor()
    {
        //If the door is not open, Play the OnStart Sound
        if(!open)
        {
            PlayOnStart();
            open = true;
            StartCoroutine(ProcessMotion());
        }
    }

    private IEnumerator ProcessMotion()
    {
        //Keep looking for whether the door is open or not 
        while (open)
        {
            var angle = doorPivot.localEulerAngles.y < 180 ? doorPivot.localEulerAngles.y :
                doorPivot.localEulerAngles.y - 360;

            angle = reverseAngleDirection ? Mathf.Abs(angle) : angle;

            if (angle <= maxAngle)
            {
                doorPivot?.Rotate(Vector3.up, doorSpeed * Time.deltaTime * (reverseAngleDirection ? -1 : 1));
            }

            else
            {
                //when done with opening turn off rigidBody
                open = false;
                var featureRigidBody = GetComponent<Rigidbody>();
                if (featureRigidBody != null && MakeKinematicOnOpen) featureRigidBody.isKinematic = true;
                {

                }
            }
                yield return null;
        }
    }
}
