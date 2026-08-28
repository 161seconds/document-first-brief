# AI Customize Database Schema - HTM_Flourist_AI

> Document First Project: hoa-theo-mua-ai-customize  
> Date: 2026-08-27

## Phạm vi và nguyên tắc

- Flow tạo hoa của STORY-030 và STORY-033 dùng chung một API đồng bộ.
- Không có bảng `flower_requests` hoặc `flower_ai_jobs`.
- API nhận input, gọi AI (kèm retry), trả kết quả; chỉ khi thành công mới lưu `generated_flowers` và `client_histories`.
- `client_histories.input` là JSON request gốc của lần tạo thành công; đây là history, không phải bảng trạng thái/tác vụ tạm.
- Card giữ logic hiện tại: cấu hình dùng bảng `Config`, còn quan hệ Card–Flower được suy ra từ `client_histories`, không dùng `generated_cards.source_flower_id`.

## Enums

```sql
Enum platform_type { zalo facebook instagram }
Enum generation_type { flower card post }
Enum system_prompt_types { flower card post }
Enum form_type { go_may calligraphy }
```

## Tables

```sql
Table system_prompts {
  id uuid [pk]
  type system_prompt_types
  content text
}

Table platform_standards {
  id uuid [pk]
  platform platform_type
  description text
}

Table Config {
  id uuid [pk]
  key varchar
  value jsonb
  is_public boolean
  group varchar
  kind varchar
  user_id uuid
  created_at timestamp
  updated_at timestamp
  is_deleted boolean
}

Table products {
  id uuid [pk]
}

Table card_templates {
  id uuid [pk]
  name varchar
  description text
  character_limit int
  image_url varchar
  metadata json
  created_at timestamp
  updated_at timestamp
  is_deleted boolean
}

Table base_posts {
  id uuid [pk]
  content text
  image_url varchar
}

Table generated_posts {
  id uuid [pk]
  content text
  image_url varchar
  user_id uuid
  created_at timestamp
}

Table generated_flowers {
  id uuid [pk]
  image_url varchar
  user_id uuid
  created_at timestamp
}

Table generated_cards {
  id uuid [pk]
  content text
  image_url varchar
  user_id uuid
  created_at timestamp
  form_type form_type
  size_key varchar
  sender_name varchar
  receiver_name varchar
  message_content text
  attached_image_url varchar
  size_name varchar
  size_width decimal
  size_height decimal
  size_base_price decimal
  size_max_words int
  word_count int
  word_config_snapshot json
  is_confirmed boolean
  base_price decimal
  extra_price decimal
  total_price decimal
}

Table post_histories {
  id uuid [pk]
  input text
  base_id uuid
  output_id uuid
  system_prompt_id uuid
  metadata json
  created_at timestamp
}

Table mockup {
  id uuid [pk]
  name varchar
  description text
  image_url varchar
  is_active boolean
  is_deleted boolean
  created_at timestamp
  updated_at timestamp
}

Table client_histories {
  id uuid [pk]
  user_id uuid
  input text
  type generation_type
  system_prompt_id uuid
  metadata json
  created_at timestamp
  base_id uuid
  output_id uuid
}
```

## Quan hệ

`client_histories` dùng các tham chiếu đa hình:

| type | base_id | output_id | metadata |
|---|---|---|---|
| flower | `products.id` | `generated_flowers.id` | Snapshot Mockup |
| card (hoa thường) | `products.id` | `generated_cards.id` | Snapshot card template |
| card (hoa AI) | `generated_flowers.id` | `generated_cards.id` | Snapshot card template |
| post | `base_posts.id` | `generated_posts.id` | Theo loại bài viết |

```sql
Ref: post_histories.system_prompt_id > system_prompts.id
Ref: client_histories.system_prompt_id > system_prompts.id
Ref: client_histories.base_id > products.id
Ref: client_histories.base_id > generated_flowers.id
Ref: client_histories.output_id > generated_flowers.id
Ref: client_histories.output_id > generated_cards.id
Ref: client_histories.output_id > generated_posts.id
```

`mockup` được truyền vào API tạo hoa và snapshot trong `client_histories.metadata`; không cần FK riêng cho flow tạo hoa.
