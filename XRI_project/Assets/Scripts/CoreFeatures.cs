using UnityEngine;
using UnityEngine.XR;

public enum FeatureUsage
{
    Once, //use once 
    Toggle //if you want to use the feature more than once 
}

public class CoreFeatures : MonoBehaviour
{
    /* Property - Common way to access code that exists outside of this class
     * Can create public variable and access them that way , or you can use properties 
     * Properties ENCAPSULATES variables as fields 
     * GET Accessor (READ) - returns encapsulated variable values 
     * SET Accessor (WRITE) - allocates new values to the property fields 
     * PROPERTY values use PascalCase.
     */

    public bool AudioSFXSourceCreated { get; set; }

    [field: SerializeField]
    public AudioClip AudioCLipOnStart { get; set; }

    [field: SerializeField]
    public AudioClip AudioCLipOnEnd { get; set; }

    private AudioSource audioSource;

    public FeatureUsage featureUsage = FeatureUsage.Once;

    protected virtual void Awake()
    {
        MakeSFXAudioSource(); 
    }
    
    public void MakeSFXAudioSource()
    {
        audioSource = GetComponent<AudioSource>();

        //If this is equal to null, create it here 

        if(audioSource != null )
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        //whether it null or not we still need to make sure this is true
        //On awake create this audiosource 

        AudioSFXSourceCreated = true;
    }
}
