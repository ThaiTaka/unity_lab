# 🌅 FIX MÀU VÀNG HOÀNG HÔN - ĐỔI SANG ÁNH SÁNG BAN NGÀY

## 🔍 VẤN ĐỀ

**Bạn thấy:** Toàn bộ game màu vàng/cam (như hoàng hôn)

**Nguyên nhân:** 
- ✅ **KHÔNG PHẢI LỖI!**
- ✅ Đây là Skybox + Lighting đang ở chế độ Sunset/Dawn
- ✅ Materials của bạn hoàn toàn OK!

**Giải pháp:** Đổi lighting sang ban ngày

---

## 🌞 GIẢI PHÁP 1: ĐỔI DIRECTIONAL LIGHT (NHANH NHẤT)

### Bước 1: Tìm Directional Light

```
Hierarchy → Tìm "Directional Light" 
(thường có biểu tượng ☀️ hoặc tên "Sun")
→ Click để chọn
```

### Bước 2: Đổi Màu Và Góc

```
Inspector:

1. COLOR:
   ┌─────────────────────┐
   │ Color: ⬜ ← Click   │  → Đổi sang TRẮNG (#FFFFFF)
   │ (hiện đang vàng/cam)│
   └─────────────────────┘

2. INTENSITY:
   Intensity: 1

3. ROTATION (góc chiếu):
   Transform → Rotation
   ┌─────────────┐
   │ X: 50       │  ← Ban ngày
   │ Y: -30      │
   │ Z: 0        │
   └─────────────┘
```

### Bước 3: Xem Kết Quả

```
Scene view → Lighting sẽ đổi sang trắng ngay lập tức
Game view → Test xem còn vàng không
```

**✅ Xong! Ánh sáng sẽ trắng, không còn vàng hoàng hôn!**

---

## 🎨 GIẢI PHÁP 2: ĐỔI SKYBOX

### Bước 1: Mở Lighting Settings

```
Menu: Window → Rendering → Lighting
→ Window mới sẽ hiện ra
```

### Bước 2: Đổi Skybox

```
Tab "Environment"
┌────────────────────────────────┐
│ Skybox Material:               │
│ [Currently: Sunset_Sky] ← Đây! │
│                                │
│ Click vào ô → Chọn:           │
│   • Default-Skybox (xanh sky) │
│   • None (trơn)               │
│   • Procedural Sky Material   │
└────────────────────────────────┘
```

### Bước 3: Generate Lighting

```
Kéo xuống dưới cùng của Lighting window
→ Click button "Generate Lighting"
→ Đợi bake xong (vài giây)
```

**✅ Skybox sẽ đổi sang xanh sky ban ngày!**

---

## 🌈 GIẢI PHÁP 3: ĐỔI AMBIENT LIGHT

### Trong Lighting Window:

```
Tab "Environment"

1. Environment Lighting:
   ┌─────────────────────────┐
   │ Source: Skybox ▼       │
   │ Intensity: 1           │
   │ Ambient Color: 🟨      │ ← Click đổi sang trắng/xanh nhạt
   └─────────────────────────┘

2. Environment Reflections:
   Source: Skybox
   Resolution: 128
   Compression: Auto
```

---

## 🎯 THIẾT LẬP CHUẨN BAN NGÀY

### Directional Light Settings:

```csharp
Directional Light (Sun):
├─ Color: RGB(255, 255, 255) - Trắng
├─ Intensity: 1
├─ Rotation X: 50° (góc ban ngày)
├─ Shadows: Soft Shadows
└─ Shadow Strength: 1
```

### Lighting Settings:

```
Environment:
├─ Skybox: Default-Skybox
├─ Sun Source: Directional Light
├─ Ambient Source: Skybox
├─ Ambient Intensity: 1
└─ Reflection Source: Skybox
```

---

## 🌅 SO SÁNH HOÀNG HÔN VS BAN NGÀY

### HOÀNG HÔN (Hiện tại):
```
Directional Light:
├─ Color: 🟨 Vàng/Cam (#FFA500)
├─ Rotation X: 10-20° (thấp, gần horizon)
└─ Intensity: 0.8

Skybox: Sunset/Dawn colors
Result: Màu vàng/cam khắp nơi
```

