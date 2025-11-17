# 🎮 Hướng Dẫn Setup Màn Hình Loading

## 📋 Tổng Quan
Màn hình Loading hiển thị giữa **IntroCutscene** và **Game Scene** với:
- ✅ Thanh progress bar
- ✅ Phần trăm loading (0% → 100%)
- ✅ Loading tips đổi liên tục
- ✅ Fade in/out mượt mà
- ✅ Async loading để không lag

---

## 🛠️ Bước 1: Tạo Scene "Loading"

### 1.1. Tạo Scene Mới
1. **File** → **New Scene**
2. Chọn **Template: Empty**
3. **Ctrl + S** → Đặt tên: `Loading.unity`
4. Lưu vào: `Assets/Scenes/`

### 1.2. Add Scene vào Build Settings
1. **File** → **Build Settings**
2. **Add Open Scenes** (thêm scene Loading)
3. Đảm bảo thứ tự:
   - `Menu` (Scene 0)
   - `IntroCutscene` (Scene 1)
   - **`Loading` (Scene 2)** ← Scene mới
   - `Game` (Scene 3)

---

## 🎨 Bước 2: Setup UI Canvas

### 2.1. Tạo Canvas
1. **Right-click Hierarchy** → **UI** → **Canvas**
2. Đổi tên: `LoadingCanvas`
3. Inspector:
   - **Render Mode**: Screen Space - Overlay
   - **Canvas Scaler**:
     - UI Scale Mode: **Scale With Screen Size**
     - Reference Resolution: **1920 x 1080**
     - Match: **0.5** (Width/Height)

### 2.2. Thêm Canvas Group
1. Select `LoadingCanvas`
2. **Add Component** → **Canvas Group**
3. Để mặc định (Alpha = 1)

---

## 🖼️ Bước 3: Tạo UI Elements

### 3.1. Background (Màn Hình Đen)
1. **Right-click LoadingCanvas** → **UI** → **Image**
2. Đổi tên: `Background`
3. Inspector:
   - **RectTransform**: Stretch All (Left=0, Top=0, Right=0, Bottom=0)
   - **Image**: Color = **Black (0, 0, 0, 255)**
   - **Raycast Target**: ✅ Checked (chặn click)

---

### 3.2. Loading Bar Container
1. **Right-click LoadingCanvas** → **UI** → **Image**
2. Đổi tên: `LoadingBarBG` (Background của thanh loading)
3. Inspector:
   - **Pos X**: 0, **Pos Y**: -200
   - **Width**: 800, **Height**: 40
   - **Image**: Color = **Dark Gray (50, 50, 50, 255)**

### 3.3. Loading Bar Fill (Thanh Đầy)
1. **Right-click LoadingBarBG** → **UI** → **Image**
2. Đổi tên: `LoadingBarFill`
3. Inspector:
   - **Anchor Preset**: Stretch All (Left=0, Top=0, Right=0, Bottom=0)
   - **Image**:
     - Color: **Green (0, 255, 0, 255)** hoặc **Cyan (0, 255, 255, 255)**
     - **Image Type**: Filled
     - **Fill Method**: Horizontal
     - **Fill Origin**: Left
     - **Fill Amount**: **0** (sẽ tự động tăng lên)

---

### 3.4. Loading Text "Loading... 0%"
1. **Right-click LoadingCanvas** → **UI** → **Text - TextMeshPro**
2. Đổi tên: `LoadingText`
3. Inspector:
   - **Pos X**: 0, **Pos Y**: -260
   - **Width**: 800, **Height**: 60
   - **Text**: `Loading... 0%`
   - **Font Size**: 28
   - **Alignment**: Center + Middle
   - **Color**: White
   - **Font Style**: Bold

---

### 3.5. Tip Text (Mẹo Chơi Game)
1. **Right-click LoadingCanvas** → **UI** → **Text - TextMeshPro**
2. Đổi tên: `TipText`
3. Inspector:
   - **Pos X**: 0, **Pos Y**: -350
   - **Width**: 1200, **Height**: 100
   - **Text**: `💡 Thu thập tài nguyên để sinh tồn!`
   - **Font Size**: 24
   - **Alignment**: Center + Top
   - **Color**: **Yellow (255, 255, 0, 255)** hoặc **Light Gray**
   - **Wrapping**: Enabled (cho text dài)

---

## ⚙️ Bước 4: Setup Script

