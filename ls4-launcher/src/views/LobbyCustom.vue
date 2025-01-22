<template>
  <div id="LobbyCustom">
    <div class="content">
      <div class="mainblock">
        <div class="lobby">
          <div class="players">
            <div class="team lolblock">
              <div class="head">
                {{
                  tWithParams("CTLOBBY_TEAM1_TITLE", {
                    serverCount
                  })
                }}
              </div>
              <div class="inner">
                <Player
                  v-for="(player, index) in players1"
                  :key="index"
                  :summoner="player"
                  :custom="true"
                />
                <div v-if="canJoinTeam(1)" class="joinTeam">
                  <button @click="switchTeam">
                    {{ t("CTLOBBY_JOIN_TEAM_BTN") }}
                  </button>
                </div>
              </div>
            </div>
            <div class="team lolblock">
              <div class="head">
                {{ t("CTLOBBY_TEAM2_TITLE") }}
              </div>
              <div class="inner">
                <Player
                  v-for="(player, index) in players2"
                  :key="index"
                  :summoner="player"
                  :custom="true"
                />
                <div v-if="canJoinTeam(2)" class="joinTeam">
                  <button @click="switchTeam">
                    {{ t("CTLOBBY_JOIN_TEAM_BTN") }}
                  </button>
                </div>
              </div>
            </div>
            <div class="actions">
              <button class="quitBtn" @click="leaveLobby">
                {{ t("CTLOBBY_QUIT_GAME_BTN") }}
              </button>
              <button class="changeBtn" @click="$router.push('/LoggedIn/play')">
                {{ t("CTLOBBY_CHANGE_LOBBY_TYPE_BTN") }}
              </button>
              <button class="startBtn" v-if="owner" @click="startQueue">
                {{ t("CTLOBBY_START_GAME_BTN") }}
              </button>
            </div>
          </div>
          <div class="chat lolblock">
            <div class="head">
              {{ t("LOBBY_ALLCHAT_TITLE") }}
            </div>
            <div class="inner">
              <div class="chat-messages" ref="chatMessages">
                <ChatMessage
                  v-for="(message, index) in lobbyChatMessages"
                  :key="index"
                  :message="message"
                />
              </div>
              <div class="chat-input">
                <input
                  type="text"
                  ref="chatInput"
                  v-on:keyup.enter="sendLobbyChatMessage"
                />
                <button @click="sendLobbyChatMessage">
                  {{ t("LOBBY_ALLCHAT_SEND_BTN") }}
                </button>
              </div>
            </div>
          </div>
        </div>
        <div class="game">
          <div class="map lolblock">
            <div class="head">
              {{ t("LOBBY_MAP_OPTIONS_TITLE") }}
            </div>
            <div class="inner">
              <img :src="getMapPreview()" class="preview" alt="" />
              <div class="details">
                <p>
                  <strong>{{ t("LOBBY_MAP_OPTIONS_LABEL_MAP") }}:</strong>
                  {{ getMapType() }}
                </p>
                <p>
                  <strong>{{ t("LOBBY_MAP_OPTIONS_LABEL_TEAMSIZE") }}:</strong>
                  {{ getTeamSize() }}
                </p>
                <p>
                  <strong>{{ t("LOBBY_MAP_OPTIONS_LABEL_QUEUETYPE") }}:</strong>
                  {{ getQueueType() }}
                </p>
              </div>
            </div>
          </div>
          <div class="invites lolblock">
            <div class="head">
              {{ t("LOBBY_INVITES_TITLE") }}
            </div>
            <div class="inner">
              <div
                class="invite"
                v-for="(invite, index) in lobbyInvites"
                :key="index"
              >
                <span>{{ index }}</span>
                <span :class="invite.toLowerCase()">
                  {{ getInviteStatus(invite) }}
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <SideBar />
  </div>
</template>

<script>
import Player from "@/components/Lobby/Player.vue";
import SideBar from "@/components/SideBar.vue";
import ChatMessage from "@/components/Lobby/ChatMessage.vue";
import { mapState } from "vuex";

