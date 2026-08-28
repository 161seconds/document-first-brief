# UT-053-05: Không xóa Mockup khi persistence thất bại

## Thông tin tài liệu

- **Mã tài liệu**: UT-053-05
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: MOCKUP
- **Loại**: Error
- **Unit under test**: Service/handler của TDD-053.
- **Kịch bản**: Không xóa Mockup khi persistence thất bại.
- **Expected output**: Trả lỗi theo TDD; transaction/state được rollback và quota không bị tiêu hao sai.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-053
- **Section**: Kịch bản UT-053-05

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-053
- **Section**: Business Rules / API examples

