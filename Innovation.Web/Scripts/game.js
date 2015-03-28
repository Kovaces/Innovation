function initialize() {
    receiveChat('server', 'initialize');

    receiveChat('server', 'send assignName');
    chat.server.assignName($('#displayname').val());

    chat.server.getCards().done(
        function (message) {
            var cards = JSON.parse(message);
            receiveChat('server', 'receive ' + cards.length + ' cards.');

            for (var c = 0; c < cards.length; c++) {
                cards[c].image = "/images/cards/" + paddy(cards[c].Age,2) + "/" + cards[c].Id.replace('C_','') + "-300x214.jpg";
                //cards[c].topIcon = getCardIcon(cards[c].Top);
                //cards[c].leftIcon = getCardIcon(cards[c].Left);
                //cards[c].centerIcon = getCardIcon(cards[c].Center);
                //cards[c].rightIcon = getCardIcon(cards[c].Right);
                //cards[c].bgcolor = getCardColor(cards[c].Color);
                //cards[c].colorText = getCardColorText(cards[c].Color).substring(0,1);
                //for (var a = 0; a < cards[c].Actions.length; a++) {
                //    cards[c].Actions[a].actionIcon = getCardIcon(cards[c].Actions[a].Symbol);

                //    if (cards[c].Actions[a].ActionText.indexOf('[') > -1) {
                //        var at = cards[c].Actions[a].ActionText;
                //        at = replaceAll(at, 'I demand ', 'I <b>demand</b> ');
                //        at = replaceAll(at, '\\[CLOCK\\]', '<img src="/images/clock.png" class="card-action-icon card-action-inline"/>');
                //        at = replaceAll(at, '\\[CROWN\\]', '<img src="/images/crown.png" class="card-action-icon card-action-inline"/>');
                //        at = replaceAll(at, '\\[LEAF\\]', '<img src="/images/leaf.png" class="card-action-icon card-action-inline"/>');
                //        at = replaceAll(at, '\\[LIGHTBULB\\]', '<img src="/images/lightbulb.png" class="card-action-icon card-action-inline"/>');
                //        at = replaceAll(at, '\\[TOWER\\]', '<img src="/images/tower.png" class="card-action-icon card-action-inline"/>');
                //        at = replaceAll(at, '\\[1\\]', '<span class="card-action-age">1</span>');
                //        at = replaceAll(at, '\\[2\\]', '<span class="card-action-age">2</span>');
                //        at = replaceAll(at, '\\[3\\]', '<span class="card-action-age">3</span>');
                //        at = replaceAll(at, '\\[4\\]', '<span class="card-action-age">4</span>');
                //        at = replaceAll(at, '\\[5\\]', '<span class="card-action-age">5</span>');
                //        at = replaceAll(at, '\\[6\\]', '<span class="card-action-age">6</span>');
                //        at = replaceAll(at, '\\[7\\]', '<span class="card-action-age">7</span>');
                //        at = replaceAll(at, '\\[8\\]', '<span class="card-action-age">8</span>');
                //        at = replaceAll(at, '\\[9\\]', '<span class="card-action-age">9</span>');
                //        at = replaceAll(at, '\\[10\\]', '<span class="card-action-age">10</span>');
                //        cards[c].Actions[a].ActionText = at;
                //    }
                //}
            }

            var scope = angular.element($("#gameWindow")).scope();
            scope.$apply(function () { scope.cards = cards; });

            //receiveChat('BLORK', 'out of getCards()');
            //var gameState = '{"ActivePlayer":null,"AgeAchievementDeck":{"Age":-1,"Cards":["C_Tools","C_RoadBuilding","C_Translation","C_Reformation","C_ThePirateCode","C_Vaccination","C_Sanitation","C_Socialism","C_Suburbia"]},"Id":"a27a1ea6-4827-48a3-9bdf-c0aa1cc735e9","Name":"sdfsdf","AgeDecks":[{"Age":1,"CardCount":11},{"Age":2,"CardCount":9},{"Age":3,"CardCount":9},{"Age":4,"CardCount":9},{"Age":5,"CardCount":9},{"Age":6,"CardCount":9},{"Age":7,"CardCount":8},{"Age":8,"CardCount":9},{"Age":9,"CardCount":9},{"Age":10,"CardCount":10}],"Players":[{"ActionsTaken":0,"Id":"b16adcb1-1536-479c-b8cc-fe77b4eb1251","Name":"one","Hand":["C_Domestication"],"Tableau":{"NumberOfAchievements":0,"ScorePile":[],"Stacks":[{"Color":1,"SplayedDirection":0,"Cards":[]},{"Color":2,"SplayedDirection":0,"Cards":[]},{"Color":3,"SplayedDirection":0,"Cards":[]},{"Color":4,"SplayedDirection":0,"Cards":[]},{"Color":5,"SplayedDirection":0,"Cards":["C_Agriculture"]}]}}]}'
            //if (gameState != '')
            //    game = JSON.parse(gameState);

            //scope.$apply(function () { scope.game = game; });

            //receiveChat('BLORK', 'game assigned');
        });
}

function terminate() {
    $chat.server.manualDisconnect();
}

