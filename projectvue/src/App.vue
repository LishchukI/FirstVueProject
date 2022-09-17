<template>
  <div id="app">
    <div class="v-message chat__own" :class="className" id="app-1">
      <ul>
        <li
          class="v-message__content"
          v-for="message in messages"
          :key="message.id"
          :title="message.text"
        >
          {{ message.text }}<br /><br />{{ message.dateTime }}<br />
          <button @click="deleteMessage(message)">Remove</button>
        </li>
      </ul>
    </div>

    <div id="app" class="input__field">
      <input
        type="text"
        class="v-user-chat__textfield"
        v-model="nextDataText"
        @keypress.enter="createMessage"
      />
      <input type="file" @change="uploadFile" ref="file"/>
      <button @click="submitFile">Upload!</button>
      <button class="material-icons" @click="createMessage">send</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "App",
  data: () => ({
    messages: [],
  }),
  mounted() {
    fetch("http://localhost:52994/api/messages/getfirst").then((data) => {
      data.json().then((messages) => {
        this.messages = messages;
      });
    });
  },
  methods: {
    async createMessage() {
      if (this.nextDataText != "") {
        const response = await fetch("http://localhost:52994/api/messages", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            text: this.nextDataText,
            datetime: new Date().toJSON(),
          }),
        });
        const data = await response.json();
        this.messages.push(data);
        this.nextDataText = "";
      }
    },
    async deleteMessage(message) {
      await fetch("http://localhost:52994/api/messages/delete", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          id: message.id,
          text: message.text,
        }),
      });
      console.log(this.messages.indexOf(message));
      this.messages.splice(this.messages.indexOf(message), 1);
    },
    async uploadFile() {
      this.Images = this.$refs.file.files[0];
    },
    async submitFile() {
      const formData = new FormData();
      formData.append("file", this.Images);
      /*Тут далі реалізація відправки файлу. Так розумію це потрібно в моєму випадку скористуватись fetch("посилання, де реалізований функціонал з прикріпленням файлу"), того цей комент не убрав :)
    const headers = { "Content-Type": "multipart/form-data" };
    axios
      .post("https://httpbin.org/post", formData, { headers })
      .then((res) => {
        res.data.files; // binary representation of the file
        res.status; // HTTP status
      });
      */
    },
  },
};
</script>

<style>
.v-message {
  margin-bottom: 32px;
}
.v-message.chat__own {
  display: flex;
  justify-content: flex-end;
}
.v-message.chat__own .v-message__content:after {
  content: "";
  position: absolute;
  right: -8px;
  bottom: 10px;
  width: 0;
  height: 0;
  border: 10px solid transparent;
  border-right-width: 0;
  border-left-color: #bad3ff;
}
.v-message__content {
  background: #bad3ff;
  border-radius: 4px;
  padding: 8px;
  position: relative;
  margin: 16px 0;
}

.v-user-chat {
  margin-bottom: 200px;
}

.v-user-chat .input__field {
  display: flex;
  justify-content: space-between;
  align-items: center;
  position: fixed;
  bottom: 70px;
  margin: 0 auto;
  right: 0;
  left: 0;
  background: #ffffff;
  padding: 0 32px;
}

.input__field .material-icons {
  font-size: 36px;
}

.v-user-chat__textfield {
  width: 78%;
  padding: 16px;
  border: 0;
  box-shadow: inset 0 0 5px 1px #bdbdbd;
  outline: transparent;
  border-radius: 24px;
  font-size: 20px;
}

li {
  list-style-type: none;
}
</style>
