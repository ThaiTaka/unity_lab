using UnityEngine;

/// <summary>
/// Spawner để tạo Boss Anti T1 khi được trigger
/// </summary>
public class BossAntiT1Spawner : MonoBehaviour
{
    [Header("Boss Prefab")]
    public GameObject bossAntiT1Prefab;
    
    [Header("Spawn Position")]
    public Transform spawnPoint; // Vị trí spawn boss
    
    private bool bossSpawned = false;
    
    public void SpawnBoss()
    {
        if (bossSpawned)
        {
            Debug.LogWarning("⚠️ Boss đã được spawn rồi!");
            return;
        }
        
        if (bossAntiT1Prefab == null)
        {
            Debug.LogError("❌ Thiếu Boss Prefab!");
            return;
        }
        
        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
        
        GameObject boss = Instantiate(bossAntiT1Prefab, spawnPos, Quaternion.identity);
        boss.name = "Boss Anti T1";
        
        bossSpawned = true;
        
        Debug.Log("✅ Boss Anti T1 đã được spawn!");
    }
}
