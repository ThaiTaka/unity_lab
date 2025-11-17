using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Victory Panel hiển thị khi người chơi thu thập đủ 6 sao
/// Hiển thị thông báo chiến thắng và các nút hành động
/// </summary>
public class VictoryPanel : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI starCountText;
    public Button continueButton;
    public Button restartButton;
    public Button mainMenuButton;
    
    [Header("Animation")]
    public CanvasGroup canvasGroup;
    public float fadeInDuration = 1f;
    public AnimationCurve fadeInCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("Particle Effects")]
    public ParticleSystem celebrationParticles;
    public GameObject[] confettiObjects;
    
    [Header("Audio")]
    public AudioClip victoryMusic;
    private AudioSource audioSource;
    
    private void Awake()
    {
        // Get AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Setup buttons
        if (continueButton != null)
            continueButton.onClick.AddListener(OnContinueClicked);
        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartClicked);
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuClicked);
            
        // Start hidden
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
    
    private void OnEnable()
    {
        // Animate panel when enabled
        StartCoroutine(AnimatePanelIn());
        
        // Update star count
        if (StarCollectionSystem.instance != null)
        {
            int stars = StarCollectionSystem.instance.GetCurrentStars();
            int maxStars = StarCollectionSystem.instance.maxStars;
            
            if (starCountText != null)
            {
                starCountText.text = $"⭐ {stars}/{maxStars} Stars Collected!";
            }
        }
        
        // Play victory music
        if (victoryMusic != null && audioSource != null)
        {
            audioSource.clip = victoryMusic;
            audioSource.loop = false;
            audioSource.Play();
        }
        
        // Play particles
        if (celebrationParticles != null)
        {
            celebrationParticles.Play();
        }
        
        // Enable confetti
        foreach (var confetti in confettiObjects)
        {
            if (confetti != null)
                confetti.SetActive(true);
        }
    }
    
    private IEnumerator AnimatePanelIn()
    {
        if (canvasGroup == null) yield break;
        
        float elapsed = 0f;
        
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeInDuration;
            float alpha = fadeInCurve.Evaluate(t);
            
            canvasGroup.alpha = alpha;
            
            yield return null;
        }
        
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    
    private void OnContinueClicked()
    {
        Debug.Log("Continue button clicked");
        
        // Resume time if paused
        Time.timeScale = 1f;
        
        // Hide panel
        gameObject.SetActive(false);
        
        // You can trigger next event here (next level, boss fight, etc.)
        // Example: LoadNextLevel();
    }
    
    private void OnRestartClicked()
    {
        Debug.Log("Restart button clicked");
        
        // Resume time
        Time.timeScale = 1f;
        
        // Reset star system
        if (StarCollectionSystem.instance != null)
        {
            StarCollectionSystem.instance.ResetStars();
        }
        
        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void OnMainMenuClicked()
    {
        Debug.Log("Main Menu button clicked");
        
        // Resume time
        Time.timeScale = 1f;
        
        // Load menu scene (assuming menu scene is at index 0)
        SceneManager.LoadScene("Menu"); // Hoặc SceneManager.LoadScene(0);
    }
}
