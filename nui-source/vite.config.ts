import { defineConfig, loadEnv } from 'vite';
import { svelte } from '@sveltejs/vite-plugin-svelte';

export default defineConfig(({ command, mode }) => {
  const env = loadEnv(mode, process.cwd(), '');
  return {
    define: {
      'process.env': env,
    },
    plugins: [svelte()],
    base: './',
    build: {
      outDir: `${env.FIVEM_SERVER_PATH}/resources/[perseverance-framework]/perseverance/nui-client`,
    },
    optimizeDeps: {
      include: ['@picocss/pico'],
    },
  };
});
