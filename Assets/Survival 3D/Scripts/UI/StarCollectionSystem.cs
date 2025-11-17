using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarCollectionSystem : MonoBehaviour
{
    public static StarCollectionSystem instance;
    
    [Header("Star Settings")]
    public int maxStars = 6; // Số sao cần thu thập
    public int currentStars = 0; // Số sao hiện tại
    
    [Header("UI References")]
    public TextMeshProUGUI starCountText; // Text hiển thị số sao (VD: "⭐ 3/6")
    public Transform starIconContainer; // Container chứa các star icons trên UI (OPTIONAL - có thể bỏ trống)
    public GameObject starIconPrefab; // Prefab của 1 star icon UI (OPTIONAL - có thể bỏ trống)
    public GameObject victoryPanel; // Panel hiển thị khi thắng (OPTIONAL - có thể bỏ trống)
    
    [Header("Star Visual (Optional)")]
    public GameObject starPrefab; // Prefab ngôi sao rơi từ zombie
    public float starDropHeight = 2f; // Độ cao ngôi sao xuất hiện
    
    [Header("Animation Settings")]
    public float starAnimationDuration = 0.5f;
    public AnimationCurve scaleAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("Audio")]
    public AudioClip starCollectSound;
    public AudioClip victorySound;
    private AudioSource audioSource;
    
    private List<GameObject> starIcons = new List<GameObject>();
    
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
        
        // Get or create AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    private void Start()
    {
        InitializeStarIcons();
        UpdateStarUI();
        
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
        
        // Setup font và alignment cho text - Sang xịn hơn
        if (starCountText != null)
        {
            starCountText.fontSize = 27;
            starCountText.fontStyle = TMPro.FontStyles.Bold;
            starCountText.alignment = TMPro.TextAlignmentOptions.Center;
            starCountText.color = Color.white;
            
            // Thêm outline để text nổi bật và đẹp hơn
            starCountText.outlineWidth = 0.2f;
            starCountText.outlineColor = new Color(0, 0, 0, 0.5f); // Viền đen mờ
            
            // Letter spacing để text trông rộng rãi, sang trọng hơn
            starCountText.characterSpacing = 2f;
            
            // Word spacing
            starCountText.wordSpacing = 5f;
        }
        
        Debug.Log($"⭐ StarCollectionSystem initialized - Need {maxStars} stars to win!");
    }
    
    // Khởi tạo các star icons trống
    private void InitializeStarIcons()
    {
        if (starIconContainer == null || starIconPrefab == null)
        {
            Debug.LogWarning("⚠️ Star Icon Container or Prefab not assigned!");
            return;
        }
        
        // Clear existing icons
        foreach (Transform child in starIconContainer)
        {
            Destroy(child.gameObject);
        }
        starIcons.Clear();
        
        // Create star icon slots
        for (int i = 0; i < maxStars; i++)
        {
            GameObject starIcon = Instantiate(starIconPrefab, starIconContainer);
            starIcon.name = $"Star_{i + 1}";
            
            // Set icon to inactive/grey state initially
            Image iconImage = starIcon.GetComponent<Image>();
            if (iconImage != null)
            {
                iconImage.color = new Color(0.3f, 0.3f, 0.3f, 0.5f); // Grey out
            }
            
            starIcons.Add(starIcon);
        }
    }
    
    // Gọi khi zombie chết
    public void AddStar(Vector3 zombiePosition)
    {
        if (currentStars >= maxStars) return; // Already won
        
        currentStars++;
        Debug.Log($"⭐ Star collected! {currentStars}/{maxStars}");
        
        // Play sound
        if (starCollectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(starCollectSound);
        }
        
        // Spawn star visual effect (optional)
        if (starPrefab != null)
        {
            Vector3 starSpawnPos = zombiePosition + Vector3.up * starDropHeight;
            GameObject star = Instantiate(starPrefab, starSpawnPos, Quaternion.identity);
            
            // Animate star flying to UI (optional - can implement later)
            StartCoroutine(AnimateStarToUI(star));
        }
        
        // Animate star icon
        if (starIcons.Count > 0 && currentStars <= starIcons.Count)
        {
            int starIndex = currentStars - 1;
            StartCoroutine(AnimateStarIcon(starIcons[starIndex]));
        }
        
        // Update UI
        UpdateStarUI();
        
        // Check victory condition
        if (currentStars >= maxStars)
        {
            OnAllStarsCollected();
        }
    }
    
    // Animate star icon khi được collect
    private IEnumerator AnimateStarIcon(GameObject starIcon)
    {
        Image iconImage = starIcon.GetComponent<Image>();
        if (iconImage == null) yield break;
        
        // Reset scale
        starIcon.transform.localScale = Vector3.zero;
        
        float elapsed = 0f;
        
        while (elapsed < starAnimationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / starAnimationDuration;
            
            // Animate scale với curve
            float scale = scaleAnimationCurve.Evaluate(t);
            starIcon.transform.localScale = Vector3.one * scale;
            
            // Change color from grey to yellow
            iconImage.color = Color.Lerp(
                new Color(0.3f, 0.3f, 0.3f, 0.5f), 
                Color.yellow, 
                t
            );
            
            // Add rotation
            starIcon.transform.Rotate(Vector3.forward, 360f * Time.deltaTime * 2f);
            
            yield return null;
        }
        
        // Final state
        starIcon.transform.localScale = Vector3.one;
        iconImage.color = Color.yellow;
    }
    
    private void UpdateStarUI()
    {
        if (starCountText != null)
        {
            // Dùng chữ "STARS" thay vì ký tự sao (vì font mặc định không support ★)
            starCountText.text = $"STARS  {currentStars} / {maxStars}";
            
            // Nếu muốn thử ký tự sao (cần font hỗ trợ Unicode):
            // starCountText.text = $"★ {currentStars} / {maxStars}";
            
            // Hoặc dùng text khác:
            // starCountText.text = $"Sao:  {currentStars} / {maxStars}";
            // starCountText.text = $"COLLECTED  {currentStars} / {maxStars}";
            
            // Set màu trắng cho toàn bộ text
            starCountText.color = Color.white;
        }
    }
    
    private void OnAllStarsCollected()
    {
        Debug.Log($"🎉 ĐỦ 6 SAO! Dừng spawn zombie!");
        
        // Play victory sound
        if (victorySound != null && audioSource != null)
        {
            audioSource.PlayOneShot(victorySound);
        }
        
        // STOP ZOMBIE SPAWNING - ĐÂY LÀ CHỨC NĂNG CHÍNH
        if (WaveManager.instance != null)
        {
            WaveManager.instance.StopAllWaves();
            Debug.Log("✅ Đã dừng spawn zombie!");
        }
        
        // OPTIONAL: Animate stars nếu có setup
        if (starIcons.Count > 0)
        {
            StartCoroutine(VictoryStarAnimation());
        }
        
        // OPTIONAL: Show victory panel nếu có setup
        if (victoryPanel != null)
        {
            StartCoroutine(ShowVictoryPanelDelayed(1.5f));
        }
        
        // ======================================
        // 🔥 THÊM SỰ KIỆN CỦA BẠN Ở ĐÂY:
        // ======================================
        // Ví dụ: Spawn boss, load level mới, unlock item, etc.
        // BossManager.instance.SpawnBoss();
        // SceneManager.LoadScene("NextLevel");
    }
    
    private IEnumerator ShowVictoryPanelDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
    }
    
    private IEnumerator VictoryStarAnimation()
    {
        // Animate all stars bouncing
        for (int i = 0; i < starIcons.Count; i++)
        {
            StartCoroutine(BounceStarIcon(starIcons[i], i * 0.1f));
        }
        yield return null;
    }
    
    private IEnumerator BounceStarIcon(GameObject starIcon, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        float bounceHeight = 20f;
        float bounceDuration = 0.3f;
        Vector3 originalPos = starIcon.transform.localPosition;
        
        // Bounce up
        float elapsed = 0f;
        while (elapsed < bounceDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / bounceDuration;
            float yOffset = Mathf.Sin(t * Mathf.PI) * bounceHeight;
            starIcon.transform.localPosition = originalPos + Vector3.up * yOffset;
            
            // Scale pulse
            float scale = 1f + Mathf.Sin(t * Mathf.PI) * 0.3f;
            starIcon.transform.localScale = Vector3.one * scale;
            
            yield return null;
        }
        
        starIcon.transform.localPosition = originalPos;
        starIcon.transform.localScale = Vector3.one;
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
        InitializeStarIcons();
        UpdateStarUI();
        
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
        
        Debug.Log($"⭐ Stars reset to 0");
    }
    
    // Get current star count
    public int GetCurrentStars()
    {
        return currentStars;
    }
    
    // Check if player has won
    public bool HasWon()
    {
        return currentStars >= maxStars;
    }
}
