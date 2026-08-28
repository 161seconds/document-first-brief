# UT-062-04: Từ chối cập nhật Config không tồn tại

## Thông tin tài liệu

- **Mã tài liệu**: UT-062-04
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: CARD CONFIG
- **Loại**: Validation / Authorization
- **Unit under test**: Service/handler của TDD-062.
- **Kịch bản**: Từ chối cập nhật Config không tồn tại.
- **Expected output**: Trả đúng HTTP/messageCode theo TDD; không gọi dependency không cần thiết và không thay đổi dữ liệu.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-062
- **Section**: Kịch bản UT-062-04

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: Business Rules / API examples

