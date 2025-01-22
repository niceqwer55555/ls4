import SocketManager from "../store/utils/socketManager";

export default {
  install(app, options) {
    const { userObj, state, dispatch } = options;

    // Initialize SocketManager
    const socketManager = new SocketManager(userObj, state, dispatch, app);

    // Attach SocketManager to the app's global properties
    app.config.globalProperties.$socket = socketManager;

    // Optionally, provide SocketManager for Composition API usage
    app.provide("$socket", socketManager);
  },
};