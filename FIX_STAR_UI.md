# 🔧 FIX: Sao Không Hiện & Căn Giữa Text

## ⚠️ VẤN ĐỀ
1. ❌ Ký tự sao không hiển thị
2. ❌ Text không căn giữa panel
3. ❌ Màu sắc chưa đẹp

---

## ✅ GIẢI PHÁP - 2 CÁCH

### 🚀 CÁCH 1: TẰM THỜI - Dùng Text "STARS" (5 giây)

Trong Unity:
1. Chọn `StarText` trong Hierarchy
2. Inspector → TextMeshPro component
3. Đổi **Text** thành: `STARS  0 / 6`

✅ **XONG!** Chạy game sẽ hiển thị: `STARS  0 / 6`

---

### 🎨 CÁCH 2: TỐT NHẤT - Auto Setup (2 phút)

#### Bước 1: Add Script Auto Setup

1. Chọn `StarText` trong Hierarchy
2. Inspector → **Add Component**
3. Gõ "auto" → Chọn **AutoSetupStarText**

#### Bước 2: Configure Settings

Script sẽ tự động setup, nhưng bạn có thể tùy chỉnh:

```
AutoSetupStarText (Script)
├─ Font Size: 40
├─ Text Color: White
├─ Font Style: Bold
├─ Alignment: Center
├─ Add Outline: ✓ (tích)
├─ Outline Color: Black
└─ Outline Width: 0.2
```

#### Bước 3: Apply Settings

Trong Inspector → AutoSetupStarText → Right Click → **Context Menu** → **Setup Text**

Hoặc chỉ cần Play game, nó sẽ tự động chạy!

✅ **XONG!** Text sẽ tự động:
- Căn giữa
- Font size đẹp
- Màu trắng
- Có outline đen (dễ đọc)

---

## 🎯 FIX KÝ TỰ SAO KHÔNG HIỆN

### Nguyên nhân:
Font mặc định của Unity không hỗ trợ Unicode star character `★`

### Giải pháp:

#### Option 1: Dùng Text Thay Sao (Đơn giản nhất)
```
STARS  0 / 6
COLLECTED  0 / 6
Sao:  0 / 6
```

#### Option 2: Dùng Image Icon
1. Tìm icon sao (PNG/Sprite)
2. Add vào StarPanel
3. Đặt icon bên trái text

#### Option 3: Import Font Hỗ Trợ Unicode
1. Download **Noto Sans** hoặc **Roboto** font
2. Drag vào Assets
3. StarText → Font Asset → Chọn font mới

---

## 📝 CODE ĐÃ CẬP NHẬT

File `StarCollectionSystem.cs` đã được update:

```csharp
// Tự động setup font khi Start
starCountText.fontSize = 40;
starCountText.fontStyle = TMPro.FontStyles.Bold;
starCountText.alignment = TMPro.TextAlignmentOptions.Center;
starCountText.color = Color.white;

// Update UI với format đẹp
starCountText.text = $"★ {currentStars} / {maxStars}";
// Hoặc nếu sao không hiện:
// starCountText.text = $"STARS  {currentStars} / {maxStars}";
```

---

## 🎮 TEST NGAY

1. **Play game**
2. **Kiểm tra góc phải màn hình**:
   - Thấy text trắng, font đậm
   - Text căn giữa panel
   - Hiển thị: `STARS  0 / 6` hoặc `★ 0 / 6`

3. **Diệt zombie**:
   - Text update: `STARS  1 / 6`
   - `STARS  2 / 6`
   - ...
   - `STARS  6 / 6` → Dừng spawn

---

## 🎨 TÙY CHỈNH THÊM

### Đổi màu background Panel:

```
StarPanel → Image component
├─ Color: Chọn màu đen
└─ Alpha (A): 150-200 (độ trong suốt)
```

### Tăng kích thước text:

```
AutoSetupStarText
└─ Font Size: 48 (hoặc 50, 60...)
```

### Thêm shadow cho text:

```
StarText → Add Component → Shadow
├─ Effect Color: Black
├─ Effect Distance: X:2, Y:-2
└─ Use Graphic Alpha: ✓
```

---

## 🐛 NẾU VẪN LỖI

### Sao vẫn không hiện?
→ Đổi sang text: `STARS  0 / 6` trong code

### Text bị cắt?
→ Tăng Width của StarPanel: `250` hoặc `300`

### Text không căn giữa?
→ Check Rect Transform của StarText:
- Anchor: Stretch-Stretch
- Left: 10, Right: 10, Top: 10, Bottom: 10

---

## ✅ CHECKLIST HOÀN CHỈNH

- [ ] Code đã update (auto push lên Git)
- [ ] StarText có component AutoSetupStarText
- [ ] Click "Setup Text" trong Inspector
- [ ] Play game → Text hiển thị đẹp
- [ ] Text màu trắng, căn giữa
- [ ] Diệt zombie → Text update
- [ ] Font size 40, Bold, có outline

---

**🎉 XONG! Giờ UI đẹp và dễ đọc rồi!**

**💡 TIP:** Nếu sao không hiện, đổi text thành "STARS" trong code là cách nhanh nhất!
