import { defineConfig, loadEnv } from 'vite';
import { svelte } from '@sveltejs/vite-plugin-svelte';
import { minify } from "html-minifier";
import { resolve } from "path";

function minifyHtml() {
  return {
    name: "html-transform",
    transformIndexHtml(html) {
      return minify(html, {
        collapseWhitespace: true,
      });
    },
  };
}

export default defineConfig(({ command, mode }) => {
  const env = loadEnv(mode, process.cwd(), '');
  const isProduction = mode === "production";
  return {
    define: {
      'process.env': env,
    },
    plugins: [svelte(), isProduction && minifyHtml()],
    base: './',
    resolve: {
      alias: {
        "@assets": resolve("./src/assets"),
        "@components": resolve("./src/components"),
        "@providers": resolve("./src/providers"),
        "@store": resolve("./src/store"),
        "@utils": resolve("./src/utils"),
        "@base": resolve("./src/components/base"),
        "@shared": resolve("./src/components/shared"),
        "@apps": resolve("./src/components/apps"),
        "@types": resolve("./src/@types"),
        "@citizen": resolve("./src/components/citizen"),
      },
    },
    build: {
      minify: isProduction,
      assetsDir: './',
      emptyOutDir: true,
      outDir: `${env.FIVEM_SERVER_PATH}/resources/[perseverance-framework]/perseverance/nui-client`,
      rollupOptions: {
        output: {
          entryFileNames: `js/[name].js`,
          chunkFileNames: `js/[name].js`,
          assetFileNames: `assets/[name].[ext]`,
        },
      },
    },
    optimizeDeps: {
      include: ['@picocss/pico'],
    },
  };
});
