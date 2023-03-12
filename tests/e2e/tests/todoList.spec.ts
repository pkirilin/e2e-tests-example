import { test, expect } from "@playwright/test";
import { APP_URL } from "../config";

test.use({
  ignoreHTTPSErrors: true,
});

test("todo list title is visible", async ({ page }) => {
  await page.goto(APP_URL);

  await expect(page.locator("h1")).toContainText(/todos/i);
});

test("todo list items are visible", async ({ page }) => {
  await page.goto(APP_URL);

  await expect(page.locator("ul > li")).toContainText([
    "First",
    "Second",
    "Third",
  ]);
});
