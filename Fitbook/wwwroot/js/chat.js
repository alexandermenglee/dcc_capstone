﻿/*"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();
let username;
let message = {
    username: "",
    MessageText: ""
};
let Username = document.getElementById('userInput');
let Message = document.getElementById('messageInput');


//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    message.MessageText = document.getElementById("messageInput").value;
    console.log(message);

    connection.invoke("SendMessage", Username.value, Message.value).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});*/

"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();
const loggedInUser = document.getElementById("username").value;

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

function sendMessage() {
    var message = document.getElementById("messageInput").value;

    if (message === "") {
        alert("Cannot send blank msg");
        return;
    }

    connection.invoke("SendMessage", loggedInUser, message).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("messageInput").value = "";
}

document.getElementById("sendButton").addEventListener("click", sendMessage);

document.addEventListener("keypress", event => {
    if (event.keyCode === 13 || event.which === 13) {
        sendMessage();
    }
});

connection.on("RecieveMessage", function (user, message) {
    console.log(user);
    console.log(`logged in user is ${loggedInUser}`);
    var messageList = document.getElementById("messagesList");
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + ": " + msg;
    var pTag = document.createElement("p");

    if (loggedInUser === user) {
        pTag.setAttribute("class", "sent-speech-bubble");
    } else {
        pTag.setAttribute("class", "recieved-speech-bubble");
    }

    pTag.textContent = encodedMsg;
    messageList.appendChild(pTag);
});