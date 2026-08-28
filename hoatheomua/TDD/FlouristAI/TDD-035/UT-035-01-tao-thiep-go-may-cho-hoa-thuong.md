# UT-035-01: Tạo thiệp Gõ máy cho hoa thường

## Thông tin tài liệu

- **Mã tài liệu**: UT-035-01
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: AI CARD
- **Loại**: Happy
- **Unit under test**: Service/handler của TDD-035.
- **Kịch bản**: Tạo thiệp Gõ máy cho hoa thường.
- **Expected output**: Tạo đúng generated_card/client_history, nguồn/snapshot/quota đúng Business Rule và không có side effect ngoài phạm vi.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-035
- **Section**: Kịch bản UT-035-01

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: Business Rules / API examples

