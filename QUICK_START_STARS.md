# ⚡ QUICK START - Star Collection System

## 🎯 MỤC TIÊU
Diệt zombie → Thu thập sao → Đủ 6 sao → Chiến thắng!

---

## 📦 SETUP 5 PHÚT

### 1️⃣ Tạo UI (Canvas)
```
Canvas
├── StarDisplayPanel (Anchor: Top-Right, Position: X:-100, Y:-50)
│   ├── StarCountText (TMP) "⭐ 0/6"
│   └── StarIconContainer (Add: Horizontal Layout Group)
│
└── VictoryPanel (INACTIVE ❌)
    └── ContentPanel
        ├── TitleText "🎉 VICTORY! 🎉"
        └── Buttons: Continue, Restart, MainMenu
```

### 2️⃣ Tạo Star Icon Prefab
- UI → Image (50x50)
- Sprite: ⭐ / Yellow
- Save as Prefab: "StarIcon"

### 3️⃣ Setup StarCollectionSystem
- GameObject → "StarCollectionSystem"
- Add Component: **StarCollectionSystem**
- Assign:
  - ✓ Star Count Text
  - ✓ Star Icon Container  
  - ✓ Star Icon Prefab
  - ✓ Victory Panel

### 4️⃣ Setup Victory Panel
- Add Component: **VictoryPanel**
- Add Component: **Canvas Group**
- Assign: Title, Message, Buttons
- ⚠️ **DEACTIVATE** panel

---

## 🎮 HOẠT ĐỘNG

```
Diệt Zombie → NPC.Die() → WaveManager → StarCollectionSystem.AddStar()
→ +1 sao (animation) → Check 6 sao → Victory Panel
```

---

## 🔧 INSPECTOR SETTINGS

### StarCollectionSystem
```
Max Stars: 6
Star Icon Container: [Drag StarIconContainer]
Star Icon Prefab: [Drag StarIcon prefab]
Victory Panel: [Drag VictoryPanel]
Star Collect Sound: [Optional audio]
Victory Sound: [Optional audio]
```

### VictoryPanel
```
Title Text: [Drag TitleText]
Continue Button: [Drag button]
Restart Button: [Drag button]
Main Menu Button: [Drag button]
Canvas Group: [Auto-assigned]
```

---

## ✅ TEST

1. **Play Scene**
2. **Diệt 1 zombie**
   - ✓ Sao xuất hiện góc phải
   - ✓ Animation grey → yellow
   - ✓ Text "⭐ 1/6"

3. **Diệt 6 zombies**
   - ✓ Stars bounce animation
   - ✓ Victory Panel fade-in
   - ✓ Buttons hoạt động

---

## 🐛 FIX NHANH

| Vấn đề | Fix |
|--------|-----|
| Sao không hiện | Check StarIconPrefab assigned? |
| Victory Panel không hiện | Panel INACTIVE at start? |
| Không +sao | Check Console logs |
| UI bị lỗi | StarIconContainer có Horizontal Layout? |

---

## 📖 TÀI LIỆU

- **Chi tiết**: `SETUP_STAR_SYSTEM.md`
- **Tổng quan**: `STAR_SYSTEM_SUMMARY.md`

---

## 🚀 NEXT: Event Sau Khi Đủ 6 Sao

Sửa `StarCollectionSystem.OnAllStarsCollected()`:

```csharp
private void OnAllStarsCollected()
{
    // Existing code...
    
    // 🔥 THÊM EVENT CỦA BẠN Ở ĐÂY:
    
    // Example 1: Spawn Boss
    // BossManager.instance.SpawnBoss();
    
    // Example 2: Load Level
    // SceneManager.LoadScene("BossLevel");
    
    // Example 3: Unlock Item
    // PlayerInventory.Unlock("SuperWeapon");
    
    // Example 4: Give Reward
    // PlayerInventory.AddGold(1000);
}
```

---

**✨ DONE! Enjoy your game! 🎮**
