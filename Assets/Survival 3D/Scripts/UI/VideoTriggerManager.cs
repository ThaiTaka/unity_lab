using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Quản lý phát video khi đạt 6 sao và trigger cutscene Anti
/// </summary>
public class VideoTriggerManager : MonoBehaviour
{
    [Header("Video Player")]
    public VideoPlayer videoPlayer;
    public RawImage videoDisplay; // UI để hiển thị video
    public CanvasGroup videoCanvasGroup;
    
    [Header("Anti Dialogue After Video")]
    public GameObject antiDialogueCanvas; // Canvas chứa dialogue Anti
    public TextMeshProUGUI antiDialogueText;
    public Image blackScreen;
    
    [Header("Audio")]
    public AudioSource typingAudioSource;
    public AudioClip typingSound;
    public AudioClip zombieRoarSound; // Tiếng gầm zombie
    
    [Header("Settings")]
    public float typingSpeed = 0.05f;
    public float loudTypingSpeed = 0.08f; // Chậm hơn cho chữ to
    public float normalFontSize = 30f;
    public float loudFontSize = 60f; // Font to cho "TÊ... LIỆT"
    
    private bool videoTriggered = false;
    private StarCollectionSystem starSystem;
    
    // Dialogue lines
    private string[] antiDialogues = new string[]
    {
        "Anti: Tất cả chỉ là quảng bá thôi, chỉ là ăn may thôi",
        "Anti: Tê liệt thì mãi là ..... TÊ..... LIỆTTTTTTTTTT"
    };
    
    private void Start()
    {
        // Tìm StarCollectionSystem
        starSystem = FindObjectOfType<StarCollectionSystem>();
        
        // Ẩn video và dialogue ban đầu
        if (videoCanvasGroup != null)
        {
            videoCanvasGroup.alpha = 0;
            videoCanvasGroup.gameObject.SetActive(false);
        }
        
        if (antiDialogueCanvas != null)
        {
            antiDialogueCanvas.SetActive(false);
        }
        
        // Setup VideoPlayer
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }
    
    private void Update()
    {
        // Kiểm tra khi đạt 6 sao
        if (!videoTriggered && starSystem != null && starSystem.GetStarCount() >= 6)
        {
            videoTriggered = true;
            StartCoroutine(PlayVideoSequence());
        }
    }
    
    private IEnumerator PlayVideoSequence()
    {
        Debug.Log("🎬 Đạt 6 sao! Bắt đầu phát video...");
        
        // Pause game
        Time.timeScale = 0f;
        
        // Fade in video
        if (videoCanvasGroup != null)
        {
            videoCanvasGroup.gameObject.SetActive(true);
            yield return StartCoroutine(FadeCanvasGroup(videoCanvasGroup, 0, 1, 0.5f));
        }
        
        // Play video
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
        
        // Đợi video kết thúc (sẽ trigger OnVideoFinished)
    }
    
