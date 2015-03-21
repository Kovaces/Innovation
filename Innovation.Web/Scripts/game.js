var cards = null;
var game = null;
var players = null;
var currentPlayerId = null;

function initialize() {
    receiveChat('server', 'initialize');

    receiveChat('server', 'send assignName');
    chat.server.assignName($('#displayname').val());

    chat.server.getCards().done(
        function (message) {
            cards = JSON.parse(message);
            receiveChat('server', 'receive ' + cards.length + ' cards.');
        });
}

function terminate() {
    $chat.server.manualDisconnect();
}

function receivePlayerList(message) {
    if (typeof message !== "undefined") {
        receiveChat('server', 'receive playerList = -' + message + '-');
        players = JSON.parse(message);

        var scope = angular.element($("#gameWindow")).scope();
        scope.$apply(function () { scope.players = players; });

        //$('#playerList').empty();
        //for (i = 0; i < players.length; i++)
        //{
        //    var node = document.createElement("li");
        //    node.appendChild(document.createTextNode(players[i].Name));
        //    $('#playerList').append(node);
        //}
    }
}

function syncGameState(message) {
    if (typeof message !== "undefined") {
        receiveChat('server', 'receive gameState = -' + message.substring(0,100) + '-');
        game = JSON.parse(message);
        $("#gameState").val(message);

        var scope = angular.element($("#gameWindow")).scope();
        scope.$apply(function () { scope.game = game; });
    }
}
function setGameId(message) {
    if (typeof message !== "undefined") {
        receiveChat('server', 'receive setGameId = -' + message + '-');

        $("#createGameModal").modal('hide');
        $("#chatParent").addClass("chat-parent-ingame");
        $("#chatParent").removeClass("chat-parent-pregame");
        //$('#chatParent').removeClass('transparent');
        $("#gameWindow").show(250);
        $('#gameWindow').removeClass('hidden');

        $('#chatMessages').scrollTop(div[0].scrollHeight);
    }
}


function chatKey(event) {
    if (event.keyCode == 13) {
        sendChatClick();
        return false;
    }
    return true;
}

function sendChatClick() {
    // Call the Send method on the hub. 
    if ($('#message').val() != '')
        sendMessage($('#displayname').val(), $('#message').val());
    // Clear text box and reset focus for next comment. 
    $('#message').val('').focus();
}

function sendMessage(name, message) {
    chat.server.send(name, message);
}


function showCreateGame() {
    //$('#chatParent').addClass('transparent');
    $('#createGameModal').css("z-index", 999);
    $('#createGameModal').modal('show');
    $('#gameName').val('');
    $('#selectedPlayers').val(currentPlayerId + ',');

    return false;
}

function receiveChat(name, message) {
    // Html encode display name and message. 
    var encodedName = $('<div />').text(name).html();
    var encodedMsg = $('<div />').text(message).html();
    // Add the message to the page. 
    div = $('#chatMessages');

    var a = div[0].scrollTop;
    var b = div.innerHeight();
    var c = div[0].scrollHeight;

    var currentDate = new Date();
    var datetime = paddy(currentDate.getHours(),2) + ':'
                    + paddy(currentDate.getMinutes(),2) + ':'
                    + paddy(currentDate.getSeconds(),2) + '&nbsp;';


    $('#discussion').append('<li><strong>' + encodedName + ": " + datetime
        + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');

    if (a + b == c)
        div.scrollTop(div[0].scrollHeight);
}

function selectCreateGamePlayer(p) {
    var ele = $(p);
    var listEle = $("#selectedPlayers");

    if (p.value == currentPlayerId)
        return false;

    var numSelectedPlayers = 0;
    ele.siblings().each(function() {
        if ($(this).hasClass('selected-player'))
            numSelectedPlayers++;
    });

    if (numSelectedPlayers >= 2)
        return;

    if (ele.hasClass('selected-player')) {
        listEle.val(listEle.val().replace(ele.val() + ',', ''));
        ele.addClass('unselected-player');
        ele.removeClass('selected-player');
    } else {
        listEle.val(listEle.val() + ele.val() + ',');
        ele.addClass('selected-player');
        ele.removeClass('unselected-player');
    }
}

function clickCreateGame() {
    var playerStrings = $('#selectedPlayers').val();
    playerStrings = playerStrings.substring(0, playerStrings.length - 1);

    var playerIds = playerStrings.split(',');
    if (playerIds.length < 2) {
        alert('select more people');
        return false;
    }

    var gameName = $('#gameName').val();
    if (gameName.length < 5) {
        alert('The game name must be at least 5 characters.');
        return false;
    }

    chat.server.createGame(gameName, playerIds);
    
    return false;
}



function paddy(n, p, c) {
    var pad_char = typeof c !== 'undefined' ? c : '0';
    var pad = new Array(1 + p).join(pad_char);
    return (pad + n).slice(-pad.length);
}
