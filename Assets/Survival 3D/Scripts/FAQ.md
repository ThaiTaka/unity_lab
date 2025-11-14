# ❓ FAQ - Câu Hỏi Thường Gặp

---

## 🔴 Q1: Tại sao có nhiều lỗi thế?

**A:** Không phải lỗi code! Dự án này sử dụng các packages mở rộng của Unity:
- **TextMeshPro** - Để hiển thị text đẹp hơn
- **Input System** - Hệ thống điều khiển mới của Unity
- **Unity UI** - Giao diện người dùng

Bạn chỉ cần **cài đặt 3 packages này** trong Unity Editor là hết lỗi!

---

## 🔴 Q2: Tôi đã cài TextMeshPro rồi nhưng vẫn lỗi?

**A:** Có thể bạn quên **Import TMP Essential Resources**:
1. Sau khi click Install TextMeshPro
2. Sẽ có popup "TMP Importer"
3. Phải click **"Import TMP Essentials"**

Hoặc thử:
- `Window → TextMeshPro → Import TMP Essential Resources`

---

## 🔴 Q3: Input System đã cài nhưng vẫn báo lỗi?

**A:** Phải cấu hình thêm:
1. `Edit → Project Settings → Player`
2. Tìm **"Active Input Handling"**
3. Đổi từ "Input Manager (Old)" → **"Both"**
4. **Restart Unity** (quan trọng!)

Nếu không restart Unity, lỗi vẫn còn!

---

## 🔴 Q4: Đã cài đủ 3 packages nhưng vẫn lỗi?