function receivePlayerList(message) {
    if (typeof message !== "undefined") {
        receiveChat('server', 'receive playerList = -' + message + '-');
        var players = JSON.parse(message);

        var scope = angular.element($("#gameWindow")).scope();
        scope.$apply(function () { scope.players = players; });
    }
}

function syncGameState(message) {
    if (typeof message !== "undefined") {
        receiveChat('server', 'receive gameState = ' + message);
        //receiveChat('server', 'receive gameState = ' + message.length);

        var game = JSON.parse(message);
        var scope = angular.element($("#gameWindow")).scope();
        scope.$apply(function () { scope.game = game; });

        receiveChat('server', 'receive gameState done');
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
    var datetime = paddy(currentDate.getHours(), 2) + ':'
                    + paddy(currentDate.getMinutes(), 2) + ':'
                    + paddy(currentDate.getSeconds(), 2) + '&nbsp;';


    $('#discussion').append('<li><strong>' + encodedName + ": " + datetime
        + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');

    if (a + b == c)
        div.scrollTop(div[0].scrollHeight);
}



function showCreateGame() {
    //$('#chatParent').addClass('transparent');
    $('#createGameModal').css("z-index", 999);
    $('#createGameModal').modal('show');
    $('#gameName').val('');
    $('#selectedPlayers').val(currentPlayerId + ',');

    return false;
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

    if (numSelectedPlayers >= 4)
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
    if (playerIds.length < 1) {
        alert('select more people');
        return false;
    }

    var gameName = $('#gameName').val();
    if (gameName.length < 1) {
        alert('The game name must be at least 1 characters.');
        return false;
    }

    chat.server.createGame(gameName, playerIds);
    
    return false;
}

function getCardColor(colorId) {
    if (colorId == 1) return "#c8e4ff";
    else if (colorId == 2) return "#baeaa4";
    else if (colorId == 3) return "#be60ba";
    else if (colorId == 4) return "#d56261";
    else if (colorId == 5) return "#d9ff79";

    return "#000000";
}
function getCardColorText(colorId) {
    if (colorId == 1) return "Blue";
    else if (colorId == 2) return "Green";
    else if (colorId == 3) return "Purple";
    else if (colorId == 4) return "Red";
    else if (colorId == 5) return "Yellow";

    return "?";
}

function getCardIcon(iconId) {
    if (iconId == 1) return "/images/clock.png";
    else if (iconId == 2) return "/images/crown.png";
    else if (iconId == 3) return "/images/factory.png";
    else if (iconId == 4) return "/images/leaf.png";
    else if (iconId == 5) return "/images/lightbulb.png";
    else if (iconId == 6) return "/images/tower.png";

    return "images/blank.png";
}

function getDirection(direction) {
    if (direction == 1) return "Left";
    else if (direction == 2) return "Right";
    else if (direction == 3) return "Up";

    return "None";
}


function pickCard(cards, minSelect, maxSelect) {
    var str = 'Select ' + minSelect + '-' + maxSelect + ' from these cards: ';
    var cardList = JSON.parse(cards);
    var strb = '';
    for (var i = 0; i < cardList.length; i++)
        strb += (strb == '' ? '' : ', ') + cardList[i];
    receiveChat('CHOICES', str + strb);

    var question = {
        'type' : 'card',
        'minSelect' : minSelect,
        'maxSelect' : maxSelect,
        'cards': cardList,
        'text': 'Select ' + minSelect + (minSelect == maxSelect ? '' : '-' + maxSelect) + ' card' + (maxSelect > 1 ? 's' : '') + ' from the following:',
        'selectedItems': null,
    };

    var scope = angular.element($("#gameWindow")).scope();
    scope.$apply(function () { scope.question = question; });
}

function askQuestion(question) {
    receiveChat('CHOICES', 'Got question : ' + question);
}

function pickPlayers(players, minSelect, maxSelect) {
    var str = 'Select ' + minSelect + '-' + maxSelect + ' from these players: ';
    var playerList = JSON.parse(players);
    var strb = '';
    for (var i = 0; i < cards.length; i++)
        strb += (strb == '' ? '' : ', ') + playerList[i];
    receiveChat('CHOICES', str + strb);
}

function askToSplay(colors, direction) {
    var str = 'Splay any of these colors ' + getDirection(direction) + ' : ';
    var strb = '';
    for (var i = 0; i < colors.length; i++)
        strb += (strb == '' ? '' : ', ') + getCardColorText(colors[i]);
    receiveChat('CHOICES', str + strb);
}




function paddy(n, p, c) {
    var pad_char = typeof c !== 'undefined' ? c : '0';
    var pad = new Array(1 + p).join(pad_char);
    return (pad + n).slice(-pad.length);
}
function replaceAll(str, find, replace) {
    return str.replace(new RegExp(find, 'g'), replace);
}



function pickCardResponse(thing) {
    var cardDiv = $(thing);
    var selectedCards = $(".card-selected");
    var scope = angular.element($("#gameWindow")).scope();

    var isSelected = cardDiv.hasClass('card-selected');

    if (isSelected) {
        cardDiv.removeClass('card-selected');
        cardDiv.addClass('card-unselected');
    }

    if (selectedCards.length >= scope.question.maxSelect)
        return;

    if (!isSelected) {
        cardDiv.removeClass('card-unselected');
        cardDiv.addClass('card-selected');
    }
}

