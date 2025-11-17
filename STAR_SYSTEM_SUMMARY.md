# ⭐ HỆ THỐNG STAR COLLECTION - TỔNG QUAN

## 📋 Tóm Tắt
Đã tạo hoàn chỉnh hệ thống:
- ✅ Diệt 1 zombie → +1 sao xuất hiện góc phải Canvas
- ✅ Hiển thị 6 ô sao trên UI (grey → yellow khi thu thập)
- ✅ Đủ 6 sao → Victory Panel xuất hiện
- ✅ Animation mượt mà + âm thanh

---

## 🎯 CÁC FILE ĐÃ TẠO

### 1. **StarCollectionSystem.cs** (Updated)
📍 `Assets/Survival 3D/Scripts/UI/StarCollectionSystem.cs`

**Chức năng:**
- Quản lý logic thu thập sao
- Tạo 6 star icons trên UI góc phải
- Animation khi thu thập sao (scale, color, rotation)
- Victory animation khi đủ 6 sao
- Phát âm thanh collect + victory

**Các thuộc tính quan trọng:**
```csharp
maxStars = 6                    // Số sao cần thu thập
currentStars = 0                // Số sao hiện tại
starIconContainer               // Container chứa 6 star icons
starIconPrefab                  // Prefab của 1 star icon
victoryPanel                    // Panel hiển thị khi thắng
starCollectSound               // Âm thanh thu thập sao
victorySound                   // Âm thanh chiến thắng
```

---

### 2. **StarParticleEffect.cs** (New)
📍 `Assets/Survival 3D/Scripts/UI/StarParticleEffect.cs`

**Chức năng:**
- Tạo hiệu ứng sao 3D bay lên khi zombie chết
- Animation: float up, rotate, scale, fade
- Tự động destroy sau thời gian

**Sử dụng:**
- Attach vào 3D star prefab (optional)
- Hiệu ứng visual đẹp mắt cho player

---

### 3. **VictoryPanel.cs** (New)
📍 `Assets/Survival 3D/Scripts/UI/VictoryPanel.cs`

**Chức năng:**
- Hiển thị màn hình chiến thắng khi đủ 6 sao
- Fade-in animation mượt mà
- 3 nút: Continue, Restart, Main Menu
- Phát victory music + particles

**Các nút:**
- **Continue**: Tiếp tục game (có thể trigger event mới)
- **Restart**: Reload scene hiện tại
- **Main Menu**: Quay về menu chính

---

### 4. **SETUP_STAR_SYSTEM.md** (New)
📍 `SETUP_STAR_SYSTEM.md`

**Hướng dẫn chi tiết:**
- Setup Canvas UI từng bước
- Tạo Star Icon Prefab
- Configure StarCollectionSystem
- Tạo Victory Panel UI
- Troubleshooting
- Customization tips

---

## 🔄 LUỒNG HOẠT ĐỘNG

```
1. Game Start
   └─> StarCollectionSystem.Start()
       └─> Tạo 6 star icons (grey) trên UI góc phải

2. Player diệt Zombie
   └─> NPC.Die()
       └─> onDeath.Invoke()
           └─> WaveManager.OnZombieDeath()
               └─> StarCollectionSystem.AddStar(zombiePosition)
                   ├─> Play sound (coin/star sound)
                   ├─> Spawn 3D star effect (optional)
                   ├─> Animate star icon (grey → yellow)
                   ├─> Update text "⭐ X/6"
                   └─> Check if currentStars >= 6
                       └─> OnAllStarsCollected()
                           ├─> Play victory sound
                           ├─> Victory star animation (bounce)
                           ├─> Show Victory Panel (fade-in)
                           └─> Stop wave spawning

3. Victory Panel
   ├─> Continue Button → Trigger next event (boss, level, etc.)
   ├─> Restart Button → Reload scene
   └─> Main Menu Button → Load menu scene
```

---

## 🎮 SETUP NHANH TRONG UNITY

### Bước 1: Tạo UI Hierarchy
```
Canvas
├── StarDisplayPanel (Top-Right Corner)
│   ├── StarCountText "⭐ 0/6"
│   └── StarIconContainer (Horizontal Layout Group)
│       └── (6 star icons sẽ tự động tạo)
│
└── VictoryPanel (Full Screen, INACTIVE)
    ├── Background (Semi-transparent)
    └── ContentPanel
        ├── TitleText "🎉 VICTORY! 🎉"
        ├── MessageText "You collected all stars!"
        ├── StarCountText "⭐ 6/6"
        └── Buttons
            ├── Continue Button
            ├── Restart Button
            └── Main Menu Button
```

### Bước 2: Tạo Star Icon Prefab
```
1. Create UI Image (50x50)
2. Sprite: Star ⭐ (hoặc dùng text)
3. Color: Yellow
4. Convert to Prefab → "StarIcon"
```

