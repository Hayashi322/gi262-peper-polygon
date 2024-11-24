using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    private Vector2 checkpointPosition = Vector2.zero;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void SetCheckpoint(Vector2 position)
    {
        checkpointPosition = position;
        Debug.Log("Checkpoint set at: " + checkpointPosition);
    }
    public Vector2 GetCheckpoint()
    {
        return checkpointPosition;
    }
}
