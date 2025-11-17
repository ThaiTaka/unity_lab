# 🎮 HƯỚNG DẪN SETUP UNITY - ĐƠN GIẢN

## 🎯 MỤC TIÊU
- Diệt zombie → +1 sao
- Hiển thị text "⭐ X/6" trên UI
- Đủ 6 sao → Dừng spawn zombie
- **KHÔNG CẦN** Victory Panel phức tạp

---

## ⚡ SETUP NHANH - CHỈ 3 BƯỚC

### BƯỚC 1: Tạo Text Hiển Thị Sao 📝

1. **Mở Canvas trong Hierarchy**
   - Nếu chưa có Canvas: `Right Click → UI → Canvas`

2. **Tạo Panel chứa text** (góc phải màn hình):
   ```
   Hierarchy → Canvas → Right Click → UI → Panel
   Rename: "StarPanel"
   ```

3. **Setup StarPanel**:
   - Chọn `StarPanel` trong Hierarchy
   - Trong Inspector → **Rect Transform**:
     - **Anchor Presets**: Click góc phải trên (hoặc nhấn Alt + click góc phải trên)
     - **Pos X**: `-150` (càng âm càng sát phải)
     - **Pos Y**: `-50` (càng âm càng sát trên)
     - **Width**: `200`
     - **Height**: `60`
   - **Image** component:
     - **Color**: Đen (0,0,0) với Alpha: `150` (trong suốt một chút)

4. **Tạo Text hiển thị sao**:
   ```
   StarPanel → Right Click → UI → Text - TextMeshPro
   Rename: "StarText"
   ```
   
   > Nếu xuất hiện popup "Import TMP Essentials" → Click "Import TMP Essentials"

5. **Setup StarText**:
   - **Rect Transform**: Click "Stretch" (icon góc trên bên phải)
     - **Left**: `10`, **Right**: `10`, **Top**: `10`, **Bottom**: `10`
   - **TextMeshPro - Text**:
     - **Text**: `⭐ 0/6`
     - **Font Size**: `36`
     - **Color**: White
     - **Alignment**: Center (cả ngang và dọc)
     - **Font Style**: Bold (nếu muốn)

✅ **Xong bước 1! Bạn đã có UI hiển thị sao góc phải màn hình**

---

### BƯỚC 2: Tạo StarCollectionSystem GameObject 🌟

1. **Tạo Empty GameObject**:
   ```
   Hierarchy → Right Click → Create Empty
   Rename: "StarCollectionSystem"
   ```

2. **Add Script**:
   - Chọn `StarCollectionSystem` trong Hierarchy
   - Inspector → **Add Component** → Gõ "star" → Chọn **StarCollectionSystem**

3. **Setup Inspector** (chỉ cần 1 dòng!):
   - Tìm mục **UI References**
   - **Star Count Text**: 
     - Kéo object `StarText` từ Hierarchy vào ô này
     - HOẶC click vòng tròn bên phải → chọn `StarText`
   
   - **CÁC Ô KHÁC BỎ TRỐNG** (Star Icon Container, Star Icon Prefab, Victory Panel)

✅ **Xong bước 2! System đã sẵn sàng hoạt động**

---

### BƯỚC 3: Test 🎮

1. **Bấm Play** trong Unity

2. **Vào game và diệt zombie**

3. **Kiểm tra**:
   - ✅ Góc phải màn hình có text "⭐ 0/6"
   - ✅ Diệt 1 zombie → Text đổi thành "⭐ 1/6"
   - ✅ Mở **Console** (Window → General → Console):
     - Thấy log: "⭐ Star collected! 1/6"
   - ✅ Diệt đủ 6 zombies:
     - Text: "⭐ 6/6"
     - Console: "🎉 ĐỦ 6 SAO! Dừng spawn zombie!"
     - Console: "✅ Đã dừng spawn zombie!"
     - **Không có zombie mới spawn nữa**

✅ **XONG! Hệ thống hoạt động!**

---

## 🖼️ HÌNH ẢNH THAM KHẢO

### Canvas Hierarchy:
```
Canvas
└── StarPanel (Panel - góc phải trên)
    └── StarText (TextMeshPro) "⭐ 0/6"

StarCollectionSystem (Empty GameObject)
```

### Inspector của StarCollectionSystem:
```
┌─ StarCollectionSystem (Script) ────────┐
│ Star Settings                           │
│   Max Stars: 6                          │
│   Current Stars: 0                      │
│                                         │
│ UI References                           │
│   Star Count Text: [StarText]  ← KÉO VÀO ĐÂY
│   Star Icon Container: None (Skip)     │
│   Star Icon Prefab: None (Skip)        │
│   Victory Panel: None (Skip)           │
│                                         │
│ Star Visual (Optional)                  │
│   Star Prefab: None (Skip)              │
│   Star Drop Height: 2                   │
│                                         │
│ Animation Settings (Skip tất cả)        │
│ Audio (Skip tất cả)                     │
└─────────────────────────────────────────┘
```

