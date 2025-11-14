# 🎨 HƯỚNG DẪN SỬA MÀU TÍM (MAGENTA) TRONG UNITY

## 🔍 VẤN ĐỀ

Màu tím/hồng (magenta) trong Unity Scene có nghĩa là:
- ❌ **Thiếu Material**
- ❌ **Thiếu Shader**  
- ❌ **Thiếu Texture**
- ❌ **Material bị missing reference**

---

## 🎯 GIẢI PHÁP NHANH

### BƯỚC 1: Kiểm Tra Console
1. Mở **Console** (Window → General → Console)
2. Tìm các warning/error về:
   - "Shader not found"
   - "Material missing"
   - "Texture missing"
3. Note lại các object bị lỗi

### BƯỚC 2: Tạo Material Mặc Định

1. **Tạo Material mới:**
   ```
   Project → Click phải → Create → Material
   Đặt tên: "Default_Material"
   ```

2. **Set Shader:**
   ```
   Inspector → Shader → Chọn "Standard"
   Hoặc: "Universal Render Pipeline/Lit" (nếu dùng URP)
   ```

3. **Chọn màu:**
   ```
   Inspector → Albedo → Chọn màu (trắng, xám, nâu đất...)
   ```

### BƯỚC 3: Gán Material Cho Objects

**Cách 1: Gán thủ công**
```
1. Chọn object màu tím trong Scene
2. Trong Inspector, tìm "Mesh Renderer"
3. Mở "Materials"
4. Kéo material "Default_Material" vào slot "Element 0"
```

**Cách 2: Gán hàng loạt** (nhanh hơn)
```
1. Chọn tất cả objects màu tím (Ctrl + Click)
2. Trong Inspector → Mesh Renderer → Materials
3. Kéo "Default_Material" vào
```

---

## 🔧 GIẢI PHÁP SÂU HƠN

### Nếu Dùng Universal Render Pipeline (URP):

1. **Kiểm tra Render Pipeline:**
   ```
   Edit → Project Settings → Graphics
   → Xem "Scriptable Render Pipeline Settings"
   ```

2. **Nếu đang dùng URP nhưng Shader là Built-in:**
   ```
   Edit → Render Pipeline → Universal Render Pipeline
   → Upgrade Project Materials to URP Materials
   ```

3. **Tạo URP Material:**
   ```
   Create → Material
   Shader → Universal Render Pipeline → Lit
   ```

### Nếu Thiếu Texture:

1. **Tạo Texture đơn giản:**
   ```
   - Tạo ảnh 512x512 trắng/xám trong Paint
   - Save as PNG
   - Kéo vào Unity Project
   ```

2. **Gán Texture vào Material:**
   ```
   Material → Albedo → Click ô vuông
   → Chọn texture vừa tạo
   ```

---

## 🎨 TẠO BỘ MATERIALS CƠ BẢN

Tạo sẵn các material này để dùng nhanh:

### 1. **Ground Material** (Đất)
```
Color: Nâu (#8B4513)
Smoothness: 0.3
Metallic: 0
```

### 2. **Wood Material** (Gỗ)
```
Color: Nâu nhạt (#D2691E)
Smoothness: 0.2
Metallic: 0
```

### 3. **Stone Material** (Đá)
```
Color: Xám (#808080)
Smoothness: 0.4
Metallic: 0
```

### 4. **Grass Material** (Cỏ)
```
Color: Xanh lá (#228B22)
Smoothness: 0.1
Metallic: 0
```

### 5. **Water Material** (Nước)
```
Color: Xanh dương nhạt (#4682B4)
Smoothness: 0.9
Metallic: 0.1
```

---

## 🚀 SCRIPT TỰ ĐỘNG SỬA (NÂNG CAO)

Nếu có quá nhiều objects màu tím, dùng script này:

**Tạo file: `FixMissingMaterials.cs`**

