# SIGNALR CONTRACT - MEETLY

## Hub

```text
/hubs/events
```

## Authentication

Sử dụng:

```text
Bearer accessToken
```

Token được lấy từ:

```http
POST /api/v1/events/{shortCode}/participants/access
```

## Connection

FE sử dụng SignalR client:

```ts
const connection = new HubConnectionBuilder()
  .withUrl(API_URL + "/hubs/events", {
    accessTokenFactory: () => accessToken
  })
  .withAutomaticReconnect()
  .build();
```

---

# SERVER -> CLIENT EVENTS

## 1. `HeatmapUpdated`

**Trigger:**

```http
PUT /api/v1/events/{shortCode}/participants/me/availability
```

Sau khi API cập nhật thời gian rảnh thành công, BE broadcast event `HeatmapUpdated` tới tất cả client đang kết nối trong cùng event.

**Payload:**

```ts
{
  shortCode: string,
  revision: number,
  heatmapGrid: {
    [datetime: string]: string[]
  }
}
```

---

## 2. `EventFinalized`

**Trigger:**

```http
POST /api/v1/events/{shortCode}/finalize
```

Sau khi Admin chốt lịch thành công, BE broadcast event `EventFinalized` tới tất cả client đang kết nối trong cùng event.

**Payload:**

```ts
{
  shortCode: string,
  isFinalized: true,
  status: 2,
  revision: number,
  finalSchedule: {
    specificDate: string | null,
    dayOfWeek: number | null,
    startTime: string,
    endTime: string
  }
}
```

---

## 3. `EventUpdated`

**Trigger:**

```http
PUT /api/v1/events/{shortCode}
```

Sau khi Admin chỉnh sửa thông tin sự kiện thành công, BE broadcast event `EventUpdated` tới tất cả client đang kết nối trong cùng event.

**Payload:**

```ts
{
  shortCode: string,
  title: string,
  eventType: number,
  availableDates: string[],
  availableWeekdays: number[],
  dailyStartTime: string,
  dailyEndTime: string,
  revision: number
}
```
