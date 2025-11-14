# ⚡ FIX TERRAIN MÀU TÍM - 30 GIÂY

## 🎯 GIẢI PHÁP NHANH NHẤT

### 3 BƯỚC ĐƠN GIẢN:

```
BƯỚC 1: Chọn Terrain trong Hierarchy
   ↓
BƯỚC 2: Inspector → Paint Terrain → Paint Texture
   ↓
BƯỚC 3: Edit Terrain Layers... → Create Layer...
   ↓
✅ XONG! Terrain không còn tím!
```

---

## 📸 HÌNH ẢNH MINH HỌA

```
┌─────────────────────────────────────┐
│ HIERARCHY                           │
├─────────────────────────────────────┤
│ > Main Camera                       │
│ > Directional Light                 │
│ > Terrain  ← CLICK VÀO ĐÂY         │
│ > Player                            │
└─────────────────────────────────────┘
            ↓
┌─────────────────────────────────────┐
│ INSPECTOR - Terrain                 │
├─────────────────────────────────────┤
│ [Paint Terrain ▼]                   │
│   > Create Neighbor Terrains        │
│   > Paint Texture    ← CLICK        │
│   > Paint Trees                     │
│   > Paint Details                   │
└─────────────────────────────────────┘
            ↓
┌─────────────────────────────────────┐
│ Paint Texture                       │
├─────────────────────────────────────┤
│ Terrain Layers:                     │
│   [Empty or Missing]                │
│                                     │
│ [Edit Terrain Layers... ▼]         │
│   > Create Layer...  ← CLICK        │
│   > Import from Scene               │
│   > Remove Layer                    │
└─────────────────────────────────────┘
            ↓
┌─────────────────────────────────────┐
│ Save Terrain Layer                  │
├─────────────────────────────────────┤
│ File name: Ground_Layer             │
│ [Save]  [Cancel]                    │
└─────────────────────────────────────┘
            ↓
        ✅ XONG!
```

---

## 🎨 TÙY CHỈNH (TUỲ CHỌN)

**Sau khi tạo layer, có thể thêm texture:**

```
1. Project → Chọn "Ground_Layer"
2. Inspector:
   - Diffuse: Kéo texture vào (hoặc để trống)
   - Tiling X: 10
   - Tiling Y: 10
3. Save
```

**Nếu không có texture:**
- Để trống = Màu xám (OK, không tím)
- Tải free texture: polyhaven.com/textures

---

## ⚠️ NẾU VẪN TÍM

### Giải pháp 1: Xóa Layer Cũ
```
Paint Texture → Terrain Layers
→ Click dấu "-" xóa hết layers cũ
→ Create Layer mới
```

### Giải pháp 2: Đổi Material
```
Inspector → Terrain Settings (biểu tượng ⚙️)
→ Material: Built-in Standard
→ Đổi sang "Built-in Legacy Diffuse"
```

### Giải pháp 3: URP (Nếu dùng URP)
```
Material → Custom
→ Tạo Material với Shader:
   "Universal Render Pipeline → Terrain → Lit"
```

---

## 📋 CHECKLIST

- [ ] Đã chọn Terrain trong Hierarchy
- [ ] Đã mở Paint Texture
- [ ] Đã xóa layers cũ (nếu có)
- [ ] Đã Create Layer mới
- [ ] Layer xuất hiện trong list
- [ ] Terrain không còn tím ✅

---

## 💡 TẠI SAO BỊ TÍM?

**Terrain màu tím có nghĩa:**
- ❌ Không có Terrain Layer
- ❌ Layer bị missing reference
- ❌ Shader không tương thích

**Giải pháp:**
- ✅ Tạo Layer mới (30 giây)
- ✅ Layer trống cũng OK (màu xám)
- ✅ Sau đó từ từ thêm texture đẹp

---

## 🚀 TIPS NHANH

**1. Tạo nhiều layers:**
```
- Ground_Dirt (nâu)
- Ground_Grass (xanh lá)
- Ground_Stone (xám)
- Ground_Sand (vàng)
```

**2. Paint terrain:**
```
Chọn layer → Brush size → Click tô
```

**3. Free textures:**
```
- polyhaven.com/textures
- textures.com
- Google: "seamless ground texture"
```

---

## 📚 XEM THÊM

**Chi tiết đầy đủ:**
👉 Xem file: `FIX_TERRAIN_MAGENTA.md`

**Script tự động:**
👉 Copy script trong file trên vào `Assets/Editor/`

---

## ✅ KẾT QUẢ

**Trước:**
- ❌ Terrain màu tím
- ❌ Không có layers
- ❌ Nhìn xấu

**Sau:**
- ✅ Terrain màu bình thường (xám/nâu/xanh)
- ✅ Có ít nhất 1 layer
- ✅ Có thể paint nhiều layers
- ✅ Nhìn đẹp hơn!

---

**🎉 Chỉ mất 30 giây! Chúc bạn thành công! 🎉**

*Nhớ Save Scene (Ctrl + S) sau khi fix!*