```csharp
using UnityEngine;
using UnityEditor;

public class FixMissingMaterials : EditorWindow
{
    public Material defaultMaterial;

    [MenuItem("Tools/Fix Missing Materials")]
    static void ShowWindow()
    {
        GetWindow<FixMissingMaterials>("Fix Materials");
    }

    void OnGUI()
    {
        GUILayout.Label("Fix Missing Materials", EditorStyles.boldLabel);
        
        defaultMaterial = (Material)EditorGUILayout.ObjectField(
            "Default Material", 
            defaultMaterial, 
            typeof(Material), 
            false
        );

        if (GUILayout.Button("Fix All Missing Materials"))
        {
            FixAllMaterials();
        }
    }

    void FixAllMaterials()
    {
        if (defaultMaterial == null)
        {
            EditorUtility.DisplayDialog("Error", "Please assign a default material first!", "OK");
            return;
        }

        MeshRenderer[] allRenderers = FindObjectsOfType<MeshRenderer>();
        int fixedCount = 0;

        foreach (MeshRenderer renderer in allRenderers)
        {
            Material[] mats = renderer.sharedMaterials;
            bool needsFix = false;

            for (int i = 0; i < mats.Length; i++)
            {
                if (mats[i] == null || mats[i].shader == null || mats[i].shader.name == "Hidden/InternalErrorShader")
                {
                    mats[i] = defaultMaterial;
                    needsFix = true;
                }
            }

            if (needsFix)
            {
                renderer.sharedMaterials = mats;
                fixedCount++;
            }
        }

        EditorUtility.DisplayDialog("Done", $"Fixed {fixedCount} objects with missing materials!", "OK");
    }
}
```

**Cách dùng:**
1. Lưu script vào `Assets/Editor/FixMissingMaterials.cs`
2. Unity sẽ compile
3. Vào menu: `Tools → Fix Missing Materials`
4. Chọn Material mặc định
5. Click "Fix All Missing Materials"

---

## 📋 CHECKLIST SỬA MÀU TÍM

- [ ] Đã tạo material mặc định (Standard shader)
- [ ] Đã kiểm tra Console có error về shader không
- [ ] Đã gán material cho objects màu tím
- [ ] Nếu dùng URP, đã upgrade materials sang URP
- [ ] Đã tạo bộ materials cơ bản (đất, gỗ, đá, cỏ)
- [ ] Đã test trong Game view (không chỉ Scene view)
- [ ] Đã save Scene sau khi sửa

---

## ⚠️ LƯU Ý QUAN TRỌNG

### 1. **Màu tím KHÔNG làm game lỗi**
   - Game vẫn chạy được
   - Chỉ ảnh hưởng visual
   - Có thể fix sau

### 2. **Backup trước khi sửa**
   - Ctrl + D để duplicate objects
   - Hoặc save Scene với tên khác
   - Git commit nếu dùng version control

### 3. **Kiểm tra Lighting**
   - Đôi khi do Lighting chưa bake
   - Vào: Window → Rendering → Lighting
   - Click "Generate Lighting"

### 4. **Kiểm tra Graphics Settings**
   - Edit → Project Settings → Graphics
   - Đảm bảo có Render Pipeline asset
   - Nếu trống, tạo mới hoặc gán default

---

## 🎥 VIDEO HƯỚNG DẪN

Search YouTube:
- "Unity fix pink materials"
- "Unity missing shader"
- "Unity URP upgrade materials"
- "Unity replace missing materials"

---

## 💡 TIPS

1. **Tạo folder Materials riêng:**
   ```
   Assets/Materials/
   ├─ Ground/
   ├─ Wood/
   ├─ Stone/
   └─ Props/
   ```

2. **Đặt tên Material có ý nghĩa:**
   ```
   ✅ Mat_Ground_Grass
   ✅ Mat_Wood_Oak
   ❌ Material_1
   ❌ New Material
   ```

3. **Dùng Material Variants:**
   ```
   Click phải Material → Create → Material Variant
   → Giữ nguyên shader, chỉ đổi màu/texture
   ```

4. **Preview Material:**
   ```
   Project → Chọn Material
   → Inspector hiện preview 3D ở dưới
   ```

---

## 🔍 TÌM VẤN ĐỀ CỤ THỂ

### Nếu một số objects vẫn tím:

1. **Chọn object màu tím**
2. **Inspector → Mesh Renderer → Materials**
3. **Xem tên Material:**
   - Nếu là "Missing (Material)": Thiếu material file
   - Nếu màu tím: Shader không support / URP mismatch
   - Nếu trống: Chưa assign material

4. **Kiểm tra Shader:**
   - Click vào Material trong Project
   - Inspector → Shader → Xem tên
   - Nếu là "Hidden/InternalErrorShader": Shader lỗi

---

## ✅ HOÀN THÀNH

Sau khi làm theo hướng dẫn:
- ✅ Không còn màu tím
- ✅ Objects có màu bình thường
- ✅ Game nhìn đẹp hơn
- ✅ Có bộ materials cơ bản để dùng

---

**Chúc bạn sửa thành công! 🎨**
