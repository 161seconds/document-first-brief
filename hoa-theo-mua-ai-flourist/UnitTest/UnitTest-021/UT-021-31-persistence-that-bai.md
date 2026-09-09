# UT-021-31: Xóa bỏ toàn bộ kết quả tạm khi lưu thiệp thất bại

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Xóa bỏ toàn bộ kết quả tạm khi lưu thiệp thất bại
- **Ghi chú**: Kiểm tra không để lại một thiệp không có lịch sử đi kèm nếu việc lưu bị lỗi giữa chừng.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-021-31
- **Phiên bản**: v1.1
- **Author (bắt buộc)**: Nguyễn Tùng Dương
- **Reviewer**: Tân Trần
- **Approver**: Tân Trần
- **Owner (bắt buộc)**: Backend Team
- **Cập nhật gần nhất**: 2026-09-09

## Đơn vị kiểm thử

- **Module (bắt buộc)**: CARD - Thiệp HandMade
- **Unit under test (bắt buộc)**: `CardService.CreateAsync` (phần chương trình tiếp nhận và tạo thiệp mới)
- **Loại**: Error
- **Precondition / Mock setup**:
  - Tất cả thông tin đầu vào, nguồn hoa, mẫu thiệp, kích thước và bảng giá đều hợp lệ.
  - Thiệp được lưu tạm thành công nhưng bước lưu lịch sử được thiết lập để báo lỗi.
  - Hai bản ghi được đặt trong cùng một lần lưu: hoặc giữ cả hai, hoặc không giữ bản nào.
- **Các trường hợp cần kiểm tra**:
  - Lỗi khi lưu lịch sử là trường hợp chính vì nó xảy ra sau khi thiệp đã được lưu tạm.
  - Lỗi khi lưu thiệp hoặc khi xác nhận lần lưu cũng phải không để lại dữ liệu dở dang.
- **Input**:
  ```text
  Yêu cầu tạo thiệp hợp lệ
  Bước lưu lịch sử = thất bại
  ```
- **Expected output (bắt buộc)**:
  ```text
  Hệ thống báo HTTP 500 với mã INTERNAL_SERVER_ERROR.
  Sau khi dừng, không có thiệp mới và cũng không có lịch sử mới trong cơ sở dữ liệu.
  Hệ thống không dùng hạn mức AI. Chi tiết lỗi được ghi lại cho người vận hành nhưng không hiển thị cho khách.
  ```

## Phân loại và trách nhiệm

- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Backend Team
- **Rationale (bắt buộc)**: Thiệp và lịch sử là một cặp không thể tách rời. Nếu chỉ giữ một bên, khách có thể thấy dữ liệu thiếu nguồn hoặc hệ thống không thể giải thích thiệp được tạo từ đâu.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-021
- **Section**: BR-021-17, BR-021-18
- **Ghi chú**: Quy tắc trực tiếp quy định kết quả phải kiểm tra trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-035
- **Section**: Luồng tạo hoặc tạo lại thiệp thiết kế
- **Ghi chú**: Nhu cầu của khách hàng mà trường hợp kiểm tra này bảo vệ.
