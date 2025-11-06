import react from '@vitejs/plugin-react';
import { defineConfig } from 'vite';
import tsconfigPaths from 'vite-tsconfig-paths';

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), tsconfigPaths()],
  server: {
    port: 3000,
    host: '0.0.0.0',
    open: process.env.OPEN_BROWSER ? process.env.OPEN_BROWSER === 'true' : true,
    watch: {
      usePolling: true,
    },
  },
  // allow the preview server to accept requests from the Render host
  preview: {
    allowedHosts: ['frontend-ys4r.onrender.com'],
  },
});
