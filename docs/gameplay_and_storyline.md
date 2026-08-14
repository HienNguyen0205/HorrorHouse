# 📜 Cốt Truyện & Hướng Dẫn Lối Chơi (Gameplay & Storyline)

## 📖 1. Cốt Truyện Dự Án (Storyline)

### **Bối Cảnh (Setting)**
Bạn tỉnh dậy trong một ngôi biệt thự cổ hoang tàn, u tối và đầy u ám mang tên **Horror House**. Mọi cửa ra vào đều bị khóa chặt bằng các loại chìa khóa cổ bí ẩn. Trong không gian yên ắng đáng sợ, những tiếng thì thầm vô hình và tiếng bước chân lạ vang lên khắp các hành lang.

### **Diễn Biến Cốt Truyện (Plot Progress)**
1. **Thức Tỉnh & Hoang Mang (The Awakening)**:
   Nhân vật chính nhận ra mình bị giam cầm trong căn nhà quái dị này. Với duy nhất một chiếc đèn pin cầm tay trên tay, bạn phải mò mẫm qua từng căn phòng tăm tối. Những câu thoại vang lên trong tâm trí: *"Có chuyện gì đang xảy ra ở đây vậy?"*, *"Chuyện này không thể là thật được!"*.

2. **Khám Phá Các Căn Phòng Khóa (Unlocking the Quarters)**:
   Để mở đường tiến sâu vào ngôi nhà, bạn phải tìm kiếm các chìa khóa được cất giấu trên các bàn cổ:
   - **Key_LivingTable**: Chìa khóa mở cửa Phòng Khách (*Living_Door*).
   - **Key_RainerTable**: Chìa khóa mở khu vực Rainer (*Rainer_Door*).
   - **Key_WandaTable**: Chìa khóa mở khu vực Wanda (*Wanda_Door*).
   - **Key_FinalTable**: Chìa khóa mở cánh cửa Cuối Cùng (*Final_Door*).

3. **Cơ Quan Bí Mật & Căn Hầm Ngầm (The Secret Chamber)**:
   Ngôi biệt thự ẩn chứa những lối đi bí mật. Bằng cách xoay chiếc đèn treo tường, một cơ quan ẩn sẽ kích hoạt mở ra cánh cửa bí mật (*Secret Door*). Xoay bức tranh cổ sẽ hé lộ căn phòng họp bí mật (*Meeting Room*) dẫn xuống căn hầm bí ẩn (*The Secret Basement*).

4. **Sự Truy Đuổi Của Quái Vật (The Ghoul Stalker)**:
   Một sinh vật đột biến hung tợn (Ghoul Zombie) đang lang thang khắp các hành lang để săn tìm kẻ xâm nhập. Khi phát hiện ra bạn, nó sẽ gầm lên kinh hoàng và lao đến tấn công. Cách duy nhất để sống sót là chạy thật nhanh hoặc trốn vào các căn phòng an toàn (*Room Center*) nơi quái vật không thể tiếp cận.

5. **Giải Thích & Thoát Khỏi Cơn Ác Mộng (The Escape / Victory)**:
   Tại căn phòng tế bí mật, bạn tìm thấy Cổ Vật Linh Hồn (*Ancient Artifact*). Đặt cổ vật lên bệ tế lễ sẽ giải trừ lời nguyền của ngôi nhà, mở ra cánh cửa thoát hiểm duy nhất để bạn trốn thoát thành công (*WinScene*).

---

## 🎮 2. Hướng Dẫn Lối Chơi (Gameplay Mechanics)

### **Bộ Phím Điều Khiển (Controls)**

| Phím / Thao Tác | Chức Năng |
| :--- | :--- |
| `W`, `A`, `S`, `D` | Di chuyển nhân vật (Tiến, Lùi, Trái, Phải) |
| `Di chuyển Chuột` | Xoay góc nhìn camera 360 độ |
| `Giữ Left Shift` | Chạy nhanh (Tăng tốc độ từ 5 lên 8) |
| `Phím E` | Bật / Tắt Đèn pin (Electric Torch) |
| `Phím F` | Tương tác (Nhặt chìa khóa, Mở cửa, Xoay tranh/đèn bí mật, Đặt cổ vật) |
| `Phím ESC` / `Enter` | Tạm dừng game (Pause Menu) / Xác nhận chuyển màn / Replay |

---

### **Các Cơ Chế Chính Trong Game (Core Mechanics)**

#### 💡 **Hệ Thống Đèn Pin & Năng Lượng (Torch & Battery System)**
- Đèn pin là nguồn sáng duy nhất giúp bạn nhìn trong bóng tối.
- Đèn pin tiêu tốn pin theo thời gian. Nhặt các cục Pin (*Battery Power Pickup*) để sạc lại độ sáng tối đa cho đèn pin.

#### 🗝️ **Hệ Thống Mở Cửa & Chìa Khóa (Door & Key System)**
- Khi nhặt chìa khóa trên bàn, chìa khóa sẽ xuất hiện trên tay nhân vật (`Key_Hand`).
- Mỗi chìa khóa chỉ mở được một cánh cửa tương ứng. Sau khi mở cửa thành công, chìa khóa sẽ tiêu biến.

#### 🚪 **Cơ Quan Bí Mật (Secret Mechanisms)**
- **Xoay Đèn Treo Tường**: Tiến lại gần và nhấn `F` để xoay đèn, kích hoạt mở cánh cửa bí mật.
- **Xoay Bức Tranh Cổ**: Nhấn `F` để xoay tranh, mở lối vào phòng họp ẩn.

#### 👹 **Hệ Thống AI Quái Vật & Núp Trốn (Enemy AI & Stealth)**
- **Tầm phát hiện**: 18m. Nếu bạn đi vào tầm nhìn của Quái vật, nó sẽ gầm lên và đuổi theo.
- **Tầm tấn công**: 3m. Nếu Quái vật tiếp cận khoảng cách 3m, nó sẽ thực hiện đòn đánh khiến bạn Thua Game ngay lập tức (*LoseScene*).
- **Điểm An Toàn (Safe Zone)**: Đi vào các vùng tâm phòng (*Room_Center*), Quái vật sẽ mất dấu và quay trở lại đường tuần tra ban đầu.

#### 🏆 **Điều Kiện Thắng / Thua (Win / Lose Conditions)**
- **Thắng (Win)**: Nhặt Cổ vật (*Item*), mang tới bệ tế (*Platform*) và nhấn `F` để hoàn thành nhiệm vụ trốn thoát.
- **Thua (Lose)**: Bị Quái vật Ghoul bắt kịp và tấn công.

---

## 📁 3. Danh Sách Màn Chơi (Scenes List)

1. `Assets/Scenes/MainMenu.unity`: Màn hình chính (Bắt đầu, Tùy chỉnh âm thanh/đồ họa, Thao tác thoát).
2. `Assets/Scenes/GamePlay.unity`: Màn chơi chính (Khám phá ngôi nhà, giải đố, chạy trốn quái vật).
3. `Assets/Scenes/LoseScene.unity`: Màn hình Thua (Hiển thị khi bị quái vật bắt).
4. `Assets/Scenes/WinScene.unity`: Màn hình Thắng (Hiển thị khi trốn thoát thành công).
