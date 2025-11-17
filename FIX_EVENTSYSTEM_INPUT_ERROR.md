# 🔧 FIX: EventSystem Input Error

## ❌ LỖI:
```
InvalidOperationException: You are trying to read Input using the UnityEngine.Input class, 
but you have switched active Input handling to Input System package in Player Settings.
UnityEngine.EventSystems.StandaloneInputModule.UpdateModule()
```

## 🎯 NGUYÊN NHÂN:
- **EventSystem** trong scene đang dùng **StandaloneInputModule** (Old Input)
- Cần đổi sang **InputSystemUIInputModule** (New Input System)

---

## ✅ CÁCH SỬA (2 PHÚT):

### BƯỚC 1: Tìm EventSystem trong scene

1. **Mở scene** đang có lỗi (Menu, IntroCutscene, hoặc Game)
2. **Tìm trong Hierarchy**: `EventSystem`
3. **Chọn EventSystem GameObject**

---

### BƯỚC 2: Replace Input Module

1. **Trong Inspector**, tìm component:
   ```
   Standalone Input Module (Script)
   ```

2. **Click vào 3 chấm** ⋮ bên phải component name

3. **Chọn "Replace with InputSystemUIInputModule"**
   
   HOẶC:
   
4. **Xóa "Standalone Input Module"**:
   - Click ⋮ → Remove Component

5. **Thêm "Input System UI Input Module"**:
   - Add Component
   - Search: `Input System UI Input Module`
   - Click để thêm

---

### BƯỚC 3: Lặp lại cho TẤT CẢ scenes

Phải làm cho các scenes:
- ✅ **Menu** scene
- ✅ **IntroCutscene** scene  
- ✅ **Game** scene

Mỗi scene có EventSystem riêng cần replace!

---

## 🎬 VIDEO GUIDE:

```
1. Hierarchy → Click "EventSystem"
2. Inspector → Tìm "Standalone Input Module"
3. Click ⋮ → "Replace with InputSystemUIInputModule"
4. Save scene (Ctrl+S)
5. Lặp lại cho scenes khác
```

---

## ✅ KIỂM TRA:

Sau khi sửa, EventSystem sẽ có:
```
EventSystem (Component)
├─ Event System (Script) ✓
└─ Input System UI Input Module (Script) ✓ ← MỚI
```

KHÔNG còn:
```
❌ Standalone Input Module (Script) ← CŨ
```

---

## 🐛 NẾU VẪN LỖI:

### Cách 1: Xóa và tạo lại EventSystem
```
1. Delete EventSystem cũ
2. Right Click Hierarchy → UI → Event System
3. Unity tự động tạo với InputSystemUIInputModule
```

### Cách 2: Check Player Settings
```
Edit → Project Settings → Player → Other Settings
Active Input Handling: Input System Package (New)
```

---

## 📋 CHECKLIST:

- [ ] Menu scene: EventSystem có InputSystemUIInputModule
- [ ] IntroCutscene scene: EventSystem có InputSystemUIInputModule  
- [ ] Game scene: EventSystem có InputSystemUIInputModule
- [ ] Không còn Standalone Input Module trong bất kỳ scene nào
- [ ] Test play - không còn lỗi InvalidOperationException

---

## 💡 TẠI SAO CẦN LÀM THỨ NÀY?

- **Old Input System** = `UnityEngine.Input` class
  - StandaloneInputModule dùng Input.mousePosition, Input.GetButtonDown()
  
- **New Input System** = `UnityEngine.InputSystem` package
  - InputSystemUIInputModule dùng Mouse.current.position, Keyboard.current
  
- UI cần **InputSystemUIInputModule** để tương thích với New Input System!

---

**🎉 SAU KHI SỬA: Không còn lỗi 999+ trong Console!** ✨
