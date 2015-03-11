
var cards = null;
function initialize() {
    chat.server.getCards().done(
        function (message) {
            receiveChat("system", message);

            cards = JSON.parse(message);
            alert(cards.length);
        });
}

function receiveCardList(message) {
    receiveChat("system", message);

    cards = JSON.parse(message);
    alert(cards.length);
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
    $('#chatParent').addClass('transparent');
    $('#createGameModal').css("z-index", 999);
    $('#createGameModal').show(250);
	$('#gameName').val('');
	$('#selectedPlayers').val('');

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
    var datetime = currentDate.getHours() + ':'
                    + currentDate.getMinutes() + ':'
                    + currentDate.getSeconds() + ':'
                    + currentDate.getMilliseconds() + '&nbsp;';


    $('#discussion').append('<li><strong>' + encodedName + ": " + datetime
        + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');

    if (a + b == c)
        div.scrollTop(div[0].scrollHeight);
}

function selectPlayer(p) {
    var ele = $(p);
    var listEle = $("#selectedPlayers");

    var thing = ele.val();

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
    sendMessage($('#displayname').val(), 'Create game with players (' + $('#selectedPlayers').val() + ')');
    return false;
}