### BAN NGÀY (Mục tiêu):
```
Directional Light:
├─ Color: ⬜ Trắng (#FFFFFF)
├─ Rotation X: 50° (cao, giữa trời)
└─ Intensity: 1

Skybox: Blue sky
Result: Màu sáng tự nhiên
```

---

## 🎨 TẠO NHIỀU PRESET LIGHTING

### Tạo Preset Ngày/Đêm:

#### **1. Ban Ngày:**
```
Directional Light:
- Color: White
- Intensity: 1
- Rotation X: 50

Skybox: Default-Skybox
Ambient: Bright
```

#### **2. Hoàng Hôn:**
```
Directional Light:
- Color: Orange (#FFA500)
- Intensity: 0.8
- Rotation X: 15

Skybox: Sunset
Ambient: Warm orange
```

#### **3. Ban Đêm:**
```
Directional Light:
- Color: Blue (#4682B4)
- Intensity: 0.3
- Rotation X: -30 (dưới horizon)

Skybox: Night sky
Ambient: Dark blue
```

---

## 🔧 SCRIPT ĐỔI LIGHTING TỰ ĐỘNG (NÂNG CAO)

### Tạo file: `LightingPresets.cs`

```csharp
using UnityEngine;

public class LightingPresets : MonoBehaviour
{
    public Light directionalLight;

    [Header("Day Preset")]
    public Color dayColor = Color.white;
    public float dayIntensity = 1f;
    public Vector3 dayRotation = new Vector3(50, -30, 0);

    [Header("Sunset Preset")]
    public Color sunsetColor = new Color(1f, 0.65f, 0f); // Orange
    public float sunsetIntensity = 0.8f;
    public Vector3 sunsetRotation = new Vector3(15, -30, 0);

    [Header("Night Preset")]
    public Color nightColor = new Color(0.3f, 0.5f, 0.7f); // Blue
    public float nightIntensity = 0.3f;
    public Vector3 nightRotation = new Vector3(-30, -30, 0);

    void Start()
    {
        if (directionalLight == null)
            directionalLight = FindObjectOfType<Light>();
    }

    void Update()
    {
        // Nhấn phím để đổi lighting
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetDayLighting();
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetSunsetLighting();
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetNightLighting();
    }

    public void SetDayLighting()
    {
        directionalLight.color = dayColor;
        directionalLight.intensity = dayIntensity;
        directionalLight.transform.rotation = Quaternion.Euler(dayRotation);
        
        RenderSettings.ambientLight = Color.white;
        Debug.Log("Switched to DAY lighting");
    }

    public void SetSunsetLighting()
    {
        directionalLight.color = sunsetColor;
        directionalLight.intensity = sunsetIntensity;
        directionalLight.transform.rotation = Quaternion.Euler(sunsetRotation);
        
        RenderSettings.ambientLight = new Color(1f, 0.8f, 0.6f); // Warm
        Debug.Log("Switched to SUNSET lighting");
    }

    public void SetNightLighting()
    {
        directionalLight.color = nightColor;
        directionalLight.intensity = nightIntensity;
        directionalLight.transform.rotation = Quaternion.Euler(nightRotation);
        
        RenderSettings.ambientLight = new Color(0.2f, 0.2f, 0.3f); // Dark blue
        Debug.Log("Switched to NIGHT lighting");
    }
}
```

### Cách dùng:

```
1. Tạo Empty GameObject: "Lighting Manager"
2. Add component: LightingPresets
3. Gán Directional Light vào slot
4. Play game:
   - Nhấn "1" → Ban ngày
   - Nhấn "2" → Hoàng hôn
   - Nhấn "3" → Ban đêm
```

---

## 📊 CHẨN ĐOÁN VẤN ĐỀ

### Kiểm Tra Xem Lighting Đang Ở Chế Độ Nào:

```
1. Chọn Directional Light
2. Inspector → Xem Color:
   - Trắng = Ban ngày ✅
   - Vàng/Cam = Hoàng hôn 🌅
   - Xanh = Ban đêm 🌙

3. Xem Rotation X:
   - 40-70° = Ban ngày ✅
   - 10-20° = Hoàng hôn 🌅
   - < 0° = Ban đêm 🌙
```

