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

    [Header("Settings")]
    [SerializeField] private float feedbackDisplayTime = 2f;

    // Cheat States
    private static bool isGodModeActive = false;
    private static bool isInfiniteHungerActive = false;
    private static bool isOneHitKillActive = false;

    private bool isPanelOpen = false;
    private float feedbackTimer = 0f;
    private Keyboard keyboard;

    void Start()
    {
        keyboard = Keyboard.current;
        
        // Ẩn panel và feedback text khi bắt đầu
        if (cheatPanel != null)
            cheatPanel.SetActive(false);
        
        if (feedbackText != null)
            feedbackText.text = "";
    }

    void Update()
    {
        // Kiểm tra keyboard có tồn tại không
        if (keyboard == null)
        {
            keyboard = Keyboard.current;
            if (keyboard == null) return;
        }
        
        // Bấm Enter để mở/đóng panel
        if (keyboard.enterKey.wasPressedThisFrame || keyboard.numpadEnterKey.wasPressedThisFrame)
        {
            if (!isPanelOpen)
            {
                OpenCheatPanel();
            }
            else
            {
                ProcessCheatCode();
            }
        }

        // Bấm ESC để đóng panel mà không xử lý
        if (keyboard.escapeKey.wasPressedThisFrame && isPanelOpen)
        {
            CloseCheatPanel();
        }

        // Xử lý feedback timer
        if (feedbackTimer > 0)
        {
            feedbackTimer -= Time.deltaTime;
            if (feedbackTimer <= 0 && feedbackText != null)
            {
                feedbackText.text = "";
            }
        }

        // Áp dụng các cheat đang active
        ApplyCheats();
    }

    private void OpenCheatPanel()
    {
        isPanelOpen = true;
        if (cheatPanel != null)
            cheatPanel.SetActive(true);
        
        if (cheatInputField != null)
        {
            cheatInputField.text = "";
            cheatInputField.Select();
            cheatInputField.ActivateInputField();
        }

        // Tạm dừng game (optional)
        Time.timeScale = 0f;
        
        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void CloseCheatPanel()
    {
        isPanelOpen = false;
        if (cheatPanel != null)
            cheatPanel.SetActive(false);

        // Resume game
        Time.timeScale = 1f;
        
        // Lock cursor lại
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ProcessCheatCode()
    {
        if (cheatInputField == null) return;

        string code = cheatInputField.text.Trim().ToLower();

        switch (code)
        {
            case "+cheath":
                isGodModeActive = !isGodModeActive;
                ShowFeedback(isGodModeActive ? "✅ GOD MODE: ON" : "❌ GOD MODE: OFF");
                break;

            case "+cheatf":
                isInfiniteHungerActive = !isInfiniteHungerActive;
                ShowFeedback(isInfiniteHungerActive ? "✅ INFINITE HUNGER: ON" : "❌ INFINITE HUNGER: OFF");
                break;

            case "+cheatd":
                isOneHitKillActive = !isOneHitKillActive;
                ShowFeedback(isOneHitKillActive ? "✅ ONE HIT KILL: ON" : "❌ ONE HIT KILL: OFF");
                break;

            default:
                ShowFeedback("❌ Invalid Code! Try: +cheath, +cheatf, +cheatd");
                break;
        }

        CloseCheatPanel();
    }

    private void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackTimer = feedbackDisplayTime;
        }
    }

    private void ApplyCheats()
    {
        if (playerNeeds == null) return;

        // God Mode: Giữ máu luôn đầy
        if (isGodModeActive)
        {
            playerNeeds.health.currentValue = playerNeeds.health.maxValue;
        }

        // Infinite Hunger: Giữ độ đói luôn đầy
        if (isInfiniteHungerActive)
        {
            playerNeeds.hunger.currentValue = playerNeeds.hunger.maxValue;
        }
    }

    // Static methods để các script khác có thể kiểm tra
    public static bool IsGodModeActive()
    {
        return isGodModeActive;
    }

    public static bool IsInfiniteHungerActive()
    {
        return isInfiniteHungerActive;
    }

    public static bool IsOneHitKillActive()
    {
        return isOneHitKillActive;
    }

    // Method để hiển thị status cheat ở GIỮA PHÍA TRÊN CÙNG (không che gì cả)
    void OnGUI()
    {
        // Chỉ hiện nếu có cheat nào đang active
        if (!isGodModeActive && !isInfiniteHungerActive && !isOneHitKillActive)
            return;
        
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
        style.normal.textColor = Color.yellow;
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.UpperCenter;
        
        // Background box style
        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.normal.background = MakeTexture(2, 2, new Color(0, 0, 0, 0.7f));
        
        // Vị trí: GIỮA PHÍA TRÊN CÙNG (x = center, y = 10)
        int width = 250;
        int height = 0;
        
        // Tính chiều cao cần thiết
        height += 30; // Title
        if (isGodModeActive) height += 25;
        if (isInfiniteHungerActive) height += 25;
        if (isOneHitKillActive) height += 25;
        height += 5; // Bottom padding
        
        // Tính vị trí x để căn giữa
        int xPos = (Screen.width - width) / 2;
        int yPos = 10;
        
        // Vẽ background
        GUI.Box(new Rect(xPos, yPos, width, height), "", boxStyle);
        
        // Vẽ title
        GUIStyle titleStyle = new GUIStyle(style);
        titleStyle.fontSize = 20;
        titleStyle.fontStyle = FontStyle.Bold;
        GUI.Label(new Rect(xPos, yPos + 5, width, 25), "🎮 CHEATS ACTIVE", titleStyle);
        yPos += 30;
        
        // Vẽ các cheat đang active
        style.fontSize = 16;
        style.alignment = TextAnchor.MiddleCenter;
        
        if (isGodModeActive)
        {
            GUI.Label(new Rect(xPos, yPos, width, 25), "🛡️ God Mode", style);
            yPos += 25;
        }

        if (isInfiniteHungerActive)
        {
            GUI.Label(new Rect(xPos, yPos, width, 25), "🍖 Infinite Hunger", style);
            yPos += 25;
        }

        if (isOneHitKillActive)
        {
            GUI.Label(new Rect(xPos, yPos, width, 25), "⚔️ One Hit Kill", style);
        }
    }
    
    // Helper để tạo texture màu
    private Texture2D MakeTexture(int width, int height, Color color)
    {
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }
        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }
}
