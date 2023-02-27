/** https://github.com/SnailyCAD/snaily-cadv4/blob/main/apps/client/tailwind.config.js */
/** @type {import('tailwindcss').Config} */
module.exports = {
  future: {
    hoverOnlyWhenSupported: true,
  },
  content: ['./src/**/*.{html,js,svelte,ts}'],
  darkMode: 'class', // 'media' or 'class'
  theme: {
    extend: {
      borderWidth: {
        DEFAULT: "1.5px",
      },
      screens: {
        nav: "900px",
      },
      colors: {
        primary: "#16151a",
        secondary: "#35343c",
        tertiary: "#1f1e26",
        quaternary: "#2f2e34",
        quinary: "#454349",
      },
      animation: {
        enter: "enter 200ms ease-out",
        leave: "leave 150ms ease-in forwards",
      },
      keyframes: {
        enter: {
          "0%": { transform: "translateY(-4px)", opacity: 0 },
          "100%": { transform: "translateY(0px)", opacity: 1 },
        },
        leave: {
          "0%": { transform: "translateY(0px)", opacity: 1 },
          "100%": { transform: "translateY(-4px)", opacity: 0 },
        },
      },
    },
  },
  plugins: [
    require("@tailwindcss/typography")
  ],
}
