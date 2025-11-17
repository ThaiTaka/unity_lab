using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Quản lý màn hình victory khi boss chết
/// </summary>
public class VictoryManager : MonoBehaviour
{
    [Header("Victory Video")]
    public VideoPlayer victoryVideoPlayer;
    public RawImage videoDisplay;
    public CanvasGroup videoCanvasGroup;
    
    [Header("Credits")]
    public GameObject creditsCanvas;
    public TextMeshProUGUI creditsText;
    public float creditsScrollSpeed = 50f;
    
    [Header("Thank You Screen")]
    public GameObject thankYouCanvas;
    public TextMeshProUGUI thankYouText;
    
    [Header("Settings")]
    public string menuSceneName = "Menu";
    
    private bool victoryTriggered = false;
    
    private string[] creditsContent = new string[]
    {
        "THANKS FOR PLAYING",
        "",
        "=== CREDITS ===",
        "",
        "Game Design",
        "Your Name Here",
        "",
        "Programming",
        "Your Name Here",
        "",
        "Art & Assets",
        "Unity Asset Store",
        "",
        "Special Thanks",
        "Faker - The GOAT",
        "T1 - World Champions",
        "",
        "Music & Sound",
        "Freesound.org",
        "",
        "=== THE END ===",
        "",
        "Press ESC to return to menu"
    };
    
    private void Start()
    {
        // ⚠️ QUAN TRỌNG: Đảm bảo không trigger ngay khi vào game
        victoryTriggered = false;
        
        // Ẩn tất cả UI ban đầu
        if (videoCanvasGroup != null)
        {
            videoCanvasGroup.alpha = 0;
            videoCanvasGroup.gameObject.SetActive(false);
        }
        
        if (creditsCanvas != null)
        {
            creditsCanvas.SetActive(false);
        }
        
        if (thankYouCanvas != null)
        {
            thankYouCanvas.SetActive(false);
        }
        
        // Setup video - QUAN TRỌNG: Tắt play on awake
        if (victoryVideoPlayer != null)
        {
            victoryVideoPlayer.Stop(); // Dừng nếu đang phát
            victoryVideoPlayer.playOnAwake = false; // Đảm bảo không tự phát
            victoryVideoPlayer.loopPointReached += OnVictoryVideoFinished;
        }
        
        Debug.Log("✅ VictoryManager initialized - Video KHÔNG phát tự động");
    }
    
    public void TriggerVictory()
    {
        if (victoryTriggered) return;
        
        victoryTriggered = true;
        StartCoroutine(VictorySequence());
    }
    
    private IEnumerator VictorySequence()
    {
        Debug.Log("🎉 VICTORY! Bắt đầu sequence...");
        
        // Pause game
        Time.timeScale = 0f;
        
        yield return new WaitForSecondsRealtime(2f);
        
        // Play victory video
        if (videoCanvasGroup != null && victoryVideoPlayer != null)
        {
            videoCanvasGroup.gameObject.SetActive(true);
            
            // Fade in
            yield return StartCoroutine(FadeCanvasGroup(videoCanvasGroup, 0, 1, 0.5f));
            
            // Play video
            victoryVideoPlayer.Play();
        }
        
        // Đợi video kết thúc
    }
    
    private void OnVictoryVideoFinished(VideoPlayer vp)
    {
        Debug.Log("✅ Victory video kết thúc! Hiện credits...");
        StartCoroutine(ShowCredits());
    }
    
    private IEnumerator ShowCredits()
    {
        // Fade out video
        if (videoCanvasGroup != null)
        {
            yield return StartCoroutine(FadeCanvasGroup(videoCanvasGroup, 1, 0, 0.5f));
            videoCanvasGroup.gameObject.SetActive(false);
        }
        
        // Hiện "Thanks For Playing"
        if (thankYouCanvas != null && thankYouText != null)
        {
            thankYouCanvas.SetActive(true);
            thankYouText.text = "THANKS FOR PLAYING";
            thankYouText.fontSize = 60;
            thankYouText.color = Color.white;
        }
        
        yield return new WaitForSecondsRealtime(3f);
        
        // Ẩn thank you, hiện credits
        if (thankYouCanvas != null)
        {
            thankYouCanvas.SetActive(false);
        }
        
        if (creditsCanvas != null && creditsText != null)
        {
            creditsCanvas.SetActive(true);
            
            // Build credits text
            string fullCredits = string.Join("\n", creditsContent);
            creditsText.text = fullCredits;
            
            // Scroll credits từ dưới lên
            yield return StartCoroutine(ScrollCredits());
        }
        
        // Resume game
        Time.timeScale = 1f;
        
        // Đợi input để quay về menu
        yield return StartCoroutine(WaitForReturnToMenu());
    }
    
    private IEnumerator ScrollCredits()
    {
        if (creditsText == null) yield break;
        
        RectTransform rectTransform = creditsText.GetComponent<RectTransform>();
        if (rectTransform == null) yield break;
        
        // Bắt đầu từ dưới màn hình
        float startY = -Screen.height;
        float endY = Screen.height + rectTransform.rect.height;
        
        rectTransform.anchoredPosition = new Vector2(0, startY);
        
        float currentY = startY;
        
        while (currentY < endY)
        {
            currentY += creditsScrollSpeed * Time.unscaledDeltaTime;
            rectTransform.anchoredPosition = new Vector2(0, currentY);
            yield return null;
        }
    }
    
    private IEnumerator WaitForReturnToMenu()
    {
        Debug.Log("⌨️ Press ESC to return to menu...");
        
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(menuSceneName);
                yield break;
            }
            
            yield return null;
        }
    }
    
    private IEnumerator FadeCanvasGroup(CanvasGroup group, float start, float end, float duration)
    {
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            group.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        
        group.alpha = end;
    }
}
