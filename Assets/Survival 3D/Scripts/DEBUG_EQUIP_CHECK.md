# 🔍 DEBUG EQUIP - TẠI SAO KHÔNG THẤY RÌU?

## 📋 CHECKLIST DEBUG

### ✅ **BƯỚC 1: Equip rìu và xem Console Log**

Sau khi equip, Console phải hiện:
```
✅ Successfully equipped: Axe
   World Position: (x, y, z)
   Local Position: (0, 0, 0) ← Phải là (0,0,0)!
   Local Scale: (1, 1, 1) ← Phải là (1,1,1)!
   Parent: Equip Camera
   Renderers found: X ← Phải > 0!
   - Renderer: AxeMesh, Enabled: True, Layer: Default
```

**NẾU "Renderers found: 0":**
→ Prefab rìu KHÔNG có MeshRenderer/SkinnedMeshRenderer
→ Không thể hiện được!

**NẾU "Enabled: False":**
→ Renderer bị tắt
→ Bật lại trong prefab

**NẾU "Layer: Player" hoặc layer khác:**
→ Camera không render layer đó
→ Phải là "Default"

---

### ✅ **BƯỚC 2: Kiểm Tra Trong Scene View**

Khi đang Play:
```
1. Scene view (tab Scene)
2. Hierarchy → Tìm "Axe(Clone)"
3. Double-click để focus camera lên object
4. XEM THẤY RÌU KHÔNG?

→ NẾU THẤY trong Scene nhưng KHÔNG THẤY trong Game:
   = Camera culling mask hoặc layer sai!
   
→ NẾU KHÔNG THẤY cả trong Scene:
   = Renderer bị tắt hoặc material missing!
```

---

### ✅ **BƯỚC 3: Kiểm Tra Main Camera**

```
Hierarchy → Main Camera → Inspector:

Camera component:
├─ Culling Mask: [Everything] hoặc [Default + Player + Equip]
│  → Phải bao gồm layer của rìu!
│
├─ Near Clipping Plane: 0.1-0.3
│  → NẾU quá lớn (>0.5) → Rìu gần camera bị clip!
│
└─ Far Clipping Plane: 1000
   → OK
```

**FIX Near Clipping:**
```
Near: 0.3 → Đổi thành 0.1
→ Rìu gần camera hơn sẽ thấy được
```

---

### ✅ **BƯỚC 4: Kiểm Tra Equip Parent Position**

```
Hierarchy → Player → CameraContainer → Equip Camera

Transform:
├─ Local Position: (0.5, -0.3, 0.5) ← Gần camera
│  
│  NẾU KHÔNG ĐÚNG:
│  X: 0.5 (bên phải)
│  Y: -0.3 (dưới một chút)
│  Z: 0.5 (PHÍA TRƯỚC camera) ← QUAN TRỌNG!
│
└─ Rotation: (0, 0, 0)
```

**Z phải DƯƠNG (>0)** để ở phía trước camera!

---

### ✅ **BƯỚC 5: Kiểm Tra Axe Prefab**

```
Project → Tìm "Axe" prefab → Double click mở

Kiểm tra:
├─ Có MeshRenderer hoặc SkinnedMeshRenderer? ✓
├─ Material có bị missing (màu tím)? ✗
├─ Layer = Default? ✓
└─ Scale đủ lớn (min 0.5)? ✓

NẾU MATERIAL MISSING (tím):
→ Gán material mới
→ Shader: Standard
```

---

### ✅ **BƯỚC 6: Test Với Gizmos**

Thêm vào EquipToolAxe.cs:
```csharp
private void OnDrawGizmos()
{
    // Draw sphere at equipped position
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, 0.1f);
    
    // Draw forward direction
    Gizmos.color = Color.blue;
    Gizmos.DrawLine(transform.position, transform.position + transform.forward * 0.5f);
}
```

→ Trong Scene view sẽ thấy hình cầu xanh = vị trí rìu!

---

## 🎯 CÁC VẤN ĐỀ THƯỜNG GẶP

### **1. Rìu Ở Sau Camera (Z âm)**

```
Problem: Local Position Z < 0
Fix: 
   Equip Camera → Local Position Z = 0.5 (dương)
   Hoặc Axe prefab → Local Position Z = 0.5
```

### **2. Near Clipping Quá Lớn**

```
Problem: Camera Near = 0.3, rìu ở 0.2 trước camera
Fix: Camera → Near = 0.1
```

### **3. Layer Không Khớp**

```
Problem: Axe layer = "Equip", Camera culling = "Default" only
Fix: 
   Option 1: Axe → Layer = Default
   Option 2: Camera → Culling Mask += Equip layer
```

### **4. Renderer Bị Tắt**

```
Problem: Renderer.enabled = false
Fix: Prefab → MeshRenderer → ✓ Enabled
```

### **5. Scale Quá Nhỏ**

```
Problem: Scale = 0.001
Fix: Scale = 1 hoặc lớn hơn
```

### **6. Material Missing**

```
Problem: Material = None (màu tím)
Fix: 
   Create Material → Standard shader
   Gán vào MeshRenderer
```

---

## 🛠️ QUICK FIX SCRIPT

Thêm vào Player để auto-fix:

```csharp
// Trong EquipManager.cs - đã có rồi!
// Code đã auto-set layer và log chi tiết
```

---

## 📊 DEBUG FLOWCHART

```
Equip Axe
    ↓
Console: "Renderers found: X"
    ↓
X > 0? → YES → Scene view thấy?
  |              ↓
  |           YES → Game view không thấy?
  |              ↓              ↓
  |           Camera       Near clipping
  |           culling       hoặc layer
  |           mask
  |
  ↓ NO
Prefab không có Renderer!
→ Thêm MeshRenderer + Material
```

---

## ✅ SOLUTION SUMMARY

**Hầu hết trường hợp:**

1. **Near Clipping = 0.3 → Đổi 0.1**
2. **Equip Parent Z = 0 → Đổi 0.5**
3. **Layer không khớp → Set "Default"**
4. **Renderer bị tắt → Enable**
5. **Material missing → Gán material**

---

## 🎮 TEST CUỐI

Sau khi fix:
```
1. Play game
2. Equip Axe
3. Console: "Renderers found: 1+"
4. Scene view: Thấy rìu ✅
5. Game view: Thấy rìu ✅
6. Move camera: Rìu follow ✅
7. Click: Animation chạy ✅
```

---

**📸 GỬI CHO TÔI:**

1. Screenshot Console log sau khi equip
2. Screenshot Hierarchy với Axe(Clone) selected
3. Screenshot Inspector của Axe(Clone)
4. Screenshot Main Camera culling mask

→ Tôi sẽ biết chính xác vấn đề!
