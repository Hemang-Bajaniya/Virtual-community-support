/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
    "./src/components/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        primary: {
          DEFAULT: '#ff6900', // Your main primary color
          hover: '#e05c00',    // A slightly darker shade for hover states
        },
        secondary: '#fcb900',
      },
    },
  },
  plugins: [
  ],
}

