const { defineConfig } = require("cypress");

module.exports = defineConfig({
  env: {
    HOST: process.env.HOST
  },
  e2e: {
    setupNodeEvents(on, config) {
      // implement node event listeners here
    },
  },
});
