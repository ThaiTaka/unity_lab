# ⚡ QUICK START - Star Collection System

## 🎯 MỤC TIÊU
Diệt zombie → Thu thập sao → Đủ 6 sao → **DỪNG SPAWN ZOMBIE**

---

## 📦 SETUP CƠ BẢN (3 PHÚT) - CHỈ CẦN TEXT

### 1️⃣ Tạo Text Hiển Thị Sao
```
Canvas → Right Click → UI → Panel
  └─ Rename: "StarPanel"
     └─ Anchor: Top-Right
     └─ Pos X: -150, Y: -50
     └─ Add: UI → Text - TextMeshPro
         └─ Rename: "StarText"
         └─ Text: "⭐ 0/6"
         └─ Font Size: 36
         └─ Color: White
```

### 2️⃣ Setup StarCollectionSystem
- GameObject → Create Empty → "StarCollectionSystem"
- Add Component: **StarCollectionSystem**
- **Star Count Text**: Kéo `StarText` vào đây
- **BỎ TRỐNG**: Star Icon Container, Star Icon Prefab, Victory Panel

### 3️⃣ Test
- Play → Diệt zombie → Text "⭐ 1/6"
- Diệt 6 zombies → "⭐ 6/6" → **Không spawn zombie nữa**

✅ **XONG! Chỉ cần vậy thôi!**

---

## 🎮 HOẠT ĐỘNG

```
Diệt Zombie → +1 sao → Text "⭐ X/6" → Đủ 6 sao → Dừng spawn zombie
```

---

## 🔧 INSPECTOR SETTINGS (Tối thiểu)

### StarCollectionSystem
```
Max Stars: 6
Star Count Text: [StarText] ← QUAN TRỌNG!
(Các ô khác: None - Bỏ trống)
```

---

## 📦 SETUP ĐẦY ĐỦ (Nếu muốn UI đẹp)

### 1️⃣ Thêm 6 Star Icons (Optional)

**Tạo Container:**
```
StarPanel
├── StarText "⭐ 0/6"
└── StarIconContainer (Add: Horizontal Layout Group)
    └── (6 stars sẽ auto tạo)
```

**Tạo Star Icon Prefab:**
- UI → Image (50x50)
- Sprite: ⭐
- Color: Yellow
- Save as Prefab: "StarIcon"

**Assign:**
- Star Icon Container: [StarIconContainer]
- Star Icon Prefab: [StarIcon prefab]

### 2️⃣ Thêm Victory Panel (Optional)

```
Canvas
└── VictoryPanel (Panel, INACTIVE ❌)
    └── ContentPanel
        ├── TitleText "Đủ 6 sao!"
        └── Buttons: Continue, Restart, Menu
```

**Setup:**
- Add Component: **VictoryPanel**
- Add Component: **Canvas Group**
- Assign references
- **⚠️ DEACTIVATE** panel

---

## ✅ TEST

1. **Setup cơ bản (chỉ text)**:
   - ✓ Text "⭐ 0/6" góc phải
   - ✓ Diệt zombie → "⭐ 1/6"
   - ✓ Đủ 6 → Dừng spawn

2. **Setup đầy đủ (có icons)**:
   - ✓ 6 star icons (grey)
   - ✓ Diệt zombie → icon chuyển yellow
   - ✓ Animation smooth

3. **Setup Victory Panel**:
   - ✓ Panel fade-in
   - ✓ Buttons hoạt động

---

## 🐛 FIX NHANH

| Vấn đề | Fix |
|--------|-----|
| Text không hiện | Check Canvas: Screen Space - Overlay |
| Text không update | Star Count Text đã assign? |
| Đủ 6 sao vẫn spawn | Check Console: "Đã dừng spawn zombie!" |
| Lỗi NullReference | Star Count Text phải được assign! |

---

## 📖 TÀI LIỆU

- **🚀 Setup đơn giản**: `SETUP_UNITY_DON_GIAN.md` ← BẮT ĐẦU ĐÂY!
- **Chi tiết**: `SETUP_STAR_SYSTEM.md`
- **Tổng quan**: `STAR_SYSTEM_SUMMARY.md`

---

## 🚀 SAU KHI ĐỦ 6 SAO

Sửa `StarCollectionSystem.OnAllStarsCollected()`:

```csharp
private void OnAllStarsCollected()
{
    // Đã có: Dừng spawn zombie
    WaveManager.instance.StopAllWaves();
    
    // 🔥 THÊM EVENT CỦA BẠN:
    
    // Spawn Boss
    // BossManager.instance.SpawnBoss();
    
    // Load Level
    // SceneManager.LoadScene("BossLevel");
    
    // Hiển thị message
    // ShowMessage("Bạn thắng rồi!");
}
```

---

## 🎯 2 CÁCH SETUP

### ⚡ CƠ BẢN (Khuyến nghị - chỉ 3 phút):
✅ Chỉ cần 1 text "⭐ X/6"  
✅ Đủ 6 sao → Dừng spawn  
✅ Không cần icon, panel phức tạp  
📖 Xem: `SETUP_UNITY_DON_GIAN.md`

### 🎨 ĐẦY ĐỦ (Nếu muốn đẹp):
✅ 6 star icons animation  
✅ Victory Panel với buttons  
✅ Âm thanh + hiệu ứng  
📖 Xem: `SETUP_STAR_SYSTEM.md`

---

**✨ DONE! Enjoy your game! 🎮**
