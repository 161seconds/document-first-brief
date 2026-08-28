# UT-055-07: Tạo Card Config với trạng thái mặc định

## Thông tin tài liệu

- **Mã tài liệu**: UT-055-07
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: CARD CONFIG
- **Loại**: Happy
- **Unit under test**: Service/handler của TDD-055.
- **Kịch bản**: Tạo Card Config với trạng thái mặc định.
- **Expected output**: Trả kết quả đúng Business Rule; kiểm tra response, dependency calls và state dữ liệu sau xử lý.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-055
- **Section**: Kịch bản UT-055-07

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: Business Rules / API examples

