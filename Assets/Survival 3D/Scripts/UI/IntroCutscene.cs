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
    
    [Header("Faker Image Reveal")]
    public Image fakerImage; // Image Faker (thay vì cube)
    public float fakerFadeInDuration = 5.0f; // Thời gian fade in ảnh Faker
    public float fakerFadeOutDuration = 2.0f; // Thời gian fade về đen
    
    [Header("Settings")]
    public float typingSpeed = 0.05f; // Tốc độ gõ chữ
    public float delayBetweenLines = 1.5f; // Delay giữa các dòng
    public float slowTypingSpeed = 0.15f; // Tốc độ chậm cho "SẼ... DÀNH..."
    
    [Header("Audio")]
    public AudioClip typingSound; // Âm thanh gõ chữ (typing hoặc robot beep)
    [Range(0f, 1f)]
    public float typingSoundVolume = 0.5f; // Âm lượng
    private AudioSource audioSource;
    
    [Header("Eye Effect")]
    public float eyeBlinkDuration = 0.3f; // Thời gian chớp mắt
    public float eyeOpenDuration = 2.0f; // Thời gian mở mắt
    public int blinkCount = 4; // Số lần chớp mắt (nhiều hơn = như vừa tỉnh dậy)
    
    [Header("Scene")]
    public string gameSceneName = "Game"; // Tên scene game
    
    [Header("Skip")]
    public bool canSkip = true;
    
    private bool isSkipping = false;
    
    // Các đoạn dialogue
    private string[] dialogues = new string[]
    {
        "Anti Fan: Lại vô địch đấy, tê liệt cũng chỉ ăn may à ?",
        "Anti Fan: Lúc nào cũng 3Ker, 3 Gà thì chửi ỏm lên",
        "Anti Fan: Giờ kêu đánh lại 6 trận lấy cúp đố lấy được đấy",
        "Feaker: Thế giờ 6 cúp sau ......",
        "Feaker: SẼ ...... DÀNH ....... CHO ...... Các ...... EM"
    };
    
    private void Start()
    {
        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.volume = typingSoundVolume;
        
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
        
        // Ẩn ảnh Faker ban đầu
        if (fakerImage != null)
        {
            // Setup FakerImage để phóng to toàn màn hình và không bị bể
            SetupFakerImageFullscreen();
            
            Color color = fakerImage.color;
            color.a = 0f; // Trong suốt
            fakerImage.color = color;
            fakerImage.gameObject.SetActive(false);
        }
        
        // Bắt đầu cutscene
        StartCoroutine(PlayCutscene());
    }
    
    private void SetupFakerImageFullscreen()
    {
        if (fakerImage == null) return;
        
        // Lấy RectTransform của FakerImage
        RectTransform rectTransform = fakerImage.GetComponent<RectTransform>();
        if (rectTransform == null) return;
        
        // Set anchor thành Stretch All để phủ toàn màn hình
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        
        // Đặt scale lớn hơn để đảm bảo phủ kín màn hình
        rectTransform.localScale = Vector3.one * 1.2f; // Phóng to 120%
        
        // Set Preserve Aspect để không bị méo
        fakerImage.preserveAspect = true;
        
        // Đặt ở layer trên BlackScreen nhưng dưới Text
        fakerImage.transform.SetSiblingIndex(1);
        
        Debug.Log("✅ FakerImage đã được setup: Fullscreen + Preserve Aspect + Scale 120%");
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
        
        // Hiển thị từng dòng dialogue (trừ dòng cuối)
        for (int i = 0; i < dialogues.Length - 1; i++)
        {
            // Đảm bảo text màu trắng và reset style cho các dòng bình thường
            if (dialogueText != null)
            {
                dialogueText.color = Color.white;
                dialogueText.fontStyle = FontStyles.Normal; // Reset bôi đậm
                dialogueText.outlineWidth = 0.2f; // Viền mỏng ban đầu
            }
            
            yield return StartCoroutine(TypeText(dialogues[i], typingSpeed));
            
            // Đợi trước khi chuyển sang dòng tiếp theo
            yield return new WaitForSeconds(delayBetweenLines);
            
            // Clear text
            if (dialogueText != null)
            {
                dialogueText.text = "";
            }
        }
        
        // DÒNG CUỐI CÙNG: Type text VÀ Fade in ảnh Faker ĐỒNG THỜI
        yield return StartCoroutine(TypeTextWithFakerReveal());
        
        // Fade về đen
        yield return StartCoroutine(FadeFakerToBlack());
        
        // Hiệu ứng chớp mắt liên tục
        yield return StartCoroutine(EyeOpenEffect());
        
        // Chuyển scene NGAY LẬP TỨC sau khi mở mắt
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
            
            // Phát âm thanh gõ chữ (trừ khoảng trắng)
            if (typingSound != null && !char.IsWhiteSpace(letter) && audioSource != null)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f); // Random pitch cho tự nhiên
                audioSource.PlayOneShot(typingSound, typingSoundVolume);
            }
            
            // Kiểm tra nếu đã gõ đến "DÀNH" trong câu cuối
            if (dialogueText.text.Contains("DÀNH"))
            {
                // Đổi màu text thành đen + bôi đậm + thêm viền
                dialogueText.color = Color.black;
                dialogueText.fontStyle = FontStyles.Bold; // Bôi đậm
                
                // Thêm viền trắng để nổi bật trên ảnh
                dialogueText.outlineColor = Color.white;
                dialogueText.outlineWidth = 0.3f; // Độ dày viền
            }
            
            yield return new WaitForSeconds(speed);
        }
    }
    
    private IEnumerator EyeOpenEffect()
    {
        if (eyeOverlay == null) yield break;
        
        // Chớp mắt nhiều lần (như vừa tỉnh dậy, chưa quen ánh sáng)
        for (int i = 0; i < blinkCount; i++)
        {
            // Tốc độ chớp nhanh dần (lần đầu chậm, sau nhanh hơn)
            float blinkSpeed = eyeBlinkDuration * (1f - (i * 0.15f));
            blinkSpeed = Mathf.Max(blinkSpeed, 0.1f); // Không quá nhanh
            
            // Nhắm mắt (đen)
            yield return StartCoroutine(FadeEye(0, 1, blinkSpeed * 0.4f));
            
            // Giữ nhắm một chút (lần đầu lâu hơn)
            float holdTime = 0.15f * (blinkCount - i) / blinkCount;
            yield return new WaitForSeconds(holdTime);
            
            // Mở mắt (trong suốt)
            yield return StartCoroutine(FadeEye(1, 0, blinkSpeed * 0.6f));
            
            // Đợi giữa các lần chớp (càng về sau càng ngắn)
            float delayTime = 0.3f * (blinkCount - i) / blinkCount;
            yield return new WaitForSeconds(delayTime);
        }
        
        // Đợi một chút sau khi chớp hết
        yield return new WaitForSeconds(0.4f);
        
        // Nhắm mắt lần cuối (chuẩn bị mở hoàn toàn)
        yield return StartCoroutine(FadeEye(0, 1, 0.2f));
        
        // Đợi một chút
        yield return new WaitForSeconds(0.2f);
        
        // MỞ MẮT HOÀN TOÀN - từ từ, như vừa tỉnh dậy thật sự
        yield return StartCoroutine(FadeEye(1, 0, eyeOpenDuration));
        
        // Đợi thêm một chút để người chơi thấy rõ
        yield return new WaitForSeconds(0.3f);
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
    
    private IEnumerator TypeTextWithFakerReveal()
    {
        // Dòng cuối cùng - Bắt đầu với text màu trắng, style bình thường
        if (dialogueText != null)
        {
            dialogueText.color = Color.white;
            dialogueText.fontStyle = FontStyles.Normal;
            dialogueText.outlineWidth = 0.2f;
        }
        
        string lastDialogue = dialogues[dialogues.Length - 1];
        
        // Bắt đầu type text
        Coroutine typingCoroutine = StartCoroutine(TypeText(lastDialogue, slowTypingSpeed));
        
        // ĐỒNG THỜI: Fade in ảnh Faker
        if (fakerImage != null)
        {
            fakerImage.gameObject.SetActive(true);
            
            float elapsed = 0f;
            Color color = fakerImage.color;
            
            // Fade in trong 5 giây
            while (elapsed < fakerFadeInDuration)
            {
                elapsed += Time.deltaTime;
                color.a = Mathf.Lerp(0f, 1f, elapsed / fakerFadeInDuration);
                fakerImage.color = color;
                yield return null;
            }
            
            // Đảm bảo alpha = 1
            color.a = 1f;
            fakerImage.color = color;
        }
        
        // Đợi typing xong
        yield return typingCoroutine;
        
        // Giữ ảnh Faker một chút
        yield return new WaitForSeconds(1f);
    }
    
    private IEnumerator FadeFakerToBlack()
    {
        if (fakerImage == null) yield break;
        
        // Clear text
        if (dialogueText != null)
        {
            dialogueText.text = "";
        }
        
        // Fade ảnh Faker về đen
        float elapsed = 0f;
        Color color = fakerImage.color;
        
        while (elapsed < fakerFadeOutDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / fakerFadeOutDuration);
            fakerImage.color = color;
            yield return null;
        }
        
        // Ẩn ảnh
        color.a = 0f;
        fakerImage.color = color;
        fakerImage.gameObject.SetActive(false);
        
        // Hiện lại màn hình đen
        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(true);
        }
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
