# 🎥 FIX CAMERA - KHÔNG THẤY MAP, TOÀN MÀU VÀNG

## 🚨 VẤN ĐỀ

**Triệu chứng:**
- ❌ Chạy game → Không thấy map
- ❌ Xung quanh toàn màu vàng đồng nhất
- ❌ Không thấy terrain, objects, chỉ thấy màu vàng
- ❌ Hoặc thấy sky nhưng không thấy ground

**Nguyên nhân có thể:**
1. **Camera Far Clipping Plane quá ngắn**
2. **Camera bị che bởi Fog**
3. **Camera Culling Mask sai**
4. **Camera ở vị trí sai (dưới terrain)**
5. **Main Camera chưa được gán tag**

---

## ⚡ GIẢI PHÁP NHANH (2 PHÚT)

### BƯỚC 1: Kiểm Tra Camera Settings

```
1. Hierarchy → Tìm "Main Camera" hoặc camera trong Player
2. Click để chọn
3. Inspector → Camera component
```

### BƯỚC 2: Fix Clipping Planes

```
Camera component:
┌─────────────────────────────┐
│ Clipping Planes:            │
│   Near: 0.3                 │
│   Far: 1000   ← QUAN TRỌNG! │
└─────────────────────────────┘

NẾU Far < 100 → ĐỔI THÀNH 1000 hoặc 5000
```

**Giải thích:**
- `Near`: Khoảng cách gần nhất camera nhìn thấy
- `Far`: Khoảng cách xa nhất camera nhìn thấy
- Nếu Far = 10 → Chỉ thấy 10m xung quanh → Còn lại màu vàng (sky)

### BƯỚC 3: Kiểm Tra Culling Mask

```
Camera component:
┌─────────────────────────────┐
│ Culling Mask: Everything   │
└─────────────────────────────┘

NẾU không phải "Everything" → Đổi lại
```

### BƯỚC 4: Kiểm Tra Clear Flags

```
Camera component:
┌─────────────────────────────┐
│ Clear Flags: Skybox         │
│ Background: (Sky color)     │
└─────────────────────────────┘

NẾU là "Solid Color" và màu vàng → Đổi thành "Skybox"
```

### BƯỚC 5: Kiểm Tra Tag

```
Inspector → Top:
┌─────────────────────────────┐
│ Tag: MainCamera  ← Phải có! │
│ Layer: Default              │
└─────────────────────────────┘
```

**✅ Sau 5 bước này, game sẽ thấy map bình thường!**

---

## 🔧 GIẢI PHÁP CHI TIẾT

### GIẢI PHÁP 1: FIX FAR CLIPPING PLANE (PHỔ BIẾN NHẤT)

**Vấn đề:** Camera chỉ render 10-50m, xa hơn = màu sky (vàng)

**Cách fix:**

```
1. Chọn Main Camera
2. Inspector → Camera:
   
   Clipping Planes:
   ├─ Near: 0.3 (giữ nguyên)
   └─ Far: 1000 hoặc 5000 ← ĐỔI SỐ NÀY
   
3. Test game → Sẽ thấy xa hơn
```

**Khuyến nghị:**
- Game nhỏ: Far = 1000
- Game lớn: Far = 5000
- Game rất lớn: Far = 10000

**⚠️ Lưu ý:** Far càng lớn → Performance càng nặng

---

### GIẢI PHÁP 2: TẮT FOG (NẾU BẬT)

**Vấn đề:** Fog quá dày che mất hết

**Cách fix:**

```
1. Window → Rendering → Lighting
2. Tab "Environment"
3. Other Settings:
   
   ┌────────────────────────┐
   │ ☐ Fog  ← Bỏ check     │
   └────────────────────────┘
   
4. Hoặc giảm Fog Density:
   Fog:
   ├─ Mode: Exponential
   └─ Density: 0.001 (rất nhạt)
```

---

### GIẢI PHÁP 3: FIX CAMERA POSITION

**Vấn đề:** Camera ở dưới terrain hoặc trong object

**Cách fix:**

```
1. Scene view → Chọn Main Camera
2. Transform:
   Position: (0, 2, 0) ← Phải trên terrain!
   
3. Hoặc camera trong Player:
   Player
   └─ CameraContainer
      └─ Main Camera
         Position Y: 1.6 (chiều cao mắt)
```

**Kiểm tra:**
- Scene view → Di chuyển camera đến vị trí thấy terrain
- Inspector → Copy position
- Paste vào camera khi game chạy

---

### GIẢI PHÁP 4: FIX CULLING MASK

**Vấn đề:** Camera không render một số layers

**Cách fix:**

