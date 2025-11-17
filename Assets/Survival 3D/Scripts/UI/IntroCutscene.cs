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
    
    [Header("Faker Cube Reveal")]
    public GameObject fakerCube; // Cube Faker (Absolute Chyssey)
    public float cubeRevealDuration = 1.0f; // Thời gian fade in cube
    public float cubeDisplayTime = 2.0f; // Thời gian hiển thị cube
    public float cubeFadeOutDuration = 0.8f; // Thời gian fade out cube
    
    [Header("Settings")]
    public float typingSpeed = 0.05f; // Tốc độ gõ chữ
    public float delayBetweenLines = 1.5f; // Delay giữa các dòng
    public float slowTypingSpeed = 0.15f; // Tốc độ chậm cho "SẼ... DÀNH..."
    
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
        "Anti: Lại vô địch đấy, tê liệt cũng chỉ ăn may à ?",
        "Anti: Lúc nào cũng 3Ker, 3 Gà thì chửi ỏm lên",
        "Giờ kêu đánh lại 6 trận lấy cúp đố lấy được đấy",
        "Feaker: Thế giờ 6 cúp sau ......",
        "Feaker: SẼ ..... DÀNH ...... CHO ...... CHÚNG ....... EM"
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
        
        // Ẩn cube Faker ban đầu
        if (fakerCube != null)
        {
            fakerCube.SetActive(false);
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
        
        // HIỂN THỊ CUBE FAKER sau khi mở mắt
        yield return StartCoroutine(ShowFakerCube());
        
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
    
    private IEnumerator ShowFakerCube()
    {
        if (fakerCube == null) yield break;
        
        // Ẩn màn hình đen và text để chỉ thấy cube
        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(false);
        }
        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false);
        }
        
        // Kích hoạt cube nhưng trong suốt ban đầu
        fakerCube.SetActive(true);
        
        // Lấy tất cả Renderer của cube và children
        Renderer[] renderers = fakerCube.GetComponentsInChildren<Renderer>();
        
        // Set alpha ban đầu = 0 (trong suốt)
        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                Color color = mat.color;
                color.a = 0f;
                mat.color = color;
                
                // Enable transparent mode nếu cần
                mat.SetFloat("_Surface", 1); // Transparent
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
            }
        }
        
        // FADE IN - Cube từ từ xuất hiện
        float elapsed = 0f;
        while (elapsed < cubeRevealDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsed / cubeRevealDuration);
            
            foreach (Renderer rend in renderers)
            {
                foreach (Material mat in rend.materials)
                {
                    Color color = mat.color;
                    color.a = alpha;
                    mat.color = color;
                }
            }
            
            yield return null;
        }
        
        // Đảm bảo alpha = 1
        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                Color color = mat.color;
                color.a = 1f;
                mat.color = color;
            }
        }
        
        // HIỂN THỊ CUBE trong 2 giây
        yield return new WaitForSeconds(cubeDisplayTime);
        
        // FADE OUT - Cube từ từ biến mất
        elapsed = 0f;
        while (elapsed < cubeFadeOutDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / cubeFadeOutDuration);
            
            foreach (Renderer rend in renderers)
            {
                foreach (Material mat in rend.materials)
                {
                    Color color = mat.color;
                    color.a = alpha;
                    mat.color = color;
                }
            }
            
            yield return null;
        }
        
        // Ẩn cube
        fakerCube.SetActive(false);
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
