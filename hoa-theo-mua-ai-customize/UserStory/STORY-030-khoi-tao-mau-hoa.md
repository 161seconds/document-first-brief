# STORY-030: Khởi tạo mẫu hoa

## Metadata

- **Story**: Là một khách hàng đã đăng nhập, tôi muốn khởi tạo yêu cầu tạo mẫu hoa từ một Combo đã chọn và cấu hình các lựa chọn như Size, kiểu bó, Mockup, giấy gói, ruy băng, để lưu lại đầy đủ đầu vào làm cơ sở cho AI tạo mẫu hoa sau này.
- **Context**: Khách hàng bắt đầu quy trình khởi tạo mẫu hoa bằng cách chọn “Tạo mẫu hoa” trên một Combo Card trong danh sách mẫu hoa / Combo.
- **Sprint**: S1
- **Priority**: Must
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Nháp
- **Cập nhật**: 12/09/2026
- **Author**: Hoàng Thị Khánh Linh
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Hoàng Thị Khánh Linh
- **Status**: Cần làm
- **Assignee**:
  - BE: Hoàng Thị Khánh Linh
  - QA: Hoàng Thị Khánh Linh
- **Creator**: Hoàng Thị Khánh Linh
- **Thống kê tài liệu**: TDDs: 1 | Rules: 15 | Unit Tests: 46 | System Tests: 13

---

## Conditions

### Preconditions

- Khách hàng đã đăng nhập.
- Tài khoản khách hàng đang hoạt động.
- Khách hàng có quyền tạo yêu cầu tạo mẫu hoa.
- Hệ thống đang hoạt động bình thường.
- Combo được khách hàng chọn tồn tại trong Core Database.

### Trigger

- Khách hàng chọn “Tạo mẫu hoa” trên một Combo Card trong danh sách mẫu hoa / Combo.

---

## Flow

### Main Flow

1. Khách hàng xem danh sách mẫu hoa / Combo.
2. Khách hàng chọn “Tạo mẫu hoa” trên một Combo Card.
3. Hệ thống ghi nhận Combo được chọn là Combo nguồn ban đầu của quy trình.
4. Hệ thống lấy thông tin Combo nguồn từ Core Database.
5. Hệ thống điều hướng khách hàng đến trang form điền thông tin khởi tạo yêu cầu tạo mẫu hoa.
6. Hệ thống hiển thị thông tin Combo nguồn, gồm hình ảnh, tên Combo, mô tả, thành phần nguyên liệu và giá Combo.
7. Khách hàng nhập Tên yêu cầu tạo mẫu hoa.
8. Khách hàng chọn “Tiếp theo”.
9. Hệ thống kiểm tra tính hợp lệ của thông tin đã nhập.
10. Nếu thông tin hợp lệ, hệ thống hiển thị danh sách Size khả dụng.
11. Khách hàng chọn một Size.
12. Hệ thống lấy cấu hình Combo hợp lệ theo Size đã chọn.
13. Hệ thống hiển thị các lựa chọn cấu hình Combo theo Size, gồm phương án tăng số lượng Combo nguồn hoặc kết hợp thêm một hay nhiều Combo khác nếu được cửa hàng cấu hình.
14. Khách hàng chọn một cấu hình Combo hợp lệ theo Size.
15. Hệ thống ghi nhận cấu hình Combo đã chọn theo Size của yêu cầu tạo mẫu hoa.
16. Hệ thống hiển thị danh sách kiểu bó có hình ảnh.
17. Khách hàng chọn một kiểu bó.
18. Hệ thống truy xuất danh sách Mockup còn khả dụng từ Core Database.
19. Hệ thống hiển thị các Mockup dưới dạng Mockup Card.
20. Khách hàng chọn duy nhất một Mockup.
21. Hệ thống hiển thị danh sách giấy gói còn khả dụng.
22. Khách hàng chọn một giấy gói.
23. Hệ thống hiển thị danh sách ruy băng còn khả dụng.
24. Khách hàng chọn một ruy băng.
25. Khách hàng chọn “Hoàn thành”.
26. Hệ thống kiểm tra lại toàn bộ dữ liệu của yêu cầu tạo mẫu hoa.
27. Hệ thống kiểm tra lại trạng thái khả dụng của Combo nguồn và các Combo trong cấu hình Combo đã chọn theo Size.
28. Hệ thống kiểm tra lại trạng thái khả dụng của Size, kiểu bó, Mockup, giấy gói và ruy băng.
29. Nếu toàn bộ dữ liệu hợp lệ, hệ thống tạo một yêu cầu tạo mẫu hoa.
30. Hệ thống liên kết yêu cầu tạo mẫu hoa với tài khoản khách hàng hiện tại.
31. Hệ thống lưu Combo nguồn ban đầu và cấu hình Combo đã chọn theo Size.
32. Hệ thống lưu Size, kiểu bó, Mockup, giấy gói và ruy băng đã chọn.
33. Hệ thống gán trạng thái ban đầu của yêu cầu tạo mẫu hoa là Bản nháp.
34. Hệ thống thông báo tạo yêu cầu tạo mẫu hoa thành công.
35. Hệ thống chuyển khách hàng đến màn hình Chi tiết yêu cầu tạo mẫu hoa.
36. Hệ thống hiển thị đầy đủ thông tin của yêu cầu vừa tạo.
37. Hệ thống hiển thị chức năng “Tạo mẫu hoa bằng AI”.
38. Hệ thống chưa gửi yêu cầu tạo mẫu hoa bằng AI cho đến khi khách hàng chủ động sử dụng chức năng này.