export default {
  components: {
    Player,
    ChatMessage,
    SideBar
  },
  data() {
    return {
      lastMessages: null,
      queueNumber: "-"
    };
  },
  computed: {
    isTeamFull() {
      if (this.players1 && this.players2) {
        let user = this.$store.state.user.summonerName;
        let team1 = this.players1.some(c => c.summonerName == user);
        let team2 = this.players2.some(c => c.summonerName == user);

        if (team1 && this.players2.length <= 5) {
          return false;
        } else if (team2 && this.players1.length <= 5) {
          return false;
        }
      }
      return true;
    },
    ...mapState({
      players1: state =>
        state.lobby.members
          ? state.lobby.members.filter(c => c.lobbyTeam === "TEAM1")
          : undefined,
      players2: state =>
        state.lobby.members
          ? state.lobby.members.filter(c => c.lobbyTeam === "TEAM2")
          : undefined,
      lobbyChatMessages: state => state.lobbyChatMessages,
      lobbyInvites: state => state.lobby.invited,
      inQueue: state => state.lobby.inQueue,
      owner: state => state.lobbyOwner,
      serverCount: state => state.serverCount,
      currentLobbyPlayer: state => {
        return state.lobby.members.filter(member => {
          return member.summonerName == state.user.summonerName;
        })[0];
      }
    })
  },
  methods: {
    canJoinTeam(team) {
      if (this.players1 && this.players2) {
        if (team == 2) {
          return (
            this.players2.length != 5 &&
            this.currentLobbyPlayer.lobbyTeam == "TEAM1" &&
            !this.isTeamFull
          );
        } else {
          return (
            this.players1.length != 5 &&
            this.currentLobbyPlayer.lobbyTeam == "TEAM2" &&
            !this.isTeamFull
          );
        }
      } else {
        return false;
      }
    },
    getInviteStatus(status) {
      return this.$translate.text(`LOBBY_INVITE_STATUS_${status}`);
    },
    inLobby(invite) {
      return (
        this.$store.state.lobby.members.filter(member => {
          return member.summonerName == invite.summonerName;
        }).length > 0
      );
    },
    getQueueType() {
      switch (this.$store.state.lobby.lobbyType) {
        case "SUMMONERS_RIFT_DRAFT":
        case "TWISTED_TREELINE_DRAFT":
          return this.$translate.text("LOBBY_MAP_OPTIONS_VALUE_QT_DRAFT");
        case "SUMMONERS_RIFT_BLIND":
        case "TWISTED_TREELINE_BLIND":
          return this.$translate.text("LOBBY_MAP_OPTIONS_VALUE_QT_BLIND");
        case "ARAM_BLIND":
          return this.$translate.text("LOBBY_MAP_OPTIONS_VALUE_QT_ARAM");
      }
    },
    getMapPreview() {
      switch (this.$store.state.lobby.lobbyType) {
        case "SUMMONERS_RIFT_DRAFT":
        case "SUMMONERS_RIFT_BLIND":
          return "static/images/general/sr-preview.png";
        case "TWISTED_TREELINE_DRAFT":
        case "TWISTED_TREELINE_BLIND":
          return "static/images/general/tt-preview.png";
        case "ARAM_BLIND":
          return "static/images/general/ha-preview.png";
      }
    },
    getMapType() {
      switch (this.$store.state.lobby.lobbyType) {
        case "SUMMONERS_RIFT_DRAFT":
        case "SUMMONERS_RIFT_BLIND":
          return this.$translate.text("LOBBY_MAP_OPTIONS_VALUE_SR");
        case "TWISTED_TREELINE_DRAFT":
        case "TWISTED_TREELINE_BLIND":
          return this.$translate.text("LOBBY_MAP_OPTIONS_VALUE_TT");
        case "ARAM_BLIND":
          return this.$translate.text("LOBBY_MAP_OPTIONS_VALUE_HA");
      }
    },
    getTeamSize() {
      switch (this.$store.state.lobby.lobbyType) {
        case "SUMMONERS_RIFT_DRAFT":
        case "SUMMONERS_RIFT_BLIND":
        case "ARAM_BLIND":
          return "5x5";
        case "TWISTED_TREELINE_DRAFT":
        case "TWISTED_TREELINE_BLIND":
          return "3x3";
      }
    },
    sendLobbyChatMessage() {
      const message = this.$refs.chatInput.value;
      if (message.trim() == "") return;
      this.$refs.chatInput.value = "";

      this.$socket.sendLobbyMessage(
        "LOBBY_CHAT",
        { data: message },
        (response, error) => {
          if (error) {
            console.log("Flyback error:");
            console.log(error);
          }
          this.$refs.chatMessages.scrollTop = this.$refs.chatMessages.scrollHeight;
        }
      );
    },
    leaveLobby() {
      const route = this.$route.name;
      const router = this.$router;
      this.$socket.sendLobbyMessage("LOBBY_LEAVE", {}, (response, error) => {
        if (error) {
          console.log("Flyback error:");
          console.log(error);
        }
        if (route != "Home") {
          router.push("/LoggedIn/home");
        }
      });
    },
    startQueue() {
      if (this.inQueue) return;

      this.$sound.template("OVERVIEW_PLAYBUTTON");

      this.$socket.sendLobbyMessage(
        "LOBBY_MATCHMAKING_START",
        {},
        (response, error) => {
          if (error) {
            console.log("Flyback error:");
            console.log(error);

            if (error && error.message && error.code === 140) {
              this.$store.dispatch("socketRecievedLobbyChat", {
                from: "SYSTEM",
                to: null,
                data: error.message
              });
            }
          }

          console.log(response);
        }
      );
    },
    stopQueue() {
      this.$sound.template("OVERVIEW_CLICK");
      this.$store.dispatch("clearLobbyTimers");

      this.$socket.sendLobbyMessage(
        "LOBBY_MATCHMAKING_STOP",
        {},
        (response, error) => {
          if (error) {
            console.log("Flyback error:");
            console.log(error);
          }
          this.$store.dispatch("setLobbyQueueState", false);

          console.log(response);
        }
      );
    },
    switchTeam() {
      if (this.inQueue) return;

      this.$socket.sendLobbyMessage(
        "LOBBY_SWITCH_TEAM",
        {},
        (response, error) => {
          if (error) {
            console.log("Flyback error:");
            console.log(error);
          }
        }
      );
    }
  },
  beforeMount() {
    this.$store.dispatch("saveUserStatus", "IN_LOBBY");
    this.$store.dispatch("changeBackgroundState", "CUSTOM_LOBBY");
  },
  updated() {
    if (this.lastMessages !== this.lobbyChatMessages.length) {
      this.lastMessages = this.lobbyChatMessages.length;
      this.$refs.chatMessages.scrollTop = this.$refs.chatMessages.scrollHeight;
    }
  },
  created() {
    this.$store.dispatch("getServerCount");
  }
};
</script>

