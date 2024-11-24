using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // ทดสอบ Respawn ด้วยปุ่ม R
        {
            Respawn();
        }
    }
    void Respawn()
    {
        Vector3 checkpointPosition = CheckpointManager.instance.GetCheckpoint();
        if (checkpointPosition != Vector3.zero)
        {
            // ทำให้แกน Z ของตำแหน่งที่ respawn เป็น -10 เสมอ
            checkpointPosition.z = -10;
            // Respawn ตัวละครที่ตำแหน่ง Checkpoint
            transform.position = checkpointPosition;
            Debug.Log("Player Respawned at: " + checkpointPosition);
            // อัปเดตตำแหน่งที่กล้องติดตาม
            CameraFollow2D cameraFollow = Camera.main.GetComponent<CameraFollow2D>();
            cameraFollow.UpdatePlayerTransform(transform); // ส่งตำแหน่งตัวละครใหม่ให้กล้อง
        }
        else
        {
            Debug.LogWarning("No checkpoint set!");
        }
    }
}
