# UT-021-29: Từ chối khi không chứng minh được sản phẩm gốc của mẫu hoa

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Từ chối khi không chứng minh được sản phẩm gốc của mẫu hoa
- **Ghi chú**: Kiểm tra chuỗi nguồn gốc bị thiếu hoặc mâu thuẫn, khiến hệ thống không biết chắc mẫu hoa bắt đầu từ sản phẩm nào.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-021-29
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
  - Mẫu hoa tồn tại, thuộc khách hàng và có ảnh dùng được.
  - Lịch sử cần có type = flower, output_id bằng mã mẫu hoa, và base_id cùng root trỏ đến một sản phẩm hợp lệ.
- **Các trường hợp cần kiểm tra**:
  - Không có lịch sử; sai type hoặc output_id; base_id hay root rỗng; base_id khác root; hoặc sản phẩm được trỏ tới không tồn tại.
  - Hệ thống không được lấy mã sản phẩm từ metadata để đoán bù.
- **Input**:
  ```text
  generatedFlowerId = flower-01
  Lịch sử nguồn gốc = thiếu hoặc mâu thuẫn
  ```
- **Expected output (bắt buộc)**:
  ```text
  Với từng trường hợp, hệ thống báo HTTP 409 với mã SOURCE_PRODUCT_PROVENANCE_INVALID.
  Hệ thống không tự chọn một sản phẩm gần đúng và không tạo thiệp hoặc lịch sử mới.
  ```

## Phân loại và trách nhiệm

- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Backend Team
- **Rationale (bắt buộc)**: Sản phẩm gốc được dùng để truy nguồn và phục vụ các bước sau. Nếu đoán sai, thiệp có thể bị gắn với sản phẩm của một chuỗi hoàn toàn khác.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-021
- **Section**: BR-021-09, BR-021-22
- **Ghi chú**: Quy tắc trực tiếp quy định kết quả phải kiểm tra trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-035
- **Section**: Luồng tạo hoặc tạo lại thiệp thiết kế
- **Ghi chú**: Nhu cầu của khách hàng mà trường hợp kiểm tra này bảo vệ.