<style lang="css" scoped>
#LobbyCustom {
  width: 100%;
  height: calc(100% - 115px);
  margin-top: 77px;
  position: relative;
  z-index: 0;
  display: flex;
}

#LobbyCustom .content {
  width: 100%;
  position: relative;
  display: flex;
  justify-content: flex-end;
}
#LobbyCustom .sidebar-holder {
  width: 20%;
  position: absolute;
  right: 0;
  top: 0;
}

.content .mainblock {
  width: 100%;
  height: 100%;
  display: flex;
}

/* LOL BLOCK */

.lolblock {
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 100%;
  border: 1px solid rgba(100, 117, 137, 0.75);
  border-radius: 5px;
  overflow: hidden;
}

.lolblock .head {
  height: 30px;
  width: 100%;
  font-size: 14px;
  font-family: LoLFont2;
  padding: 5px;
  background-image: linear-gradient(
    180deg,
    #192e49 0%,
    #192e49 40%,
    #172b46 50%,
    #142131 100%
  );
}

.lolblock .inner {
  background-color: rgba(5, 12, 20, 0.85);
  width: 100%;
  height: calc(100% - 30px);
}

.content .mainblock .lobby {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.content .mainblock .lobby .players {
  width: 100%;
  height: 70%;
  display: flex;
  position: relative;
}

.content .mainblock .lobby .players .team {
  width: 50%;
  height: 100%;
}

.content .mainblock .lobby .players .team .inner {
  padding: 5px;
  position: relative;
}

.content .mainblock .lobby .players .team .inner .joinTeam {
  height: 17%;
  width: 100%;
  position: relative;
}

.content .mainblock .lobby .players .team .inner .joinTeam button {
  position: absolute;
  left: 50%;
  top: 50%;
  padding: 5px 40px;
  transform: translate(-50%, -50%);
  background: linear-gradient(
    180deg,
    #3c73b4 0%,
    #20477e 45%,
    #1e3e6d 50%,
    #0e284b 100%
  );
  outline: none;
  border: none;
  border-radius: 2px;
  text-align: center;
  border: 1px solid #304b69;
  color: white;
  font-family: LoLFont2;
  transition: filter ease-in-out 200ms;
  cursor: pointer;
}

.content .mainblock .lobby .players .team .inner .joinTeam button:hover {
  filter: brightness(1.25);
}

.content .mainblock .lobby .players .actions {
  position: absolute;
  bottom: 0px;
  left: 50%;
  transform: translateX(-50%);
  padding: 5px 0;
  display: flex;
  height: 40px;
  width: 98%;
  border-top: 1px solid #304b69;
  background-color: rgba(5, 12, 20, 1);
}

.content .mainblock .lobby .players .actions button {
  background: linear-gradient(
    180deg,
    #3c73b4 0%,
    #20477e 45%,
    #1e3e6d 50%,
    #0e284b 100%
  );
  outline: none;
  border: none;
  border-radius: 2px;
  margin-right: 10px;
  padding: 5px 40px;
  border: 1px solid #304b69;
  color: white;
  font-family: LoLFont2;
  transition: filter ease-in-out 200ms;
  cursor: pointer;
}

.content .mainblock .lobby .players .actions button:hover {
  filter: brightness(1.25);
}

.content .mainblock .lobby .players .actions button.startBtn {
  background: url("../assets/images/button-default.png");
  background-size: 100% 100%;
  border: none;
  margin-left: auto;
  margin-right: 0;
}

.content .mainblock .lobby .chat {
  width: 100%;
  height: 30%;
}

.content .mainblock .lobby .chat .inner {
  display: flex;
  flex-direction: column;
}

.content .mainblock .lobby .chat .inner .chat-messages {
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 80%;
  padding: 0 3px;
  overflow: auto;
}

.content .mainblock .lobby .chat .inner .chat-input {
  width: 100%;
  height: 20%;
  display: flex;
  justify-content: space-between;
  padding: 0 2px;
}

.content .mainblock .lobby .chat .inner .chat-input input {
  border: 1px solid #404549;
  border-radius: 3px;
  box-shadow: inset 0 0 5px 1px #1c2932;
  width: 89.5%;
  outline: none;
}

.content .mainblock .lobby .chat .inner .chat-input button {
  width: 10%;
  background: linear-gradient(
    180deg,
    #3c73b4 0%,
    #20477e 45%,
    #1e3e6d 50%,
    #0e284b 100%
  );
  outline: none;
  border: none;
  border-radius: 2px;
  text-align: center;
  border: 1px solid #304b69;
  color: white;
  font-family: LoLFont2;
  transition: filter ease-in-out 200ms;
  cursor: pointer;
}

.content .mainblock .lobby .chat .inner .chat-input button:hover {
  filter: brightness(1.25);
}

.content .mainblock .game {
  width: 30%;
  height: 100%;
  display: flex;
  flex-direction: column;
}

.content .mainblock .game .map {
  width: 100%;
  height: 55%;
  display: flex;
  flex-direction: column;
}
.content .mainblock .game .map .inner {
  padding: 2px;
}
.content .mainblock .game .map .inner img {
  height: 155px;
  width: 100%;
  object-fit: cover;
  border: 1px solid #566676;
  object-position: center;
}

.content .mainblock .game .map .inner .details {
  height: calc(100% - 155px);
  width: 100%;
  display: flex;
  flex-direction: column;
  padding: 0 5px;
}

.content .mainblock .game .map .inner .details p {
  font-size: 12px;
  font-family: LoLFont2;
  margin: 0;
}

.content .mainblock .game .invites {
  width: 100%;
  height: 45%;
  overflow: auto;
}

.content .mainblock .game .invites .inner {
  display: flex;
  flex-direction: column;
  padding: 3px;
  overflow: auto;
}

.content .mainblock .game .invites .inner .invite {
  font-size: 13px;
  font-family: LoLFont2;
  display: flex;
  justify-content: space-between;
  padding: 0 5px;
  margin-bottom: 5px;
}

.content .mainblock .game .invites .inner .invite .accepted {
  color: #306d32;
}

.content .mainblock .game .invites .inner .invite .pending {
  color: #c8a91a;
}
</style>
