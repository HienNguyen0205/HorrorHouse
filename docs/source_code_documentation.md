# 🛠️ Tài Liệu Mã Nguồn & Kiến Trúc Dự Án (Source Code Documentation)

Tài liệu này tổng hợp toàn bộ cấu trúc mã nguồn C#, luồng xử lý (Architecture & Data Flow), và mô tả chi tiết các script thuộc dự án **Horror House** (Unity 2021.3.45f2 / URP 12.1.15).

---

## 🏗️ 1. Tổng Quan Kiến Trúc (Architecture Overview)

Dự án được xây dựng theo mô hình **Component-Based Architecture** tiêu chuẩn của Unity, kết hợp với các mẫu thiết kế:
- **Singleton Pattern**: Quản lý nhạc nền duy nhất giữa các Scene (`MusicControlScript`).
- **Static State Management**: Quản lý trạng thái chia sẻ giữa các hệ thống (ví dụ: `TriggerDoorControler.keyType`, `PauseMenu.GameIsPaused`, `BossController.isAttack`).
- **NavMesh Pathfinding**: Hệ thống di chuyển AI cho quái vật `BossController`.
- **Async Scene Management**: Chuyển đổi màn chơi không gây giật lag bằng `SceneManager.LoadSceneAsync`.

---

## 📁 2. Chi Tiết Các Script Trong Dự Án (Scripts Breakdown)

### 👤 **Nhóm Điều Khiển Nhân Vật & Đèn Pin**

#### 1. `PlayerController.cs`
- **Mục đích**: Quản lý di chuyển nhân vật góc nhìn thứ nhất (FPS), xoay camera theo chuột, chạy nhanh (`Shift`), và phát âm thanh bước chân.
- **Biến chính**:
  - `Speed`: Tốc độ di chuyển (Đi bộ = 5, Chạy = 8).
  - `Sensitivity`: Độ nhạy cảm ứng chuột.
  - `footStep`, `runSound`: Audio clip bước chân và tiếng chạy.
- **Điểm cốt lõi**: Phương thức `footStepControl()` sử dụng `audioSource.PlayOneShot` để phát âm thanh bước chân khi nhân vật di chuyển mà không làm gián đoạn luồng âm thanh.

#### 2. `ElectricTorchOnOff.cs`
- **Mục đích**: Bật/tắt đèn pin (`Key E`) và điều khiển cường độ sáng (`Light.intensity`).
- **Biến chính**:
  - `_flashLightOn`: Cờ trạng thái bật/tắt đèn pin.
  - `intensityLight`: Cường độ ánh sáng đèn pin.
- **Tương tác**: Gọi `_emissionMaterialFade.OnEmission()` / `OffEmission()` để đồng bộ hiệu ứng phát sáng vật liệu mặt kính đèn.

#### 3. `EmissionMaterialGlassTorchFadeOut.cs`
- **Mục đích**: Điều khiển màu sắc phát sáng (Emission Color) của vật liệu kính đèn pin theo thời gian và năng lượng pin.
- **Phương thức chính**:
  - `OnEmission()` / `OffEmission()`: Cập nhật `_EmissionColor` trên Material rendering.
  - `TimeEmission(float t)`: Giảm dần độ sáng khi pin cạn kiệt.

#### 4. `BatteryPowerPickup.cs`
- **Mục đích**: Xử lý va chạm Trigger khi người chơi nhặt Pin để sạc lại năng lượng cho đèn pin.
- **Phương thức chính**: `OnTriggerEnter(Collider other)` đặt `_torchOnOff.intensityLight = PowerIntensityLight`.

---

### 👹 **Nhóm AI Quái Vật & Tương Tác Cửa / Cơ Quan**

#### 5. `BossController.cs`
- **Mục đích**: Quản lý AI Quái vật (Ghoul Zombie), di chuyển bằng `NavMeshAgent`, phát hiện nhân vật, tấn công và phát tiếng gầm kinh dị.
- **Biến tĩnh**: `public static bool isAttack = false;`
- **Luồng xử lý**:
  - Tính khoảng cách `Vector3.Distance` đến người chơi.
  - Nếu `distance <= 18.0f` và người chơi không trốn trong `Room_Center` (Safe Zone): Quái vật đuổi theo.
  - Nếu `distance <= 3.0f`: Phát hoạt ảnh `Attack1`, đặt `isAttack = true`, và nạp `LoseScene` (Scene ID: 2).
  - `isAttack` và `isSceneLoading` được bảo vệ để không bị gọi lặp vô hạn.

