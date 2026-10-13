const express = require("express");
const students = require("../data/students");
const { sendSuccess, sendFailure } = require("../utils/response");

const router = express.Router();

router.get("/", (req, res) => {
  const currentYear = new Date().getFullYear();
  const keyword = req.query.keyword ? String(req.query.keyword).trim().toLowerCase() : null;
  const pageIndex = Math.max(1, parseInt(req.query.pageIndex, 10) || 1);
  const pageSize = Math.min(100, Math.max(1, parseInt(req.query.pageSize, 10) || 10));

  let filtered = students;
  if (keyword) {
    filtered = filtered.filter((s) => s.fullName.toLowerCase().includes(keyword));
  }

  const totalCount = filtered.length;
  const totalPages = Math.ceil(totalCount / pageSize);
  const items = pageIndex > totalPages && totalCount > 0
    ? []
    : filtered
        .slice((pageIndex - 1) * pageSize, pageIndex * pageSize)
        .map((s) => ({
          ...s,
          age: Math.max(0, currentYear - s.yearOfBirth)
        }));

  return sendSuccess(
    res,
    {
      items,
      totalCount,
      pageIndex,
      pageSize,
      totalPages
    },
    "Lấy danh sách sinh viên thành công"
  );
});

router.get("/:id", (req, res) => {
  const id = Number(req.params.id);

  if (!Number.isInteger(id) || id <= 0) {
    return sendFailure(res, "ID sinh viên phải lớn hơn 0", 400);
  }

  const student = students.find((item) => item.id === id);
  if (!student) {
    return sendFailure(res, `Không tìm thấy sinh viên với ID ${id}`, 404);
  }

  const currentYear = new Date().getFullYear();
  return sendSuccess(
    res,
    {
      ...student,
      age: Math.max(0, currentYear - student.yearOfBirth)
    },
    "Lấy thông tin sinh viên thành công"
  );
});

router.post("/", (req, res) => {
  const fullName = req.body?.fullName?.trim();
  const yearOfBirth = Number(req.body?.yearOfBirth);
  const currentYear = new Date().getFullYear();

  if (!fullName || fullName.length < 2 || fullName.length > 100) {
    return sendFailure(res, "Họ và tên phải từ 2 đến 100 ký tự và không được để trống.", 400);
  }

  if (!Number.isInteger(yearOfBirth) || yearOfBirth < 1900 || yearOfBirth > currentYear) {
    return sendFailure(res, `Năm sinh phải nằm trong khoảng từ 1900 đến ${currentYear}.`, 400);
  }

  const nextId = students.reduce((max, item) => Math.max(max, item.id), 0) + 1;
  const student = {
    id: nextId,
    fullName,
    yearOfBirth
  };

  students.push(student);

  return sendSuccess(
    res,
    {
      ...student,
      age: Math.max(0, currentYear - student.yearOfBirth)
    },
    "Tạo sinh viên thành công",
    201
  );
});

router.put("/:id", (req, res) => {
  const id = Number(req.params.id);

  if (!Number.isInteger(id) || id <= 0) {
    return sendFailure(res, "ID sinh viên phải lớn hơn 0", 400);
  }

  const student = students.find((item) => item.id === id);
  if (!student) {
    return sendFailure(res, `Không tìm thấy sinh viên với ID ${id}`, 404);
  }

  const rawFullName = req.body?.fullName;
  const rawYearOfBirth = req.body?.yearOfBirth;
  const currentYear = new Date().getFullYear();

  if (rawFullName !== undefined) {
    const trimmed = typeof rawFullName === "string" ? rawFullName.trim() : "";
    if (trimmed.length < 2 || trimmed.length > 100) {
      return sendFailure(res, "Họ và tên phải từ 2 đến 100 ký tự và không được để trống.", 400);
    }
    student.fullName = trimmed;
  }

  if (rawYearOfBirth !== undefined) {
    const yearOfBirth = Number(rawYearOfBirth);
    if (!Number.isInteger(yearOfBirth) || yearOfBirth < 1900 || yearOfBirth > currentYear) {
      return sendFailure(res, `Năm sinh phải nằm trong khoảng từ 1900 đến ${currentYear}.`, 400);
    }
    student.yearOfBirth = yearOfBirth;
  }

  return sendSuccess(
    res,
    {
      ...student,
      age: Math.max(0, currentYear - student.yearOfBirth)
    },
    "Cập nhật sinh viên thành công"
  );
});

router.delete("/:id", (req, res) => {
  const id = Number(req.params.id);

  if (!Number.isInteger(id) || id <= 0) {
    return sendFailure(res, "ID sinh viên phải lớn hơn 0", 400);
  }

  const index = students.findIndex((item) => item.id === id);
  if (index === -1) {
    return sendFailure(res, `Không tìm thấy sinh viên với ID ${id}`, 404);
  }

  students.splice(index, 1);

  return sendSuccess(res, { deletedId: id }, "Xóa sinh viên thành công");
});

module.exports = router;