### Alternative Flow

#### ALT-01 — Khách hàng quay lại bước trước trong quá trình khởi tạo
1. Khách hàng đang ở một bước sau bước đầu tiên của quy trình.
2. Khách hàng chọn “Quay lại”.
3. Hệ thống chuyển khách hàng về bước trước đó.
4. Hệ thống giữ nguyên các thông tin và lựa chọn khách hàng đã thực hiện nếu các lựa chọn đó còn hợp lệ.
5. Khách hàng có thể chỉnh sửa dữ liệu và tiếp tục quy trình.

#### ALT-02 — Khách hàng xem preview hình ảnh trong các bước chọn bằng hình
1. Khách hàng đang ở bước chọn kiểu bó, Mockup, giấy gói hoặc ruy băng.
2. Khách hàng chọn xem hình ảnh chi tiết.
3. Hệ thống hiển thị Image Preview Modal với hình ảnh kích thước lớn hơn.
4. Khách hàng đóng Image Preview Modal và tiếp tục lựa chọn.

#### ALT-03 — Size cho phép tăng số lượng Combo nguồn
1. Khách hàng đã chọn một Size.
2. Cấu hình của cửa hàng cho Size đó cho phép tăng số lượng Combo nguồn.
3. Hệ thống hiển thị lựa chọn tăng số lượng Combo nguồn.
4. Khách hàng chọn lựa chọn này.
5. Hệ thống ghi nhận cấu hình Combo đã chọn theo Size gồm Combo nguồn và số lượng tương ứng theo cấu hình Size.

#### ALT-04 — Size cho phép kết hợp thêm Combo khác
1. Khách hàng đã chọn một Size.
2. Cấu hình của cửa hàng cho Size đó cho phép kết hợp thêm một hoặc nhiều Combo khác.
3. Hệ thống hiển thị các Combo được phép kết hợp và giới hạn số lượng Combo được kết hợp theo cấu hình Size.
4. Khách hàng chọn các Combo kết hợp trong phạm vi được phép.
5. Hệ thống ghi nhận cấu hình Combo đã chọn theo Size gồm Combo nguồn và các Combo kết hợp đã chọn.

### Exception Flow

#### EXC-01 — Tên yêu cầu tạo mẫu hoa không hợp lệ
1. Khách hàng để trống hoặc nhập Tên yêu cầu tạo mẫu hoa không hợp lệ.
2. Khách hàng chọn “Tiếp theo” hoặc “Hoàn thành”.
3. Hệ thống loại bỏ khoảng trắng ở đầu và cuối của Tên trước khi kiểm tra.
4. Hệ thống kiểm tra Tên có độ dài từ 1 đến 100 ký tự.
5. Hệ thống hiển thị lỗi tại trường Tên nếu dữ liệu không hợp lệ.
6. Hệ thống không cho phép khách hàng chuyển sang bước tiếp theo hoặc hoàn thành cho đến khi Tên hợp lệ.

#### EXC-02 — Combo nguồn hoặc Combo trong cấu hình Combo đã chọn theo Size không còn khả dụng
1. Khách hàng đã chọn cấu hình Combo theo Size.
2. Khách hàng chọn “Hoàn thành”.
3. Hệ thống kiểm tra lại Combo nguồn và các Combo trong cấu hình Combo đã chọn theo Size.
4. Hệ thống phát hiện ít nhất một Combo không còn khả dụng.
5. Hệ thống không tạo yêu cầu tạo mẫu hoa.
6. Hệ thống thông báo Combo đã chọn không còn khả dụng và yêu cầu khách hàng chọn lại cấu hình phù hợp.

#### EXC-03 — Size hoặc cấu hình Combo theo Size không hợp lệ
1. Khách hàng chọn một Size hoặc cấu hình Combo theo Size.
2. Hệ thống kiểm tra cấu hình hiện hành của cửa hàng.
3. Hệ thống phát hiện Size không còn khả dụng hoặc cấu hình Combo đã chọn không còn hợp lệ.
4. Hệ thống không cho phép tiếp tục.
5. Hệ thống yêu cầu khách hàng chọn lại Size hoặc cấu hình Combo hợp lệ.