```
Camera component:
┌────────────────────────────┐
│ Culling Mask: Everything  │ ← Click dropdown
└────────────────────────────┘

Đảm bảo check:
☑ Default
☑ TransparentFX
☑ Ignore Raycast
☑ Water
☑ UI
☑ (Tất cả layers cần thiết)
```

---

### GIẢI PHÁP 5: FIX CLEAR FLAGS

**Vấn đề:** Camera background màu vàng solid

**Cách fix:**

```
Camera component:
┌─────────────────────────────┐
│ Clear Flags: Skybox ▼      │ ← Đổi từ "Solid Color"
│ Background: (N/A)           │
└─────────────────────────────┘

Options:
• Skybox (Khuyến nghị) - Hiển thị sky
• Solid Color - Màu đơn sắc
• Depth only - Transparent
• Don't Clear - Overlay
```

---

## 🎯 SETTINGS CAMERA CHUẨN

### Main Camera Settings:

```csharp
Camera Component:
├─ Clear Flags: Skybox
├─ Background: (N/A)
├─ Culling Mask: Everything
├─ Projection: Perspective
├─ Field of View: 60
├─ Clipping Planes:
│  ├─ Near: 0.3
│  └─ Far: 1000 (hoặc cao hơn)
├─ Viewport Rect:
│  ├─ X: 0, Y: 0
│  └─ W: 1, H: 1
├─ Depth: -1 (Main camera)
└─ Target Display: Display 1
```

### Camera Transform (trong Player):

```
Player (Position: 0, 1, 0)
└─ CameraContainer (Position: 0, 1.6, 0)
   └─ Main Camera (Position: 0, 0, 0)
      Rotation: (0, 0, 0)
```

---

## 🔍 CHẨN ĐOÁN VẤN ĐỀ

### Test 1: Scene View vs Game View

```
1. Scene view → Di chuyển xung quanh
   → Thấy terrain? → Camera settings sai
   → Không thấy? → Terrain/objects bị lỗi

2. Game view → Chạy game
   → So sánh với Scene view
```

### Test 2: Check Console

```
Ctrl + Shift + C → Mở Console
Xem errors:
- "Camera.main not found" → Thiếu tag MainCamera
- "NullReferenceException: Camera" → Camera chưa gán
```

### Test 3: Check Camera Transform

```
Window → Analysis → Frame Debugger
→ Xem camera position có hợp lý không
```

---

## 🛠️ SCRIPT FIX CAMERA TỰ ĐỘNG

### Tạo file: `FixCameraSettings.cs`

```csharp
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FixCameraSettings : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Tools/Fix Camera Settings")]
    static void FixAllCameras()
    {
        Camera[] cameras = FindObjectsOfType<Camera>();
        
        if (cameras.Length == 0)
        {
            EditorUtility.DisplayDialog("Error", "No cameras found in scene!", "OK");
            return;
        }

        foreach (Camera cam in cameras)
        {
            // Fix clipping planes
            cam.nearClipPlane = 0.3f;
            cam.farClipPlane = 1000f;

            // Fix clear flags
            cam.clearFlags = CameraClearFlags.Skybox;

            // Fix culling mask
            cam.cullingMask = -1; // Everything

            // Ensure tag
            if (cam.CompareTag("Untagged"))
            {
                cam.tag = "MainCamera";
            }

            Debug.Log($"Fixed camera: {cam.name}");
        }

        EditorUtility.DisplayDialog("Success", 
            $"Fixed {cameras.Length} camera(s)!\n\n" +
            $"Settings applied:\n" +
            $"• Far Clip: 1000\n" +
            $"• Clear Flags: Skybox\n" +
            $"• Culling Mask: Everything\n" +
            $"• Tag: MainCamera", 
            "OK");
    }

    [MenuItem("Tools/Print Camera Info")]
    static void PrintCameraInfo()
    {
        Camera[] cameras = FindObjectsOfType<Camera>();
        
        foreach (Camera cam in cameras)
        {
            Debug.Log($"=== Camera: {cam.name} ===");
            Debug.Log($"Position: {cam.transform.position}");
            Debug.Log($"Far Clip: {cam.farClipPlane}");
            Debug.Log($"Clear Flags: {cam.clearFlags}");
            Debug.Log($"Culling Mask: {cam.cullingMask}");
            Debug.Log($"Tag: {cam.tag}");
            Debug.Log("---");
        }
    }
#endif
}
```

**Cách dùng:**

```
1. Lưu vào Assets/Editor/FixCameraSettings.cs
2. Unity compile
3. Menu: Tools → Fix Camera Settings
4. Tất cả cameras sẽ được fix tự động!
```

---

## 📊 SO SÁNH VẤN ĐỀ

