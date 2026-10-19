<div align="center">

# 🚀 Dual Backend REST API: .NET 10 & Node.js Express

**Side-by-side comparison and reference RESTful API implementations in ASP.NET Core (.NET 10) and Node.js (Express).**  
*Features clean architecture, thread safety, robust validation, pagination, full-text search, and automated OpenAPI documentation.*

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![CI Build](https://github.com/Tuannll/dual-backend-rest-api/actions/workflows/ci.yml/badge.svg)](https://github.com/Tuannll/dual-backend-rest-api/actions/workflows/ci.yml)
[![Node.js](https://img.shields.io/badge/Node.js-18+-339933?logo=nodedotjs&logoColor=white)](https://nodejs.org/)
[![Express](https://img.shields.io/badge/Express-4.21-000000?logo=express&logoColor=white)](https://expressjs.com/)
[![Scalar](https://img.shields.io/badge/API_Docs-Scalar-0A85EA?logo=openapiinitiative&logoColor=white)](https://scalar.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

**English** | [Tiếng Việt](README.vi.md)

**[✨ Features](#features) • [📁 Structure](#structure) • [🚀 Quick Start](#quick-start) • [📡 Endpoints](#endpoints) • [🧪 Testing](#testing) • [🛡️ Highlights](#highlights)**

</div>

---

<a id="features"></a>
## ✨ Architecture Comparison

| Capability | 🟣 ASP.NET Core (.NET 10) | 🟢 Node.js Express |
| :--- | :--- | :--- |
| **Framework** | C# 13, .NET 10 Web API | JavaScript, Express 4.x |
| **API Docs** | Embedded **Scalar API Reference** | Standard JSON OpenAPI / RESTful |
| **Response Format** | Standardized `ApiResponse<T>` envelope | Matching `{ isSuccess, message, statusCode, data }` envelope |
| **Thread Safety** | `ConcurrentDictionary<T>` + `Interlocked.Increment` | Non-blocking Event Loop |
| **Pagination & Filtering** | Overflow-protected integer slicing, case-insensitive search | Slice boundary handling, case-insensitive query matching |
| **Validation** | DataAnnotations + Dynamic Custom Attribute (`[ValidYearOfBirth]`) | Middleware type check, whitespace trimming |
| **Error Handling** | Global Exception Middleware + Model State Factory | Global Express Error Handler middleware |

---

<a id="structure"></a>
## 📁 Project Structure

```text
├── Controllers/                 # ASP.NET RESTful Controllers
│   ├── HelloController.cs       # Greeting & echo endpoints
│   └── StudentController.cs     # Student CRUD endpoints
├── Dtos/                        # Data Transfer Objects & Validation Attributes
│   └── StudentDtos.cs           # Student creation, update, and read DTOs
├── Models/                      # Entities & Common response envelopes
│   ├── Common/ApiResponse.cs    # Generic ApiResponse<T> & PagedResult<T>
│   └── Student.cs               # Student domain model
├── Services/                    # Business logic & thread-safe in-memory store
│   ├── IStudentService.cs       # Service contract
│   └── StudentService.cs        # In-memory implementation
├── Properties/launchSettings.json# Local dev ports (5000/5001)
├── Program.cs                   # App bootstrap & DI container
├── DemoWebApi.csproj            # .NET 10 project file
├── DemoWebApi.http              # HTTP Client test scenarios
│
└── node-express-api/            # Equivalent Node.js Express implementation
    ├── src/
    │   ├── app.js               # Express app bootstrap (port 3000)
    │   ├── data/students.js     # Seed data
    │   ├── routes/hello.js      # Hello router
    │   ├── routes/students.js   # Student CRUD router
    │   └── utils/response.js    # Envelope helper functions
    └── package.json
```

---

<a id="quick-start"></a>
## 🚀 Quick Start

### 1. Run ASP.NET Core (.NET 10)

```bash
# Clone and enter directory
git clone https://github.com/Tuannll/dual-backend-rest-api.git
cd dual-backend-rest-api

# Restore dependencies & run
dotnet restore
dotnet run
```

- 🌐 **Base URL:** `http://localhost:5000` or `https://localhost:5001`
- 📑 **Scalar Docs:** `http://localhost:5000/scalar/v1`

---

### 2. Run Node.js Express

```bash
# Navigate to Node.js backend
cd node-express-api

# Install dependencies & start
npm install
npm run dev
```

- 🌐 **Base URL:** `http://localhost:3000`

---

<a id="endpoints"></a>
## 📡 API Endpoints

> [!NOTE]
> Both backend implementations return an identical envelope structure, allowing frontend applications to interchange backends without client-side modifications.

```json
{
  "isSuccess": true,
  "message": "Operation successful",
  "statusCode": 200,
  "data": { ... }
}
```

### 1. Hello Endpoints

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/hello` | Returns default greeting |
| `GET` | `/api/hello/{name}` | Personalized greeting by name |
| `POST` | `/api/hello` | Echoes request body with timestamp |

### 2. Student Endpoints

| Method | Endpoint | Query / Body | Description |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/students` | `?keyword=...&pageIndex=1&pageSize=10` | Filtered & paginated student list |
| `GET` | `/api/students/{id}` | `id` (integer > 0) | Retrieve student by ID |
| `POST` | `/api/students` | `{ "fullName": "...", "yearOfBirth": 2004 }` | Create new student |
| `PUT` | `/api/students/{id}` | `{ "fullName": "...", "yearOfBirth": 2004 }` | Update student details |
| `DELETE` | `/api/students/{id}` | `id` (integer > 0) | Remove student |

---

<a id="testing"></a>
## 🧪 Testing

### Via `.http` File (VS Code)
1. Open `DemoWebApi.http` in VS Code with the **REST Client** extension.
2. Click **Send Request** directly above each endpoint to execute positive and validation failure cases.

### Via cURL

```bash
# Get page 1
curl -X GET "http://localhost:5000/api/students?pageIndex=1&pageSize=10"

# Create student
curl -X POST "http://localhost:5000/api/students" \
  -H "Content-Type: application/json" \
  -d '{"fullName": "Lê Bảo Ngọc", "yearOfBirth": 2005}'
```

---

<a id="highlights"></a>
## 🛡️ Technical Highlights

1. **Thread-Safe Architecture**: Combines `ConcurrentDictionary` and `Interlocked.Increment` for atomic counter management under concurrent loads.
2. **Dynamic Validation**: `[ValidYearOfBirth]` evaluates boundaries dynamically against `DateTime.UtcNow.Year`.
3. **Strict Whitespace Filtering**: Disallows empty names or whitespace strings through regex validation and setter trimming.
4. **Pagination Overflow Protection**: Handles integer overflow edge cases safely when large indices are supplied.

---

<a id="license"></a>
## 📄 License

Distributed under the [MIT License](LICENSE).
