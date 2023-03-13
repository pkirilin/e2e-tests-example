import mysql from "mysql2/promise";
import fs from "fs/promises";

export async function seedData() {
  const connection = await mysql.createConnection({
    host: "localhost",
    port: 3306,
    user: "root",
    password: "root",
    database: "e2e_tests_example",
    multipleStatements: true,
  });

  try {
    const sql = await fs.readFile(__dirname + "/seed.sql", "utf-8");
    await connection.query(sql);
  } finally {
    await connection.end();
  }
}
