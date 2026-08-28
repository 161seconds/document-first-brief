# UT-054-02: Chặn xem Card Config không có quyền

## Thông tin tài liệu

- **Mã tài liệu**: UT-054-02
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: CARD CONFIG
- **Loại**: Validation / Authorization
- **Unit under test**: Service/handler của TDD-054.
- **Kịch bản**: Chặn xem Card Config không có quyền.
- **Expected output**: Trả đúng HTTP/messageCode theo TDD; không gọi dependency không cần thiết và không thay đổi dữ liệu.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-054
- **Section**: Kịch bản UT-054-02

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: Business Rules / API examples

