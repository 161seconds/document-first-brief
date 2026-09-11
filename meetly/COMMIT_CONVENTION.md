# Quy Ước Commit & Chuẩn Hóa Phát Triển — Meetly

> **Dự án**: `meetly` (Meeting Scheduling & Visual Heatmap Polling Platform)  
> **Áp dụng cho**: Toàn bộ thành viên phát triển và AI Assistant  
> **Cập nhật**: 2026-09-11  
> **Trạng thái**: Hiệu lực bắt buộc  

---

## 📌 1. Cấu Trúc Commit Chuẩn (Commit Message Structure)

Mọi commit khi ghi nhận thay đổi mã nguồn bắt buộc tuân thủ cấu trúc đầu ra sau:

```text
Commit message: <short message>
Impact bullets:
* <bullet 1>
* <bullet 2>
```

### 🎯 Quy tắc chi tiết:
1. **Commit message (Tiêu đề commit)**:
   - Dạng thức mệnh lệnh ngắn gọn (`imperative mood`, `present tense`).
   - Độ dài tối đa: **50 ký tự** (`max 50 characters`).
   - Ví dụ: `Add heatmap cell hover tooltip`, `Fix timezone offset in vote parser`.

2. **Impact bullets (Các gạch đầu dòng tác động)**:
   - Số lượng: **4 đến 8 bullets** ngắn gọn (mỗi bullet đúng 1 dòng).
   - Mô tả các tác động quan trọng nhất gồm:
     - `behavior`: Thay đổi hành vi tương tác hoặc luồng người dùng.
     - `logic`: Thay đổi thuật toán, xử lý nghiệp vụ, tính toán thời gian.
     - `i18n`: Thay đổi chuỗi bản dịch đa ngôn ngữ.
     - `removals`: Dọn dẹp, xóa bỏ mã hoặc kiểu dữ liệu thừa.
     - `integrations`: Kết nối API, SignalR hub, hoặc dịch vụ bên ngoài.

3. **Ràng buộc nghiêm ngặt (Strict Constraints)**:
   - ❌ **KHÔNG** đưa tên file, đường dẫn (paths) hay file tags vào commit message hoặc impact bullets (ví dụ: không ghi `src/...`).
   - ❌ **KHÔNG** dùng thì quá khứ; luôn dùng câu ngắn gọn, thì hiện tại (`present tense`).
   - 🎯 **Ưu tiên**: Tác động hiển thị cho người dùng (`user-visible`) hoặc thay đổi có độ ảnh hưởng cao (`high-impact`).
   - 🗑️ **Xóa file / kiểu dữ liệu**: Khi có file hoặc kiểu bị xóa, chỉ ghi `"remove obsolete types"` (tuyệt đối không kèm đường dẫn).

---

## 🌐 2. Quy Chuẩn Đa Ngôn Ngữ (i18n Workflow)

Trước khi đóng gói commit cho bất kỳ tính năng UI / thông báo nào:

1. **Rà soát chuỗi ký tự cứng**: Đọc lại toàn bộ git changes và trích xuất tất cả hardcoded text strings thành các key tương ứng.
2. **Đồng bộ từ điển**:
   - Ghi nhận key vào cả hai file: `en.json` và `vi.json`.
   - Kiểm tra kỹ các key chưa tồn tại, bắt buộc bổ sung đầy đủ vào cả 2 ngôn ngữ trước khi tham chiếu sử dụng trong code.

---

## 📝 3. Nhật Ký Triển Khai (`_implementation-notes.md`)

Trong suốt quá trình lập trình (implement), bắt buộc duy trì và cập nhật tệp `_implementation-notes.md`:

- Ghi lại các quyết định kỹ thuật phát sinh không có sẵn trong tài liệu đặc tả ban đầu.
- Ghi lại các điểm buộc phải thay đổi so với kế hoạch hoặc đặc tả ban đầu.
- Phân tích các đánh đổi kỹ thuật (`trade-offs`).
- Tất cả các lưu ý quan trọng khác phục vụ quá trình review và kiểm thử.

---

## 📋 4. Prompt Mẫu Cho AI Assistant (Prompt Template)

Sao chép prompt dưới đây khi yêu cầu AI sinh commit message hoặc thực hiện task:

```text
Read the current GIT changes and from it, produce:
* A short imperative commit message (max 50 characters).
* 4-8 concise impact bullets (one line each) describing the most important effects (behavior, logic, i18n, removals, integrations).
Constraints:
* Do NOT include file names, paths, or file tags.
* Keep language concise and present tense.
* Prioritize user-visible or high-impact changes.
* If types/files were removed, say "remove obsolete types" (no paths).
Output only this structure, nothing else:
Commit message: <short message>
Impact bullets:
* <bullet 1>
* <bullet 2>

read the GIT changes and replace all harcoded text strings into keys at en.json vi.json then use it, make sure to check for non existed keys and add them too
implement ... and while u do keep a running _implementation-notes.md file with decisions u had to make that weren't in the ..., things u had to change, tradeoffs u had to make or everything else I should know
```
