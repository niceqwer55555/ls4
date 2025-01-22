<template>
	<div id="app">
		<private-chat />
		<friend-contextmenu />
		<logout />
		<setter-modal />
		<matchmaking />
		<summonericonmodal />
		<reconnectmodal />
		<Menu />
		<div id="content-holder">
			<Background />
			<router-view class="router-content" />
		</div>
	</div>
</template>

<script>
import Menu from "./components/MenuBar.vue";
import Background from "./components/Background.vue";
export default {
	components: {
		Menu,
		Background
	},
	methods: {
		closeApp() {
			window.electron.ipcRenderer.send("closeApp");
		},
		minimizeApp() {
			window.electron.ipcRenderer.send("minimizeApp");
		},
		openExternal() {
			window.api.openExternal("URL");
		}
	}
};
</script>

<style src="./assets/css/global.css"></style>

<style lang="css">
@import "./assets/css/fonts.css";

* {
	box-sizing: border-box;
}

body {
	position: relative;
	margin: 0;
	padding: 0;
	overflow: hidden;
	color: white;
}

#app {
	-webkit-app-region: no-drag;
	height: 720px;
	width: 1280px;
	overflow: hidden;
	font-family: LoLFont4;
}

#content-holder {
	width: 100%;
	height: 100%;
}

#content-holder .router-content {
	position: relative;
	z-index: 1 !important;
	padding: 0 10px;
}

#content-holder * {
	z-index: inherit;
}

a,
a:visited,
a:hover,
a:link {
	text-decoration: none;
	color: unset;
}

::-webkit-scrollbar {
	width: 7px;
}

::-webkit-scrollbar-thumb {
	background-size: 100% 100%;
	background-color: transparent;
	border-radius: 7px;
	background-image: url("./assets/images/scroll-thumb.png");
}

::-webkit-scrollbar-button {
	display: none;
}

::-webkit-scrollbar-track {
	display: none;
}

::-webkit-scrollbar-track-piece {
	display: none;
}

* {
	user-select: none;
	-webkit-user-drag: none;
}
</style>