function sendSuccess(res, data, message = "Thành công", statusCode = 200) {
  return res.status(statusCode).json({
    isSuccess: true,
    message,
    statusCode,
    data
  });
}

function sendFailure(res, message = "Thất bại", statusCode = 400) {
  return res.status(statusCode).json({
    isSuccess: false,
    message,
    statusCode
  });
}

module.exports = {
  sendSuccess,
  sendFailure
};
