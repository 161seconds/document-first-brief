# UT-062-02: Cập nhật Calligraphy Config hợp lệ

## Thông tin tài liệu

- **Mã tài liệu**: UT-062-02
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: CARD CONFIG
- **Loại**: Happy
- **Unit under test**: Service/handler của TDD-062.
- **Kịch bản**: Cập nhật Calligraphy Config hợp lệ.
- **Expected output**: Trả kết quả đúng Business Rule; kiểm tra response, dependency calls và state dữ liệu sau xử lý.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-062
- **Section**: Kịch bản UT-062-02

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: Business Rules / API examples