| Triệu Chứng | Nguyên Nhân | Giải Pháp |
|-------------|-------------|-----------|
| **Màu vàng đồng nhất** | Far Clip quá ngắn | Far = 1000 |
| **Mờ mịt, không rõ** | Fog quá dày | Tắt Fog hoặc giảm Density |
| **Chỉ thấy sky** | Camera ngửa lên | Fix rotation X = 0 |
| **Màu đen** | Far Clip = 0 | Far = 1000 |
| **Không thấy UI** | Culling Mask sai | Check UI layer |
| **Nhấp nháy** | 2 cameras cùng depth | Đổi depth |

---

## ⚠️ LƯU Ý QUAN TRỌNG

### 1. **Camera.main Cần Tag**
```
Camera phải có Tag = "MainCamera"
Nếu không → Scripts không tìm thấy camera
```

### 2. **Far Clip vs Performance**
```
Far Clip càng cao → Performance càng nặng
Khuyến nghị:
- Mobile: 500-1000
- PC: 1000-5000
- High-end: 5000-10000
```

### 3. **Multiple Cameras**
```
Nếu có nhiều cameras:
- Main Camera: Depth = -1
- UI Camera: Depth = 1
- Effect Camera: Depth = 0
```

### 4. **Player Camera Setup**
```
Đúng:
Player → CameraContainer → Camera
      (Position Y = 1.6)

Sai:
Camera riêng lẻ (không follow player)
```

---

## 📋 CHECKLIST FIX CAMERA

- [ ] Camera có Tag = "MainCamera"
- [ ] Far Clipping Plane ≥ 1000
- [ ] Clear Flags = Skybox (không phải Solid Color)
- [ ] Culling Mask = Everything
- [ ] Camera Position trên terrain (Y > 0)
- [ ] Field of View = 60-90
- [ ] Fog tắt hoặc Density < 0.01
- [ ] Test trong Game view
- [ ] Save Scene ✅

---

## 🎮 TEST CUỐI CÙNG

### Sau khi fix:

```
1. Play game (▶️)
2. Quan sát:
   ✅ Thấy terrain rõ ràng
   ✅ Thấy objects xung quanh
   ✅ Nhìn xa được ít nhất 100m
   ✅ Màu sắc bình thường (không toàn vàng)
   ✅ Sky ở trên, ground ở dưới

3. Di chuyển (WASD)
   ✅ Camera follow player
   ✅ Nhìn xung quanh (mouse) mượt

4. Performance:
   ✅ FPS ổn định (>30)
   ✅ Không lag
```

---

## 💡 TIPS THÊM

### 1. **Depth of Field (Blur)**
```
Nếu game bị blur:
Camera → Post Processing → Tắt Depth of Field
```

### 2. **Anti-Aliasing**
```
Nếu game răng cưa:
Camera → Anti-aliasing → FXAA hoặc MSAA
```

### 3. **HDR**
```
Camera → Allow HDR: ☑
→ Màu sắc đẹp hơn
```

### 4. **Occlusion Culling**
```
Window → Rendering → Occlusion Culling
→ Bake → Tăng performance
```

---

## 🆘 NẾU VẪN KHÔNG FIX ĐƯỢC

### Thử các cách sau:

**1. Tạo Camera mới:**
```
GameObject → Camera
→ Tag: MainCamera
→ Position: (0, 2, 0)
→ Test xem thấy map không
```

**2. Check Terrain:**
```
Chọn Terrain → Inspector
→ Terrain Settings → Draw: ☑
→ Pixel Error: 5
```

**3. Check Layers:**
```
Edit → Project Settings → Tags and Layers
→ Đảm bảo có Default layer
```

**4. Reimport Scene:**
```
Assets → Reimport All
→ Restart Unity
```

**5. Check Console Errors:**
```
Có error về Camera.main? → Fix tag
Có error về Terrain? → Reimport terrain
```

---

## ✅ KẾT QUẢ MONG ĐỢI

**Trước:**
- ❌ Toàn màu vàng
- ❌ Không thấy map
- ❌ Như trong sương mù

**Sau:**
- ✅ Thấy terrain rõ ràng
- ✅ Thấy objects xung quanh
- ✅ Nhìn xa hàng trăm mét
- ✅ Game bình thường!

---

## 📖 TÓM TẮT

**Vấn đề:** Camera Far Clipping Plane quá ngắn

**Giải pháp 10 giây:**
```
Chọn Camera → Far: 1000
✅ XONG!
```

**Các vấn đề khác:**
- Fog: Tắt hoặc giảm
- Culling Mask: Everything
- Clear Flags: Skybox
- Tag: MainCamera

---

**🎥 Chúc bạn fix camera thành công! 🎥**

*Hầu hết trường hợp: Far Clipping Plane = 10, cần đổi thành 1000!*