#### EXC-04 — Chưa chọn dữ liệu bắt buộc trong các bước cấu hình
1. Khách hàng chưa chọn Size, cấu hình Combo theo Size, kiểu bó, Mockup, giấy gói hoặc ruy băng.
2. Khách hàng chọn “Hoàn thành”.
3. Hệ thống không tạo yêu cầu tạo mẫu hoa.
4. Hệ thống hiển thị lỗi tại bước hoặc trường còn thiếu.
5. Hệ thống yêu cầu khách hàng hoàn tất các lựa chọn bắt buộc trước khi tiếp tục.

#### EXC-05 — Mockup, kiểu bó, giấy gói hoặc ruy băng không còn khả dụng
1. Khách hàng đã chọn Mockup, kiểu bó, giấy gói hoặc ruy băng.
2. Khách hàng chọn “Hoàn thành”.
3. Hệ thống kiểm tra lại trạng thái khả dụng của các dữ liệu đã chọn.
4. Hệ thống phát hiện ít nhất một dữ liệu không còn khả dụng.
5. Hệ thống không tạo yêu cầu tạo mẫu hoa.
6. Hệ thống thông báo dữ liệu đã chọn không còn khả dụng và yêu cầu khách hàng chọn lại.

#### EXC-06 — Lỗi khi lưu dữ liệu
1. Khách hàng đã cung cấp đầy đủ dữ liệu hợp lệ.
2. Khách hàng chọn “Hoàn thành”.
3. Hệ thống gặp lỗi trong quá trình lưu.
4. Hệ thống không tạo yêu cầu tạo mẫu hoa.
5. Hệ thống thông báo: “Tạo yêu cầu mẫu hoa thất bại. Vui lòng thử lại.”
6. Dữ liệu khách hàng đã nhập được giữ lại nếu có thể.
7. Khách hàng có thể thử lại.

---

## Acceptance Criteria

#### AC-001
- **Given**: Khách hàng đã đăng nhập và đang xem danh sách mẫu hoa / Combo.
- **When**: Khách hàng chọn “Tạo mẫu hoa” trên một Combo Card.
- **Then**: Hệ thống phải điều hướng khách hàng đến trang form khởi tạo yêu cầu tạo mẫu hoa.
- **And**: Hệ thống phải ghi nhận Combo được chọn là Combo nguồn ban đầu của quy trình.

#### AC-002
- **Given**: Khách hàng đang ở trang form khởi tạo yêu cầu tạo mẫu hoa.
- **When**: Form được hiển thị.
- **Then**: Hệ thống phải hiển thị thông tin Combo nguồn từ Core Database.
- **And**: Thông tin hiển thị gồm hình ảnh, tên Combo, mô tả, thành phần nguyên liệu và giá Combo.

#### AC-003
- **Given**: Khách hàng đang nhập Tên yêu cầu tạo mẫu hoa.
- **When**: Khách hàng chọn “Tiếp theo”.
- **Then**: Hệ thống phải loại bỏ khoảng trắng ở đầu và cuối của Tên trước khi kiểm tra.
- **And**: Tên phải có độ dài từ 1 đến 100 ký tự; nếu Tên không hợp lệ, hệ thống không được chuyển sang bước tiếp theo và phải hiển thị lỗi tại trường Tên.

#### AC-004
- **Given**: Tên yêu cầu tạo mẫu hoa hợp lệ.
- **When**: Khách hàng chuyển sang bước chọn Size.
- **Then**: Hệ thống phải hiển thị danh sách Size khả dụng.
- **And**: Tại một thời điểm khách hàng chỉ được chọn một Size.

#### AC-005
- **Given**: Khách hàng đã chọn một Size.
- **When**: Hệ thống tải cấu hình Combo theo Size.
- **Then**: Hệ thống phải hiển thị các lựa chọn cấu hình Combo hợp lệ theo cấu hình của cửa hàng.
- **And**: Các lựa chọn có thể gồm tăng số lượng Combo nguồn hoặc kết hợp thêm một hay nhiều Combo khác.

#### AC-006
- **Given**: Khách hàng đã chọn một Size có cấu hình cho phép tăng số lượng Combo nguồn.
- **When**: Khách hàng chọn phương án tăng số lượng Combo nguồn.
- **Then**: Hệ thống phải ghi nhận Combo nguồn và số lượng tương ứng theo cấu hình Size.
- **And**: Số lượng Combo nguồn không được vượt quá cấu hình mà cửa hàng cho phép đối với Size đã chọn.

#### AC-007
- **Given**: Khách hàng đã chọn một Size có cấu hình cho phép kết hợp Combo khác.
- **When**: Khách hàng chọn phương án kết hợp Combo khác.
- **Then**: Hệ thống phải cho phép khách hàng chọn Combo kết hợp trong danh sách được cửa hàng cấu hình cho Size đó.
- **And**: Số lượng Combo được kết hợp không được vượt quá giới hạn mà cửa hàng cấu hình cho Size đã chọn.

