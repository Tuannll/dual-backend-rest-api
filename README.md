<div align="center">

# 🚀 Dual Backend REST API: .NET 10 & Node.js Express

**Bộ khung mẫu chuẩn RESTful API so sánh song song giữa ASP.NET Core (.NET 10) và Node.js (Express).**  
*Được thiết kế chuẩn kiến trúc, thread-safe, validation chặt chẽ, phân trang, tìm kiếm và tài liệu hóa tự động.*

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![CI Build](https://github.com/Tuannll/dual-backend-rest-api/actions/workflows/ci.yml/badge.svg?style=flat-square)](https://github.com/Tuannll/dual-backend-rest-api/actions/workflows/ci.yml)
[![Node.js](https://img.shields.io/badge/Node.js-18+-339933?style=flat-square&logo=nodedotjs&logoColor=white)](https://nodejs.org/)
[![Express](https://img.shields.io/badge/Express-4.21-000000?style=flat-square&logo=express&logoColor=white)](https://expressjs.com/)
[![Scalar](https://img.shields.io/badge/API_Docs-Scalar-0A85EA?style=flat-square&logo=openapiinitiative&logoColor=white)](https://scalar.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](LICENSE)

<br />

<p align="center">
  <img src="./assets/scalar.png" alt="Scalar API Reference Demo" width="850" />
</p>

[Tính năng](#-tính-năng-nổi-bật) • [Cấu trúc](#-cấu-trúc-thư-mục) • [Yêu cầu](#-yêu-cầu-môi-trường) • [Hướng dẫn chạy](#-hướng-dẫn-chạy) • [Danh sách API](#-danh-sách-api) • [Kiểm thử](#-kiểm-thử-api) • [Kỹ thuật nổi bật](#-điểm-kỹ-thuật-nổi-bật) • [Giấy phép](#-giấy-phép)

---

</div>

## 🌟 Tính năng nổi bật

| Hạng mục | 🟣 ASP.NET Core (.NET 10) | 🟢 Node.js Express |
| :--- | :--- | :--- |
| **Framework** | C# 13, .NET 10 Web API | JavaScript, Express 4.x |
| **API Docs** | Tích hợp **Scalar API Reference** hiện đại | Chuẩn JSON RESTful |
| **Response Format** | Chuẩn hóa Envelope `ApiResponse<T>` | Đồng bộ Envelope `{ isSuccess, message, statusCode, data }` |
| **Thread Safety** | `ConcurrentDictionary<T>` + `Interlocked.Increment` | Non-blocking Event Loop + an toàn Call Stack |
| **Phân trang & Lọc** | Phân trang an toàn, chống tràn số nguyên, tìm kiếm không phân biệt hoa thường | Phân trang, slice an toàn, tìm kiếm case-insensitive |
| **Validation** | DataAnnotations + Custom Dynamic Validator (`[ValidYearOfBirth]`) | Middleware kiểm tra kiểu dữ liệu, chặn khoảng trắng |
| **Xử lý lỗi** | Global Exception Handler + Model State Factory chuẩn | Error Handling Response đồng bộ |

---

## 📂 Cấu trúc thư mục

```text
├── Controllers/                 # ASP.NET Controllers (RESTful)
│   ├── HelloController.cs       # Endpoint chào hỏi & echo
│   └── StudentController.cs     # Endpoint CRUD sinh viên
├── Dtos/                        # Data Transfer Objects & Custom Validators
│   └── StudentDtos.cs           # DTO tạo, cập nhật và hiển thị sinh viên
├── Models/                      # Entity & Common response models
│   ├── Common/ApiResponse.cs    # Generic ApiResponse<T> & PagedResult<T>
│   └── Student.cs               # Entity Sinh viên
├── Services/                    # Business Logic & In-Memory Data Access
│   ├── IStudentService.cs       # Interface trừu tượng
│   └── StudentService.cs        # Thread-safe in-memory repository
├── Properties/launchSettings.json# Cấu hình cổng & môi trường (port 5000/5001)
├── Program.cs                   # Điểm khởi chạy app & DI container
├── DemoWebApi.csproj            # Cấu hình project .NET 10
├── DemoWebApi.http              # Kịch bản kiểm thử HTTP trực tiếp
├── .gitignore                   # Loại trừ file rác, bin, obj, node_modules
│
└── node-express-api/            # Phiên bản tương đương trên Node.js
    ├── src/
    │   ├── app.js               # Khởi chạy Express server (port 3000)
    │   ├── data/students.js     # Mock data ban đầu
    │   ├── routes/hello.js      # Router hello
    │   ├── routes/students.js   # Router CRUD sinh viên
    │   └── utils/response.js    # Helper chuẩn hóa format ApiResponse
    └── package.json             # Cấu hình npm
```

---

## ⚡ Yêu cầu môi trường

- [.NET 10 SDK](https://dotnet.microsoft.com/download) trở lên.
- [Node.js](https://nodejs.org/) (phiên bản v18 trở lên) & npm.
- IDE đề xuất: [Visual Studio Code](https://code.visualstudio.com/) (kèm extension *REST Client* hoặc *C# Dev Kit*) hoặc Visual Studio 2022/2025.

---

## 🚀 Hướng dẫn chạy

### 1. Chạy ASP.NET Core API (.NET 10)

```bash
# Clone repository & di chuyển vào thư mục dự án
git clone https://github.com/Tuannll/dual-backend-rest-api.git
cd dual-backend-rest-api

# Restore & Build dự án
dotnet restore
dotnet build

# Chạy ứng dụng
dotnet run
```

- 🌐 **API Base URL:** `http://localhost:5000` hoặc `https://localhost:5001`
- 📑 **Scalar API Docs:** Truy cập trình duyệt tại `http://localhost:5000/scalar/v1`

---

### 2. Chạy Node.js Express API

```bash
# Di chuyển vào thư mục node-express-api
cd node-express-api

# Cài đặt thư viện phụ thuộc
npm install

# Chạy ứng dụng ở chế độ dev (tự reload khi đổi code)
npm run dev

# Hoặc chạy thông thường:
npm start
```

- 🌐 **API Base URL:** `http://localhost:3000`

---

## 📖 Danh sách API

> [!NOTE]
> Cả hai backend (.NET 10 và Node.js Express) đều sử dụng chung cấu trúc phản hồi `ApiResponse` đồng bộ. Phía Client / Frontend có thể chuyển đổi linh hoạt giữa hai nền tảng mà không cần thay đổi logic xử lý dữ liệu.

Tất cả phản hồi đều tuân theo chuẩn Envelope thống nhất:

```json
{
  "isSuccess": true,
  "message": "Thao tác thành công",
  "statusCode": 200,
  "data": { ... }
}
```

### 🔹 1. Hello Endpoints

| Method | Endpoint | Mô tả |
| :--- | :--- | :--- |
| `GET` | `/api/hello` | Trả thông điệp chào hỏi mặc định |
| `GET` | `/api/hello/{name}` | Chào hỏi cá nhân hóa theo tên |
| `POST` | `/api/hello` | Phản hồi thông điệp kèm timestamp |

---

### 🔹 2. Student Endpoints (Quản lý sinh viên)

| Method | Endpoint | Tham số Query / Body | Mô tả |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/students` | `?keyword=...&pageIndex=1&pageSize=10` | Danh sách sinh viên có tìm kiếm & phân trang |
| `GET` | `/api/students/{id}` | `id` (số nguyên > 0) | Chi tiết sinh viên theo ID |
| `POST` | `/api/students` | `{ "fullName": "...", "yearOfBirth": 2004 }` | Thêm sinh viên mới (tự động tính tuổi) |
| `PUT` | `/api/students/{id}` | `{ "fullName": "...", "yearOfBirth": 2004 }` | Cập nhật thông tin sinh viên |
| `DELETE` | `/api/students/{id}` | `id` (số nguyên > 0) | Xóa sinh viên |

---

## 🧪 Kiểm thử API

### Dùng file `DemoWebApi.http` (VS Code)
Dự án tích hợp sẵn file `DemoWebApi.http` với 11 kịch bản kiểm thử:
1. Mở file `DemoWebApi.http` trong VS Code.
2. Cài extension **REST Client**.
3. Nhấn **Send Request** trên từng mục để kiểm thử tức thì (hỗ trợ kiểm thử cả case dữ liệu hợp lệ và validation lỗi).

### Dùng cURL mẫu

```bash
# 1. Lấy danh sách sinh viên trang 1
curl -X GET "http://localhost:5000/api/students?pageIndex=1&pageSize=10"

# 2. Thêm mới sinh viên
curl -X POST "http://localhost:5000/api/students" \
  -H "Content-Type: application/json" \
  -d '{"fullName": "Lê Bảo Ngọc", "yearOfBirth": 2005}'

# 3. Test validation lỗi (tên toàn khoảng trắng)
curl -X POST "http://localhost:5000/api/students" \
  -H "Content-Type: application/json" \
  -d '{"fullName": "   ", "yearOfBirth": 2005}'
```

---

## 🛡️ Điểm kỹ thuật nổi bật

1. **Kiến trúc Thread-Safe:** Sử dụng `ConcurrentDictionary` kết hợp `Interlocked.Increment` đảm bảo an toàn đa luồng, loại bỏ nguy cơ race condition khi xử lý đồng thời dưới tải cao.
2. **Dynamic Validation:** Attribute tùy biến `[ValidYearOfBirth]` tính toán giới hạn dựa trên `DateTime.UtcNow.Year` động theo thời gian thực, đảm bảo tính hợp lệ lâu dài.
3. **Chặn khoảng trắng toàn diện:** Loại trừ triệt để tên rỗng hoặc chuỗi toàn khoảng trắng thông qua Regex kết hợp setter trim tự động.
4. **Bảo vệ phân trang chống tràn số:** Xử lý an toàn hiện tượng integer overflow khi nhận `pageIndex` hoặc `pageSize` vượt ngưỡng số nguyên (`int.MaxValue`).
5. **Giao thức đồng bộ đa nền tảng:** Cả hai phiên bản .NET và Node.js dùng chung cấu trúc phản hồi chuẩn, giúp tầng Frontend thay thế hoặc hoán đổi backend liền mạch.

---

## 📝 Giấy phép

Dự án phát hành theo giấy phép [MIT License](LICENSE).
Tự do clone, học tập và phát triển!
