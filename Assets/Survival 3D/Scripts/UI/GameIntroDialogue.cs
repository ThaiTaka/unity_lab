using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Dialogue intro khi vào game - Player bất động
/// </summary>
public class GameIntroDialogue : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel; // Panel chứa dialogue
    public TextMeshProUGUI dialogueText;
    public Image blackScreen; // Màn hình đen
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip typingSound;
    [Range(0f, 1f)]
    public float typingSoundVolume = 0.5f;
    
    [Header("Settings")]
    public float typingSpeed = 0.05f;
    public float delayBetweenLines = 1.5f;
    
    private bool dialogueFinished = false;
    
    // Các dòng dialogue
    private string[] dialogues = new string[]
    {
        "Feaker: WTF, Đây là ở đâu ?",
        "Anti Fan: Hỏi làm cái *** gì ?",
        "Anti Fan: Mày chỉ cần biết m thắng 6 đội hạng \"2\" kia",
        "Anti Fan: Thì mày được về nhà, haha see yaaaaa!"
    };
    
    private void Start()
    {
        // Setup AudioSource
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.volume = typingSoundVolume;
        
        // Ẩn UI ban đầu
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        
        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(true);
            blackScreen.color = Color.black;
        }
        
        // ĐÓNG BĂNG PLAYER
        FreezePlayer(true);
        
        // Bắt đầu dialogue sau 1 giây
        StartCoroutine(PlayIntroDialogue());
    }
    
    private IEnumerator PlayIntroDialogue()
    {
        yield return new WaitForSeconds(1f);
        
        // Hiện dialogue panel
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }
        
        // Hiển thị từng dòng
        foreach (string line in dialogues)
        {
            yield return StartCoroutine(TypeText(line));
            yield return new WaitForSeconds(delayBetweenLines);
            
            // Clear text
            if (dialogueText != null)
            {
                dialogueText.text = "";
            }
        }
        
        // Kết thúc dialogue
        yield return new WaitForSeconds(0.5f);
        
        // Fade out dialogue panel
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        
        // Fade out black screen
        if (blackScreen != null)
        {
            yield return StartCoroutine(FadeBlackScreen(1f, 0f, 1f));
            blackScreen.gameObject.SetActive(false);
        }
        
        // MỞ BĂNG PLAYER - Cho phép di chuyển
        FreezePlayer(false);
        
        dialogueFinished = true;
        
        Debug.Log("✅ Intro dialogue kết thúc! Player có thể di chuyển.");
    }
    
    private IEnumerator TypeText(string text)
    {
        if (dialogueText == null) yield break;
        
        dialogueText.text = "";
        dialogueText.color = Color.white;
        dialogueText.fontSize = 30;
        
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            
            // Typing sound
            if (typingSound != null && !char.IsWhiteSpace(letter) && audioSource != null)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(typingSound, typingSoundVolume);
            }
            
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
    private IEnumerator FadeBlackScreen(float startAlpha, float endAlpha, float duration)
    {
        if (blackScreen == null) yield break;
        
        float elapsed = 0f;
        Color color = blackScreen.color;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            blackScreen.color = color;
            yield return null;
        }
        
        color.a = endAlpha;
        blackScreen.color = color;
    }
    
    private void FreezePlayer(bool freeze)
    {
        // Tìm player
        PlayerController player = FindObjectOfType<PlayerController>();
        
        if (player != null)
        {
            player.enabled = !freeze; // Tắt script di chuyển
        }
        
        // Lock cursor khi freeze
        if (freeze)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        // Pause thời gian (optional - nếu muốn freeze toàn bộ game)
        // Time.timeScale = freeze ? 0f : 1f;
        
        Debug.Log($"🎮 Player {(freeze ? "FROZEN" : "UNFROZEN")}");
    }
    
    public bool IsDialogueFinished()
    {
        return dialogueFinished;
    }
}
