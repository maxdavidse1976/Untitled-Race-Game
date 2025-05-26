using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    [SerializeField] List<Transform> _checkpoints = new List<Transform>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _checkpoints.Clear();
        //foreach (Checkpoint checkpoint in GetComponentInChildren<Checkpoint>())
        //{

        //}
    }
}
