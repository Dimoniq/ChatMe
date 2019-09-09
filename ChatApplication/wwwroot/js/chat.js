"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage",
    function(user, message) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = user + ": " + msg;
        var messageOutput = document.getElementById("messageOutput");
        if (!messageOutput.value.isEmpty()) {
            messageOutput.value += "\n";
        }
        messageOutput.value += encodedMsg;
        $(messageOutput).scrollTop($(messageOutput)[0].scrollHeight);
    });

connection.start().then(function() {
    document.getElementById("sendButton").disabled = false;
}).catch(function(err) {
    return console.error(err.toString());
});


function sendMessage() {
    var messageInputArea = document.getElementById("messageInput");
    var message = messageInputArea.value;
    if (message.isEmpty()) {
        return;
    }

    messageInputArea.value = "";
    messageInputArea.focus();
    var user = document.getElementById("usernameHidden").value;
    connection.invoke("SendMessage", user, message).catch(function(err) {
        return console.error(err.toString());
    });
};

document.getElementById("sendButton").addEventListener("click",
    function (event) {
        sendMessage();
        event.preventDefault();
    });


document.getElementById("messageInput").addEventListener("keypress",
    function(event) {
        var key = event.which || event.keyCode;
        if (key === 13) {
            sendMessage();
            event.preventDefault();
        }
    });


// ReSharper disable once NativeTypePrototypeExtending
String.prototype.isEmpty = function() {
    return (this.length === 0 || this.trim().length === 0);
};