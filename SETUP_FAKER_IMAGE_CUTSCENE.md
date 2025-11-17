# 🎬 HƯỚNG DẪN SETUP FAKER IMAGE CUTSCENE (BẢN MỚI)

## 🎯 LUỒNG CUTSCENE MỚI:

```
Dialogue 1: "Anti Fan: Lại vô địch đấy..."
    ↓
Dialogue 2: "Anti Fan: Lúc nào cũng 3Ker..."
    ↓
Dialogue 3: "Anti Fan: Giờ kêu đánh lại 6 trận..."
    ↓
Dialogue 4: "Feaker: Thế giờ 6 cúp sau ......"
    ↓
🆕 Dialogue 5: "Feaker: SẼ ... DÀNH .... CHO .... Các .... EM"
    + ĐỒNG THỜI: Hình Faker từ từ fade in (5 giây)
    ↓
🆕 Hình Faker chiếm toàn màn hình (giữ 1 giây)
    ↓
🆕 Fade về đen (2 giây)
    ↓
🆕 Chớp mắt liên tục (4 lần)
    ↓
🆕 Mở mắt xong → CHUYỂN SCENE NGAY LẬP TỨC
    ↓
Game Scene
```

---

## ⚡ SETUP TRONG UNITY

### BƯỚC 1: Tạo Faker Image UI

1. **Chọn CutsceneCanvas** trong Hierarchy

2. **Tạo Image mới**:
   ```
   CutsceneCanvas → Right Click → UI → Image
   Rename: "FakerImage"
   ```

3. **Setup FakerImage**:
   ```
   Inspector → FakerImage
   ├─ Anchor: Stretch All (Left: 0, Right: 0, Top: 0, Bottom: 0)
   ├─ Source Image: [Kéo ảnh Ga6QT-IWkAA7-b1.jpg vào đây]
   ├─ Color: White (255, 255, 255, 0) ← Alpha = 0 (trong suốt)
   ├─ Preserve Aspect: ✓ (hoặc không, tùy ý)
   └─ Raycast Target: OFF
   ```

4. **Thứ tự Hierarchy** (từ dưới lên trên):
   ```
   CutsceneCanvas
   ├── BlackScreen (bottom layer)
   ├── FakerImage (middle layer) 🆕
   ├── DialogueText (top layer)
   └── EyeOverlay (overlay)
   ```

---

### BƯỚC 2: Assign vào Script

1. **Chọn IntroCutsceneManager**

2. **Inspector → IntroCutscene (Script)**

3. **Assign references**:
   ```
   IntroCutscene (Script)
   ├─ UI References
   │  ├─ Dialogue Text: [DialogueText]
   │  ├─ Black Screen: [BlackScreen]
   │  ├─ Eye Overlay: [EyeOverlay]
   │  └─ Canvas Group: [CutsceneCanvas]
   │
   ├─ Faker Image Reveal 🆕
   │  ├─ Faker Image: [FakerImage] ← KÉO IMAGE VÀO ĐÂY
   │  ├─ Faker Fade In Duration: 5.0 (ảnh fade in trong 5s)
   │  └─ Faker Fade Out Duration: 2.0 (fade về đen trong 2s)
   │
   ├─ Settings
   │  ├─ Typing Speed: 0.05
   │  ├─ Delay Between Lines: 1.5
   │  └─ Slow Typing Speed: 0.15
   │
   ├─ Eye Effect
   │  ├─ Eye Blink Duration: 0.3
   │  ├─ Eye Open Duration: 2.0
   │  └─ Blink Count: 4
   │
   └─ Scene
      └─ Game Scene Name: "Game"
   ```

---

### BƯỚC 3: XÓA Cube Faker (Không dùng nữa)

1. **Trong scene IntroCutscene**:
   - Tìm GameObject "cube"
   - **Delete** (không cần nữa)

2. **Trong Inspector**:
   - IntroCutscene → Faker Cube: **None** (bỏ trống)

---

## 🎬 TIMELINE CHI TIẾT:

### Phase 1: Dialogue 1-4 (Bình thường)
```
0:00 - Dialogue 1 type → clear
0:02 - Dialogue 2 type → clear
0:04 - Dialogue 3 type → clear
0:06 - Dialogue 4 type → clear
```

### Phase 2: Dialogue 5 + Faker Reveal (ĐỒNG THỜI)
```
0:08 - Bắt đầu type: "Feaker: SẼ ... DÀNH ..."
     ↓
     [CÙNG LÚC: Ảnh Faker bắt đầu fade in từ alpha 0 → 1]
     ↓
0:13 - Typing xong, ảnh Faker đã hiện 100%
     ↓
0:14 - Giữ ảnh 1 giây
```

### Phase 3: Fade to Black
```
0:14 - Clear text, bắt đầu fade ảnh về đen
     ↓
0:16 - Ảnh biến mất hoàn toàn, màn hình đen
```

### Phase 4: Eye Blink + Transition
```
0:16 - Chớp mắt lần 1
0:17 - Chớp mắt lần 2
0:18 - Chớp mắt lần 3
0:19 - Chớp mắt lần 4
0:20 - Mở mắt cuối
     ↓
0:21 - CHUYỂN SCENE NGAY (không đợi)
```

---

## 🎨 TÙY CHỈNH:

### Thay đổi tốc độ fade ảnh:
```
Faker Fade In Duration: 7.0 (fade in chậm hơn, kịch tính hơn)
Faker Fade Out Duration: 3.0 (fade out chậm hơn)
```

