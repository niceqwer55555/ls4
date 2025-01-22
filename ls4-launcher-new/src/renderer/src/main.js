import { createApp } from "vue";
import App from "./App.vue";
import router from "./Router";

// VUEX
import store from "./store";

// PLUGINS
import ModalsPlugin from "./plugins/modals";
import ChatPlugin from "./plugins/chat";
import SoundPlugin from "./plugins/sound";
import VueTranslate from "./plugins/translate";
import SocketManagerPlugin from "./plugins/socketManagerPlugin";

const app = createApp(App);

// PLUGIN REGISTERS
app.use(VueTranslate);
app.use(ModalsPlugin);
app.use(ChatPlugin);
app.use(SoundPlugin, { store });
app.use(SocketManagerPlugin, {
    userObj: store.state.user, // Pass the user object from the store
    state: store.state, // Pass the entire state
    dispatch: store.dispatch, // Pass the dispatch function
});

// Mount after registering plugins and store
app
  .use(router)
  .use(store)
  .mount("#app");