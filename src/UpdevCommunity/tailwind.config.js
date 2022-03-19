module.exports = {
    content: ["./Pages/**/*.{razor,cshtml}",
        "./Shared/**/*.{razor,cshtml}",
        "./Features/**/*.{razor,cshtml}",
        "./Components/**/*.{razor,cshtml}",
        "./Area/**/*.{razor,cshtml}"],
  theme: {
      extend: {
          colors: {
              blue: {
                  50: '#f2f7fb',
                  100: '#e6f0f8',
                  200: '#bfd9ed',
                  300: '#99c3e2',
                  400: '#4d95cd',
                  500: '#0068b7',
                  600: '#005ea5',
                  700: '#004e89',
                  800: '#003e6e',
                  900: '#00335a'
              }
          }
      },
  },
    plugins: [
        require("@tailwindcss/forms"),
        require("daisyui")
    ],
}