#### AC-008
- **Given**: Khách hàng đã chọn cấu hình Combo theo Size.
- **When**: Hệ thống chuyển sang bước chọn kiểu bó.
- **Then**: Hệ thống phải hiển thị danh sách kiểu bó có hình ảnh.
- **And**: Tại một thời điểm khách hàng chỉ được chọn một kiểu bó.

#### AC-009
- **Given**: Khách hàng đang ở bước chọn Mockup.
- **When**: Danh sách Mockup được hiển thị.
- **Then**: Hệ thống phải truy xuất danh sách Mockup còn khả dụng từ Core Database.
- **And**: Tại một thời điểm khách hàng chỉ được chọn một Mockup.

#### AC-010
- **Given**: Khách hàng đang ở bước chọn giấy gói.
- **When**: Danh sách giấy gói được hiển thị.
- **Then**: Hệ thống phải hiển thị danh sách giấy gói còn khả dụng.
- **And**: Tại một thời điểm khách hàng chỉ được chọn một giấy gói.

#### AC-011
- **Given**: Khách hàng đang ở bước chọn ruy băng.
- **When**: Danh sách ruy băng được hiển thị.
- **Then**: Hệ thống phải hiển thị danh sách ruy băng còn khả dụng.
- **And**: Tại một thời điểm khách hàng chỉ được chọn một ruy băng.

#### AC-012
- **Given**: Khách hàng đã nhập đầy đủ thông tin hợp lệ và đã chọn Size, cấu hình Combo theo Size, kiểu bó, Mockup, giấy gói và ruy băng.
- **When**: Khách hàng chọn “Hoàn thành”.
- **Then**: Hệ thống phải kiểm tra lại trạng thái khả dụng của toàn bộ dữ liệu đã chọn.
- **And**: Nếu có dữ liệu không còn khả dụng, hệ thống không được tạo yêu cầu tạo mẫu hoa.

#### AC-013
- **Given**: Toàn bộ dữ liệu khởi tạo hợp lệ.
- **When**: Khách hàng chọn “Hoàn thành”.
- **Then**: Hệ thống phải tạo một yêu cầu tạo mẫu hoa mới.
- **And**: Yêu cầu phải liên kết với tài khoản khách hàng hiện tại, Combo nguồn ban đầu, cấu hình Combo đã chọn theo Size, Size, kiểu bó, Mockup, giấy gói và ruy băng đã chọn.

#### AC-014
- **Given**: Yêu cầu tạo mẫu hoa được tạo thành công.
- **When**: Hệ thống lưu yêu cầu tạo mẫu hoa.
- **Then**: Yêu cầu tạo mẫu hoa phải có trạng thái ban đầu là Bản nháp.
- **And**: Hệ thống không được tự động chuyển sang trạng thái tạo AI.

#### AC-015
- **Given**: Yêu cầu tạo mẫu hoa đã được tạo thành công.
- **When**: Khách hàng được chuyển đến màn hình Chi tiết yêu cầu tạo mẫu hoa.
- **Then**: Hệ thống phải hiển thị đầy đủ thông tin của yêu cầu vừa được tạo.
- **And**: Thông tin hiển thị gồm tên yêu cầu, ngày tạo, trạng thái, Combo nguồn ban đầu, cấu hình Combo đã chọn theo Size, Size, kiểu bó, Mockup, giấy gói và ruy băng.

#### AC-016
- **Given**: Yêu cầu tạo mẫu hoa đã được tạo thành công.
- **When**: Màn hình Chi tiết yêu cầu tạo mẫu hoa được hiển thị.
- **Then**: Hệ thống phải hiển thị chức năng “Tạo mẫu hoa bằng AI”.
- **And**: Hệ thống chưa tự động gửi yêu cầu tạo mẫu hoa bằng AI cho đến khi khách hàng chủ động sử dụng chức năng này.

#### AC-017
- **Given**: Khách hàng đã hoàn thành ít nhất một bước trong quy trình.
- **When**: Khách hàng chọn “Quay lại”.
- **Then**: Hệ thống phải hiển thị lại các thông tin và lựa chọn đã thực hiện trước đó.
- **And**: Khách hàng có thể thay đổi dữ liệu trước đó nếu dữ liệu đó còn hợp lệ.

---

## References

### Rules

