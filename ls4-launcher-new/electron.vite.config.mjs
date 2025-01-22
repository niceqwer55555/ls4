import { resolve } from 'path';
import { defineConfig, externalizeDepsPlugin } from 'electron-vite';
import vue from '@vitejs/plugin-vue';
import renderer from 'vite-plugin-electron-renderer';

export default defineConfig({
  base: './',
  css: {
    preprocessorOptions: {
      scss: {
        sassOptions: { quietDeps: true },
      },
    },
  },
  main: {
    plugins: [externalizeDepsPlugin()]
  },
  preload: {
    plugins: [externalizeDepsPlugin()]
  },
  renderer: {
    resolve: {
      alias: {
        '@': resolve('src/renderer/src'),  // Add this common alias
        '@renderer': resolve('src/renderer/src')
      }
    },
    plugins: [
      vue({
        // Add Vue 3 specific options
        template: {
          compilerOptions: {
            // Add any compiler options if needed
            isCustomElement: (tag) => tag.startsWith('ion-')
          }
        }
      })
    ],
    optimizeDeps: {
      exclude: ['electron']  // Exclude electron from optimization
    },
    build: {
      chunkSizeWarningLimit: 1000,  // Adjust if needed
      rollupOptions: {
        output: {
          manualChunks: (id) => {
            // Optional: configure chunk splitting
            if (id.includes('node_modules')) {
              return 'vendor';
            }
          }
        }
      }
    }
  }
});