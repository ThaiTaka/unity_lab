using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene cutscene: Anti T1 xuất hiện và giận dữ
/// Dialogue hiện lên → Boss spawn → Chuyển sang Boss Arena
/// </summary>
public class BossIntroSceneManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI speakerNameText;
    public Image speakerPortrait;
    
    [Header("Boss")]
    public GameObject bossModel; // Model của boss (ẩn ban đầu)
    public Transform bossSpawnPoint;
    public AudioSource bossRoarSound;
    
    [Header("Camera")]
    public Animator cameraAnimator; // Animator cho camera cinematic
    
    [Header("Scene Transition")]
    public string bossArenaSceneName = "BossArenaScene";
    public float transitionDelay = 2f;
    
    [Header("Dialogue Content")]
    public DialogueLine[] dialogueLines;
    
    private int currentLineIndex = 0;
    private bool dialogueFinished = false;
    
    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName;
        [TextArea(3, 5)]
        public string text;
        public Sprite portrait;
        public float displayDuration = 3f;
        public bool spawnBossAfter = false; // Spawn boss sau dòng này
    }
    
    private void Start()
    {
        Debug.Log("😈 Boss Intro Scene started!");
        
        // Ẩn boss ban đầu
        if (bossModel != null)
        {
            bossModel.SetActive(false);
        }
        
        // Setup UI
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }
        
        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        
        // Bắt đầu dialogue
        StartCoroutine(PlayDialogueSequence());
    }
    
    private IEnumerator PlayDialogueSequence()
    {
        // Delay trước khi bắt đầu
        yield return new WaitForSeconds(1f);
        
        // Chạy qua tất cả dialogue lines
        for (currentLineIndex = 0; currentLineIndex < dialogueLines.Length; currentLineIndex++)
        {
            DialogueLine line = dialogueLines[currentLineIndex];
            
            // Hiện dialogue
            ShowDialogue(line);
            
            // Đợi
            yield return new WaitForSeconds(line.displayDuration);
            
            // Spawn boss nếu cần
            if (line.spawnBossAfter && bossModel != null)
            {
                SpawnBoss();
                yield return new WaitForSeconds(2f); // Đợi boss spawn animation
            }
        }
        
        // Dialogue hết
        dialogueFinished = true;
        
        // Ẩn dialogue panel
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        
        Debug.Log("✅ Dialogue finished! Transitioning to Boss Arena...");
        
        // Chuyển sang Boss Arena
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(bossArenaSceneName);
    }
    
    private void ShowDialogue(DialogueLine line)
    {
        Debug.Log($"💬 {line.speakerName}: {line.text}");
        
        if (speakerNameText != null)
        {
            speakerNameText.text = line.speakerName;
        }
        
        if (dialogueText != null)
        {
            dialogueText.text = line.text;
        }
        
        if (speakerPortrait != null && line.portrait != null)
        {
            speakerPortrait.sprite = line.portrait;
            speakerPortrait.gameObject.SetActive(true);
        }
    }
    
    private void SpawnBoss()
    {
        Debug.Log("👹 BOSS SPAWNING!");
        
        if (bossModel != null)
        {
            bossModel.SetActive(true);
            
            // Set position nếu có spawn point
            if (bossSpawnPoint != null)
            {
                bossModel.transform.position = bossSpawnPoint.position;
                bossModel.transform.rotation = bossSpawnPoint.rotation;
            }
            
            // Play roar sound
            if (bossRoarSound != null)
            {
                bossRoarSound.Play();
            }
            
            // Trigger camera animation nếu có
            if (cameraAnimator != null)
            {
                cameraAnimator.SetTrigger("BossAppear");
            }
        }
    }
    
    private void Update()
    {
        // Cho phép skip dialogue bằng Space hoặc Enter
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (!dialogueFinished)
            {
                // Skip to boss arena
                Debug.Log("⏭️ Dialogue skipped!");
                StopAllCoroutines();
                SceneManager.LoadScene(bossArenaSceneName);
            }
        }
    }
}
