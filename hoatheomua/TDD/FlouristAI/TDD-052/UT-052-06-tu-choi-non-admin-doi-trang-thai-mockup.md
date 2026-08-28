# UT-052-06: Từ chối non-Admin đổi trạng thái Mockup

## Thông tin tài liệu

- **Mã tài liệu**: UT-052-06
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: MOCKUP
- **Loại**: Validation / Authorization
- **Unit under test**: Service/handler của TDD-052.
- **Kịch bản**: Từ chối non-Admin đổi trạng thái Mockup.
- **Expected output**: Trả đúng HTTP/messageCode theo TDD; không gọi dependency không cần thiết và không thay đổi dữ liệu.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-052
- **Section**: Kịch bản UT-052-06

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-052
- **Section**: Business Rules / API examples

