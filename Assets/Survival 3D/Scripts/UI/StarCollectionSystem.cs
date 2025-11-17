using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarCollectionSystem : MonoBehaviour
{
    public static StarCollectionSystem instance;
    
    [Header("Star Settings")]
    public int maxStars = 6; // Số sao cần thu thập
    public int currentStars = 0; // Số sao hiện tại
    
    [Header("UI References")]
    public TextMeshProUGUI starCountText; // Text hiển thị số sao (VD: "⭐ 3/6")
    public GameObject victoryPanel; // Panel hiển thị khi thắng
    
    [Header("Star Visual (Optional)")]
    public GameObject starPrefab; // Prefab ngôi sao rơi từ zombie
    public float starDropHeight = 2f; // Độ cao ngôi sao xuất hiện
    
    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    private void Start()
    {
        UpdateStarUI();
        
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
        
        Debug.Log($"⭐ StarCollectionSystem initialized - Need {maxStars} stars to win!");
    }
    
    // Gọi khi zombie chết
    public void AddStar(Vector3 zombiePosition)
    {
        currentStars++;
        Debug.Log($"⭐ Star collected! {currentStars}/{maxStars}");
        
        // Spawn star visual effect (optional)
        if (starPrefab != null)
        {
            Vector3 starSpawnPos = zombiePosition + Vector3.up * starDropHeight;
            GameObject star = Instantiate(starPrefab, starSpawnPos, Quaternion.identity);
            
            // Animate star flying to UI (optional - can implement later)
            StartCoroutine(AnimateStarToUI(star));
        }
        
        // Update UI
        UpdateStarUI();
        
        // Check victory condition
        if (currentStars >= maxStars)
        {
            OnAllStarsCollected();
        }
    }
    
    private void UpdateStarUI()
    {
        if (starCountText != null)
        {
            // Display: "⭐ 3/6"
            starCountText.text = $"⭐ {currentStars}/{maxStars}";
        }
    }
    
    private void OnAllStarsCollected()
    {
        Debug.Log($"🎉 ALL STARS COLLECTED! Victory!");
        
        // Show victory panel
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        
        // Stop wave spawning
        if (WaveManager.instance != null)
        {
            WaveManager.instance.StopAllWaves();
        }
        
        // Optional: Pause game or show celebration
        // Time.timeScale = 0f; // Pause game
    }
    
    private IEnumerator AnimateStarToUI(GameObject star)
    {
        if (star == null) yield break;
        
        float duration = 1f;
        float elapsed = 0f;
        Vector3 startPos = star.transform.position;
        
        // Animate star moving up and fading
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            // Move up
            star.transform.position = startPos + Vector3.up * t * 3f;
            
            // Rotate
            star.transform.Rotate(Vector3.up, 360f * Time.deltaTime);
            
            // Scale down
            star.transform.localScale = Vector3.one * (1f - t);
            
            yield return null;
        }
        
        // Destroy after animation
        Destroy(star);
    }
    
    // Reset system (for testing or restart)
    public void ResetStars()
    {
        currentStars = 0;
        UpdateStarUI();
        
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
        
        Debug.Log($"⭐ Stars reset to 0");
    }
}
