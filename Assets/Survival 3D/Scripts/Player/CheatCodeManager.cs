using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CheatCodeManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject cheatPanel;
    [SerializeField] private TMP_InputField cheatInputField;
    [SerializeField] private TextMeshProUGUI feedbackText;

    [Header("Player References")]
    [SerializeField] private PlayerNeeds playerNeeds;
    [SerializeField] private PlayerController playerController;

    [Header("Cheat States")]
    private bool godModeActive = false;
    private bool infiniteHungerActive = false;
    private bool oneHitKillActive = false;

    [Header("Visual Feedback")]
    [SerializeField] private float feedbackDisplayTime = 2f;
    private float feedbackTimer = 0f;

    private Keyboard keyboard;
    private bool isCheatPanelOpen = false;

    void Start()
    {
        keyboard = Keyboard.current;

        // Ẩn cheat panel ban đầu
        if (cheatPanel != null)
            cheatPanel.SetActive(false);

        if (feedbackText != null)
            feedbackText.text = "";

        // Đảm bảo input field không active ban đầu
        if (cheatInputField != null)
            cheatInputField.text = "";
    }

    void Update()
    {
        // Kiểm tra phím Enter để mở/đóng cheat panel
        if (keyboard != null && keyboard.enterKey.wasPressedThisFrame)
        {
            if (!isCheatPanelOpen)
            {
                OpenCheatPanel();
            }
            else
            {
                ProcessCheatCode();
            }
        }

        // ESC để đóng cheat panel
        if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame && isCheatPanelOpen)
        {
            CloseCheatPanel();
        }

        // Update feedback text timer
        if (feedbackTimer > 0)
        {
            feedbackTimer -= Time.deltaTime;
            if (feedbackTimer <= 0 && feedbackText != null)
            {
                feedbackText.text = "";
            }
        }

        // Apply active cheats
        ApplyCheats();
    }

    void OpenCheatPanel()
    {
        isCheatPanelOpen = true;
        
        if (cheatPanel != null)
            cheatPanel.SetActive(true);

        if (cheatInputField != null)
        {
            cheatInputField.text = "";
            cheatInputField.ActivateInputField();
            cheatInputField.Select();
        }

        // Không pause game, chỉ hiện panel
        Debug.Log("🎮 Cheat Panel Opened - Enter code and press Enter again");
    }

    void CloseCheatPanel()
    {
        isCheatPanelOpen = false;

        if (cheatPanel != null)
            cheatPanel.SetActive(false);

        if (cheatInputField != null)
            cheatInputField.text = "";

        Debug.Log("❌ Cheat Panel Closed");
    }

    void ProcessCheatCode()
    {
        if (cheatInputField == null)
            return;

        string code = cheatInputField.text.Trim().ToLower();

        if (string.IsNullOrEmpty(code))
        {
            CloseCheatPanel();
            return;
        }

        bool validCode = true;

        switch (code)
        {
            case "+cheath":
                godModeActive = !godModeActive;
                ShowFeedback(godModeActive ? "✅ GOD MODE: ON (Bất Tử)" : "❌ GOD MODE: OFF");
                Debug.Log($"🛡️ God Mode: {godModeActive}");
                break;

            case "+cheatf":
                infiniteHungerActive = !infiniteHungerActive;
                ShowFeedback(infiniteHungerActive ? "✅ INFINITE HUNGER: ON (Luôn No)" : "❌ INFINITE HUNGER: OFF");
                Debug.Log($"🍖 Infinite Hunger: {infiniteHungerActive}");
                break;

            case "+cheatd":
                oneHitKillActive = !oneHitKillActive;
                ShowFeedback(oneHitKillActive ? "✅ ONE HIT KILL: ON (Zombie 1 Hit)" : "❌ ONE HIT KILL: OFF");
                Debug.Log($"⚔️ One Hit Kill: {oneHitKillActive}");
                break;

            default:
                ShowFeedback("❌ Invalid Code! Try: +cheath, +cheatf, +cheatd");
                validCode = false;
                break;
        }

        if (validCode)
        {
            // Phát âm thanh thành công (nếu có)
            // AudioManager.instance?.PlayCheatSound();
        }

        CloseCheatPanel();
    }

    void ApplyCheats()
    {
        // God Mode - Giữ máu ở mức tối đa
        if (godModeActive && playerNeeds != null)
        {
            if (playerNeeds.health < playerNeeds.maxHealth)
            {
                playerNeeds.health = playerNeeds.maxHealth;
            }
        }

        // Infinite Hunger - Giữ độ no ở mức tối đa
        if (infiniteHungerActive && playerNeeds != null)
        {
            if (playerNeeds.hunger < playerNeeds.maxHunger)
            {
                playerNeeds.hunger = playerNeeds.maxHunger;
            }
        }
    }

    void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackTimer = feedbackDisplayTime;
        }
    }

    // Public getter để các script khác kiểm tra (ví dụ: zombie khi bị đánh)
    public bool IsOneHitKillActive()
    {
        return oneHitKillActive;
    }

    public bool IsGodModeActive()
    {
        return godModeActive;
    }

    public bool IsInfiniteHungerActive()
    {
        return infiniteHungerActive;
    }

    // Hiển thị status của các cheat đang active
    void OnGUI()
    {
        if (godModeActive || infiniteHungerActive || oneHitKillActive)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 14;
            style.normal.textColor = Color.yellow;
            style.alignment = TextAnchor.UpperRight;

            string statusText = "🎮 CHEATS ACTIVE:\n";
            if (godModeActive) statusText += "🛡️ God Mode\n";
            if (infiniteHungerActive) statusText += "🍖 Infinite Hunger\n";
            if (oneHitKillActive) statusText += "⚔️ One Hit Kill\n";

            GUI.Label(new Rect(Screen.width - 200, 10, 190, 100), statusText, style);
        }
    }
}
