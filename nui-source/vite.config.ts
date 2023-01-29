import { defineConfig } from 'vite';
import { svelte } from '@sveltejs/vite-plugin-svelte';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [svelte()],
  base: './',
  build: {
    outDir: '%FIVEM_SERVER_PATH_CH%/resources/[perseverance-framework]/perseverance/nui-client',
  },
  optimizeDeps: {
    include: ['@picocss/pico'],
  },
});
