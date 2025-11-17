# 🎬 HƯỚNG DẪN SETUP INTRO CUTSCENE

## 🎯 CHỨC NĂNG
- Màn hình đen với dialogue xuất hiện từng dòng
- Dialogue giữa Anti và Faker
- Hiệu ứng chớp mắt và mở mắt (như vừa tỉnh dậy)
- Chuyển vào game sau cutscene
- Có thể skip bằng Space

---

## ⚡ SETUP TRONG UNITY

### BƯỚC 1: Tạo Scene IntroCutscene

1. **Tạo Scene mới**:
   - File → New Scene
   - Save As: `IntroCutscene`
   - Lưu trong thư mục `Scenes`

2. **Thêm vào Build Settings**:
   - File → Build Settings
   - Add Open Scenes
   - Đảm bảo thứ tự:
     ```
     0. Menu
     1. IntroCutscene ← THÊM MỚI
     2. Game
     ```

---

### BƯỚC 2: Tạo UI Canvas

1. **Tạo Canvas**:
   ```
   Hierarchy → Right Click → UI → Canvas
   Rename: "CutsceneCanvas"
   ```

2. **Setup Canvas**:
   - Canvas Scaler → UI Scale Mode: **Scale With Screen Size**
   - Reference Resolution: `1920 x 1080`

3. **Tạo các UI elements**:

   **a) Black Screen (Background):**
   ```
   CutsceneCanvas → Right Click → UI → Image
   Rename: "BlackScreen"
   ```
   - Anchor: Stretch All
   - Color: Black (0, 0, 0, 255)
   - Raycast Target: OFF

   **b) Dialogue Text:**
   ```
   CutsceneCanvas → Right Click → UI → Text - TextMeshPro
   Rename: "DialogueText"
   ```
   - Anchor: Bottom-Center
   - Pos Y: 100
   - Width: 1600, Height: 300
   - Font Size: 40
   - Color: White
   - Alignment: Center-Middle
   - Text: (để trống)

   **c) Eye Overlay (Hiệu ứng mở mắt):**
   ```
   CutsceneCanvas → Right Click → UI → Image
   Rename: "EyeOverlay"
   ```
   - Anchor: Stretch All
   - Color: Black (0, 0, 0, 0) ← Alpha = 0
   - Raycast Target: OFF

---

### BƯỚC 3: Setup IntroCutscene Manager

1. **Tạo GameObject**:
   ```
   Hierarchy → Right Click → Create Empty
   Rename: "IntroCutsceneManager"
   ```

2. **Add Script**:
   - Add Component → **IntroCutscene**

3. **Assign References trong Inspector**:
   ```
   IntroCutscene (Script)
   ├─ UI References
   │  ├─ Dialogue Text: [DialogueText]
   │  ├─ Black Screen: [BlackScreen]
   │  ├─ Eye Overlay: [EyeOverlay]
   │  └─ Canvas Group: [CutsceneCanvas] (Add Canvas Group component)
   │
   ├─ Settings
   │  ├─ Typing Speed: 0.05
   │  ├─ Delay Between Lines: 1.5
   │  └─ Slow Typing Speed: 0.15
   │
   ├─ Eye Effect
   │  ├─ Eye Blink Duration: 0.3
   │  ├─ Eye Open Duration: 1.5
   │  └─ Blink Count: 2
   │
   ├─ Scene
   │  └─ Game Scene Name: "Game"
   │
   └─ Skip
      ├─ Skip Key: Space
      └─ Can Skip: ✓
   ```

4. **Add Canvas Group**:
   - Chọn `CutsceneCanvas`
   - Add Component → **Canvas Group**

---

### BƯỚC 4: Update Menu Scene

**Menu.cs đã được update tự động!**

Nếu cần kiểm tra:
- Menu → Play Button → OnClick() → Menu.OnNewGameButton()
- Sẽ load scene "IntroCutscene" thay vì "Game"

---

## 🎬 LUỒNG HOẠT ĐỘNG

```
Menu Scene
    ↓
Click "Bắt Đầu Chơi"
    ↓
Load IntroCutscene Scene
    ↓
[Màn hình đen]
    ↓
Dialogue 1: "Anti: Lại vô địch đấy..."
    ↓ (1.5s)
Dialogue 2: "Anti: Lúc nào cũng 3Ker..."
    ↓ (1.5s)
Dialogue 3: "Giờ kêu đánh lại 6 trận..."
    ↓ (1.5s)
Dialogue 4: "Faker: Thế giờ 6 cúp sau......"
    ↓ (1.5s)
Dialogue 5: "Faker: SẼ.....DÀNH......CHO......" (typing chậm)
    ↓ (2s)
[Hiệu ứng chớp mắt 2 lần]
    ↓
[Mở mắt từ từ]
    ↓
Load Game Scene
```

---

## ✅ TEST