### Thay đổi số lần chớp mắt:
```
Blink Count: 6 (chớp nhiều hơn)
Blink Count: 2 (chớp ít hơn, nhanh hơn)
```

### Giữ ảnh Faker lâu hơn:
Sửa trong code, dòng:
```csharp
yield return new WaitForSeconds(1f); // Đổi thành 2f hoặc 3f
```

### Thêm hiệu ứng glow cho ảnh:
```
FakerImage → Add Component → Shadow
├─ Effect Color: White
├─ Effect Distance: (10, -10)
└─ Use Graphic Alpha: ✓
```

---

## ✅ TEST:

1. **Play IntroCutscene Scene**

2. **Kiểm tra:**
   - ✓ Dialogue 1-4 hiện bình thường
   - ✓ Dialogue 5: Text type chậm VÀ ảnh Faker fade in đồng thời
   - ✓ Ảnh Faker chiếm toàn màn hình (5 giây)
   - ✓ Fade về đen (2 giây)
   - ✓ Chớp mắt 4 lần
   - ✓ Mở mắt xong → Vào game NGAY

3. **Test Skip**:
   - Nhấn **Space** → Skip toàn bộ, vào game

---

## 🐛 TROUBLESHOOTING:

### ❌ Ảnh không hiện:
**Fix:**
- FakerImage đã assign vào script?
- Source Image đã set ảnh Faker?
- FakerImage color alpha = 0 ban đầu?
- FakerImage ở trên BlackScreen trong Hierarchy?

### ❌ Ảnh bị méo/kéo dãn:
**Fix:**
- Image → Preserve Aspect: ✓
- Hoặc Image Type: Filled / Sliced

### ❌ Text bị che bởi ảnh:
**Fix:**
- DialogueText phải ở TRÊN FakerImage trong Hierarchy
- Hoặc DialogueText → Canvas Renderer → Sort Order: 1

### ❌ Ảnh fade quá nhanh/chậm:
**Fix:**
- Faker Fade In Duration: Tăng/giảm giá trị
- Faker Fade Out Duration: Tăng/giảm giá trị

### ❌ Không chuyển scene sau chớp mắt:
**Fix:**
- Game Scene Name = "Game" (đúng tên)?
- Scene "Game" có trong Build Settings?

---

## 💡 NÂNG CAO:

### Thêm hiệu ứng zoom ảnh:
```csharp
// Trong TypeTextWithFakerReveal(), thêm:
RectTransform rt = fakerImage.rectTransform;
Vector3 startScale = Vector3.one;
Vector3 endScale = Vector3.one * 1.2f; // Zoom 120%

while (elapsed < fakerFadeInDuration)
{
    // ... fade alpha code ...
    rt.localScale = Vector3.Lerp(startScale, endScale, elapsed / fakerFadeInDuration);
}
```

### Thêm âm thanh dramatic:
```
IntroCutsceneManager → Add Component → Audio Source
├─ Audio Clip: [Dramatic Music]
├─ Play On Awake: OFF
├─ Loop: OFF
└─ Volume: 0.5

// Trong TypeTextWithFakerReveal():
audioSource.Play();
```

### Thêm text "SIX CHAMPIONSHIPS":
```
Canvas → UI → Text - TextMeshPro
├─ Text: "SIX CHAMPIONSHIPS"
├─ Font Size: 80
├─ Position: Center-Top
├─ Color: Gold
└─ Fade cùng lúc với ảnh Faker
```

---

## 📋 CHECKLIST:

- [ ] FakerImage đã tạo trong Canvas
- [ ] FakerImage anchor = Stretch All
- [ ] Source Image = Ga6QT-IWkAA7-b1.jpg
- [ ] FakerImage color alpha = 0
- [ ] FakerImage đã assign vào script
- [ ] Cube cũ đã xóa
- [ ] Test: Dialogue 5 + ảnh fade in đồng thời
- [ ] Test: Fade về đen → Chớp mắt → Chuyển scene ngay

---

## 🎬 HIERARCHY STRUCTURE:

```
IntroCutscene Scene
├── IntroCutsceneManager (IntroCutscene script)
├── CutsceneCanvas (Canvas + Canvas Group)
│   ├── BlackScreen (Image - Black) ← Bottom layer
│   ├── FakerImage (Image - Faker) 🆕 ← Middle layer
│   ├── DialogueText (TextMeshPro) ← Top layer
│   └── EyeOverlay (Image - Black, Alpha 0) ← Overlay
└── Main Camera
```

---

## 🎉 KẾT QUẢ:

**CUTSCENE GIỐNG PHIM ĐIỆN ẢNH:**
1. ✅ Anti Fan chê bai Faker
2. ✅ Faker nói: "SẼ ... DÀNH ... CHO ... Các ... EM"
3. ✅ **Hình Faker từ từ hiện ra (5s) - EPIC MOMENT**
4. ✅ Fade về đen - Dramatic
5. ✅ Chớp mắt như vừa tỉnh dậy
6. ✅ Vào game ngay lập tức

**🎬 LEVEL: AAA CINEMATICS!** ✨🔥

---

## 📝 LƯU Ý:

- **XÓA Cube cũ** đi, không dùng nữa
- **Dùng Image UI** thay vì 3D Object (mượt hơn, dễ control hơn)
- **Fade đồng thời** với typing tạo hiệu ứng dramatic
- **Không đợi sau khi mở mắt** - chuyển scene ngay để giữ momentum

**READY TO DOMINATE!** 🏆✨
