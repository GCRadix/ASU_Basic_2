using UnityEngine;

// This is a derived class from Monobehaviour, because we need Unity to call runtime functions like Update and Start.
public class ProximitySFX : MonoBehaviour
{
    // Minimum and Maximum Distance through which we interpolate the retriggering of our sound.
    public float minDistance = 10.0f;
    public float maxDistance = 30.0f;
    // The minimum and maximum Retrigger Rate in seconds. 
    // Note: This is from the start of each sound, so they will overlap at some point if the minTriggerRate is too short!
    public float minTriggerRate = 0.5f;
    public float maxTriggerRate = 1.5f;
    // Set this reference on the component in the Unity Editor.
    public AudioClip audioClip;
    // The other Component that acts as the "Speaker"
    public AudioSource audioSource;
    
    // Internal STATE that we use to track things.
    private float secondsSinceLastTrigger = 0.0f;
    private GameObject player = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    // We create things and cache references here, which saves performance on runtime.
    void Start()
    {
        // Especially a "Find" would be expensive to do every frame.
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();

        if (player == null || audioClip == null)
        {
            Debug.LogError("NO PLAYER FOUND (FOR PROXIMITY SFX)!!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 distanceV = playerPos - gameObject.transform.position;
        
        float distance = distanceV.magnitude;
        float retriggerTime = minTriggerRate;
        
        // This works, because if clauses are evaluated left to right. 
        // So `retriggerTime` is set here:  ||     and if ShouldRetrigger returns true,     ||
        //                                  \/     we use the new value here:               \/
        if (ShouldRetrigger(distance, out retriggerTime) && secondsSinceLastTrigger > retriggerTime)
        {
            // Reset State and Trigger
            secondsSinceLastTrigger = 0.0f;
            audioSource.PlayOneShot(audioClip);
        }
        
        Debug.LogWarning(secondsSinceLastTrigger);
    }

    // out declares that the parameter fed into the function is a reference, so we can change it's value
    // and use the new value in the calling function.
    bool ShouldRetrigger(float distance, out float retriggerTime)
    {
        // If we are too far away we don't trigger anything.
        if (distance > maxDistance)
        {
            secondsSinceLastTrigger += Time.deltaTime;
            retriggerTime = -1.0f;
            return false;
        }
        else // We are within range.
        {
            secondsSinceLastTrigger += Time.deltaTime;
            
            // Simplest conditions first. Below minDistance retriggerTime is just the minTriggerRate. 
            if (distance < minDistance)
            {
                retriggerTime = minTriggerRate;
            }
            else // now these are all other cases. Between min and max distance we have to interpolate.
            {
                // The amount of trigger rate that we interpolate and then add to minTriggerRate.
                float triggerRateDynamic = (maxTriggerRate - minTriggerRate);
                // The normalized factor (0-1) of where we are between minDistance and MaxDistance.
                float distanceFactor = (distance - minDistance) / (maxDistance - minDistance);
                // Now just multiply by the factor and add to minTriggerRate.
                retriggerTime = minTriggerRate + ( triggerRateDynamic * distanceFactor );
            }

            return true;
        }
    }
}
