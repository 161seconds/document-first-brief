# STORY-030: Phát hành và quản lý lịch đăng bài

## Metadata

- **Story**: Là một Quản trị viên, tôi muốn đăng ngay, lưu ở trạng thái chưa đăng hoặc lên lịch cho nội dung đã hoàn thiện để phát hành bài đăng theo thời điểm phù hợp và theo dõi kết quả của từng nền tảng.
- **Context**: Sau khi kiểm tra nội dung của từng nền tảng, Quản trị viên có thể đăng ngay, chọn ngày giờ để đăng sau hoặc lưu bài ở trạng thái chưa đăng. Hệ thống cung cấp danh sách bài đăng để theo dõi trạng thái, xem trước, chỉnh sửa lịch hoặc hủy lịch khi trạng thái còn cho phép.
- **Sprint**: S1
- **Priority**: Must
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Đang duyệt
- **Cập nhật**: 11/09/2026
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Hồ Hoàng Nam
- **Status**: Cần làm
- **Assignee**:
  - BE: Tân Trần
- **Creator**: Hồ Hoàng Nam
- **Thống kê tài liệu**: Rules: 18 | Unit Tests: 0 | System Tests: 0

---

## Conditions

### Preconditions
- Ít nhất một nền tảng có Content hợp lệ hoặc ít nhất một hình ảnh hợp lệ được chọn để đăng.
- Hình ảnh được chọn, nếu có, đã được xác định thứ tự.
- Quản trị viên đã đăng nhập và có quyền phát hành bài đăng.

### Trigger
- Quản trị viên chọn phương thức phát hành trên màn hình xem trước.

---

## Flow

### Main Flow — Phát hành và lên lịch bài đăng
1. Hệ thống hiển thị các lựa chọn “Đăng ngay”, “Đăng sau” và “Chưa đăng”.
2. Quản trị viên chọn “Đăng sau”.
3. Hệ thống hiển thị trường Ngày đăng, Giờ đăng và múi giờ `Asia/Ho_Chi_Minh`.
4. Quản trị viên chọn ngày và giờ trong tương lai.
5. Hệ thống kiểm tra nền tảng, thời gian đăng và tính hợp lệ của bài đăng; mỗi nền tảng phải có Content hợp lệ hoặc ít nhất một hình ảnh hợp lệ.
6. Quản trị viên chọn “Lên lịch đăng”.
7. Hệ thống tạo lịch thực hiện riêng cho từng nền tảng đã chọn.
8. Hệ thống hiển thị thông báo lên lịch thành công.
9. Quản trị viên mở Danh sách bài đăng.
10. Hệ thống hiển thị nội dung, hình ảnh, nền tảng, thời gian và trạng thái của từng bài đăng.
11. Quản trị viên có thể xem trước hoặc thực hiện thao tác phù hợp với trạng thái hiện tại.

### Alternative Flow

#### ALT-01 — Quản trị viên chọn đăng ngay
- Hệ thống không yêu cầu ngày và giờ đăng.
- Quản trị viên chọn “Đăng ngay”.
- Hệ thống gửi bài đăng đến từng nền tảng đã chọn và cập nhật trạng thái xử lý riêng.

#### ALT-02 — Quản trị viên chọn chưa đăng
- Hệ thống không yêu cầu ngày và giờ đăng.
- Quản trị viên chọn “Lưu chưa đăng”.
- Hệ thống lưu nội dung ở trạng thái “Chưa đăng” và không gửi đến nền tảng.

#### ALT-03 — Quản trị viên thay đổi thứ tự danh sách bài đăng
- Quản trị viên chọn sắp xếp gần nhất hoặc cũ nhất trước.
- Hệ thống hiển thị lại danh sách theo thứ tự đã chọn.

#### ALT-04 — Quản trị viên chỉnh sửa một lịch còn được phép sửa
- Quản trị viên mở menu thao tác và chọn chỉnh sửa lịch.
- Hệ thống hiển thị thông tin hiện tại của lịch.
- Quản trị viên cập nhật dữ liệu hợp lệ và lưu.
- Hệ thống cập nhật lịch theo dữ liệu mới.

#### ALT-05 — Quản trị viên hủy một lịch còn được phép hủy
- Quản trị viên mở menu thao tác và chọn hủy lịch.
- Hệ thống yêu cầu xác nhận.
- Sau khi Quản trị viên xác nhận, hệ thống hủy lịch và không tiếp tục thực hiện lịch đó.

### Exception Flow

#### EXC-01 — Ngày hoặc giờ đăng sau chưa được nhập hoặc không nằm trong tương lai
- Hệ thống không tạo lịch đăng.
- Hệ thống yêu cầu Quản trị viên nhập ngày và giờ hợp lệ.
- Nội dung đã chuẩn bị được giữ nguyên.

