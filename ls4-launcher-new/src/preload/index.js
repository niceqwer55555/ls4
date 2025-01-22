import { contextBridge, ipcRenderer } from 'electron'
import { electronAPI } from '@electron-toolkit/preload'

// Custom APIs for renderer
const api = {
	openExternal: (url) => shell.openExternal(url), // Expose shell.openExternal
}

// Define IPC specific methods
const ipcAPI = {
  ipcRenderer: {
	invoke: (channel, ...args) => ipcRenderer.invoke(channel, ...args),
    send: (channel, ...args) => {
      // Whitelist channels
      const validChannels = ['minimizeApp', 'closeApp', 'update', 'focusApp', 'ping', 'getPath', 'openFileDialog', 'getStatic', 'translate_missing_langkeys']
      if (validChannels.includes(channel)) {
        ipcRenderer.send(channel, ...args)
      }
    },
    on: (channel, func) => {
      const validChannels = ['minimizeApp', 'closeApp', 'update', 'focusApp', 'ping', 'getPath', 'openFileDialog', 'getStatic', 'translate_missing_langkeys']
      if (validChannels.includes(channel)) {
        // Strip event as it includes `sender`
        ipcRenderer.on(channel, (event, ...args) => func(...args))
      }
    },
    once: (channel, func) => {
      const validChannels = ['minimizeApp', 'closeApp', 'update', 'focusApp', 'ping', 'getPath', 'openFileDialog', 'getStatic', 'translate_missing_langkeys']
      if (validChannels.includes(channel)) {
        ipcRenderer.once(channel, (event, ...args) => func(...args))
      }
    },
    removeListener: (channel, func) => {
      const validChannels = ['minimizeApp', 'closeApp', 'update', 'focusApp', 'ping', 'getPath', 'openFileDialog', 'getStatic', 'translate_missing_langkeys']
      if (validChannels.includes(channel)) {
        ipcRenderer.removeListener(channel, func)
      }
    }
  },
  getRememberTokenPath: () => ipcRenderer.invoke('getRememberTokenPath'),
}

// Use `contextBridge` to expose Electron APIs to the renderer
if (process.contextIsolated) {
  try {
    contextBridge.exposeInMainWorld('electron', {
      ...electronAPI,
      ...ipcAPI
    });
    contextBridge.exposeInMainWorld('api', api); // Expose custom APIs
  } catch (error) {
    console.error(error);
  }
} else {
  window.electron = {
    ...electronAPI,
    ...ipcAPI
  }
  window.api = api;
}