- [BR-068](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/b1017f3c-b500-4d66-83a3-46b3a488d37f): Giới hạn ký tự tên yêu cầu
- [BR-069](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/a925fb05-3c5e-4592-8e0c-22a516df58a2): Validation thông tin bắt buộc
- [BR-070](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/e85f0e09-9b6a-49a8-b62d-ccc467c2bb94): Combo nguồn và cấu hình Combo theo Size
- [BR-071](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/30761eaa-4df8-4e44-8d1e-efaba44782bf): Duy nhất một cấu hình Combo theo Size
- [BR-072](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/2371ded1-c7f4-42a2-98a4-1c4f71d8095c): Quyền sở hữu yêu cầu tạo mẫu hoa
- [BR-076](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/c5e7d260-7968-449d-b0a6-8de5602056d0): Nguồn và điều kiện hiển thị Mockup
- [BR-077](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/c67cdf93-5604-438a-a247-e5195bf0e46d): Một yêu cầu tạo mẫu hoa chỉ có một Mockup
- [BR-078](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/4e2249f4-842f-4159-a04c-295904f50ec3): Mockup bắt buộc khi hoàn thành
- [BR-079](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/71fd8ff3-1fbd-4ab8-969b-1256d2e70618): Nguồn dữ liệu Combo
- [BR-080](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/25e0fc0c-5927-4bab-8a46-86ff656fe108): Trạng thái ban đầu Bản nháp
- [BR-267](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/cd17ed46-c350-48b4-8b58-db7f94e22bdf): Chọn Size khi khởi tạo mẫu hoa
- [BR-268](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/73ba5a6b-3deb-48e4-a62c-efafae4cb33a): Cấu hình Combo theo Size
- [BR-269](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/baa36bab-ae33-4c6c-827d-f7f775bcaede): Chọn kiểu bó khi khởi tạo mẫu hoa
- [BR-270](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/922030ff-fc05-4b71-8059-2f0eb41dca4c): Chọn giấy gói khi khởi tạo mẫu hoa
- [BR-271](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/0c3e391d-29a6-4747-bd28-a0d056f2e2e1): Chọn ruy băng khi khởi tạo mẫu hoa

---

## Non-Functional

- API tạo yêu cầu tạo mẫu hoa có thời gian phản hồi p95 $\le 1.5$ giây, không tính thời gian bất khả kháng từ hệ thống bên thứ ba nếu team có cách đo tách riêng dependency.
- Validation trên giao diện phải phản hồi trong vòng $\le 300$ ms.
- Một thao tác “Hoàn thành” chỉ được tạo tối đa một yêu cầu tạo mẫu hoa.
- Việc khách hàng chọn “Hoàn thành” nhiều lần liên tiếp không được tạo duplicate ngoài ý muốn.
- Không được tồn tại yêu cầu tạo mẫu hoa ở trạng thái dữ liệu lưu dở dang.
- Backend phải kiểm tra lại Combo từ nguồn dữ liệu chính thức thay vì tin trực tiếp `comboId` hoặc thông tin Combo từ frontend.
- Backend phải xác thực khách hàng đã đăng nhập trước khi tạo yêu cầu.

---

## Out of Scope

- Xem danh sách Combo.
- Tạo mới hoặc chỉnh sửa Combo.
- Tạo mới hoặc chỉnh sửa Size.
- Tạo mới hoặc chỉnh sửa kiểu bó.
- Tạo mới hoặc chỉnh sửa Mockup.
- Tạo mới hoặc chỉnh sửa giấy gói.
- Tạo mới hoặc chỉnh sửa ruy băng.
- Quản lý Hoa.
- Thực hiện sinh mẫu hoa bằng AI.
- Chỉnh sửa kết quả mẫu hoa AI.
- Tải xuống mẫu hoa AI.

---

## Chi tiết Business Rules

