const defaultTheme = require("tailwindcss/defaultTheme");
const { colors } = defaultTheme;

const purple = {
  dark: "#141631",
  DEFAULT: "#5b5c7f",
  light: "#b2b4c1",
  lightest: "#e3e3ee",
};

module.exports = {
  mode: 'jit',
  purge: ['./dist/**/*.js'],
  theme: {
    container: {
      center: true,
      padding: "2rem",
    },
    extend: {
      colors: {
        primary: purple.dark,
        secondary: purple.DEFAULT,
        success: colors.green[400],
        purple,
      },
      fontFamily: {
        mono: ["JetBrains Mono", ...defaultTheme.fontFamily.mono],
        sans: ["Roboto", ...defaultTheme.fontFamily.sans],
        title: ["JetBrains", ...defaultTheme.fontFamily.sans],
      },
      textColor: {
        primary: "#444444",
      },
    },
  },
  plugins: [require("@tailwindcss/forms")],
}