---

## 🐛 LỖI THƯỜNG GẶP VÀ CÁCH FIX

### ❌ Text không hiện trên màn hình
**Fix:**
- Chọn Canvas → Inspector → Canvas → Render Mode: **Screen Space - Overlay**
- Chọn StarPanel → Kiểm tra Position X, Y đúng chưa
- Chọn StarText → Kiểm tra màu chữ là **White** (không phải đen)

### ❌ Text không cập nhật khi diệt zombie
**Fix:**
1. Mở **Console** (Window → General → Console)
2. Diệt zombie, xem có log "⭐ Star collected!" không?
   - **CÓ LOG**: StarText chưa được assign
     - Chọn StarCollectionSystem → Inspector → Star Count Text → Kéo StarText vào
   - **KHÔNG CÓ LOG**: WaveManager chưa được setup đúng
     - Kiểm tra WaveManager có trong scene không
     - Kiểm tra Zombie prefab có NPC component với onDeath event

### ❌ Diệt đủ 6 zombie vẫn spawn tiếp
**Fix:**
1. Mở Console, tìm log "✅ Đã dừng spawn zombie!"
   - **CÓ LOG**: WaveManager không nghe lệnh StopAllWaves()
   - **KHÔNG CÓ LOG**: StarCollectionSystem không chạy OnAllStarsCollected()
2. Kiểm tra WaveManager trong scene có **WaveManager.instance** không null

### ❌ Console báo lỗi "NullReferenceException"
**Fix:**
- Lỗi thường xảy ra vì thiếu reference
- Đọc dòng lỗi, thường nói thiếu gì
- Kiểm tra lại **Star Count Text** đã assign chưa

---

## 🎨 TÙY CHỈNH

### Đổi vị trí hiển thị sao

**Góc trái trên:**
```
StarPanel → Rect Transform
  Anchor: Top-Left
  Pos X: 150
  Pos Y: -50
```

**Góc phải dưới:**
```
StarPanel → Rect Transform
  Anchor: Bottom-Right
  Pos X: -150
  Pos Y: 50
```

**Giữa màn hình trên:**
```
StarPanel → Rect Transform
  Anchor: Top-Center
  Pos X: 0
  Pos Y: -50
```

### Đổi số sao cần thu thập

```
StarCollectionSystem → Inspector
  Max Stars: 10 (thay vì 6)
```

### Đổi màu text

```
StarText → TextMeshPro
  Color: Yellow / Red / Green / ...
```

### Đổi kích thước text

```
StarText → TextMeshPro
  Font Size: 48 (lớn hơn)
  Font Size: 24 (nhỏ hơn)
```

---

## 🔥 THÊM SỰ KIỆN KHI ĐỦ 6 SAO

Mở file `StarCollectionSystem.cs`, tìm hàm `OnAllStarsCollected()`:

```csharp
private void OnAllStarsCollected()
{
    Debug.Log($"🎉 ĐỦ 6 SAO! Dừng spawn zombie!");
    
    // ... existing code ...
    
    // 🔥 THÊM CODE CỦA BẠN Ở ĐÂY:
    
    // Ví dụ 1: Spawn Boss
    // FindObjectOfType<BossManager>().SpawnBoss();
    
    // Ví dụ 2: Load Scene mới
    // SceneManager.LoadScene("BossLevel");
    
    // Ví dụ 3: Hiển thị message
    // Debug.Log("Bạn đã chiến thắng! Chuẩn bị cho boss fight!");
    
    // Ví dụ 4: Unlock item
    // PlayerInventory.instance.UnlockItem("SuperWeapon");
}
```

---

## ✅ CHECKLIST HOÀN THÀNH

- [ ] Canvas có StarPanel ở góc phải
- [ ] StarPanel có StarText với text "⭐ 0/6"
- [ ] Có GameObject "StarCollectionSystem" với script
- [ ] Star Count Text đã assign = StarText
- [ ] Test: Diệt zombie → Text cập nhật
- [ ] Test: Đủ 6 sao → Dừng spawn zombie
- [ ] Console có log "✅ Đã dừng spawn zombie!"

---

## 🚀 NÂNG CAO (SAU NÀY)

Khi bạn muốn thêm UI đẹp hơn:
- Tạo 6 icon sao riêng lẻ (xem file `SETUP_STAR_SYSTEM.md`)
- Thêm Victory Panel với buttons
- Thêm animation cho sao
- Thêm âm thanh

**NHƯNG BÂY GIỜ KHÔNG CẦN!** Chỉ cần text "⭐ X/6" là đủ!

---

## 📞 CẦN TRỢ GIÚP?

1. Mở **Console** xem có lỗi gì
2. Kiểm tra lại 3 bước setup
3. Verify references đã assign đúng chưa
4. Test từng zombie một, xem log

---

**🎉 XONG! Setup siêu đơn giản, chỉ cần 1 text là chạy được!**

**📌 GHI NHỚ:** Chỉ cần assign **Star Count Text**, các ô khác bỏ trống!
