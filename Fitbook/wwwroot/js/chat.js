/*"use strict";

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

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    /*var user = document.getElementById("userInput").value;*/
    var message = document.getElementById("messageInput").value;
    const user = document.getElementById("username").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    
    event.preventDefault();
});

connection.on("RecieveMessage", function (user, message) {
    var messageList = document.getElementById("messagesList");
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + ": " + msg;
    var pTag = document.createElement("p");
    pTag.textContent = encodedMsg;
    pTag.classList.add("speech-bubble");
    messageList.appendChild(pTag);
});