| Rule ID | Tên rule | Danh mục | Phát biểu (Statement) | Điều kiện (When) | Hành vi (Then) | Ngoại lệ (Except) | Nguồn | Người sở hữu | Story liên quan | Trạng thái | Version | Ngày hiệu lực | Ghi chú / Link logic |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| [BR-068](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/b1017f3c-b500-4d66-83a3-46b3a488d37f) | Giới hạn ký tự | Khởi tạo mẫu hoa | Tên yêu cầu tạo mẫu hoa phải tuân thủ giới hạn ký tự được quy định. | Khách hàng nhập thông tin tại Step 1. | Hệ thống loại bỏ khoảng trắng ở đầu và cuối. Tên phải có độ dài từ 1 đến 100 ký tự. | Không chấp nhận null, chuỗi rỗng, chuỗi chỉ chứa khoảng trắng hoặc chuỗi vượt quá 100 ký tự. | Google Sheet rule source | Đức Bình | STORY-030 | Draft | v0 | 2026-08-14 | — |
| [BR-069](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/a925fb05-3c5e-4592-8e0c-22a516df58a2) | Validation thông tin bắt buộc | Khởi tạo mẫu hoa | Tên là thông tin bắt buộc của yêu cầu tạo mẫu hoa. | Khách hàng ấn chọn “Tiếp theo” tại Step 1. | Hệ thống loại bỏ khoảng trắng ở đầu và cuối của Tên, sau đó kiểm tra Tên có độ dài từ 1 đến 100 ký tự. | Không chấp nhận null, chuỗi rỗng, chuỗi chỉ chứa khoảng trắng hoặc chuỗi vượt quá 100 ký tự. Nếu Tên không hợp lệ, hệ thống không chuyển sang Step 2. | Google Sheet rule source | Đức Bình | STORY-030 | Draft | v0 | 2026-08-14 | — |
| [BR-070](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/e85f0e09-9b6a-49a8-b62d-ccc467c2bb94) | Combo nguồn và cấu hình Combo đã chọn theo Size của yêu cầu tạo mẫu hoa | Khởi tạo mẫu hoa | Mỗi yêu cầu tạo mẫu hoa được tạo từ chức năng “Tạo mẫu hoa” phải ghi nhận Combo nguồn ban đầu và cấu hình Combo đã chọn theo Size. | Khách hàng chọn “Tạo mẫu hoa” trên một Combo Card và tiếp tục chọn Size trong luồng khởi tạo mẫu hoa. | Hệ thống ghi nhận Combo được chọn từ Combo Card là Combo nguồn ban đầu. Sau khi khách hàng chọn Size và lựa chọn cấu hình Combo hợp lệ, hệ thống lưu cấu hình Combo đã chọn theo Size của yêu cầu tạo mẫu hoa. Cấu hình này có thể là Combo nguồn với số lượng được tăng lên hoặc Combo nguồn kết hợp thêm một hay nhiều Combo khác theo cấu hình của cửa hàng cho Size đã chọn. | Không cho phép hoàn thành yêu cầu tạo mẫu hoa nếu không xác định được Combo nguồn, Size, cấu hình Combo đã chọn theo Size hoặc nếu bất kỳ Combo nào trong cấu hình đó không còn khả dụng. | Product discussion 2026-09-11; STORY-030 | Đức Bình | STORY-030 | Draft | v0 | 2026-09-11 | Rule này thay thế cách hiểu cũ chỉ lưu một Combo nguồn đơn lẻ. STORY-030 vẫn cần giữ được Combo nguồn ban đầu để truy vết lựa chọn đầu tiên của khách hàng. |
| [BR-071](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/30761eaa-4df8-4e44-8d1e-efaba44782bf) | Một yêu cầu tạo mẫu hoa chỉ có một cấu hình Combo đã chọn theo Size | Khởi tạo mẫu hoa | Một yêu cầu tạo mẫu hoa chỉ được lưu một cấu hình Combo đã chọn theo Size tại một thời điểm. | Khách hàng chọn Size và lựa chọn cấu hình Combo trong luồng khởi tạo mẫu hoa. | Hệ thống chỉ lưu một cấu hình Combo đã chọn theo Size cho yêu cầu tạo mẫu hoa. Cấu hình này có thể gồm một Combo nguồn với số lượng tương ứng hoặc gồm Combo nguồn kết hợp thêm một hay nhiều Combo khác theo cấu hình Size của cửa hàng. | Không cho phép một yêu cầu tạo mẫu hoa đồng thời có nhiều cấu hình Combo đã chọn theo Size. Không cho phép lưu danh sách Combo kết hợp vượt quá giới hạn mà cửa hàng cấu hình cho Size đã chọn. | Product discussion 2026-09-11; STORY-030 | Đức Bình | STORY-030 | Draft | v0 | 2026-09-11 | Rule này cập nhật rule cũ “một yêu cầu tạo mẫu hoa chỉ có một Combo” để phù hợp với luồng Size có thể kết hợp nhiều Combo. |
| [BR-072](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/2371ded1-c7f4-42a2-98a4-1c4f71d8095c) | Quyền sở hữu yêu cầu tạo mẫu hoa | Khởi tạo mẫu hoa | Mỗi yêu cầu tạo mẫu hoa phải thuộc về một khách hàng cụ thể. | Yêu cầu tạo mẫu hoa được tạo. | Hệ thống phải liên kết yêu cầu với tài khoản khách hàng đang đăng nhập. | Không cho phép tồn tại yêu cầu tạo mẫu hoa không có chủ sở hữu. | Google Sheet rule source | Đức Bình | STORY-030 | Draft | v0 | 2026-08-14 | — |
| [BR-076](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/c5e7d260-7968-449d-b0a6-8de5602056d0) | Nguồn và điều kiện hiển thị Mockup | Khởi tạo mẫu hoa | Danh sách Mockup trong luồng khởi tạo mẫu hoa phải được lấy từ Core Database và chỉ hiển thị các Mockup còn khả dụng. | Khách hàng chuyển đến bước chọn Mockup trong luồng khởi tạo mẫu hoa. | Hệ thống lấy danh sách Mockup còn khả dụng từ Core Database, hiển thị dưới dạng Mockup Card, hỗ trợ xem hình ảnh lớn hơn và duy trì Mockup đã chọn khi khách hàng chuyển trang. | Mockup không còn khả dụng không được hiển thị và không được dùng để hoàn thành yêu cầu tạo mẫu hoa. | Product discussion 2026-09-11; STORY-030 | Đức Bình | STORY-030 | Draft | v0 | 2026-09-11 | Rule này bỏ phụ thuộc vào số thứ tự Step vì luồng khởi tạo mới có thêm Size, cấu hình Combo, kiểu bó, giấy gói và ruy băng trước hoặc sau bước Mockup. |
| [BR-077](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/c67cdf93-5604-438a-a247-e5195bf0e46d) | Một yêu cầu tạo mẫu hoa chỉ có một Mockup | Khởi tạo mẫu hoa | Một yêu cầu tạo mẫu hoa chỉ được liên kết với duy nhất một Mockup tại một thời điểm. | Khách hàng chọn Mockup trong luồng khởi tạo mẫu hoa. | Hệ thống ghi nhận Mockup được chọn cho yêu cầu tạo mẫu hoa. Khi khách hàng chọn Mockup khác, Mockup trước đó phải được bỏ chọn. | Không cho phép một yêu cầu tạo mẫu hoa đồng thời liên kết với nhiều Mockup. | Product discussion 2026-09-11; STORY-030 | Đức Bình | STORY-030 | Draft | v0 | 2026-09-11 | Rule này giữ nguyên ràng buộc một Mockup, chỉ cập nhật wording để không phụ thuộc vào Step cũ. |
| [BR-078](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/4e2249f4-842f-4159-a04c-295904f50ec3) | Mockup bắt buộc khi hoàn thành | Khởi tạo mẫu hoa | Yêu cầu tạo mẫu hoa phải có một Mockup được chọn trước khi được tạo. | Khách hàng chọn “Hoàn thành” trong luồng khởi tạo mẫu hoa. | Hệ thống kiểm tra Mockup đã chọn cùng với các dữ liệu bắt buộc khác của yêu cầu tạo mẫu hoa. | Nếu chưa có Mockup hoặc Mockup đã chọn không còn khả dụng, hệ thống không tạo yêu cầu tạo mẫu hoa và yêu cầu khách hàng chọn một Mockup hợp lệ. | Product discussion 2026-09-11; STORY-030 | Đức Bình | STORY-030 | Draft | v0 | 2026-09-11 | Mockup vẫn là dữ liệu bắt buộc trong luồng mới. |
| [BR-079](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/71fd8ff3-1fbd-4ab8-969b-1256d2e70618) | Nguồn dữ liệu Combo | Khởi tạo mẫu hoa | Combo sử dụng trong quy trình khởi tạo mẫu hoa phải được lấy và xác thực từ Core Database. | Hệ thống cần hiển thị hoặc kiểm tra Combo nguồn. | Backend phải sử dụng Core Database làm nguồn xác thực thông tin Combo. | Không sử dụng dữ liệu Combo lưu tạm ở frontend làm nguồn xác thực cuối cùng. | Google Sheet rule source | Đức Bình | STORY-030 | Draft | v0 | 2026-08-14 | — |
| [BR-080](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/25e0fc0c-5927-4bab-8a46-86ff656fe108) | Trạng thái ban đầu | Khởi tạo mẫu hoa | Yêu cầu tạo mẫu hoa mới được tạo phải có trạng thái ban đầu là Bản nháp. | Yêu cầu tạo mẫu hoa được tạo thành công. | Hệ thống gán trạng thái Bản nháp. | STORY-030 không tự động chuyển trạng thái sang trạng thái tạo AI. | Google Sheet rule source | Đức Bình | STORY-030 | Draft | v0 | 2026-08-14 | — |
| [BR-267](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/cd17ed46-c350-48b4-8b58-db7f94e22bdf) | Chọn Size khi khởi tạo mẫu hoa | Khởi tạo mẫu hoa | Khách hàng phải chọn một Size hợp lệ khi khởi tạo yêu cầu tạo mẫu hoa. | Khách hàng hoàn tất bước nhập Tên yêu cầu tạo mẫu hoa và chuyển sang bước chọn Size. | Hệ thống hiển thị danh sách Size còn khả dụng để khách hàng chọn. Tại một thời điểm, khách hàng chỉ được chọn một Size. Size đã chọn là cơ sở để hệ thống hiển thị cấu hình Combo hợp lệ ở bước tiếp theo. | Nếu khách hàng chưa chọn Size hoặc Size đã chọn không còn khả dụng, hệ thống không cho phép khách hàng hoàn thành yêu cầu tạo mẫu hoa. | Product discussion 2026-09-11; STORY-030 | Đức Bình | STORY-030 | Draft | v0 | 2026-09-11 | Ví dụ Size gồm `S`, `M`, `L`, nhưng danh sách Size thực tế lấy theo cấu hình hiện hành của cửa hàng. |
| [BR-268](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/73ba5a6b-3deb-48e4-a62c-efafae4cb33a) | Cấu hình Combo theo Size | Khởi tạo mẫu hoa | Lựa chọn cấu hình Combo sau khi chọn Size phải tuân theo cấu hình của cửa hàng cho Size đó. | Khách hàng đã chọn một Size trong luồng khởi tạo mẫu hoa. | Hệ thống hiển thị các lựa chọn cấu hình Combo hợp lệ theo Size đã chọn. Cửa hàng được cấu hình việc tăng số lượng Combo nguồn hoặc kết hợp thêm một hay nhiều Combo khác cho từng Size. Số lượng Combo được phép kết hợp cho mỗi Size do cửa hàng cấu hình. | Không cho phép khách hàng chọn cấu hình Combo không thuộc danh sách được cửa hàng cấu hình cho Size đã chọn. Không cho phép số lượng Combo kết hợp vượt quá giới hạn cấu hình của Size. | Product discussion 2026-09-11; STORY-030 | Đức Bình | STORY-030 | Draft | v0 | 2026-09-11 | Nếu khách hàng chọn tăng số lượng Combo nguồn, hệ thống lưu Combo nguồn kèm số lượng tương ứng. Nếu khách hàng chọn kết hợp Combo khác, hệ thống lưu cấu hình Combo đã chọn theo Size gồm Combo nguồn và các Combo kết hợp đã chọn. |
| [BR-269](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/baa36bab-ae33-4c6c-827d-f7f775bcaede) | Chọn kiểu bó khi khởi tạo mẫu hoa | Khởi tạo mẫu hoa | Khách hàng phải chọn một kiểu bó hợp lệ khi khởi tạo yêu cầu tạo mẫu hoa. | Khách hàng đã chọn cấu hình Combo hợp lệ theo Size và chuyển sang bước chọn kiểu bó. | Hệ thống hiển thị danh sách kiểu bó còn khả dụng kèm hình ảnh. Tại một thời điểm, khách hàng chỉ được chọn một kiểu bó. Kiểu bó đã chọn được lưu vào yêu cầu tạo mẫu hoa. | Nếu khách hàng chưa chọn kiểu bó hoặc kiểu bó đã chọn không còn khả dụng, hệ thống không cho phép hoàn thành yêu cầu tạo mẫu hoa. | Product discussion 2026-09-11; STORY-030 | Đức Bình | STORY-030 | Draft | v0 | 2026-09-11 | Nguồn dữ liệu kiểu bó cần được xác nhận hoặc cấu hình trong Core Database theo thiết kế hệ thống. |
| [BR-270](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/922030ff-fc05-4b71-8059-2f0eb41dca4c) | Chọn giấy gói khi khởi tạo mẫu hoa | Khởi tạo mẫu hoa | Khách hàng phải chọn một giấy gói hợp lệ khi khởi tạo yêu cầu tạo mẫu hoa. | Khách hàng chuyển đến bước chọn giấy gói trong luồng khởi tạo mẫu hoa. | Hệ thống hiển thị danh sách giấy gói còn khả dụng. Tại một thời điểm, khách hàng chỉ được chọn một giấy gói. Giấy gói đã chọn được lưu vào yêu cầu tạo mẫu hoa. | Nếu khách hàng chưa chọn giấy gói hoặc giấy gói đã chọn không còn khả dụng, hệ thống không cho phép hoàn thành yêu cầu tạo mẫu hoa. | Product discussion 2026-09-11; STORY-030 | Đức Bình | STORY-030 | Draft | v0 | 2026-09-11 | Nguồn dữ liệu giấy gói cần được xác nhận hoặc cấu hình trong Core Database theo thiết kế hệ thống. |
| [BR-271](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/0c3e391d-29a6-4747-bd28-a0d056f2e2e1) | Chọn ruy băng khi khởi tạo mẫu hoa | Khởi tạo mẫu hoa | Khách hàng phải chọn một ruy băng hợp lệ khi khởi tạo yêu cầu tạo mẫu hoa. | Khách hàng chuyển đến bước chọn ruy băng trong luồng khởi tạo mẫu hoa. | Hệ thống hiển thị danh sách ruy băng còn khả dụng. Tại một thời điểm, khách hàng chỉ được chọn một ruy băng. Ruy băng đã chọn được lưu vào yêu cầu tạo mẫu hoa. | Nếu khách hàng chưa chọn ruy băng hoặc ruy băng đã chọn không còn khả dụng, hệ thống không cho phép hoàn thành yêu cầu tạo mẫu hoa. | Product discussion 2026-09-11; STORY-030 | Đức Bình | STORY-030 | Draft | v0 | 2026-09-11 | Nguồn dữ liệu ruy băng cần được xác nhận hoặc cấu hình trong Core Database theo thiết kế hệ thống. |
