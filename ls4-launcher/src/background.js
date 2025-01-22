"use strict";

const { app, protocol, BrowserWindow, ipcMain, dialog } = require("electron");
const installExtension = require("electron-devtools-installer");
const { VUEJS_DEVTOOLS } = require("electron-devtools-installer");
const ping = require("ping");
const path = require("path-browserify"); // Use path-browserify
const { writeFileSync } = require("fs");
const env = require("dotenv");

env.config();

const isDevelopment = process.env.NODE_ENV !== "production";

let win;

protocol.registerSchemesAsPrivileged([
  { scheme: "app", privileges: { secure: false, standard: true } }
]);

const enableDevTools = !app.isPackaged;

function createWindow() {
  win = new BrowserWindow({
    width: 1280,
    height: 720,
    resizable: false,
    autoHideMenuBar: false,
    backgroundColor: "#00ffffff",
    transparent: true,
    fullscreen: false,
    fullscreenable: false,
    frame: false,
    maximizable: false,
    webPreferences: {
      spellcheck: false,
      devTools: enableDevTools,
      nodeIntegration: process.env.ELECTRON_NODE_INTEGRATION === "true"
    }
  });

  if (process.env.WEBPACK_DEV_SERVER_URL) {
    win.loadURL(process.env.WEBPACK_DEV_SERVER_URL);
    if (!process.env.IS_TEST) win.webContents.openDevTools();
  } else {
    win.loadURL(`file://${__dirname}/index.html`);
  }

  win.on("closed", () => {
    win = null;
  });

  win.on("close", () => {
    win = null;
  });
}

app.on("window-all-closed", () => {
  if (process.platform !== "darwin") {
    app.quit();
  }
});

app.on("activate", () => {
  if (win === null) {
    createWindow();
  }
});

app.on("ready", async () => {
  if (isDevelopment && !process.env.IS_TEST) {
    try {
      await installExtension(VUEJS_DEVTOOLS);
    } catch (e) {
      console.error("Vue Devtools failed to install:", e.toString());
    }
  }
  createWindow();
});

if (isDevelopment) {
  if (process.platform === "win32") {
    process.on("message", data => {
      if (data === "graceful-exit") {
        app.quit();
      }
    });
  } else {
    process.on("SIGTERM", () => {
      app.quit();
    });
  }
}

ipcMain.on("minimizeApp", () => {
  win.minimize();
});

ipcMain.on("focusApp", () => {
  win.setAlwaysOnTop(true);
  win.setAlwaysOnTop(false);
});

ipcMain.on("closeApp", () => {
  app.exit(0);
});

ipcMain.handle("ping", async (event, domain) => {
  return await ping.promise.probe(domain, { min_reply: 3 });
});

ipcMain.handle("getPath", async () => {
  return app.getPath("userData");
});

ipcMain.handle("openFileDialog", async () => {
  return dialog.showOpenDialog({ properties: ["openFile"] }).then(result => {
    if (result && result.filePaths && result.filePaths.length == 1) {
      return result.filePaths[0];
    }
  });
});

ipcMain.handle("getStatic", async () => {
  return path.join(__static, "static");
});

ipcMain.on("translate_missing_langkeys", async (event, data) => {
  writeFileSync("missing_langkeys.json", JSON.stringify(data, null, 2));
});