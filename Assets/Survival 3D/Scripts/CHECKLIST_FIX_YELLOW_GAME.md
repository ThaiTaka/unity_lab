# 🎮 CHECKLIST FIX GAME TOÀN MÀU VÀNG (Camera OK)

## ✅ ĐÃ KIỂM TRA

Camera settings: **HOÀN HẢO!**
- Far Clip: 1000 ✅
- Clear Flags: Skybox ✅
- Tag: MainCamera ✅

## 🔍 CẦN KIỂM TRA TIẾP

### 1. XÓA SCRIPT MISSING TRÊN CAMERA ⚠️

**QUAN TRỌNG! Có warning:**
```
⚠️ Script: Missing (Mono Script)
```

**Cách fix:**
```
1. Inspector → Main Camera
2. Scroll xuống tìm component "(Script)" với ⚠️
3. Click menu ⋮ (3 chấm)
4. Remove Component
5. Save Scene (Ctrl + S)
```

---

### 2. KIỂM TRA PLAYER POSITION

**Player có thể ở dưới terrain:**

```
Hierarchy → Chọn "Player"
Inspector → Transform:
  Position:
    X: (bất kỳ)
    Y: > 0 ← PHẢI DƯƠNG! (trên terrain)
    Z: (bất kỳ)

Nếu Y âm hoặc = 0:
→ Đổi Y thành 5 hoặc 10
→ Test game
```

---

### 3. KIỂM TRA DIRECTIONAL LIGHT

**Ánh sáng có thể màu vàng:**

```
Hierarchy → Tìm "Directional Light Moon" hoặc tương tự
Inspector:
  Light component:
    Color: Phải là TRẮNG (#FFFFFF)
    Intensity: 1
    
Nếu màu vàng/cam:
→ Click color box
→ Đổi thành trắng
→ Test game
```

---

### 4. KIỂM TRA POST-PROCESSING

**Có Post-Processing trong scene:**

```
Hierarchy → Chọn "Post-Processing"
Inspector → Volume component:
  Profile: (xem profile name)
  
Click vào Profile:
→ Tìm "Color Grading"
→ Nếu có "Temperature" hoặc "Tint"
→ Đặt về 0

Hoặc đơn giản:
→ Disable Post-Processing component
→ Test game
```

---

### 5. KIỂM TRA FOG

**Fog có thể màu vàng:**

```
Window → Rendering → Lighting
Tab "Environment":
  Fog: 
    Nếu checked:
      → Color: Phải là xám/trắng, KHÔNG vàng
      → Density: < 0.01
    
    Hoặc:
      → ☐ Bỏ check Fog
```

---

### 6. KIỂM TRA SKYBOX

**Skybox có thể quá vàng:**

```
Window → Rendering → Lighting
Tab "Environment":
  Skybox Material: (xem tên)
  
Nếu là "Sunset" hoặc "Dawn":
→ Đổi sang "Default-Skybox"
→ Generate Lighting
```

---

### 7. TEST TRONG SCENE VIEW

**So sánh Scene view vs Game view:**

```
1. Scene view (tab Scene) → Nhìn thấy gì?
   ✅ Thấy terrain xanh/màu bình thường
   ✅ Thấy trees, objects
   
2. Game view (tab Game) → Play → Nhìn thấy gì?
   ❌ Toàn màu vàng
   
→ Vấn đề là Runtime, không phải Scene setup
→ Kiểm tra scripts chạy lúc Play
```

---

### 8. KIỂM TRA DayNight.cs SCRIPT

**Có script DayNight.cs điều khiển lighting:**

```
Hierarchy → Tìm object có DayNight script
Inspector → DayNight component:
  - Time of Day: Xem giá trị
  - Lighting Intensity Multiplier: Xem curve
  
Có thể script đang set lighting màu hoàng hôn!

Test:
→ Disable DayNight component
→ Play game
→ Xem còn vàng không
```

---

### 9. KIỂM TRA RENDER PIPELINE

**Có thể URP settings:**