#### 6. `TriggerDoorControler.cs`
- **Mục đích**: Quản lý trạng thái đóng/mở của 5 loại cửa và cơ chế mở cửa bằng chìa khóa tương ứng.
- **Biến tĩnh**:
  - `public static string keyType`: Tên chìa khóa người chơi đang cầm trên tay.
  - `public static string currDoor`: Tên cánh cửa người chơi đang đứng gần.
  - `public static Dictionary<string, string> lockDoor`: Từ điển ánh xạ cửa và chìa khóa tương ứng (`Living_Door` -> `Key_LivingTable`, v.v.).
- **Tự động Reset**: Phương thức `ResetLockDoors()` trong `Awake()` đảm bảo danh sách cửa bị khóa luôn được khôi phục khi chơi lại.

#### 7. `TriggerSecretDoor.cs` & `OpenSecretMeetingroom.cs`
- **Mục đích**: Điều khiển hoạt ảnh xoay Đèn / xoay Bức Tranh để mở cánh cửa ẩn và lối vào phòng họp bí mật.
- **Luồng xử lý**: Sử dụng Coroutine `Wait()` phát hoạt ảnh xoay trước 1.5 giây, sau đó phát hoạt ảnh mở cửa.

#### 8. `PickUpItem.cs`
- **Mục đích**: Xử lý việc nhặt Cổ vật và đặt lên bệ tế để Thắng Game (Win).
- **Luồng xử lý**:
  - Khi khoảng cách tới cổ vật `< 3.5m` và nhấn `F`: Nhặt cổ vật.
  - Khi khoảng cách tới bệ tế `< 3.5m` và nhấn `F`: Đặt cổ vật lên bệ và nạp `WinScene` (Scene ID: 3).

---

### 🖥️ **Nhóm Giao Diện UI & Menu Systems**

#### 9. `MainMenu.cs`
- **Mục đích**: Điều khiển màn hình chính (Start Game, Fade transitions, Quit).
- **Phương thức chính**:
  - `LoadScene(int sceneId)`: Khởi chạy Coroutine `LoadSceneAsync`.
  - Khởi tạo `Time.timeScale = 1f` và mở khóa con trỏ chuột `Cursor.visible = true` trong `Start()`.

#### 10. `SubMenu.cs`
- **Mục đích**: Xử lý giao diện màn kết thúc game (`LoseScene` & `WinScene`).
- **Điểm cải tiến**:
  - `Start()` mở khóa con trỏ chuột để người chơi tương tác với nút bấm UI.
  - Phím `Enter` / `Esc` hỗ trợ nạp lại Main Menu (`LoadScene(0)`), phục vụ cơ chế Replay mượt mà.

#### 11. `PauseMenu.cs`
- **Mục đích**: Quản lý menu Tạm dừng (`ESC`).
- **Biến tĩnh**: `public static bool GameIsPaused = false;`
- **Phương thức chính**:
  - `Pause()`: Hiện UI pauseMenu, khóa thời gian `Time.timeScale = 0f`, mở con trỏ chuột.
  - `Resume()`: Ẩn UI pauseMenu, đặt `Time.timeScale = 1f`, ẩn con trỏ chuột.

#### 12. `OptionMenuScript.cs`, `BgVolumnSlider.cs`, `VfxSlider.cs`, `SoundManager.cs`
- **Mục đích**: Điều chỉnh độ phân giải, chế độ toàn màn hình, chất lượng đồ họa và âm thanh nền / hiệu ứng thông qua `PlayerPrefs`.

#### 13. `MusicControlScript.cs`
- **Mục đích**: Singleton phát nhạc nền xuyên suốt game bằng `DontDestroyOnLoad(this.gameObject)`.

---

## 🛠️ 3. Script Công Cụ Build Tự Động (CLI Build Tool)

#### `Assets/Editor/BuildScript.cs`
- **Mục đích**: Cho phép build bản cài đặt Windows Standalone 64-bit tự động từ dòng lệnh (Command Line Interface).
- **Đường dẫn đầu ra**: `Builds/HorrorHouse.exe`
- **Các Scene được đóng gói**:
  1. `Assets/Scenes/MainMenu.unity`
  2. `Assets/Scenes/GamePlay.unity`
  3. `Assets/Scenes/LoseScene.unity`
  4. `Assets/Scenes/WinScene.unity`