#### EXC-02 — Một nền tảng đăng bài thất bại
- Hệ thống cập nhật nền tảng đó thành “Đăng thất bại”.
- Hệ thống giữ nguyên kết quả thành công của các nền tảng khác.
- Hệ thống lưu nguyên nhân lỗi nếu đã nhận được.

#### EXC-03 — Không thể lưu hoặc tạo lịch đăng
- Hệ thống không ghi nhận thao tác thành công.
- Hệ thống không lưu dữ liệu hoặc lịch chưa hoàn chỉnh.
- Hệ thống giữ nội dung đang chuẩn bị để Quản trị viên thử lại.

#### EXC-04 — Bài đăng không có Content và hình ảnh
- Hệ thống xác định một nền tảng được chọn không có Content và không có hình ảnh hợp lệ.
- Hệ thống không đăng ngay, không tạo lịch đăng và không lưu bài ở trạng thái “Chưa đăng”.
- Hệ thống chỉ rõ nền tảng chưa có dữ liệu hợp lệ.
- Quản trị viên bổ sung Content hoặc chọn ít nhất một hình ảnh rồi thử lại.

---

## Acceptance Criteria

- **AC-001 — Đăng ngay bài đăng hợp lệ**:
  - **Given**: Bài đăng có ít nhất một nền tảng đã chọn và mỗi nền tảng có Content hợp lệ hoặc ít nhất một hình ảnh hợp lệ.
  - **When**: Quản trị viên chọn “Đăng ngay”.
  - **Then**: Hệ thống gửi bài đăng đến từng nền tảng đã chọn.
  - **And**: Trạng thái xử lý được ghi nhận riêng theo từng nền tảng.

- **AC-002 — Lưu bài đăng ở trạng thái chưa đăng**:
  - **Given**: Mỗi nền tảng được chọn có Content hợp lệ hoặc ít nhất một hình ảnh hợp lệ.
  - **When**: Quản trị viên chọn “Chưa đăng” và lưu.
  - **Then**: Hệ thống lưu bài ở trạng thái “Chưa đăng”.
  - **And**: Hệ thống không gửi bài đến bất kỳ nền tảng nào.

- **AC-003 — Lên lịch đăng bài trong tương lai**:
  - **Given**: Mỗi nền tảng được chọn có Content hợp lệ hoặc ít nhất một hình ảnh hợp lệ và Quản trị viên chọn “Đăng sau”.
  - **When**: Quản trị viên nhập ngày, giờ trong tương lai và chọn “Lên lịch đăng”.
  - **Then**: Hệ thống tạo lịch theo múi giờ `Asia/Ho_Chi_Minh`.
  - **And**: Mỗi nền tảng được theo dõi trạng thái riêng bởi job và chờ đến thời điểm đăng.

- **AC-005 — Hiển thị danh sách bài đăng đa trạng thái**:
  - **Given**: Hệ thống có các bài đăng ở nhiều trạng thái.
  - **When**: Quản trị viên mở Danh sách bài đăng.
  - **Then**: Hệ thống hiển thị nội dung, hình ảnh, nền tảng, thời gian và trạng thái của từng bài đăng.
  - **And**: Danh sách hỗ trợ sắp xếp theo thời gian.

- **AC-006 — Cô lập trạng thái đăng riêng từng nền tảng**:
  - **Given**: Một nền tảng đăng thất bại nhưng nền tảng khác đăng thành công.
  - **When**: Hệ thống hoàn tất xử lý.
  - **Then**: Hệ thống giữ trạng thái thành công của nền tảng đã đăng thành công.
  - **And**: Nền tảng thất bại được ghi nhận riêng cùng nguyên nhân nếu có.

- **AC-007 — Chỉnh sửa hoặc hủy lịch hợp lệ**:
  - **Given**: Một lịch đang ở trạng thái được phép chỉnh sửa hoặc hủy.
  - **When**: Quản trị viên thực hiện thao tác tương ứng.
  - **Then**: Hệ thống cập nhật hoặc hủy đúng lịch đã chọn.
  - **And**: Các lịch khác không bị thay đổi.

- **AC-008 — Phát hành bài đăng chỉ có Content**:
  - **Given**: Nền tảng được chọn có Content hợp lệ và không có hình ảnh.
  - **When**: Quản trị viên chọn một phương thức phát hành hợp lệ.
  - **Then**: Hệ thống cho phép lưu, đăng ngay hoặc lên lịch bài đăng.
  - **And**: Hệ thống không yêu cầu chọn hình ảnh.

