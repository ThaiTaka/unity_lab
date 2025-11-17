using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName = "Wave 1";
        public GameObject zombiePrefab; // Enemy Zombie prefab
        public Transform spawnPosition; // Vị trí spawn
    }

    [Header("Wave Settings")]
    public Wave[] waves; // Danh sách các waves
    private int currentWaveIndex = 0;
    private GameObject currentZombie;
    private bool waveActive = false;

    [Header("Star System")]
    public int currentStars = 0;
    public GameObject starPrefab; // Prefab ngôi sao UI
    public Transform starContainer; // Container chứa stars trong UI
    private List<GameObject> spawnedStars = new List<GameObject>();

    [Header("Events")]
    public UnityEvent<int> onWaveComplete; // Event khi hoàn thành wave (số sao)
    public UnityEvent onAllWavesComplete; // Event khi hoàn thành tất cả

    [Header("UI")]
    public TMPro.TextMeshProUGUI waveText; // Hiển thị "Wave X/Y"
    
    [Header("Audio & Effects")]
    public AudioClip waveStartSound; // Âm thanh bắt đầu wave
    public AudioClip waveCompleteSound; // Âm thanh hoàn thành wave
    public AudioClip allWavesCompleteSound; // Âm thanh hoàn thành tất cả
    public GameObject waveCompleteEffect; // Particle effect khi hoàn thành wave
    private AudioSource audioSource;
    
    public static WaveManager instance;

    private void Awake()
    {
        instance = this;
        
        // Tạo AudioSource nếu chưa có
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        // Kiểm tra waves config
        if (waves == null || waves.Length == 0)
        {
            Debug.LogError("❌ WaveManager ERROR: Waves array is empty! Please configure waves in Inspector.");
            Debug.LogError("📋 SETUP GUIDE:\n" +
                          "1. Select WaveManager object in Hierarchy\n" +
                          "2. Inspector → Wave Settings → Waves: Size = 5\n" +
                          "3. For each Wave[0-4]:\n" +
                          "   - Wave Name: 'Wave 1', 'Wave 2', etc.\n" +
                          "   - Zombie Prefab: Drag Enemy Zombie prefab\n" +
                          "   - Spawn Position: Drag SpawnPoint object");
            return;
        }
        
        Debug.Log($"✅ WaveManager initialized with {waves.Length} waves");
        UpdateWaveUI();
        StartNextWave();
    }

    public void StartNextWave()
    {
        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("🎉 All waves completed!");
            PlaySound(allWavesCompleteSound);
            onAllWavesComplete?.Invoke();
            return;
        }

        waveActive = true;
        Wave currentWave = waves[currentWaveIndex];
        
        Debug.Log($"🌊 Starting {currentWave.waveName}");
        
        // Play wave start sound
        PlaySound(waveStartSound);
        
        // Validate wave configuration
        if (currentWave.zombiePrefab == null)
        {
            Debug.LogError($"⚠️ Wave {currentWaveIndex + 1}: Zombie Prefab is missing! Assign prefab in Inspector.");
            return;
        }
        
        if (currentWave.spawnPosition == null)
        {
            Debug.LogError($"⚠️ Wave {currentWaveIndex + 1}: Spawn Position is missing! Assign SpawnPoint in Inspector.");
            return;
        }
        
        // Spawn zombie at SpawnPoint position
        Vector3 spawnPos = currentWave.spawnPosition.position;
        Quaternion spawnRot = currentWave.spawnPosition.rotation;
        
        Debug.Log($"🔍 Attempting to spawn zombie prefab: {currentWave.zombiePrefab.name}");
        Debug.Log($"📍 Spawn position: {spawnPos}");
        
        currentZombie = Instantiate(currentWave.zombiePrefab, spawnPos, spawnRot);
        
        if (currentZombie == null)
        {
            Debug.LogError("❌ Failed to instantiate zombie!");
            return;
        }
        
        currentZombie.name = $"{currentWave.waveName}_Zombie"; // Rename for debugging
        
        // Force enable zombie and all children (in case prefab is disabled)
        currentZombie.SetActive(true);
        
        int childCount = currentZombie.transform.childCount;
        Debug.Log($"🔧 Zombie has {childCount} children. Enabling all...");
        
        foreach (Transform child in currentZombie.transform)
        {
            child.gameObject.SetActive(true);
            Debug.Log($"  ✓ Enabled child: {child.name}");
        }
        
        Debug.Log($"✅ Zombie spawned successfully!");
        Debug.Log($"   - Name: {currentZombie.name}");
        Debug.Log($"   - Position: {currentZombie.transform.position}");
        Debug.Log($"   - Active: {currentZombie.activeSelf}");
        Debug.Log($"   - Layer: {LayerMask.LayerToName(currentZombie.layer)}");
        
        // Check components
        var renderer = currentZombie.GetComponentInChildren<Renderer>();
        Debug.Log($"   - Has Renderer: {renderer != null}");
        if (renderer != null)
        {
            Debug.Log($"   - Renderer enabled: {renderer.enabled}");
            Debug.Log($"   - Material: {(renderer.material != null ? renderer.material.name : "NULL")}");
        }
        
        var navAgent = currentZombie.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Debug.Log($"   - Has NavMeshAgent: {navAgent != null}");
        if (navAgent != null)
        {
            Debug.Log($"   - NavAgent enabled: {navAgent.enabled}");
        }
        
        // Subscribe to zombie death event
        NPC zombieNPC = currentZombie.GetComponent<NPC>();
        if (zombieNPC != null)
        {
            zombieNPC.onDeath.AddListener(OnZombieDeath);
            Debug.Log($"📡 Subscribed to zombie death event");
            Debug.Log($"   - NPC Health: {zombieNPC.health}");
            Debug.Log($"   - NPC AI Type: {zombieNPC.aiType}");
        }
        else
        {
            Debug.LogError($"⚠️ Zombie prefab does NOT have NPC component! Wave system will not work!");
        }

        UpdateWaveUI();
    }

    private void OnZombieDeath()
    {
        if (!waveActive) return;

        Debug.Log($"✅ Zombie defeated! Wave {currentWaveIndex + 1} complete!");
        
        // Get zombie position before it's destroyed
        Vector3 zombiePosition = currentZombie != null ? currentZombie.transform.position : Vector3.zero;
        
        waveActive = false;
        
        // Play wave complete sound
        PlaySound(waveCompleteSound);
        
        // Spawn particle effect
        if (waveCompleteEffect != null && currentZombie != null)
        {
            Instantiate(waveCompleteEffect, currentZombie.transform.position, Quaternion.identity);
        }
        
        // ADD STAR to StarCollectionSystem
        if (StarCollectionSystem.instance != null)
        {
            StarCollectionSystem.instance.AddStar(zombiePosition);
        }
        else
        {
            // Fallback: Old star system
            currentStars++;
            AddStar();
        }
        
        // Trigger event
        onWaveComplete?.Invoke(currentStars);
        
        // Chuyển sang wave tiếp theo
        currentWaveIndex++;
        
        // Delay trước khi spawn wave mới
        Invoke("StartNextWave", 2f);
    }

    private void AddStar()
    {
        if (starPrefab != null && starContainer != null)
        {
            GameObject star = Instantiate(starPrefab, starContainer);
            spawnedStars.Add(star);
            
            Debug.Log($"⭐ Stars: {currentStars}/{waves.Length}");
        }
    }

    private void UpdateWaveUI()
    {
        if (waveText != null)
        {
            if (currentWaveIndex < waves.Length)
            {
                waveText.text = $"Wave {currentWaveIndex + 1}/{waves.Length}";
            }
            else
            {
                waveText.text = $"Complete! ⭐{currentStars}";
            }
        }
    }

    public Transform GetCurrentZombiePosition()
    {
        if (currentZombie != null)
        {
            return currentZombie.transform;
        }
        return null;
    }

    public bool IsWaveActive()
    {
        return waveActive && currentZombie != null;
    }
    
    // Stop all waves (called when player wins)
    public void StopAllWaves()
    {
        waveActive = false;
        CancelInvoke("StartNextWave");
        
        Debug.Log($"🛑 All waves stopped - Player collected all stars!");
    }
    
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
