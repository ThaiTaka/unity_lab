using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene độc lập chỉ để phát video chiến thắng
/// Sau khi video hết → Tự động chuyển sang BossIntroScene
/// </summary>
public class VictoryVideoSceneManager : MonoBehaviour
{
    [Header("Video Setup")]
    public VideoPlayer videoPlayer;
    public RawImage videoDisplay;
    
    [Header("Scene Transition")]
    public string nextSceneName = "BossIntroScene"; // Scene tiếp theo
    public float delayAfterVideo = 1f; // Delay trước khi chuyển scene
    
    [Header("Skip Settings")]
    public bool allowSkip = true; // Cho phép bấm Space để skip
    public KeyCode skipKey = KeyCode.Space;
    
    private bool videoFinished = false;
    
    private void Start()
    {
        Debug.Log("🎬 Victory Video Scene started!");
        
        // Setup video
        if (videoPlayer != null)
        {
            // Tạo RenderTexture
            RenderTexture rt = new RenderTexture(1920, 1080, 24);
            rt.Create();
            
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            videoPlayer.targetTexture = rt;
            
            if (videoDisplay != null)
            {
                videoDisplay.texture = rt;
                
                // Setup RawImage full screen
                RectTransform rectTransform = videoDisplay.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchorMin = Vector2.zero;
                    rectTransform.anchorMax = Vector2.one;
                    rectTransform.offsetMin = Vector2.zero;
                    rectTransform.offsetMax = Vector2.zero;
                }
            }
            
            // Subscribe to video finished event
            videoPlayer.loopPointReached += OnVideoFinished;
            
            // Prepare và play
            StartCoroutine(PrepareAndPlayVideo());
        }
        else
        {
            Debug.LogError("❌ VideoPlayer is NULL!");
        }
        
        // Unlock cursor để xem video thoải mái
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false; // Ẩn cursor khi xem video
    }
    
    private IEnumerator PrepareAndPlayVideo()
    {
        Debug.Log("⏳ Preparing video...");
        
        videoPlayer.Prepare();
        
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }
        
        Debug.Log("✅ Video prepared! Starting playback...");
        videoPlayer.Play();
    }
    
    private void Update()
    {
        // Cho phép skip video bằng Space
        if (allowSkip && Input.GetKeyDown(skipKey) && !videoFinished)
        {
            Debug.Log("⏭️ Video skipped by player!");
            SkipVideo();
        }
    }
    
    private void OnVideoFinished(VideoPlayer vp)
    {
        if (videoFinished) return;
        
        videoFinished = true;
        Debug.Log("✅ Victory video finished!");
        
        StartCoroutine(TransitionToNextScene());
    }
    
    private void SkipVideo()
    {
        if (videoFinished) return;
        
        videoFinished = true;
        videoPlayer.Stop();
        
        StartCoroutine(TransitionToNextScene());
    }
    
    private IEnumerator TransitionToNextScene()
    {
        Debug.Log($"🔄 Transitioning to {nextSceneName} in {delayAfterVideo}s...");
        
        yield return new WaitForSeconds(delayAfterVideo);
        
        // Load next scene
        SceneManager.LoadScene(nextSceneName);
    }
    
    private void OnDestroy()
    {
        // Unsubscribe
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}
