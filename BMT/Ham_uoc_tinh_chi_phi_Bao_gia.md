# HÀM ƯỚC TÍNH CHI PHÍ - TRANG BÁO GIÁ

Công thức tính: **01 Loại hình → 02 Diện tích → 03 Ngân sách → 04 Gói → 05 Ước tính**

## 1. Đầu vào

| Bước | Trường | Kiểu | Ràng buộc |
|---|---|---|---|
| 01 Loại hình | `loaiHinh` | enum | `nha_o` · `van_phong` · `tham_my_vien_showroom` · `nha_hang_khach_san` |
| 02 Diện tích | `dienTich` | number (m² sàn) | 10 ≤ dienTich ≤ 50.000 |
| 03 Ngân sách | `nganSach` | number (đ) \| null | 0 ≤ nganSach ≤ 1.000 tỷ |
| 04 Gói | `goi` | enum | `xay_dung_tron_goi` · `thiet_ke_kien_truc_noi_that` · `thi_cong_xay_dung` · `cai_tao_sua_chua` |

## 2. Công thức ước tính

**Khoảng giá thị trường của tổ hợp (đ/m² sàn, bảng mục 3):**

```
[giaMin, giaMax] = BANG_GIA_THI_TRUONG[loaiHinh][goi]
```

**Khoảng ước tính (đ):**

```
min = dienTich × giaMin
max = dienTich × giaMax
```

**Đơn giá hiển thị (đ/m²):**

```
donGia = (giaMin + giaMax) / 2
```

**Làm tròn:**

- min, max, donGia làm tròn đến 1.000 đ; bước làm tròn của min, max đổi được qua `tuyChon.lamTron` (đơn vị đ).

**Hiển thị (màn 05):**

```
hienThi.khoang = "{min}đ - {max}đ"
hienThi.donGia = "~ {donGia} đ/m²"
hienThi.moTa   = "~ {donGia} đ/m² - {Tên loại hình} {dienTich} m² - Bao gồm {tên gói viết thường}"
```

- Số tiền: nhóm nghìn bằng dấu chấm, hậu tố "đ" liền số; diện tích giữ nguyên như khách nhập, phần lẻ dùng dấu phẩy.

## 3. Bảng giá thị trường (đ/m² sàn)

| Loại hình | Gói | Khoảng thị trường | Đơn giá hiển thị |
|---|---|---|---|
| **Nhà ở** | Xây dựng trọn gói | 5.200.000 - 6.500.000 | 5.850.000 |
| | Thiết kế KT & NT | 270.000 - 300.000 | 285.000 |
| | Thi công xây dựng | 3.350.000 - 4.300.000 | 3.825.000 |
| | Cải tạo & sửa chữa | 2.000.000 - 5.000.000 | 3.500.000 |
| **Văn phòng** | Xây dựng trọn gói | 6.750.000 - 7.150.000 | 6.950.000 |
| | Thiết kế KT & NT | 100.000 - 260.000 | 180.000 |
| | Thi công xây dựng | 3.000.000 - 4.000.000 | 3.500.000 |
| | Cải tạo & sửa chữa | 2.000.000 - 6.000.000 | 4.000.000 |
| **Thẩm mỹ viện, showroom** | Xây dựng trọn gói | 5.000.000 - 10.000.000 | 7.500.000 |
| | Thiết kế KT & NT | 140.000 - 350.000 | 245.000 |
| | Thi công xây dựng | 3.000.000 - 4.000.000 | 3.500.000 |
| | Cải tạo & sửa chữa | 2.000.000 - 6.000.000 | 4.000.000 |
| **Nhà hàng, khách sạn** | Xây dựng trọn gói | 5.200.000 - 7.500.000 | 6.350.000 |
| | Thiết kế KT & NT | 250.000 - 300.000 | 275.000 |
| | Thi công xây dựng | 3.400.000 - 4.200.000 | 3.800.000 |
| | Cải tạo & sửa chữa | 5.000.000 - 8.000.000 | 6.500.000 |

### Quy ước lấy giá

- Khoảng thị trường = mức phổ biến (gói tiêu chuẩn → nâng cao) trong báo giá công khai 2026 của các đơn vị thiết kế - thi công; không tính gói cao cấp/luxury.
- Giá tính trên m² sàn khách nhập; hệ số quy đổi móng, mái, sân để bước tư vấn xử lý.
- Xây dựng trọn gói = phần thô + hoàn thiện, chưa gồm nội thất rời. Thẩm mỹ viện/showroom lấy tổng của thô + hoàn thiện vì thị trường báo giá tách phần.
- Thiết kế kiến trúc & nội thất = phí thiết kế trọn bộ. Thi công xây dựng = phần thô + nhân công hoàn thiện. Cải tạo & sửa chữa = hoàn thiện lại mặt bằng hiện hữu (fit-out).
- Bảng giá cập nhật định kỳ theo báo giá thị trường; chỉ đổi số trong bảng (hoặc truyền qua `tuyChon.bangGia`), công thức giữ nguyên.

## 4. Ngân sách - so sánh và gợi ý gói

Ngân sách không làm đổi khoảng ước tính; hàm so ngân sách với [min, max] của gói đã chọn:

```
trangThai = thieu     nếu nganSach < min
          = phu_hop   nếu min ≤ nganSach ≤ max
          = du        nếu nganSach > max

chenhLech = nganSach − min   (thieu)
          | 0                (phu_hop)
          | nganSach − max   (du)

tyLe = chenhLech / min   (thieu)
     | 0                 (phu_hop)
     | chenhLech / max   (du)

trenM2 = nganSach / dienTich
```

- `tyLe` làm tròn 3 số lẻ; `trenM2` làm tròn đến đồng.

**Thông điệp:**

```
thieu    → "Ngân sách {nganSach} thấp hơn mức ước tính tối thiểu {min} khoảng {|tyLe|×100}%."
phu_hop  → "Ngân sách {nganSach} nằm trong khoảng ước tính {min} - {max}."
du       → "Ngân sách {nganSach} cao hơn mức ước tính tối đa {max} khoảng {tyLe×100}%."
```
