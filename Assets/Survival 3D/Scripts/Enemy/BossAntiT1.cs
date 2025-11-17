using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Boss Anti T1 - 3 phases: Attack → Vulnerable → Repeat
/// </summary>
public class BossAntiT1 : MonoBehaviour
{
    [Header("Boss Stats")]
    public int maxHealthSegments = 3; // 3 đoạn máu (mỗi lần đánh mất 1/3)
    private int currentHealthSegments = 3;
    
    [Header("UI")]
    public Slider healthBar; // Thanh máu trên đầu
    public TextMeshProUGUI bossNameText; // Text "Anti T1"
    public Canvas bossCanvas; // Canvas trên đầu boss
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip roarSound; // Tiếng gầm
    public AudioClip skillSound; // Tiếng cast skill
    
    [Header("Phase Settings")]
    public float attackPhaseDuration = 15f; // 15 giây tung skill
    public float vulnerablePhaseWaitTime = 3f; // Thời gian đợi player đánh
    public float healthBarFillDuration = 3f; // Thời gian thanh máu fill từ 0 → 100%
    
    [Header("Skills")]
    public GameObject meteorPrefab; // Prefab thiên thạch
    public GameObject warningZonePrefab; // Prefab vùng cảnh báo đỏ
    public float skillCastInterval = 2f; // Cast skill mỗi 2 giây
    
    private bool isInvulnerable = true; // Boss bất tử khi đang attack
    private bool isVulnerablePhase = false;
    private bool isDead = false;
    
    private BossPhase currentPhase = BossPhase.Spawning;
    
    public enum BossPhase
    {
        Spawning,      // Đang spawn, fill thanh máu
        Attacking,     // Đang tung skill, bất tử
        Vulnerable,    // Đứng yên, player có thể đánh
        Dead           // Boss chết
    }
    
    private void Start()
    {
        currentHealthSegments = maxHealthSegments;
        
        // Setup UI
        if (bossNameText != null)
        {
            bossNameText.text = "Anti T1";
        }
        
        if (healthBar != null)
        {
            healthBar.maxValue = 1f;
            healthBar.value = 0f; // Bắt đầu từ 0
        }
        
        // Bắt đầu spawn sequence
        StartCoroutine(SpawnSequence());
    }
    
    private IEnumerator SpawnSequence()
    {
        currentPhase = BossPhase.Spawning;
        
        // Gầm lần đầu
        PlayRoar(1.5f); // Volume to
        
        yield return new WaitForSeconds(1f);
        
        // Fill thanh máu từ 0 → 1 (100%)
        if (healthBar != null)
        {
            float elapsed = 0f;
            
            while (elapsed < healthBarFillDuration)
            {
                elapsed += Time.deltaTime;
                healthBar.value = Mathf.Lerp(0f, 1f, elapsed / healthBarFillDuration);
                yield return null;
            }
            
            healthBar.value = 1f;
        }
        
        yield return new WaitForSeconds(0.5f);
        
        // Bắt đầu phase 1: Attack
        StartCoroutine(AttackPhase());
    }
    
