using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Loading screen riêng cho Game → Victory Video
/// </summary>
public class Loading1Screen : MonoBehaviour
{
    [Header("UI References")]
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI tipText;
    public CanvasGroup canvasGroup;
    
    [Header("Settings")]
    public string targetSceneName = "VictoryVideoScene"; // Mặc định load Victory Video
    public float minLoadingTime = 2.0f;
    public float tipChangeInterval = 3.0f;
    
    // Static để lưu scene đích từ code khác
    private static string nextSceneToLoad = "";
    
    [Header("Loading Tips")]
    [TextArea(2, 5)]
    public string[] loadingTips = new string[]
    {
        "🎉 Bạn đã hoàn thành nhiệm vụ thu thập sao!",
        "👑 Chuẩn bị chiến đấu với Boss mạnh nhất!",
        "⚔️ Boss sẽ xuất hiện sau cutscene...",
        "💪 Hãy chuẩn bị vũ khí và vật phẩm tốt nhất!",
        "🔥 Trận chiến khó khăn sắp bắt đầu!",
        "⭐ Bạn đã chứng minh mình là chiến binh giỏi!",
        "🎬 Thưởng thức cutscene chiến thắng của bạn!"
    };
    
    private void Start()
    {
        Debug.Log("========================================");
        Debug.Log("🔄 LOADING1 SCENE STARTED");
        
        // Nếu có scene được set từ code, dùng nó
        if (!string.IsNullOrEmpty(nextSceneToLoad))
        {
            targetSceneName = nextSceneToLoad;
            nextSceneToLoad = ""; // Reset
            Debug.Log($"✅ Loading1 scene from CODE: {targetSceneName}");
        }
        else
        {
            Debug.Log($"⚠️ Loading1 scene from INSPECTOR: {targetSceneName}");
            Debug.LogWarning("⚠️ WARNING: nextSceneToLoad was empty! Using Inspector value!");
        }
        
        Debug.Log($"🎯 FINAL TARGET SCENE: {targetSceneName}");
        Debug.Log("========================================");
        
        // Bắt đầu loading
        StartCoroutine(LoadSceneAsync());
    }
    
    /// <summary>
    /// Static method để load scene từ bất kỳ đâu
    /// VD: Loading1Screen.LoadScene("VictoryVideoScene");
    /// </summary>
    public static void LoadScene(string sceneName)
    {
        Debug.Log("========================================");
        Debug.Log($"🎬 Loading1Screen.LoadScene() CALLED");
        Debug.Log($"🎯 Target Scene: {sceneName}");
        Debug.Log("========================================");
        
        nextSceneToLoad = sceneName;
        SceneManager.LoadScene("loading 1"); // ⚠️ TÊN SCENE PHẢI KHỚP BUILD SETTINGS
        
        Debug.Log($"🔄 Loading 'loading 1' scene to transition to {sceneName}");
    }
    
    private IEnumerator LoadSceneAsync()
    {
        // Fade in màn hình loading
        yield return StartCoroutine(FadeIn());
        
        // Bắt đầu load scene async
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);
        asyncLoad.allowSceneActivation = false;
        
        float startTime = Time.time;
        float currentTipTime = 0f;
        int currentTipIndex = 0;
        
        // Hiển thị tip đầu tiên
        if (tipText != null && loadingTips.Length > 0)
        {
            tipText.text = loadingTips[0];
        }
        
        float fakeProgress = 0f;
        
        while (!asyncLoad.isDone)
        {
            float realProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            float elapsedTime = Time.time - startTime;
            
            fakeProgress = Mathf.MoveTowards(fakeProgress, realProgress, Time.deltaTime * 0.5f);
            
            if (loadingBar != null)
            {
                loadingBar.value = fakeProgress;
            }
            
            if (loadingText != null)
            {
                loadingText.text = $"Loading... {Mathf.RoundToInt(fakeProgress * 100)}%";
            }
            
            // Đổi tip
            currentTipTime += Time.deltaTime;
            if (currentTipTime >= tipChangeInterval && loadingTips.Length > 0)
            {
                currentTipTime = 0f;
                currentTipIndex = (currentTipIndex + 1) % loadingTips.Length;
                if (tipText != null)
                {
                    tipText.text = loadingTips[currentTipIndex];
                }
            }
            
            // Đợi đủ thời gian tối thiểu VÀ load xong
            if (fakeProgress >= 0.99f && elapsedTime >= minLoadingTime)
            {
                asyncLoad.allowSceneActivation = true;
            }
            
            yield return null;
        }
        
        // Fade out trước khi chuyển scene
        yield return StartCoroutine(FadeOut());
    }
    
    private IEnumerator FadeIn()
    {
        if (canvasGroup == null) yield break;
        
        float duration = 0.5f;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            yield return null;
        }
        
        canvasGroup.alpha = 1f;
    }
    
    private IEnumerator FadeOut()
    {
        if (canvasGroup == null) yield break;
        
        float duration = 0.5f;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            yield return null;
        }
        
        canvasGroup.alpha = 0f;
    }
}
