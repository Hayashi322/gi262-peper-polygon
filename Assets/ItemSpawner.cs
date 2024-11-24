using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemSpawner : MonoBehaviour
{
    // ตัวแปรสำหรับ Tilemap และ Tile ที่ใช้อยู่ในแผนที่
    public Tilemap floorTileMap;  // Tilemap สำหรับ FLOOR
    public Tile Floor;            // Tile สำหรับ FLOOR
    public Tile wallTile;         // Tile สำหรับ WALL
                                  // ตัวแปรสำหรับเก็บไอเท็มที่สามารถ spawn
    public GameObject[] itemPrefabs;  // อาร์เรย์ของไอเท็มที่สามารถ spawn
    public float spawnInterval = 2f;  // เวลาระหว่างการ spawn ไอเท็มใหม่ (ในวินาที)
                                      // ฟังก์ชันการ spawn ไอเท็ม
    IEnumerator Start()
    {
        while (true)
        {
            // สุ่มตำแหน่งที่เป็น Floor (FLOOR) ใน Tilemap
            List<Vector3Int> floorPositions = new List<Vector3Int>();
            // ทำการค้นหาตำแหน่งทั้งหมดที่เป็น FLOOR
            foreach (var pos in floorTileMap.cellBounds.allPositionsWithin)
            {
                // ตรวจสอบว่าเป็นตำแหน่งที่เป็น FLOOR และไม่ใช่ WALL
                if (floorTileMap.HasTile(pos) && floorTileMap.GetTile(pos) == Floor && !IsWall(pos))
                {
                    floorPositions.Add(pos);
                }
            }
            // ถ้ามีตำแหน่งที่เป็น FLOOR ให้สุ่มตำแหน่ง
            if (floorPositions.Count > 0)
            {
                Vector3Int randomPos = floorPositions[Random.Range(0, floorPositions.Count)];
                GameObject randomItem = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
                // Spawn ไอเท็มในตำแหน่งที่สุ่ม
                Instantiate(randomItem, new Vector3(randomPos.x + 0.5f, randomPos.y + 0.5f, 0), Quaternion.identity);
            }
            // รอเวลาที่กำหนดแล้ว spawn ใหม่
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    // ฟังก์ชันตรวจสอบว่าตำแหน่งเป็น "Wall" หรือไม่
    bool IsWall(Vector3Int position)
    {
        return floorTileMap.HasTile(position) && floorTileMap.GetTile(position) == wallTile;
    }
}
