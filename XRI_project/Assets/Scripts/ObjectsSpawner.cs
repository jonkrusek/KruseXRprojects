using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem.XR; // communicating with quest controllers 
/*
Select to spawn
Where object spawns
Cooldown period
Input - Button
Hand
*/

public class ObjectsSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // object to spawn
    public Transform spawnPoint; //where it spawns
    public XRNode controllerNode = XRNode.RightHand; // assigning right hand controller 
    public float spawnCooldown = 1.0f; // need a coroutine 
    private bool canSpawn = true; // Time in seconds between spawns

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if  (canSpawn && IsAButtonPressed())
        {
            StartCoroutine(SpawnObjectWithCooldown());
        }
    }

    bool IsAButtonPressed()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        bool buttonPressed = false;

        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed) && buttonPressed)
        {
            return true;
        }
        return false;
    }
    IEnumerator SpawnObjectWithCooldown()
    {
        canSpawn = false; //Prevent immediate respawning 
        SpawnObject();
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true; // alow us to spawn again 
    }

    void SpawnObject()
    {
        if(objectPrefab != null && spawnPoint != null)
        {
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        else
        {
            Debug.LogError("Assign objectPrefab and spawnPoint in the inspector");
        }
    }
}
