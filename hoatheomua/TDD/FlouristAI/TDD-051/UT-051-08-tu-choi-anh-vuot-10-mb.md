# UT-051-08: Từ chối ảnh vượt 10 MB

## Thông tin tài liệu

- **Mã tài liệu**: UT-051-08
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: MOCKUP
- **Loại**: Validation / Authorization
- **Unit under test**: Service/handler của TDD-051.
- **Kịch bản**: Từ chối ảnh vượt 10 MB.
- **Expected output**: Trả đúng HTTP/messageCode theo TDD; không gọi dependency không cần thiết và không thay đổi dữ liệu.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-051
- **Section**: Kịch bản UT-051-08

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Business Rules / API examples