1. **Play Menu Scene**
2. **Click "Bắt Đầu Chơi"**
3. **Xem cutscene**:
   - ✓ Màn hình đen
   - ✓ Text xuất hiện từng chữ
   - ✓ 5 dòng dialogue
   - ✓ Dòng cuối gõ chậm hơn
   - ✓ Chớp mắt 2 lần
   - ✓ Mở mắt từ từ
   - ✓ Vào game

4. **Test Skip**:
   - Nhấn **Space** bất kỳ lúc nào → Skip ngay vào game

---

## 🎨 TÙY CHỈNH

### Đổi tốc độ typing:
```
IntroCutscene → Settings
├─ Typing Speed: 0.03 (nhanh hơn)
└─ Typing Speed: 0.08 (chậm hơn)
```

### Đổi delay giữa các dòng:
```
Delay Between Lines: 2.0 (chờ lâu hơn)
Delay Between Lines: 1.0 (chuyển nhanh hơn)
```

### Đổi số lần chớp mắt:
```
Blink Count: 3 (chớp 3 lần)
Blink Count: 1 (chớp 1 lần)
```

### Thêm/Sửa dialogue:
Mở `IntroCutscene.cs`, tìm:
```csharp
private string[] dialogues = new string[]
{
    "Dòng 1",
    "Dòng 2",
    "Dòng 3", // Thêm hoặc sửa tại đây
};
```

### Đổi phím skip:
```
Skip Key: Escape (dùng ESC)
Skip Key: Return (dùng Enter)
```

### Tắt skip:
```
Can Skip: ✗ (bỏ tích)
```

---

## 🎨 NÂNG CAO

### Thêm âm thanh:
1. Import audio files:
   - `typing_sound.mp3` (âm thanh gõ chữ)
   - `blink_sound.mp3` (âm thanh chớp mắt)

2. Thêm vào script:
```csharp
[Header("Audio")]
public AudioClip typingSound;
public AudioClip blinkSound;
private AudioSource audioSource;

// Trong TypeText():
audioSource.PlayOneShot(typingSound);
```

### Thêm background music:
```
IntroCutsceneManager → Add Component → Audio Source
├─ Audio Clip: [Background Music]
├─ Play On Awake: ✓
├─ Loop: ✓
└─ Volume: 0.3
```

### Thêm skip indicator:
```
Canvas → UI → Text
Text: "Nhấn Space để bỏ qua"
Position: Bottom-Right
Font Size: 20
Color: White (200 alpha)
```

---

## 🐛 TROUBLESHOOTING

### ❌ Không load IntroCutscene:
**Fix:**
1. Check Build Settings có scene "IntroCutscene"?
2. Check Menu.cs: `SceneManager.LoadScene("IntroCutscene")`

### ❌ Text không hiện:
**Fix:**
- DialogueText đã assign trong Inspector?
- Font color = White?
- Canvas render mode = Screen Space - Overlay?

### ❌ Hiệu ứng mắt không hoạt động:
**Fix:**
- EyeOverlay đã assign?
- Eye Overlay color alpha = 0 ban đầu?

### ❌ Không vào game sau cutscene:
**Fix:**
- Game Scene Name = "Game" (đúng tên)?
- Scene "Game" có trong Build Settings?

---

## 📋 DIALOGUE CONTENT

```
Dòng 1: "Anti: Lại vô địch đấy, tê liệt cũng chỉ ăn may à ?"
Dòng 2: "Anti: Lúc nào cũng 3Ker, 3 Gà thì chửi ỏm lên"
Dòng 3: "Giờ kêu đánh lại 6 trận lấy cúp đố lấy được đấy"
Dòng 4: "Faker: Thế giờ 6 cúp sau ......"
Dòng 5: "Faker: SẼ ..... DÀNH ...... CHO ...... CHÚNG ....... EM"
```

---

## ✅ CHECKLIST

- [ ] Scene "IntroCutscene" đã tạo và add vào Build Settings
- [ ] Canvas với BlackScreen, DialogueText, EyeOverlay
- [ ] IntroCutsceneManager với script IntroCutscene
- [ ] Tất cả references đã assign
- [ ] Canvas Group đã add vào Canvas
- [ ] Menu.cs load "IntroCutscene"
- [ ] Test: Menu → Play → Cutscene → Game
- [ ] Test: Skip bằng Space

---

## 🎬 HIERARCHY STRUCTURE

```
IntroCutscene Scene
├── IntroCutsceneManager (IntroCutscene script)
├── CutsceneCanvas (Canvas + Canvas Group)
│   ├── BlackScreen (Image - Black)
│   ├── DialogueText (TextMeshPro)
│   └── EyeOverlay (Image - Black, Alpha 0)
└── Main Camera
```

---

**🎉 HOÀN THÀNH! Bây giờ game có intro cinematic như game AAA!** 🎬✨

**💡 TIP:** Bạn có thể thêm nhiều dialogue hơn bằng cách edit mảng `dialogues[]` trong code!
