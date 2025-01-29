import { defineConfig } from "cypress";

export default defineConfig({
  defaultCommandTimeout: 20000,
  pageLoadTimeout: 20000,
  watchForFileChanges: false,
  chromeWebSecurity: false,
  video: false,
  retries: 1,
  e2e: {
    setupNodeEvents(on, config) {
      // implement node event listeners here
      on("before:run", () => {
        // Map cypress env vars to process env vars for usage outside of Cypress run environment
        process.env = config.env;
    });
    config.baseUrl = config.env.url;

    return config;
    },
  },
});
