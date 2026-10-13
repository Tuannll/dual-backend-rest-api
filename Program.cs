using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using DemoWebApi.Models.Common;
using DemoWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register application services
builder.Services.AddSingleton<IStudentService, StudentService>();

// Configure validation error response format
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var firstError = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .FirstOrDefault(msg => !string.IsNullOrWhiteSpace(msg))
            ?? "Dữ liệu đầu vào không hợp lệ.";

        var response = ApiResponse<object>.Failure(firstError, 400);
        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Global Exception Handler
app.UseExceptionHandler(exceptionApp =>
{
    exceptionApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var response = ApiResponse<object>.Failure("Đã xảy ra lỗi máy chủ nội bộ.", 500);
        await context.Response.WriteAsJsonAsync(response);
    });
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();
