# UT-062-10: Không thay đổi Config khi lưu thất bại

## Thông tin tài liệu

- **Mã tài liệu**: UT-062-10
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: CARD CONFIG
- **Loại**: Error
- **Unit under test**: Service/handler của TDD-062.
- **Kịch bản**: Không thay đổi Config khi lưu thất bại.
- **Expected output**: Trả lỗi theo TDD; transaction/state được rollback và quota không bị tiêu hao sai.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-062
- **Section**: Kịch bản UT-062-10

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: Business Rules / API examples

