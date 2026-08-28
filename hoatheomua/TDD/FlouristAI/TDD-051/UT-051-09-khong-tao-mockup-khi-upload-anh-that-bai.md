# UT-051-09: Không tạo Mockup khi upload ảnh thất bại

## Thông tin tài liệu

- **Mã tài liệu**: UT-051-09
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: MOCKUP
- **Loại**: Error
- **Unit under test**: Service/handler của TDD-051.
- **Kịch bản**: Không tạo Mockup khi upload ảnh thất bại.
- **Expected output**: Trả lỗi theo TDD; transaction/state được rollback và quota không bị tiêu hao sai.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-051
- **Section**: Kịch bản UT-051-09

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Business Rules / API examples