    private void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("✅ Video kết thúc! Bắt đầu dialogue Anti...");
        StartCoroutine(ShowAntiDialogue());
    }
    
    private IEnumerator ShowAntiDialogue()
    {
        // Fade out video
        if (videoCanvasGroup != null)
        {
            yield return StartCoroutine(FadeCanvasGroup(videoCanvasGroup, 1, 0, 0.5f));
            videoCanvasGroup.gameObject.SetActive(false);
        }
        
        // Hiện màn hình đen
        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(true);
            blackScreen.color = Color.black;
        }
        
        // Hiện canvas dialogue
        if (antiDialogueCanvas != null)
        {
            antiDialogueCanvas.SetActive(true);
        }
        
        yield return new WaitForSecondsRealtime(1f);
        
        // Dialogue 1: Bình thường
        if (antiDialogueText != null)
        {
            antiDialogueText.fontSize = normalFontSize;
            yield return StartCoroutine(TypeDialogue(antiDialogues[0], typingSpeed, normalFontSize));
        }
        
        yield return new WaitForSecondsRealtime(2f);
        
        // Clear text
        if (antiDialogueText != null)
        {
            antiDialogueText.text = "";
        }
        
        yield return new WaitForSecondsRealtime(0.5f);
        
        // Dialogue 2: Có chữ TO
        yield return StartCoroutine(TypeDialogueWithLoudText(antiDialogues[1]));
        
        yield return new WaitForSecondsRealtime(1f);
        
        // Phát tiếng gầm zombie THẬT TO
        if (zombieRoarSound != null && typingAudioSource != null)
        {
            typingAudioSource.pitch = 0.7f; // Giọng trầm
            typingAudioSource.PlayOneShot(zombieRoarSound, 1.5f); // Volume to
        }
        
        yield return new WaitForSecondsRealtime(2f);
        
        // Resume game và spawn boss
        Time.timeScale = 1f;
        
        // Ẩn dialogue
        if (antiDialogueCanvas != null)
        {
            antiDialogueCanvas.SetActive(false);
        }
        
        // Trigger spawn boss Anti T1
        SpawnBossAntiT1();
    }
    
    private IEnumerator TypeDialogue(string text, float speed, float fontSize)
    {
        if (antiDialogueText == null) yield break;
        
        antiDialogueText.text = "";
        antiDialogueText.fontSize = fontSize;
        
        foreach (char letter in text.ToCharArray())
        {
            antiDialogueText.text += letter;
            
            // Typing sound
            if (typingSound != null && !char.IsWhiteSpace(letter) && typingAudioSource != null)
            {
                typingAudioSource.pitch = Random.Range(0.9f, 1.1f);
                typingAudioSource.PlayOneShot(typingSound, 0.5f);
            }
            
            yield return new WaitForSecondsRealtime(speed);
        }
    }
    
    private IEnumerator TypeDialogueWithLoudText(string text)
    {
        if (antiDialogueText == null) yield break;
        
        antiDialogueText.text = "";
        antiDialogueText.fontSize = normalFontSize;
        
        string currentText = "";
        
        foreach (char letter in text.ToCharArray())
        {
            currentText += letter;
            
            // Kiểm tra nếu đã gõ đến "TÊ"
            if (currentText.Contains("TÊ"))
            {
                antiDialogueText.fontSize = loudFontSize; // FONT TO HƠN
                antiDialogueText.fontStyle = FontStyles.Bold;
                antiDialogueText.color = Color.red; // Màu đỏ cho dramatic
                
                // Typing sound TO HƠN
                if (typingSound != null && !char.IsWhiteSpace(letter) && typingAudioSource != null)
                {
                    typingAudioSource.pitch = Random.Range(0.7f, 0.9f); // Pitch thấp hơn
                    typingAudioSource.PlayOneShot(typingSound, 1.0f); // Volume to hơn
                }
                
                antiDialogueText.text = currentText;
                yield return new WaitForSecondsRealtime(loudTypingSpeed);
            }
            else
            {
                // Typing bình thường
                if (typingSound != null && !char.IsWhiteSpace(letter) && typingAudioSource != null)
                {
                    typingAudioSource.pitch = Random.Range(0.9f, 1.1f);
                    typingAudioSource.PlayOneShot(typingSound, 0.5f);
                }
                
                antiDialogueText.text = currentText;
                yield return new WaitForSecondsRealtime(typingSpeed);
            }
        }
    }
    
    private IEnumerator FadeCanvasGroup(CanvasGroup group, float start, float end, float duration)
    {
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime; // Unscaled vì Time.timeScale = 0
            group.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        
        group.alpha = end;
    }
    
    private void SpawnBossAntiT1()
    {
        Debug.Log("👹 Spawning Boss Anti T1...");
        
        // Tìm BossSpawner component và trigger spawn
        BossAntiT1Spawner spawner = FindObjectOfType<BossAntiT1Spawner>();
        if (spawner != null)
        {
            spawner.SpawnBoss();
        }
        else
        {
            Debug.LogWarning("⚠️ Không tìm thấy BossAntiT1Spawner!");
        }
    }
}
