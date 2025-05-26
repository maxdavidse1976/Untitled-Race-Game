using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] int _checkpointIndex;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player checkpoint tracker
            // if tracker exists
            // Say we reached the checkpoint
        }
    }
}
