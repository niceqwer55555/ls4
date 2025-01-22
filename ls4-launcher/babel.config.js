module.exports = {
    presets: ["@babel/preset-env"],
    plugins: [
      ["module-resolver", {
        alias: {
          "path": "path-browserify",
          "os": "os-browserify/browser",
          "crypto": "crypto-browserify",
          "fs": false,
          "child_process": false
        }
      }]
    ]
};