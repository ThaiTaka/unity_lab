# 🎬 HƯỚNG DẪN SETUP FAKER CUBE REVEAL EFFECT

## 🎯 CHỨC NĂNG MỚI:
Sau khi chớp mở mắt trong cutscene, **Cube Faker (Absolute Chyssey)** sẽ:
1. **Fade in** từ từ xuất hiện (1 giây)
2. **Hiển thị toàn màn hình** (2 giây)
3. **Fade out** từ từ biến mất (0.8 giây)
4. Rồi mới vào Game scene

---

## ⚡ SETUP TRONG UNITY

### BƯỚC 1: Đặt Cube vào IntroCutscene

1. **Mở scene IntroCutscene**

2. **Chọn Cube "cube" trong Hierarchy**

3. **Đặt vị trí trước Camera**:
   ```
   Inspector → Transform
   ├─ Position: X = 0, Y = 0, Z = 5
   ├─ Rotation: X = 0, Y = 0, Z = 0
   ├─ Scale: X = 3, Y = 3, Z = 3 (hoặc lớn hơn nếu muốn)
   ```

4. **Camera Position** (Main Camera):
   ```
   Position: X = 0, Y = 0, Z = 0
   Rotation: X = 0, Y = 0, Z = 0
   ```
   
   Để cube hiện **toàn màn hình**, có thể:
   - Tăng Scale của Cube lên 5-10
   - Hoặc đưa Cube gần Camera hơn (Z = 2-3)

---

### BƯỚC 2: Setup Material cho Cube

1. **Chọn Cube**

2. **Trong Inspector → Mesh Renderer → Materials**:
   - Material đang dùng: `Ga6QT-IWkAA7-b1`

3. **Kiểm tra Material Settings**:
   ```
   Material: Ga6QT-IWkAA7-b1
   ├─ Surface Type: Transparent (Script sẽ tự set)
   ├─ Rendering Mode: Transparent
   └─ Base Map: Hình ảnh Absolute Chyssey
   ```

**LƯU Ý:** Script sẽ TỰ ĐỘNG chuyển Material sang Transparent mode khi fade!

---

### BƯỚC 3: Assign Cube vào Script

1. **Chọn IntroCutsceneManager** trong Hierarchy

2. **Inspector → IntroCutscene (Script)**

3. **Kéo Cube vào field "Faker Cube"**:
   ```
   IntroCutscene (Script)
   ├─ UI References
   │  ├─ Dialogue Text: [DialogueText]
   │  ├─ Black Screen: [BlackScreen]
   │  ├─ Eye Overlay: [EyeOverlay]
   │  └─ Canvas Group: [CutsceneCanvas]
   │
   ├─ Faker Cube Reveal
   │  ├─ Faker Cube: [cube] ← KÉO CUBE VÀO ĐÂY
   │  ├─ Cube Reveal Duration: 1.0 (fade in)
   │  ├─ Cube Display Time: 2.0 (hiển thị)
   │  └─ Cube Fade Out Duration: 0.8 (fade out)
   │
   ├─ Settings
   │  └─ ...
   ```

---

### BƯỚC 4: Camera Settings

Để Cube hiện **toàn màn hình**, điều chỉnh:

**Option 1: Tăng Scale Cube**
```
Cube Scale: X = 10, Y = 10, Z = 10
Position: X = 0, Y = 0, Z = 5
```

**Option 2: Đưa Cube gần Camera**
```
Cube Scale: X = 5, Y = 5, Z = 5
Position: X = 0, Y = 0, Z = 2
```

**Option 3: Zoom Camera** (nếu muốn)
```
Main Camera → Field of View: 30 (zoom in)
```

---

## 🎬 LUỒNG HOẠT ĐỘNG MỚI

```
Menu Scene
    ↓
Click "Bắt Đầu Chơi"
    ↓
Load IntroCutscene Scene
    ↓
[Màn hình đen]
    ↓
Dialogue 1-5 (Anti và Faker)
    ↓
[Chớp mắt 4 lần]
    ↓
[Mở mắt hoàn toàn]
    ↓
🆕 [CUBE FAKER FADE IN - 1 giây]
    ↓
🆕 [HIỂN THỊ CUBE TOÀN MÀN HÌNH - 2 giây]
    ↓
🆕 [CUBE FADE OUT - 0.8 giây]
    ↓
Load Game Scene
```

---

## 🎨 TÙY CHỈNH

