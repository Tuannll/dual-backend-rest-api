const express = require("express");
const helloRouter = require("./routes/hello");
const studentRouter = require("./routes/students");

const app = express();
const port = process.env.PORT || 3000;

app.use(express.json());

app.get("/", (_req, res) => {
  res.json({ message: "Node.js Express API is running" });
});

app.use("/api/hello", helloRouter);
app.use("/api/students", studentRouter);

app.listen(port, () => {
  console.log(`Server listening on port ${port}`);
});
