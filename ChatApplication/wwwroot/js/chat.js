"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var connectedUsers = new Array();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage",
    function(user, message) {
        var encodedMsg = user + ": " + message;
        addOutputMessage(encodedMsg);
    });

connection.on("ReceiveOnlineUsers",
    function(onlineUsers) {
        connectedUsers = Array.from(onlineUsers);
        updateOnlineUsers();
    });

connection.on("UserLoggedIn",
    function(loggedInUser) {
        connectedUsers.push(loggedInUser);
        updateOnlineUsers();
        addOutputMessage(loggedInUser + " has just logged in.");
    }
);

connection.on("UserLoggedOut",
    function (loggedOutUser) {
        var indexOfUser = connectedUsers.indexOf(loggedOutUser);
        if (indexOfUser >= 0) {
            connectedUsers.splice(indexOfUser, 1);
            updateOnlineUsers();
        }
        
        addOutputMessage(loggedOutUser + " has just logged out.");
    }
);

connection.start().then(function() {
    document.getElementById("sendButton").disabled = false;
}).catch(function(err) {
    return console.error(err.toString());
});

function updateOnlineUsers() {
    var onlineUsersArea = document.getElementById("onlineUsers");
    onlineUsersArea.value = connectedUsers.join("\n");
};

function addOutputMessage(message) {
    var messageOutput = document.getElementById("messageOutput");

    if (!messageOutput.value.isEmpty()) {
        messageOutput.value += "\n";
    }

    messageOutput.value += message;
    $(messageOutput).scrollTop($(messageOutput)[0].scrollHeight);
};

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