### 4.1. Gắn Script vào Canvas
1. Select `LoadingCanvas`
2. **Add Component** → `LoadingScreen` (script vừa tạo)

### 4.2. Assign References
Trong Inspector của `LoadingCanvas`:

**UI References:**
- **Loading Bar**: Kéo `LoadingBarFill` vào đây
- **Loading Text**: Kéo `LoadingText` vào đây
- **Tip Text**: Kéo `TipText` vào đây
- **Canvas Group**: Kéo `LoadingCanvas` (CanvasGroup component) vào đây

**Settings:**
- **Target Scene Name**: `Game` (scene cuối cùng)
- **Min Loading Time**: `2.0` (giây - thời gian tối thiểu để đọc tip)
- **Tip Change Interval**: `3.0` (giây - đổi tip sau bao lâu)

**Loading Tips:**
- Mặc định đã có 10 tips tiếng Việt
- Bạn có thể thêm/sửa tips trong Inspector

---

## 🔗 Bước 5: Kết Nối IntroCutscene

### 5.1. Mở Scene IntroCutscene
1. **File** → **Open Scene**
2. Chọn `IntroCutscene.unity`

### 5.2. Update Script Reference
1. Select `CutsceneCanvas` (GameObject có script IntroCutscene)
2. Inspector → **Scene** section:
   - **Loading Scene Name**: `Loading` ← Scene loading vừa tạo
   - **Game Scene Name**: `Game` (giữ nguyên, không dùng nữa)

---

## ✅ Bước 6: Test Toàn Bộ Flow

### 6.1. Test Flow
1. **Play từ Menu Scene**
2. **Menu** → **IntroCutscene** (dialogue + ảnh Faker)
3. **Ảnh Faker fade out** → **Loading Screen** (thanh loading + tips)
4. **Loading xong** → **Game Scene**

### 6.2. Kiểm Tra
- ✅ Thanh loading từ 0% → 100%
- ✅ Text "Loading... X%" cập nhật
- ✅ Tips đổi sau mỗi 3 giây
- ✅ Loading tối thiểu 2 giây (để đọc tip)
- ✅ Fade in/out mượt mà

---

## 🎨 Tùy Chỉnh (Optional)

### 1. Đổi Màu Loading Bar
- Select `LoadingBarFill`
- Đổi **Color** thành:
  - **Blue**: (0, 150, 255)
  - **Purple**: (150, 0, 255)
  - **Gold**: (255, 215, 0)

### 2. Thêm Icon Loading (Spinner)
1. **Right-click LoadingCanvas** → **UI** → **Image**
2. Đổi tên: `LoadingIcon`
3. Assign sprite icon (bánh xe xoay)
4. Thêm script xoay:
```csharp
void Update() {
    transform.Rotate(0, 0, -120 * Time.deltaTime); // Xoay ngược chiều kim đồng hồ
}
```

### 3. Thêm Logo Game
1. **Right-click LoadingCanvas** → **UI** → **Image**
2. Đổi tên: `GameLogo`
3. **Pos Y**: 200 (phía trên loading bar)
4. Assign logo sprite của game

---

## 🐛 Troubleshooting

### ❌ Lỗi: "Scene 'Loading' couldn't be loaded"
**Giải pháp:**
- Mở **Build Settings** → **Add Open Scenes**
- Đảm bảo `Loading.unity` có trong danh sách

### ❌ Loading Bar không tăng
**Giải pháp:**
- Kiểm tra `LoadingBarFill` → **Image Type = Filled**
- Kiểm tra `Fill Amount` ban đầu = 0

### ❌ Tips không đổi
**Giải pháp:**
- Kiểm tra `Tip Change Interval` > 0
- Kiểm tra array `loadingTips` có > 1 phần tử

### ❌ Chuyển scene quá nhanh
**Giải pháp:**
- Tăng `Min Loading Time` lên 3-4 giây

---

## 📊 Kết Quả Cuối Cùng

```
Menu Scene
   ↓
IntroCutscene (Dialogue + Faker Image)
   ↓
Loading Screen (2-5 giây với tips)
   ↓
Game Scene (Bắt đầu chơi)
```

---

## 🎉 Hoàn Thành!

Màn hình loading giờ đây:
- ✅ Hiển thị progress bar đầy đủ
- ✅ Có loading tips thú vị
- ✅ Chuyển tiếp mượt mà giữa các scene
- ✅ Không bị lag khi load scene lớn

Chúc bạn thành công! 🚀