```
Edit → Project Settings → Graphics
  Scriptable Render Pipeline Settings:
    → Xem có UniversalRenderPipelineAsset không
    
Nếu có:
  Click vào asset
  Inspector:
    → General → Rendering → Render Scale: 1
    → Post-processing: Check settings
```

---

### 10. CHECK CONSOLE ERRORS

**Mở Console khi Play:**

```
Window → General → Console (Ctrl + Shift + C)
Play game
Xem có errors/warnings gì:
  - Shader errors?
  - Material errors?
  - Script errors?
  
Copy error messages để debug
```

---

## 🎯 THỨ TỰ ƯU TIÊN

**Làm theo thứ tự:**

1. ⚠️ **XÓA SCRIPT MISSING** (Camera Inspector)
2. 🎮 **KIỂM TRA PLAYER Y POSITION** (> 0)
3. 🌞 **KIỂM TRA DIRECTIONAL LIGHT** (màu trắng)
4. 🎨 **TẮT POST-PROCESSING** (test)
5. 📅 **TẮT DayNight SCRIPT** (test)
6. 🌫️ **TẮT FOG** (test)
7. 🎨 **ĐỔI SKYBOX** (Default-Skybox)
8. 🎮 **SO SÁNH SCENE vs GAME VIEW**
9. 📊 **CHECK CONSOLE**
10. ⚙️ **KIỂM TRA RENDER PIPELINE**

---

## 📸 SCREENSHOT CẦN THÊM

**Để debug chính xác, cần thêm:**

1. **Screenshot Game view khi Play** (đang toàn vàng)
2. **Screenshot Console khi Play** (có errors không)
3. **Screenshot Player Inspector** (Transform position)
4. **Screenshot Directional Light Inspector** (color)
5. **Screenshot Post-Processing Inspector** (nếu có)

---

## 🎮 TEST NHANH

**Test từng bước:**

```
Test 1: Disable Post-Processing → Play
  → Còn vàng? → Tiếp tục
  → Hết vàng? → Vấn đề là Post-Processing!

Test 2: Disable DayNight script → Play
  → Còn vàng? → Tiếp tục
  → Hết vàng? → Vấn đề là DayNight!

Test 3: Directional Light → Color → Trắng → Play
  → Còn vàng? → Tiếp tục
  → Hết vàng? → Vấn đề là Light color!

Test 4: Player Position Y = 10 → Play
  → Còn vàng? → Tiếp tục
  → Hết vàng? → Vấn đề là Player position!
```

---

## 💡 GỢI Ý

**Từ Scene view, tôi thấy:**
- Terrain màu xanh dương (water) ✅
- Có trees xanh lá ✅
- Sky màu xanh nhạt/trắng ✅

**Trong Game khi Play:**
- Toàn màu vàng ❌

**→ Có thể:**
1. **DayNight script** đang set sunset lighting
2. **Post-Processing** có yellow color grading
3. **Player camera** đang nhìn vào object vàng

---

## 🆘 NẾU VẪN KHÔNG FIX

**Thử cách này:**

### Create New Scene Test:

```
1. File → New Scene
2. GameObject → 3D Object → Plane (ground)
3. GameObject → 3D Object → Cube
4. Main Camera → Position: (0, 1, -10)
5. Play game

Nếu scene test này bình thường:
→ Vấn đề nằm ở scene gốc
→ Có script/component đang làm màu vàng

Nếu scene test này cũng vàng:
→ Vấn đề là Project Settings
→ Graphics / Quality / Render Pipeline
```

---

## 📋 BÁO CÁO LẠI

**Sau khi thử các bước trên, báo cáo:**

1. Script missing đã xóa chưa? (⚠️)
2. Player Y position là bao nhiêu?
3. Directional Light màu gì?
4. Disable Post-Processing → còn vàng không?
5. Disable DayNight → còn vàng không?
6. Console có errors gì?
7. Scene view vs Game view khác nhau như thế nào?

---

**🎮 Làm từng bước và báo lại kết quả nhé! 🎮**
