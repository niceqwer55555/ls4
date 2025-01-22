import { createStore } from "vuex";
import path from "path";

// Exported actions
import systemActions from "./actions/systemActions";
import socketActions from "./actions/socketActions";
import collectionActions from "./actions/collectionActions";
import storeActions from "./actions/storeActions";

// Exported mutations
import systemMutations from "./mutations/systemMutations";
import socketMutations from "./mutations/socketMutations";

export default createStore({
    state: {
        version: import.meta.env.VITE_APP_VERSION !== "undefined" ? `Build: ${import.meta.env.VITE_APP_VERSION}` : "Development",
        token: "",
        commitList: [],
        newsList: [],
        alertList: [],
        friendList: [],
        friendOut: [],
        friendIn: [],
        sidebarComponent: "friends",
        privateChat: {
            chatMessages: [],
            openChats: []
        },
        user: {
            summonerName: "Preloaded",
            summonerIconId: 29,
            summonerMotto: "PreloadedMotto",
            summonerLevel: 30,
            email: "testmail",
            ownedIcons: [],
            roles: [],
            summonerExperienceNeeded: undefined,
            summonerStatus: "OFFLINE",
            userName: "",
            uuid: "",
            s4Coins: 0
        },
        config: {
            download: {
                host:
                    import.meta.env.VITE_APP_CDN !== "undefined" &&
                        import.meta.env.VITE_APP_CDN !== undefined
                        ? `http://${import.meta.env.VITE_APP_CDN}`
                        : "http://localhost:3000/cdn/", //https://cdn.leagues4.com
                port:
                    import.meta.env.VITE_APP_CDN_PORT !== "undefined" &&
                        import.meta.env.VITE_APP_CDN_PORT !== undefined
                        ? 3000 //import.meta.env.VITE_APP_CDN_PORT
                        : 443
            },
            api: {
                host:
                    import.meta.env.VITE_APP_API !== "undefined" &&
                        import.meta.env.VITE_APP_API !== undefined
                        ? `http://${import.meta.env.VITE_APP_API}`
                        : "http://127.0.0.1:8080", //https://api.leagues4.com
                port:
                    import.meta.env.VITE_APP_API_PORT !== "undefined" &&
                        import.meta.env.VITE_APP_API_PORT !== undefined
                        ? 8080 //import.meta.env.VITE_APP_API_PORT
                        : 443
            },
            path: {
                client: ""
            }
        },
        rememberToken: null,
        rememberTokenPath: new Promise((resolve) => {
            // Use the exposed method to get the path
            window.electron.getRememberTokenPath().then((result) => {
                resolve(result);
            });
        }),
        backgroundState: "LOGIN",
        collection: {
            champions: [],
            selectedChampion: {},
            icons: []
        },
        lobby: {},
        lobbyChatMessages: [],
        lobbyOwner: false,
        lobbyInvites: [],
        lobbyQueueTimer: null,
        lobbyQueueTime: 1,
        lobbyQueueCount: "-",
        serverCount: "-",
        matchFoundState: {
            accepted: 0,
            pending: 10,
            denied: 0
        },
        champselect: {},
        champselectMessages: [],
        csEnemyTeam: [],
        csAllyBans: [],
        csEnemyBans: [],
        csGlobalState: {},
        csCurrentPlayer: {},
        availableSpells: [
            "SummonerHeal",
            "SummonerFlash",
            "SummonerBoost",
            "SummonerDot",
            "SummonerExhaust",
            "SummonerHaste",
            "SummonerMana",
            "SummonerRevive",
            "SummonerSmite",
            "SummonerTeleport",
            "SummonerBarrier",
            "SummonerOdinGarrison",
            "SummonerClairvoyance"
        ]
    },
    mutations: {
        ...systemMutations,
        ...socketMutations
    },
    actions: {
        ...systemActions,
        ...socketActions,
        ...collectionActions,
        ...storeActions
    },
    modules: {},
    getters: {}
});