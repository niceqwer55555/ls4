process.env.VUE_APP_VERSION = process.env.CI_COMMIT_SHORT_SHA;
process.env.VUE_APP_ENV = process.env.APP_ENV;
process.env.VUE_APP_CDN = process.env.APP_CDN;
process.env.VUE_APP_CDN_PORT = process.env.APP_CDN_PORT;
process.env.VUE_APP_API = process.env.APP_API;
process.env.VUE_APP_API_PORT = process.env.APP_API_PORT;

module.exports = {
    pluginOptions: {
        electronBuilder: {
            nodeIntegration: true,
            builderOptions: {
                appId: "com.leagues4.launcher",
                productName: "LeagueS4 Launcher",
                copyright: "Copyright (C) 2020  LeagueS4",
                publish: [
                    {
                        provider: "generic",
                        url: "https://git.jandev.de"
                    }
                ]
            },
            customFileProtocol: "./"
        }
    }
};