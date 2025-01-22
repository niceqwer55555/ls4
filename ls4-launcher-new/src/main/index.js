"use strict";

import { app, protocol, shell, BrowserWindow, ipcMain, dialog } from 'electron';
const installExtension = require("electron-devtools-installer");
const { VUEJS3_DEVTOOLS } = require("electron-devtools-installer");
import { join } from 'path';
const { writeFileSync } = require("fs");
import { electronApp, optimizer, is } from '@electron-toolkit/utils';
import icon from '../../resources/icon.png?asset';
const env = require("dotenv");
const ping = require("ping");

env.config();
let mainWindow;
const isDevelopment = process.env.NODE_ENV !== "production";

protocol.registerSchemesAsPrivileged([
	{ scheme: "app", privileges: { secure: false, standard: true } }
]);

const enableDevTools = !app.isPackaged;

function createWindow() {
	// Create the browser window.
	mainWindow = new BrowserWindow({
		width: 1280,
		height: 720,
		resizable: false,
		autoHideMenuBar: true,
		backgroundColor: "#00ffffff",
		transparent: true,
		fullscreen: false,
		fullscreenable: false,
		frame: true,
		maximizable: false,
		show: false,
		...(process.platform === 'linux' ? { icon } : {}),
		webPreferences: {
			nodeIntegration: process.env.ELECTRON_NODE_INTEGRATION === "true",
    		contextIsolation: true, // protect against prototype pollution
			preload: join(__dirname, '../preload/index.js'),
			sandbox: false,
			spellcheck: false,
			devTools: enableDevTools
		}
	});

	mainWindow.on('ready-to-show', () => {
		mainWindow.show();
	});

	mainWindow.webContents.setWindowOpenHandler((details) => {
		shell.openExternal(details.url);
		return { action: 'deny' };
	});

	// HMR for renderer base on electron-vite cli.
	// Load the remote URL for development or the local html file for production.
	if (is.dev && process.env['ELECTRON_RENDERER_URL']) {
		mainWindow.loadURL(process.env['ELECTRON_RENDERER_URL']);
	}
	else {
		mainWindow.loadFile(join(__dirname, '../renderer/index.html'));
	}

	mainWindow.on("closed", () => {
		mainWindow = null;
	});
	
	mainWindow.on("close", () => {
		mainWindow = null;
	});
}

app.whenReady().then(async () => {
	// Set app user model id for windows
	electronApp.setAppUserModelId('com.electron');

	app.on('browser-window-created', (_, window) => {
		optimizer.watchWindowShortcuts(window);
	});

	if (isDevelopment && !process.env.IS_TEST) {
		try {
			await installExtension(VUEJS3_DEVTOOLS);
		}
		catch (e) {
			console.error("Vue Devtools failed to install:", e.toString());
		}
	}

	if (isDevelopment) {
		if (process.platform === "win32") {
			process.on("message", data => {
				if (data === "graceful-exit") {
					app.quit();
				}
			});
		}
		else {
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

	ipcMain.handle('getRememberTokenPath', async () => {
		const userDataPath = app.getPath('userData');
		return join(userDataPath, 'rememberToken.json');
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

	createWindow();

	app.on('activate', function () {
		// On macOS it's common to re-create a window in the app when the
		// dock icon is clicked and there are no other windows open.
		if (BrowserWindow.getAllWindows().length === 0) createWindow();
	});
});

app.on('window-all-closed', () => {
	if (process.platform !== 'darwin') {
		app.quit();
	}
});