    private IEnumerator AttackPhase()
    {
        currentPhase = BossPhase.Attacking;
        isInvulnerable = true;
        
        Debug.Log($"👹 Boss Attack Phase {4 - currentHealthSegments}/3");
        
        // Gầm trước khi tấn công
        PlayRoar(1.0f);
        
        yield return new WaitForSeconds(1f);
        
        // Cast skill liên tục trong 15 giây
        float elapsed = 0f;
        float nextSkillTime = 0f;
        
        while (elapsed < attackPhaseDuration)
        {
            if (Time.time >= nextSkillTime)
            {
                CastRandomSkill();
                nextSkillTime = Time.time + skillCastInterval;
            }
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        // Kết thúc attack phase → Chuyển sang vulnerable
        StartCoroutine(VulnerablePhase());
    }
    
    private IEnumerator VulnerablePhase()
    {
        currentPhase = BossPhase.Vulnerable;
        isInvulnerable = false;
        isVulnerablePhase = true;
        
        Debug.Log("💥 Boss Vulnerable! Player có thể đánh!");
        
        // Gầm và dừng lại
        PlayRoar(0.8f);
        
        // Đợi player đánh (hoặc timeout)
        float waitTime = 0f;
        
        while (isVulnerablePhase && waitTime < vulnerablePhaseWaitTime)
        {
            waitTime += Time.deltaTime;
            yield return null;
        }
        
        // Nếu player không đánh trong thời gian cho phép
        if (isVulnerablePhase)
        {
            Debug.Log("⏰ Player không đánh kịp! Boss quay lại attack phase.");
            isInvulnerable = true;
            isVulnerablePhase = false;
            StartCoroutine(AttackPhase());
        }
    }
    
    public void TakeDamage()
    {
        if (isInvulnerable || !isVulnerablePhase || isDead)
        {
            Debug.Log("❌ Boss đang bất tử! Không thể gây damage.");
            return;
        }
        
        // Giảm 1/3 HP
        currentHealthSegments--;
        
        Debug.Log($"💔 Boss mất 1/3 HP! Còn lại: {currentHealthSegments}/3");
        
        // Update health bar
        if (healthBar != null)
        {
            float targetValue = (float)currentHealthSegments / maxHealthSegments;
            StartCoroutine(AnimateHealthBar(healthBar.value, targetValue, 0.3f));
        }
        
        // Boss không còn vulnerable nữa
        isVulnerablePhase = false;
        isInvulnerable = true;
        
        // Gầm sau khi bị đánh
        PlayRoar(1.2f);
        
        // Kiểm tra chết
        if (currentHealthSegments <= 0)
        {
            StartCoroutine(DeathSequence());
        }
        else
        {
            // Quay lại attack phase
            StartCoroutine(AttackPhase());
        }
    }
    
    private IEnumerator AnimateHealthBar(float start, float end, float duration)
    {
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            healthBar.value = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        
        healthBar.value = end;
    }
    
    private void CastRandomSkill()
    {
        // Random vị trí trên map
        Vector3 randomPos = transform.position + new Vector3(
            Random.Range(-10f, 10f),
            0f,
            Random.Range(-10f, 10f)
        );
        
        // Spawn warning zone trước
        if (warningZonePrefab != null)
        {
            GameObject warning = Instantiate(warningZonePrefab, randomPos, Quaternion.identity);
            Destroy(warning, 1.5f); // Warning tồn tại 1.5 giây
        }
        
        // Sau 1.5 giây spawn meteor
        StartCoroutine(SpawnMeteorDelayed(randomPos, 1.5f));
        
        // Play skill sound
        if (audioSource != null && skillSound != null)
        {
            audioSource.PlayOneShot(skillSound, 0.7f);
        }
    }
    
    private IEnumerator SpawnMeteorDelayed(Vector3 position, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (meteorPrefab != null)
        {
            Vector3 spawnPos = position + Vector3.up * 20f; // Spawn trên cao
            GameObject meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
            
            // Meteor sẽ rơi xuống và gây damage (xử lý trong script riêng)
        }
    }
    
    private void PlayRoar(float volume)
    {
        if (audioSource != null && roarSound != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.0f);
            audioSource.PlayOneShot(roarSound, volume);
        }
    }
    
    private IEnumerator DeathSequence()
    {
        currentPhase = BossPhase.Dead;
        isDead = true;
        
        Debug.Log("💀 Boss Anti T1 đã chết!");
        
        // Gầm lần cuối
        PlayRoar(2.0f);
        
        yield return new WaitForSeconds(2f);
        
        // Trigger victory video
        VictoryManager victoryManager = FindObjectOfType<VictoryManager>();
        if (victoryManager != null)
        {
            victoryManager.TriggerVictory();
        }
        
        // Destroy boss
        Destroy(gameObject, 3f);
    }
    
    private void Update()
    {
        // Health bar luôn quay về camera
        if (bossCanvas != null && Camera.main != null)
        {
            bossCanvas.transform.LookAt(Camera.main.transform);
            bossCanvas.transform.Rotate(0, 180, 0); // Flip lại
        }
    }
}
