# UT-030-13: Tạo hoa thành công ở retry hợp lệ

## Thông tin tài liệu

- **Mã tài liệu**: UT-030-13
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: AI FLOWER
- **Loại**: Branch
- **Unit under test**: Service/handler của TDD-030.
- **Kịch bản**: Tạo hoa thành công ở retry hợp lệ.
- **Expected output**: Tạo đúng generated_flowers/client_histories, gắn logo, trừ quota sau thành công và không tạo bảng trung gian.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-030
- **Section**: Kịch bản UT-030-13

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: Business Rules / API examples

