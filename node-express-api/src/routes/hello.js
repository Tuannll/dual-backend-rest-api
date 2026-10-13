const express = require("express");
const { sendSuccess, sendFailure } = require("../utils/response");

const router = express.Router();

router.get("/", (_req, res) => {
  return sendSuccess(res, { message: "Xin chào từ Express API!" }, "Lấy thông điệp thành công");
});

router.get("/:name", (req, res) => {
  const name = req.params.name?.trim();

  if (!name) {
    return sendFailure(res, "Tên không được để trống", 400);
  }

  return sendSuccess(res, { message: `Xin chào, ${name}!` }, "Lấy thông điệp thành công");
});

router.post("/", (req, res) => {
  const text = req.body?.text?.trim();

  if (!text) {
    return sendFailure(res, "Thông điệp không được để trống", 400);
  }

  return sendSuccess(
    res,
    {
      message: text,
      timestamp: new Date().toISOString()
    },
    "Gửi thông điệp thành công",
    201
  );
});

module.exports = router;