**A:** Thử xóa cache của Unity:
1. **Đóng Unity** hoàn toàn
2. Vào thư mục project: `E:\3D-Survival-Game-Unity\`
3. **Xóa** thư mục `Library`
4. Mở lại Unity
5. Đợi Unity rebuild (3-5 phút)

---

## 🔴 Q5: Không tìm thấy Package Manager?

**A:** 
- Menu: `Window → Package Manager`
- Hoặc phím tắt: `Ctrl + Shift + P` (Windows)

Nếu vẫn không có:
- Kiểm tra phiên bản Unity (cần 2019.4 trở lên)
- Thử update Unity lên bản mới hơn

---

## 🔴 Q6: Không thấy TextMeshPro trong Package Manager?

**A:** Đảm bảo:
1. Đã chọn **"Packages: Unity Registry"** (không phải "In Project")
2. Scroll xuống danh sách hoặc dùng ô Search
3. Tìm "TextMesh Pro" (có thể viết liền hoặc cách)

Nếu vẫn không thấy:
- Kiểm tra kết nối Internet
- Unity cần download danh sách packages từ server

---

## 🔴 Q7: Input System không cho chọn "Both"?

**A:** Có thể Unity của bạn quá cũ:
- Cần Unity **2019.4** trở lên
- Khuyên dùng: Unity **2020.3 LTS** hoặc **2021.3 LTS**

Để kiểm tra phiên bản:
- Mở Unity Hub
- Xem version bên cạnh tên project

---

## 🔴 Q8: Lỗi "Assembly reference missing"?

**A:** Thử:
1. `Assets → Reimport All` (mất thời gian)
2. Hoặc xóa thư mục `Library` (nhanh hơn)

---

## 🔴 Q9: Cài package rồi nhưng Unity không nhận?

**A:** 
1. Đợi Unity import xong (thanh progress bar góc dưới phải)
2. Kiểm tra Console có lỗi import không
3. Thử restart Unity
4. Xóa Library folder và rebuild

---

## 🔴 Q10: Package Manager bị lỗi hoặc loading mãi?

**A:**
1. Kiểm tra **kết nối Internet**
2. Kiểm tra **firewall** có chặn Unity không
3. Thử đóng Package Manager và mở lại
4. Restart Unity Editor
5. Nếu vẫn không được, xóa file cache:
   - Đóng Unity
   - Xóa: `C:\Users\[YourName]\AppData\Local\Unity\cache`
   - Mở lại Unity

---

## 🔴 Q11: Lỗi "403 Forbidden" khi cài package?

**A:**
- Do proxy hoặc firewall công ty/trường
- Thử tắt VPN
- Thử đổi mạng (dùng 4G thay vì WiFi)
- Liên hệ IT để mở quyền truy cập Unity servers

---

## 🔴 Q12: Thư mục Library ở đâu? Không thấy!

**A:** Thư mục Library có thể bị ẩn:

**Windows:**
1. Mở File Explorer
2. Vào thư mục project: `E:\3D-Survival-Game-Unity\`
3. Vào tab `View` trên menu bar
4. Check ô **"Hidden items"**
5. Thư mục Library sẽ hiện ra (màu xám)

---

## 🔴 Q13: Xóa Library có mất dữ liệu không?

**A:** **KHÔNG!** Library chỉ chứa cache và temp files:
- Assets của bạn KHÔNG bị mất
- Scripts của bạn KHÔNG bị mất
- Scenes của bạn KHÔNG bị mất

Library sẽ được Unity tự động tạo lại khi mở project.

**Lưu ý:** Đừng xóa các thư mục:
- ❌ Assets
- ❌ ProjectSettings
- ❌ Packages
- ✅ Library (xóa được, an toàn)

---

## 🔴 Q14: Cài xong package nhưng code vẫn báo đỏ trong VSCode?

**A:** VSCode cần refresh:
1. Đóng VSCode
2. Trong Unity: `Edit → Preferences → External Tools`
3. Click **"Regenerate project files"**
4. Mở lại VSCode

Hoặc:
- Trong VSCode: `Ctrl + Shift + P`
- Gõ: "Reload Window"
- Enter

---

## 🔴 Q15: Mất bao lâu để cài xong?

**A:**
- **Cài 3 packages:** 2-5 phút
- **Restart Unity:** 30 giây - 1 phút
- **Rebuild Library (nếu cần):** 3-10 phút

**Tổng:** Khoảng **10-15 phút** là xong!

---

## 🔴 Q16: Có thể cài package bằng code không?

**A:** Có, nhưng phức tạp hơn:
1. Đóng Unity
2. Edit file `Packages/manifest.json`
3. Thêm vào "dependencies":
```json
"com.unity.textmeshpro": "3.0.6",
"com.unity.inputsystem": "1.7.0",
"com.unity.ugui": "1.0.0"
```
4. Save file
5. Mở Unity, đợi import

**Khuyên dùng Package Manager** vì dễ hơn!

---

## 🔴 Q17: Unity của tôi là phiên bản cũ, có cập nhật được không?

**A:** Có 2 cách:

**Cách 1: Update Unity Editor**
- Mở Unity Hub
- Tab "Installs"
- Add version mới (2020.3 LTS hoặc 2021.3 LTS)
- Mở project bằng version mới

**Cách 2: Dùng phiên bản cũ**
- Nếu Unity >= 2019.4: Vẫn cài được packages
- Nếu Unity < 2019.4: Nên update Unity

---

## 🔴 Q18: Lỗi khác không liên quan đến packages?

**A:** Dự án này đã sửa các lỗi:
- ✅ `Unity.Mathematics` → đã thay bằng `Quaternion`
- ✅ `quaternion` → đã đổi thành `Quaternion`

Nếu có lỗi khác:
1. Kiểm tra Console chi tiết
2. Google search lỗi đó
3. Hỏi trên Unity Forum

---

## 🔴 Q19: Tôi mới học Unity, có nên dùng dự án này không?

**A:** 
- ✅ **Nên**: Nếu bạn đã biết cơ bản Unity
- ⚠️ **Cẩn thận**: Nếu bạn mới bắt đầu

**Khuyên:**
1. Học Unity cơ bản trước (3-7 ngày)
2. Làm theo tutorial đơn giản
3. Sau đó quay lại dự án này

---

## 🔴 Q20: Có thể tắt các tính năng không cần không?

**A:** Không khuyến khích vì:
- TextMeshPro: Hiển thị text trong game
- Input System: Điều khiển nhân vật
- Unity UI: Giao diện menu, inventory

Tắt sẽ làm game không chạy được!

---

## 🔴 Q21: Sau khi sửa xong, game có chạy ngay không?

**A:** 
- ✅ Nếu cài đủ packages: Compile được
- ⚠️ Nhưng vẫn cần:
  - Các assets (models, textures, prefabs)
  - Các scenes được setup đúng
  - Các scriptable objects

Nếu thiếu assets, game sẽ chạy nhưng thiếu hình ảnh/âm thanh.

---

## 🔴 Q22: File .meta là gì? Có xóa được không?

**A:** 
- ❌ **ĐỪNG XÓA** file .meta
- Đó là file metadata của Unity
- Xóa sẽ mất reference giữa các assets

Nếu đã xóa nhầm:
- Dùng Git để restore
- Hoặc để Unity tạo lại (có thể mất reference)

---

## 🔴 Q23: Tôi dùng Unity trên Mac, có khác gì không?

**A:** Giống 90%:
- Package Manager: giống hệt
- Cách cài package: giống hệt
- Khác biệt nhỏ:
  - Phím tắt: `Cmd` thay vì `Ctrl`
  - File path: `/Users/...` thay vì `C:\...`

---

## 🔴 Q24: Có thể chia sẻ dự án cho bạn bè không?

**A:** Có, nhưng lưu ý:
1. **Không nên** share thư mục Library (dung lượng lớn, không cần)
2. **Nên** share:
   - Assets/
   - ProjectSettings/
   - Packages/
   - README files

Bạn bè vẫn phải:
- Cài các packages tương tự
- Unity sẽ tự tạo lại Library

---

## 🔴 Q25: Có hỗ trợ thêm không?

**A:**
- 📄 Đọc file **SUA_LOI_CHI_TIET.md** - hướng dẫn từng bước
- 📄 Đọc file **VISUAL_GUIDE.md** - có hình minh họa
- 🎥 Xem file **VIDEO_TUTORIALS.md** - link video YouTube
- 🔧 Chạy **Check_Project.bat** - tool kiểm tra tự động

Hoặc:
- Unity Forum: https://forum.unity.com/
- Unity Answers: https://answers.unity.com/
- Discord: Unity Vietnam Community

---

## ✅ CHECKLIST CUỐI CÙNG

Nếu đã làm hết các bước này mà vẫn lỗi:

- [ ] Đã cài TextMeshPro + Import TMP Essentials
- [ ] Đã cài Input System
- [ ] Đã cấu hình Active Input Handling = "Both"
- [ ] Đã restart Unity sau khi cài Input System
- [ ] Đã kiểm tra Unity UI có trong project
- [ ] Đã đợi Unity import xong hết
- [ ] Đã thử xóa Library và rebuild
- [ ] Unity version >= 2019.4
- [ ] Đã restart máy (đôi khi cần)
- [ ] Đã kiểm tra Console chi tiết

Nếu vẫn không được → Chụp ảnh Console và hỏi trên Unity Forum!

---

**Chúc bạn thành công! 🎉**
