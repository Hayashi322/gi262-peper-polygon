using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectSpawner : MonoBehaviour
{
    // ตัวแปรสำหรับ Tilemap และ Tile ที่ใช้อยู่ในแผนที่
    public Tilemap floorTileMap;  // Tilemap สำหรับ FLOOR
    public Tile floorTile;        // Tile สำหรับ FLOOR
    public Tile wallTile;         // Tile สำหรับ WALL

    // ตัวแปรสำหรับเก็บวัตถุ (Object) ที่สามารถ spawn
    public GameObject[] objectPrefabs;  // อาร์เรย์ของวัตถุที่สามารถ spawn

    // ขนาดของพื้นที่ที่ต้องการ spawn วัตถุ (9x9)
    public Vector3Int spawnAreaSize = new Vector3Int(9, 9, 1);

    // ฟังก์ชันการ spawn วัตถุ (ทำการ spawn หลังจาก 5 วินาที)
    void Start()
    {
        // เรียก Coroutine เพื่อให้มันทำงานหลังจาก 5 วินาที
        StartCoroutine(WaitForTimeAndSpawn());
    }

    // Coroutine สำหรับการรอ 5 วินาที
    IEnumerator WaitForTimeAndSpawn()
    {
        // รอ 5 วินาที
        yield return new WaitForSeconds(2);

        // เริ่มกระบวนการ spawn วัตถุหลังจากรอ 2 วินาที
        SpawnObject();
    }

    // ฟังก์ชันการ spawn วัตถุ
    void SpawnObject()
    {
        // หาตำแหน่งที่เป็น FLOOR ในขอบเขต 9x9
        List<Vector3Int> floorPositions = new List<Vector3Int>();

        // กำหนดขอบเขตการ spawn (ตำแหน่งจากตรงกลางแผนที่)
        Vector3Int startPos = new Vector3Int((floorTileMap.cellBounds.xMin + floorTileMap.cellBounds.xMax) / 2,
                                              (floorTileMap.cellBounds.yMin + floorTileMap.cellBounds.yMax) / 2, 0);
        BoundsInt spawnArea = new BoundsInt(startPos.x - spawnAreaSize.x / 2, startPos.y - spawnAreaSize.y / 2, 0, spawnAreaSize.x, spawnAreaSize.y, 1);

        // ค้นหาตำแหน่งที่เป็น "FLOOR" ภายในพื้นที่ 9x9
        foreach (var pos in spawnArea.allPositionsWithin)
        {
            // ตรวจสอบว่าเป็นตำแหน่งที่เป็น FLOOR และไม่ใช่ WALL
            if (floorTileMap.HasTile(pos) && floorTileMap.GetTile(pos) == floorTile && !IsWall(pos))
            {
                floorPositions.Add(pos);
            }
        }

        // ถ้ามีตำแหน่งที่เป็น FLOOR ให้สุ่มตำแหน่ง
        if (floorPositions.Count > 0)
        {
            // สุ่มตำแหน่งจาก floorPositions ที่ได้
            Vector3Int randomPos = floorPositions[Random.Range(0, floorPositions.Count)];

            // สุ่มเลือกวัตถุจาก objectPrefabs ที่มี 2 แบบ
            GameObject randomObject = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

            // Spawn วัตถุในตำแหน่งที่สุ่ม
            Instantiate(randomObject, new Vector3(randomPos.x + 0.5f, randomPos.y + 0.5f, 0), Quaternion.identity);
        }
    }

    // ฟังก์ชันตรวจสอบว่าตำแหน่งเป็น "Wall" หรือไม่
    bool IsWall(Vector3Int position)
    {
        return floorTileMap.HasTile(position) && floorTileMap.GetTile(position) == wallTile;
    }
}