- **AC-009 — Phát hành bài đăng chỉ có Hình ảnh**:
  - **Given**: Nền tảng được chọn không có Content nhưng có ít nhất một hình ảnh hợp lệ.
  - **When**: Quản trị viên chọn một phương thức phát hành hợp lệ.
  - **Then**: Hệ thống cho phép lưu, đăng ngay hoặc lên lịch bài đăng.
  - **And**: Hệ thống không yêu cầu nhập Content hoặc hashtag.

- **AC-010 — Chặn phát hành khi thiếu cả Content và Hình ảnh**:
  - **Given**: Một nền tảng được chọn không có Content và không có hình ảnh hợp lệ.
  - **When**: Quản trị viên chọn “Đăng ngay”, “Lưu chưa đăng” hoặc “Lên lịch đăng”.
  - **Then**: Hệ thống không thực hiện phương thức phát hành đã chọn.
  - **And**: Hệ thống yêu cầu bổ sung Content hoặc ít nhất một hình ảnh cho đúng nền tảng.

---

## References

### Rules
- [BR-001: Định dạng file ảnh đính kèm bài đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-001.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/f47d6261-cae6-4ed8-b879-249b26d17464))
- [BR-004: Thời điểm hẹn giờ đăng bài hợp lệ](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-004.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/0b6c451c-d250-4513-aa5d-7354e6e4537b))
- [BR-005: Bắt buộc chọn nền tảng mạng xã hội](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-005.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/b69f2298-64eb-4355-ab2d-d8f18afdc46e))
- [BR-006: Chống trùng lặp lịch đăng bài](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-006.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/94ad04a1-10ed-4d41-9345-aa73112ca1cb))
- [BR-007: Sắp xếp danh sách lịch đăng bài](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-007.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/64397395-3bee-4810-b0fb-36a88065b493))
- [BR-008: Trạng thái hợp lệ để chỉnh sửa lịch đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-008.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/c90b51f9-dde9-4ad8-8374-d7a0f1ba3c2e))
- [BR-009: Cập nhật dữ liệu khi sửa lịch đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-009.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/13ba9f66-51a7-4cc9-9596-bb5725b8aac1))
- [BR-010: Trạng thái hợp lệ để xóa lịch đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-010.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/70d8230c-9045-4024-9853-595928975dfc))
- [BR-011: Xóa lệnh Cron Job khi xóa lịch đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-011.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/eab55803-cba0-42a1-bf12-6f66349b3055))
- [BR-012: Thứ tự ổn định khi nhiều lịch có cùng thời điểm](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-012.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/82bb5dcd-988b-449a-9f8d-0676025e2e25))
- [BR-013: Tổng số lịch phải khớp danh sách hiển thị](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-013.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/ea2b2e70-3b1b-4843-af44-02c91f3c6050))
- [BR-014: Không hiển thị danh sách lịch chưa đầy đủ](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-014.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/4792eb80-b0dd-4e70-9238-cee45ac86922))
- [BR-046: Nội dung hợp lệ của lịch đăng bài](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-046.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/a90db8e1-0a6f-4bac-90d1-637449f6c024))
- [BR-047: Nền tảng hợp lệ và không trùng trong lịch đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-047.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/f8a54fb3-59bc-4856-a49f-ad5155633da0))
- [BR-048: Trạng thái đăng được ghi nhận riêng theo nền tảng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-048.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/853f7ce5-9493-499a-8304-68c56403ac74))
- [BR-049: Xử lý lỗi từ nền tảng khi tự động đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-049.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/841ae103-814f-49d5-bba4-2f3a7e354756))
- [BR-053: Dữ liệu hợp lệ khi sửa lịch đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-053.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/9c59b3ec-029f-420a-8eae-643b64d2319d))
- [BR-054: Không ghi đè lịch đã thay đổi trong lúc sửa](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-054.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/fb457dd2-6461-4c3d-8732-fdbd18217696))

### Dependencies
- [STORY-029: Xem trước và chuẩn bị phát hành bài đăng](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/ce411d48-9091-4aaf-8b8e-188402ff3dfd)

---

## Non-Functional

- Trạng thái đăng phải được ghi nhận riêng theo từng nền tảng.
- Thời gian đăng sau phải được hiển thị và xử lý theo múi giờ `Asia/Ho_Chi_Minh`.
- Không được ghi nhận thành công khi dữ liệu bài đăng hoặc lịch chưa được lưu đầy đủ.

---

## Out of Scope

- Không cấu hình kết nối tài khoản nền tảng.
- Không chỉnh sửa System Prompt trong Story này.
- Không tạo content hoặc hình ảnh bằng AI trong Story này.