### Thay đổi thời gian hiển thị:
```
IntroCutscene (Script) → Faker Cube Reveal
├─ Cube Reveal Duration: 1.5 (fade in chậm hơn)
├─ Cube Display Time: 3.0 (hiển thị lâu hơn)
└─ Cube Fade Out Duration: 1.2 (fade out chậm hơn)
```

### Thay đổi kích thước Cube:
```
Cube → Transform → Scale
├─ X = 15, Y = 15, Z = 15 (cực lớn)
hoặc
├─ X = 5, Y = 5, Z = 5 (vừa phải)
```

### Thêm hiệu ứng xoay Cube:
Thêm component **Rotate** script (tùy chọn):
```csharp
void Update()
{
    transform.Rotate(0, 10 * Time.deltaTime, 0);
}
```

---

## ✅ TEST

1. **Play IntroCutscene**
2. **Xem cutscene**:
   - ✓ Dialogue 5 dòng
   - ✓ Chớp mắt 4 lần
   - ✓ Mở mắt từ từ
   - ✓ **CUBE FAKER xuất hiện từ từ**
   - ✓ **Hiển thị toàn màn hình 2 giây**
   - ✓ **Biến mất từ từ**
   - ✓ Vào game

3. **Test Skip**:
   - Nhấn **Space** → Skip toàn bộ, vào game ngay

---

## 🐛 TROUBLESHOOTING

### ❌ Cube không hiện:
**Fix:**
- Cube đã assign vào "Faker Cube" field?
- Cube có Material với texture?
- Cube position Z > 0 (trước camera)?

### ❌ Cube không trong suốt/không fade:
**Fix:**
- Script sẽ TỰ ĐỘNG set Material thành Transparent
- Nếu vẫn lỗi, thử đổi Material Rendering Mode = Transparent trong Inspector

### ❌ Cube quá nhỏ:
**Fix:**
- Tăng Scale: X = 10, Y = 10, Z = 10
- Hoặc đưa gần Camera: Position Z = 2

### ❌ Cube quá lớn/tràn màn hình:
**Fix:**
- Giảm Scale: X = 3, Y = 3, Z = 3
- Hoặc đẩy xa Camera: Position Z = 8

### ❌ Material bị lỗi sau fade:
**Fix:**
Script tự động set Transparent mode. Nếu cần reset:
```csharp
// Material sẽ về trạng thái ban đầu khi Cube.SetActive(false)
```

---

## 💡 TIPS

### Thêm hiệu ứng Glow:
1. Material → Emission: ON
2. Emission Color: White
3. Emission Intensity: 2.0

### Thêm âm thanh:
```csharp
[Header("Audio")]
public AudioClip fakerRevealSound;

// Trong ShowFakerCube():
if (fakerRevealSound != null)
{
    AudioSource.PlayClipAtPoint(fakerRevealSound, Camera.main.transform.position);
}
```

### Thêm text "ABSOLUTE CHYSSEY":
```
Canvas → UI → Text - TextMeshPro
Position: Center
Text: "ABSOLUTE CHYSSEY"
Font Size: 100
Fade cùng lúc với Cube
```

---

## 📋 CHECKLIST

- [ ] Cube "cube" đã đặt trong scene IntroCutscene
- [ ] Cube Position/Scale đã điều chỉnh để hiện toàn màn hình
- [ ] Cube có Material với texture Faker
- [ ] Cube đã assign vào IntroCutscene script → Faker Cube field
- [ ] Test: Cube fade in → hiển thị 2s → fade out → vào game
- [ ] Test: Skip bằng Space hoạt động

---

## 🎬 HIERARCHY STRUCTURE

```
IntroCutscene Scene
├── IntroCutsceneManager (IntroCutscene script)
├── CutsceneCanvas (Canvas + Canvas Group)
│   ├── BlackScreen (Image - Black)
│   ├── DialogueText (TextMeshPro)
│   └── EyeOverlay (Image - Black, Alpha 0)
├── Main Camera
│   └── Position: (0, 0, 0)
└── cube (Faker Cube) 🆕
    ├── Material: Ga6QT-IWkAA7-b1
    ├── Position: (0, 0, 5)
    └── Scale: (5, 5, 5)
```

---

## 🎉 KẾT QUẢ

**Cutscene giờ sẽ có:**
1. ✅ Dialogue Anti và Faker
2. ✅ Hiệu ứng chớp mắt như vừa tỉnh dậy
3. ✅ **Cube Faker (Absolute Chyssey) xuất hiện EPIC**
4. ✅ Fade in/out mượt mà như phim điện ảnh
5. ✅ Vào game sau khi xem xong

**🎬 CUTSCENE LEVEL: AAA GAME!** ✨