---

## 🎨 TẢI SKYBOX MIỄN PHÍ

### Nếu muốn skybox đẹp hơn:

**Free Skybox Assets:**
1. **Unity Asset Store:**
   - "AllSky Free"
   - "Fantasy Skybox FREE"
   - "Low Poly Sky"

2. **Websites:**
   - [polyhaven.com/hdris](https://polyhaven.com/hdris)
   - [hdrihaven.com](https://hdrihaven.com)

### Cách import:

```
1. Download HDRI file (.hdr)
2. Unity: Assets → Import New Asset
3. Chọn HDRI
4. Window → Rendering → Lighting
5. Skybox Material: Kéo HDRI vào
```

---

## ⚠️ LƯU Ý QUAN TRỌNG

### 1. **Lighting ≠ Material Error**
- Màu vàng do lighting (OK)
- Màu tím do missing material (Lỗi)
- Hai vấn đề khác nhau!

### 2. **Skybox Ảnh Hưởng Toàn Bộ Scene**
- Skybox cam/vàng → Game cam/vàng
- Skybox xanh → Game xanh/sáng
- Không chỉ background!

### 3. **Ambient Light Quan Trọng**
- Ambient = Ánh sáng môi trường
- Ảnh hưởng shadows, màu sắc
- Nên set theo skybox

### 4. **Real-time vs Baked**
- Đổi Directional Light → Real-time (ngay lập tức)
- Đổi Skybox → Cần Generate Lighting (vài giây)

---

## 📋 CHECKLIST ĐỔI SANG BAN NGÀY

- [ ] Chọn Directional Light
- [ ] Đổi Color → Trắng (#FFFFFF)
- [ ] Đổi Rotation X → 50°
- [ ] Intensity → 1
- [ ] Window → Lighting → Skybox → Default-Skybox
- [ ] Generate Lighting
- [ ] Test trong Game view
- [ ] Save Scene ✅

---

## 🎯 KẾT QUẢ MONG ĐỢI

**Trước (Hoàng hôn):**
- 🟨 Toàn bộ màu vàng/cam
- 🌅 Skybox sunset
- 🔆 Ánh sáng ấm

**Sau (Ban ngày):**
- ⬜ Màu sắc tự nhiên, sáng
- 🌤️ Skybox xanh sky
- ☀️ Ánh sáng trắng

---

## 💡 TIPS

### 1. **Test Nhiều Lighting:**
```
Thử nhiều preset khác nhau
→ Tìm lighting đẹp nhất cho game
```

### 2. **Dùng Post Processing:**
```
Asset Store → Download "Post Processing"
→ Color Grading để điều chỉnh tone màu
```

### 3. **Dynamic Day/Night:**
```
Dùng script LightingPresets ở trên
→ Tự động đổi lighting theo thời gian
→ Realistic!
```

### 4. **Save Lighting Settings:**
```
Lighting → Scene → New Lighting Settings
→ Tạo preset riêng, dùng lại được
```

---

## 🆘 NẾU VẪN KHÔNG WORK

### 1. **Clean Lighting Cache:**
```
Edit → Preferences → GI Cache → Clean All Caches
Restart Unity
```

### 2. **Rebuild Lighting:**
```
Window → Rendering → Lighting
→ Clear Baked Data
→ Generate Lighting (lại từ đầu)
```

### 3. **Check Console:**
```
Ctrl + Shift + C
→ Xem có lighting errors không
```

### 4. **Reset Render Settings:**
```
Edit → Project Settings → Graphics
→ Reset to defaults
```

---

## ✅ TÓM TẮT

**Vấn đề của bạn:**
- ✅ Materials OK (không phải lỗi!)
- ✅ Chỉ là lighting đang ở chế độ hoàng hôn
- ✅ Đổi Directional Light sang trắng là xong!

**3 bước nhanh nhất:**
```
1. Chọn Directional Light
2. Color → Trắng
3. Rotation X → 50°
✅ XONG!
```

---

**🌞 Chúc bạn có lighting đẹp! 🌞**

*Nhớ: Màu vàng = Lighting effect, không phải lỗi material!*
