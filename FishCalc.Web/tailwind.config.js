// tailwind.config.js
/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    //  QUAN TRỌNG: Đảm bảo đường dẫn này trỏ đến tất cả các file Razor Pages của bạn ⚡
    "./Pages/**/*.cshtml", 
    "./Views/**/*.cshtml" // Nếu bạn cũng dùng View Components
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}