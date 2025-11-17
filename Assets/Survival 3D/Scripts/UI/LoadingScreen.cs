using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Màn hình loading với thanh progress bar và tips
/// </summary>
public class LoadingScreen : MonoBehaviour
{
    [Header("UI References")]
    public Image loadingBar; // Thanh loading (Fill Amount)
    public TextMeshProUGUI loadingText; // Text "Loading..."
    public TextMeshProUGUI tipText; // Text hiển thị tips
    public CanvasGroup canvasGroup;
    
    [Header("Settings")]
    public string targetSceneName = "Game"; // Scene cần load
    public float minLoadingTime = 2.0f; // Thời gian loading tối thiểu (để người chơi đọc tip)
    public float tipChangeInterval = 3.0f; // Thời gian đổi tip
    
    [Header("Loading Tips")]
    [TextArea(2, 5)]
    public string[] loadingTips = new string[]
    {
        "💡 Thu thập tài nguyên để sinh tồn trong môi trường khắc nghiệt!",
        "⚒️ Chế tạo công cụ và vũ khí để bảo vệ bản thân.",
        "🔥 Hãy giữ ấm vào ban đêm bằng lửa trại.",
        "🍎 Ăn uống đầy đủ để duy trì sức khỏe.",
        "🌳 Khai thác cây cối bằng rìu để lấy gỗ.",
        "🪨 Đập đá bằng búa để lấy khoáng sản.",
        "🏠 Xây dựng nơi trú ẩn an toàn.",
        "🗺️ Khám phá bản đồ để tìm tài nguyên quý hiếm!",
        "⭐ Hoàn thành nhiệm vụ để nhận phần thưởng.",
        "👾 Hãy cẩn thận với quái vật vào ban đêm!"
    };
    
    private void Start()
    {
        // Bắt đầu loading
        StartCoroutine(LoadSceneAsync());
    }
    
    private IEnumerator LoadSceneAsync()
    {
        // Fade in màn hình loading
        yield return StartCoroutine(FadeIn());
        
        // Bắt đầu load scene async
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);
        asyncLoad.allowSceneActivation = false; // Không tự động chuyển scene
        
        float startTime = Time.time;
        float currentTipTime = 0f;
        int currentTipIndex = 0;
        
        // Hiển thị tip đầu tiên
        if (tipText != null && loadingTips.Length > 0)
        {
            tipText.text = loadingTips[0];
        }
        
        // Fake loading progress để mượt mà hơn
        float fakeProgress = 0f;
        
        while (!asyncLoad.isDone)
        {
            // Tính progress thực tế (Unity load từ 0 -> 0.9)
            float realProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            
            // Tính thời gian đã load
            float elapsedTime = Time.time - startTime;
            
            // Fake progress smooth (không nhảy cóc)
            fakeProgress = Mathf.MoveTowards(fakeProgress, realProgress, Time.deltaTime * 0.5f);
            
            // Update loading bar
            if (loadingBar != null)
            {
                loadingBar.fillAmount = fakeProgress;
            }
            
            // Update loading text
            if (loadingText != null)
            {
                int percent = Mathf.RoundToInt(fakeProgress * 100);
                loadingText.text = $"Loading... {percent}%";
            }
            
            // Đổi tip sau mỗi khoảng thời gian
            currentTipTime += Time.deltaTime;
            if (currentTipTime >= tipChangeInterval && loadingTips.Length > 1)
            {
                currentTipTime = 0f;
                currentTipIndex = (currentTipIndex + 1) % loadingTips.Length;
                
                if (tipText != null)
                {
                    tipText.text = loadingTips[currentTipIndex];
                }
            }
            
            // Chỉ chuyển scene khi:
            // 1. Load xong (progress >= 0.9)
            // 2. ĐÃ QUÁ thời gian loading tối thiểu
            if (asyncLoad.progress >= 0.9f && elapsedTime >= minLoadingTime)
            {
                // Đảm bảo thanh loading đầy 100%
                if (loadingBar != null)
                {
                    loadingBar.fillAmount = 1f;
                }
                if (loadingText != null)
                {
                    loadingText.text = "Loading... 100%";
                }
                
                yield return new WaitForSeconds(0.3f); // Hiển thị 100% một chút
                
                // Fade out và chuyển scene
                yield return StartCoroutine(FadeOut());
                
                asyncLoad.allowSceneActivation = true; // Cho phép chuyển scene
            }
            
            yield return null;
        }
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
