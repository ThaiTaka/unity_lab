using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Thêm Input System

/// <summary>
/// Cutscene intro với dialogue trước khi bắt đầu game
/// Hiển thị text từng đoạn, sau đó hiệu ứng mở mắt
/// </summary>
public class IntroCutscene : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI dialogueText;
    public Image blackScreen;
    public Image eyeOverlay; // Image cho hiệu ứng mở mắt
    public CanvasGroup canvasGroup;
    
    [Header("Settings")]
    public float typingSpeed = 0.05f; // Tốc độ gõ chữ
    public float delayBetweenLines = 1.5f; // Delay giữa các dòng
    public float slowTypingSpeed = 0.15f; // Tốc độ chậm cho "SẼ... DÀNH..."
    
    [Header("Eye Effect")]
    public float eyeBlinkDuration = 0.3f; // Thời gian chớp mắt
    public float eyeOpenDuration = 1.5f; // Thời gian mở mắt
    public int blinkCount = 2; // Số lần chớp mắt
    
    [Header("Scene")]
    public string gameSceneName = "Game"; // Tên scene game
    
    [Header("Skip")]
    public bool canSkip = true;
    
    private bool isSkipping = false;
    
    // Các đoạn dialogue
    private string[] dialogues = new string[]
    {
        "Anti: Lại vô địch đấy, tê liệt cũng chỉ ăn may à ?",
        "Anti: Lúc nào cũng 3Ker, 3 Gà thì chửi ỏm lên",
        "Giờ kêu đánh lại 6 trận lấy cúp đố lấy được đấy",
        "Faker: Thế giờ 6 cúp sau ......",
        "Faker: SẼ ..... DÀNH ...... CHO ...... CHÚNG ....... EM"
    };
    
    private void Start()
    {
        // Setup initial state
        if (blackScreen != null)
        {
            blackScreen.color = Color.black;
        }
        
        if (dialogueText != null)
        {
            dialogueText.text = "";
            dialogueText.color = Color.white;
        }
        
        if (eyeOverlay != null)
        {
            eyeOverlay.color = new Color(0, 0, 0, 0); // Trong suốt ban đầu
        }
        
        // Bắt đầu cutscene
        StartCoroutine(PlayCutscene());
    }
    
    private void Update()
    {
        // Skip cutscene bằng Space, Enter, hoặc Click chuột (dùng New Input System)
        if (canSkip && !isSkipping)
        {
            // Check keyboard
            if (Keyboard.current != null && 
                (Keyboard.current.spaceKey.wasPressedThisFrame || 
                 Keyboard.current.enterKey.wasPressedThisFrame ||
                 Keyboard.current.escapeKey.wasPressedThisFrame))
            {
                SkipCutscene();
            }
            
            // Check mouse click
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                SkipCutscene();
            }
        }
    }
    
    private void SkipCutscene()
    {
        isSkipping = true;
        StopAllCoroutines();
        LoadGameScene();
    }
    
    private IEnumerator PlayCutscene()
    {
        // Đợi 1 giây trước khi bắt đầu
        yield return new WaitForSeconds(1f);
        
        // Hiển thị từng dòng dialogue
        for (int i = 0; i < dialogues.Length; i++)
        {
            // Dòng cuối cùng (Faker: SẼ...) dùng typing chậm hơn
            float currentTypingSpeed = (i == dialogues.Length - 1) ? slowTypingSpeed : typingSpeed;
            
            yield return StartCoroutine(TypeText(dialogues[i], currentTypingSpeed));
            
            // Đợi trước khi chuyển sang dòng tiếp theo
            yield return new WaitForSeconds(delayBetweenLines);
            
            // Clear text
            if (dialogueText != null)
            {
                dialogueText.text = "";
            }
        }
        
        // Đợi 1 giây sau dialogue cuối
        yield return new WaitForSeconds(1f);
        
        // Hiệu ứng chớp mắt và mở mắt
        yield return StartCoroutine(EyeOpenEffect());
        
        // Load game scene
        LoadGameScene();
    }
    
    private IEnumerator TypeText(string text, float speed)
    {
        if (dialogueText == null) yield break;
        
        dialogueText.text = "";
        
        foreach (char letter in text.ToCharArray())
        {
            if (isSkipping) yield break;
            
            dialogueText.text += letter;
            yield return new WaitForSeconds(speed);
        }
    }
    
    private IEnumerator EyeOpenEffect()
    {
        if (eyeOverlay == null) yield break;
        
        // Chớp mắt nhiều lần (như vừa tỉnh dậy)
        for (int i = 0; i < blinkCount; i++)
        {
            // Mở mắt (fade in black)
            yield return StartCoroutine(FadeEye(0, 1, eyeBlinkDuration * 0.5f));
            
            // Đợi một chút
            yield return new WaitForSeconds(0.1f);
            
            // Nhắm mắt (fade out black)
            yield return StartCoroutine(FadeEye(1, 0, eyeBlinkDuration * 0.5f));
            
            // Đợi giữa các lần chớp
            yield return new WaitForSeconds(0.2f);
        }
        
        // Mở mắt lần cuối và giữ mở
        yield return StartCoroutine(FadeEye(0, 1, eyeOpenDuration * 0.3f));
        
        // Đợi một chút
        yield return new WaitForSeconds(0.3f);
        
        // Từ từ mở mắt hoàn toàn (fade to transparent)
        yield return StartCoroutine(FadeEye(1, 0, eyeOpenDuration));
    }
    
    private IEnumerator FadeEye(float startAlpha, float endAlpha, float duration)
    {
        if (eyeOverlay == null) yield break;
        
        float elapsed = 0f;
        Color color = eyeOverlay.color;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            eyeOverlay.color = color;
            
            yield return null;
        }
        
        color.a = endAlpha;
        eyeOverlay.color = color;
    }
    
    private void LoadGameScene()
    {
        // Fade out trước khi load scene
        if (canvasGroup != null)
        {
            StartCoroutine(FadeOutAndLoad());
        }
        else
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
    
    private IEnumerator FadeOutAndLoad()
    {
        float duration = 1f;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            yield return null;
        }
        
        SceneManager.LoadScene(gameSceneName);
    }
}
