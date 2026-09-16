<!-- Mỗi file chứa một test, đúng một dòng dữ liệu và 13 cột theo thứ tự bên dưới.
Thay UT-001 ở heading và Test ID. Trong ô bảng: xuống dòng bằng <br>, dấu | viết thành \|.
Loại: Happy / Branch / Boundary / Error / Quirk / Determinism. Suite: SMOKE / REGRESSION / FULL. Priority: P0 / P1 / P2 / P3.
TEST_LINKS là nguồn liên kết khi import; cột Trace to chỉ để đọc. Giữ hai nơi nhất quán.
Mỗi liên kết dùng DOC-KEY/section: ghi chú. Owner là tên hiển thị; phê duyệt thực hiện sau import.
-->

# UT-001

## Unit Test

| Test ID | Module | Unit under test | Loại | Suite | Priority | Precondition / Mock setup | Input | Expected output | Trace to (requirement / BR) | Rationale | Owner | Trạng thái |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| UT-001 | [Tên module] | [Tên hàm hoặc service] | Happy | FULL | P1 | [Thiết lập mock và điều kiện] | [Dữ liệu đầu vào] | [Kết quả và assertion cụ thể] | BR-001/Statement | [Lý do chọn ca kiểm thử] | [Tên người phụ trách] | Draft |

## TEST_LINKS

- BR-001/Statement
