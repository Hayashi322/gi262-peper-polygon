using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform playerTransform; // ตัวแปรสำหรับเก็บตำแหน่งตัวละคร
    private void Start()
    {
        // ตั้งค่าเริ่มต้นให้กล้องติดตามตำแหน่งตัวละคร
        if (playerTransform != null)
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        }
    }
    // ฟังก์ชันนี้จะถูกเรียกใน PlayerRespawn เพื่ออัปเดตตำแหน่งของกล้อง
    public void UpdatePlayerTransform(Transform newPlayerTransform)
    {
        playerTransform = newPlayerTransform; // อัปเดตตำแหน่งตัวละครใหม่
    }
    private void LateUpdate()
    {
        // กล้องจะติดตามตัวละครในทุกๆ เฟรม
        if (playerTransform != null)
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        }
    }
}
