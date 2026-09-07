<!-- Mỗi file chứa một test, đúng một dòng dữ liệu và 13 cột theo thứ tự bên dưới.
Thay ST-001 ở heading và Test ID, STORY-001 bằng mã Story cần kiểm thử.
Trong ô bảng: xuống dòng bằng <br>, dấu | viết thành \|. Steps gồm các bước đánh số.
Loại: Main / ALT / EXC / NFR / Integration boundary; ghép nhiều loại bằng dấu /.
Suite: SMOKE / REGRESSION / FULL. Priority: P0 / P1 / P2 / P3.
TEST_LINKS là nguồn liên kết khi import; cột Trace to chỉ để đọc. Giữ hai nơi nhất quán.
Owner là tên hiển thị; phê duyệt thực hiện sau import.
-->

# ST-001

## System Test

| Test ID | Story | Loại | Suite | Priority | Precondition | Steps | Test data | Expected result | Trace to (requirement / BR) | Rationale | Owner | Trạng thái |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| ST-001 | STORY-001 | Main | FULL | P1 | [Điều kiện môi trường và tài khoản] | 1. [Mở màn hình]<br>2. [Nhập dữ liệu và gửi]<br>3. [Kiểm tra kết quả] | [Bộ dữ liệu kiểm thử] | [Kết quả giao diện, API và dữ liệu cần xác minh] | STORY-001/AC-001 | [Lý do và phạm vi kiểm thử] | [Tên người phụ trách] | Draft |

## TEST_LINKS

- STORY-001/AC-001