### Bước 3: Setup StarCollectionSystem GameObject
```
1. Create Empty GameObject "StarCollectionSystem"
2. Add Component: StarCollectionSystem script
3. Assign trong Inspector:
   ✓ Star Count Text
   ✓ Star Icon Container
   ✓ Star Icon Prefab
   ✓ Victory Panel
   ✓ Audio Clips (optional)
```

### Bước 4: Setup Victory Panel
```
1. Tạo UI Panel theo hierarchy trên
2. Add Component: VictoryPanel script
3. Add Component: Canvas Group
4. Assign references trong Inspector
5. ⚠️ DEACTIVATE panel (uncheck ✅)
```

---

## 🎨 TÍNH NĂNG CHI TIẾT

### ⭐ Star Icon Animation
Khi thu thập sao:
- Scale từ 0 → 1 (với animation curve)
- Color từ grey → yellow
- Rotate 360°
- Duration: 0.5s

### 🎉 Victory Animation
Khi đủ 6 sao:
- Tất cả 6 sao bounce lần lượt
- Delay 0.1s giữa mỗi sao
- Scale pulse effect
- Victory Panel fade-in sau 1.5s

### 🔊 Audio
- **Star Collect Sound**: Phát mỗi khi +1 sao
- **Victory Sound**: Phát khi đủ 6 sao

---

## 🛠️ CUSTOMIZATION

### Thay đổi số sao cần thu thập
```csharp
StarCollectionSystem → maxStars = 10
```

### Thay đổi vị trí hiển thị sao
```
Canvas → StarDisplayPanel → Anchor Points
- Top-Right: X: -100, Y: -50
- Top-Left: X: 100, Y: -50
- Bottom-Right: X: -100, Y: 50
```

### Thêm sự kiện khi đủ 6 sao
Sửa trong `StarCollectionSystem.cs`:
```csharp
private void OnAllStarsCollected()
{
    // ... existing code ...
    
    // Thêm event mới:
    BossManager.instance.SpawnBoss();
    // hoặc
    SceneManager.LoadScene("BossLevel");
    // hoặc
    PlayerInventory.UnlockReward();
}
```

---

## 🐛 TROUBLESHOOTING

### Sao không xuất hiện
✅ Kiểm tra:
- StarIconPrefab đã assign?
- StarIconContainer có Horizontal Layout Group?
- Console có lỗi?

### Victory Panel không hiện
✅ Kiểm tra:
- Victory Panel INACTIVE khi bắt đầu game?
- victoryPanel reference đã assign?
- currentStars có đạt maxStars?

### Zombie chết nhưng không +sao
✅ Kiểm tra:
- Console có log "⭐ Star collected!"?
- WaveManager có gọi StarCollectionSystem.AddStar()?
- NPC component có onDeath event?

---

## 📦 FILES TRONG GIT

```
Assets/Survival 3D/Scripts/UI/
├── StarCollectionSystem.cs       [Modified] ⭐ Main system
├── StarCollectionSystem.cs.meta
├── StarParticleEffect.cs         [New] 🌟 3D star effect
├── StarParticleEffect.cs.meta
├── VictoryPanel.cs                [New] 🏆 Victory screen
└── VictoryPanel.cs.meta

Root/
└── SETUP_STAR_SYSTEM.md           [New] 📖 Setup guide
```

---

## 🚀 NEXT STEPS

Sau khi đủ 6 sao, bạn có thể:

1. **Spawn Boss Fight**
   ```csharp
   BossManager.instance.StartBossFight();
   ```

2. **Load Next Level**
   ```csharp
   SceneManager.LoadScene("Level2");
   ```

3. **Unlock Rewards**
   ```csharp
   PlayerInventory.UnlockItem("SuperWeapon");
   PlayerInventory.AddGold(1000);
   ```

4. **Show Cutscene**
   ```csharp
   CutsceneManager.PlayVictoryCutscene();
   ```

5. **Spawn Reward Chest**
   ```csharp
   Instantiate(rewardChest, spawnPoint, Quaternion.identity);
   ```

---

## ✅ CHECKLIST HOÀN THÀNH

- ✅ StarCollectionSystem với UI đẹp
- ✅ 6 star icons animation
- ✅ Victory Panel với buttons
- ✅ Audio support
- ✅ Particle effects (optional)
- ✅ Documentation đầy đủ
- ✅ Integrated với WaveManager + NPC
- ✅ Push lên GitHub

---

## 📞 SUPPORT

Nếu gặp vấn đề:
1. Đọc `SETUP_STAR_SYSTEM.md`
2. Kiểm tra Console logs
3. Verify tất cả references đã assign
4. Test từng component riêng lẻ

---

**🎉 HỆ THỐNG ĐÃ SẴN SÀNG! CHÚC BẠN TẠO GAME VUI VẺ! 🚀**
