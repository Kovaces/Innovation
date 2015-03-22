function initialize() {
    receiveChat('server', 'initialize');

    receiveChat('server', 'send assignName');
    chat.server.assignName($('#displayname').val());

    chat.server.getCards().done(
        function (message) {
            var cards = JSON.parse(message);
            receiveChat('server', 'receive ' + cards.length + ' cards.');

            for (var c = 0; c < cards.length; c++) {
                cards[c].topIcon = getCardIcon(cards[c].Top);
                cards[c].leftIcon = getCardIcon(cards[c].Left);
                cards[c].centerIcon = getCardIcon(cards[c].Center);
                cards[c].rightIcon = getCardIcon(cards[c].Right);
                cards[c].bgcolor = getCardColor(cards[c].Color);
                cards[c].colorText = getCardColorText(cards[c].Color);
                for (var a = 0; a < cards[c].Actions.length; a++)
                    cards[c].Actions[a].actionIcon = getCardIcon(cards[c].Actions[a].Symbol);
            }

            var scope = angular.element($("#gameWindow")).scope();
            scope.$apply(function () { scope.cards = cards; });

            //receiveChat('BLORK', 'out of getCards()');
            //var gameState = '{"Id":"00000000-0000-0000-0000-000000000000","Name":"5645447","Players":[{"Id":"95816a87-ab2a-4058-859f-f68a11a8a8f3","Name":"two","Tableau":{"Stacks":{"Blue":{"Cards":[],"SplayedDirection":0},"Green":{"Cards":[],"SplayedDirection":0},"Purple":{"Cards":[],"SplayedDirection":0},"Red":{"Cards":[],"SplayedDirection":0},"Yellow":{"Cards":[],"SplayedDirection":0}},"NumberOfAchievements":0,"ScorePile":[]},"Hand":[{"Name":"Archery","Age":1,"Color":4,"Top":6,"Left":5,"Center":0,"Right":6,"Actions":[{"ActionType":2,"Symbol":6,"ActionText":"I demand you draw a [1], then transfer the highest card in your hand to my hand!"}],"Id":"C_Archery"},{"Name":"Clothing","Age":1,"Color":2,"Top":0,"Left":2,"Center":4,"Right":4,"Actions":[{"ActionType":1,"Symbol":4,"ActionText":"Meld a card from your hand of different color from any card on your board."},{"ActionType":1,"Symbol":4,"ActionText":"Draw and score a [1] for each color present on your board not present on any other player\'s board."}],"Id":"C_Clothing"}],"Team":null,"ActionsTaken":0},{"Id":"2de60c9e-17e6-4b90-8925-7df9e990f6ea","Name":"one","Tableau":{"Stacks":{"Blue":{"Cards":[],"SplayedDirection":0},"Green":{"Cards":[],"SplayedDirection":0},"Purple":{"Cards":[],"SplayedDirection":0},"Red":{"Cards":[],"SplayedDirection":0},"Yellow":{"Cards":[],"SplayedDirection":0}},"NumberOfAchievements":0,"ScorePile":[]},"Hand":[{"Name":"City States","Age":1,"Color":3,"Top":0,"Left":2,"Center":2,"Right":6,"Actions":[{"ActionType":2,"Symbol":2,"ActionText":"I demand you transfer a top card with a [TOWER] from your board to my board if you have at least four [TOWER] on your board! If you do, draw a [1]!"}],"Id":"C_CityStates"},{"Name":"Code of Laws","Age":1,"Color":3,"Top":0,"Left":2,"Center":2,"Right":4,"Actions":[{"ActionType":0,"Symbol":2,"ActionText":"You may tuck a card from your hand of the same color as any card on your board. If you do, you may splay that color of your cards left."}],"Id":"C_CodeofLaws"}],"Team":null,"ActionsTaken":0}],"ActivePlayer":null,"AgeDecks":[{"Cards":[{"Name":"Domestication","Age":1,"Color":5,"Top":6,"Left":2,"Center":0,"Right":6,"Actions":[{"ActionType":1,"Symbol":6,"ActionText":"Meld the lowest card in your hand. Draw a [1]."}],"Id":"C_Domestication"},{"Name":"Masonry","Age":1,"Color":5,"Top":6,"Left":0,"Center":6,"Right":6,"Actions":[{"ActionType":0,"Symbol":6,"ActionText":"You may meld any number of cards from your hand, each with a [TOWER]. If you melded four or more cards in this way, claim the Monument achievement."}],"Id":"C_Masonry"},{"Name":"Metalworking","Age":1,"Color":4,"Top":6,"Left":6,"Center":0,"Right":6,"Actions":[{"ActionType":1,"Symbol":6,"ActionText":"Draw and reveal a [1]. If it has a [TOWER], score it and repeat this dogma effect. Otherwise, keep it."}],"Id":"C_Metalworking"},{"Name":"Mysticism","Age":1,"Color":3,"Top":0,"Left":6,"Center":6,"Right":6,"Actions":[{"ActionType":1,"Symbol":6,"ActionText":"Draw and reveal a [1]. If it is the same color as any card on your board, meld it and draw a [1]."}],"Id":"C_Mysticism"},{"Name":"Oars","Age":1,"Color":4,"Top":6,"Left":2,"Center":0,"Right":6,"Actions":[{"ActionType":2,"Symbol":6,"ActionText":"I demand you transfer a card with a [CROWN] from your hand to my score pile! If you do, draw a [1]!"},{"ActionType":1,"Symbol":6,"ActionText":"If no cards were transferred due to this demand, draw a [1]."}],"Id":"C_Oars"},{"Name":"Pottery","Age":1,"Color":1,"Top":0,"Left":4,"Center":4,"Right":4,"Actions":[{"ActionType":0,"Symbol":4,"ActionText":"You may return up to three cards from your hand. If you returned any cards, draw and score a card of value equal to the number of cards you returned."},{"ActionType":1,"Symbol":4,"ActionText":"Draw a [1]."}],"Id":"C_Pottery"},{"Name":"Sailing","Age":1,"Color":2,"Top":2,"Left":2,"Center":0,"Right":4,"Actions":[{"ActionType":1,"Symbol":2,"ActionText":"Draw and meld a [1]."}],"Id":"C_Sailing"},{"Name":"The Wheel","Age":1,"Color":2,"Top":0,"Left":6,"Center":6,"Right":6,"Actions":[{"ActionType":1,"Symbol":6,"ActionText":"Draw two [1]."}],"Id":"C_TheWheel"},{"Name":"Tools","Age":1,"Color":1,"Top":0,"Left":5,"Center":5,"Right":6,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may return three cards from your hand. If you do, draw and meld a [3]."},{"ActionType":0,"Symbol":5,"ActionText":"You may return a [3] from your hand. If you do, draw three [1]."}],"Id":"C_Tools"}],"Age":1},{"Cards":[{"Name":"Canal Building","Age":2,"Color":5,"Top":0,"Left":2,"Center":4,"Right":2,"Actions":[{"ActionType":0,"Symbol":2,"ActionText":"You may exchange all the highest cards in your hand with all the highest cards in your score pile."}],"Id":"C_CanalBuilding"},{"Name":"Construction","Age":2,"Color":4,"Top":6,"Left":0,"Center":6,"Right":6,"Actions":[{"ActionType":2,"Symbol":6,"ActionText":"I demand you transfer two cards from your hand to my hand! Draw a [2]!"},{"ActionType":1,"Symbol":6,"ActionText":"If you are the only player with five top cards, claim the Empire achievement."}],"Id":"C_Construction"},{"Name":"Currency","Age":2,"Color":2,"Top":4,"Left":2,"Center":0,"Right":2,"Actions":[{"ActionType":0,"Symbol":2,"ActionText":"You may return any number of cards from your hand. If you do, draw and score a [2] for every different value card you returned."}],"Id":"C_Currency"},{"Name":"Fermenting","Age":2,"Color":5,"Top":4,"Left":4,"Center":0,"Right":6,"Actions":[{"ActionType":1,"Symbol":4,"ActionText":"Draw a [2] for every two [LEAF] icons on your board."}],"Id":"C_Fermenting"},{"Name":"Mapmaking","Age":2,"Color":2,"Top":0,"Left":2,"Center":2,"Right":6,"Actions":[{"ActionType":2,"Symbol":2,"ActionText":"I demand you transfer a [1] from your score pile, if it has any, to my score pile!"},{"ActionType":1,"Symbol":2,"ActionText":"If any card was transferred due to the demand, draw and score a [1]."}],"Id":"C_Mapmaking"},{"Name":"Mathematics","Age":2,"Color":1,"Top":0,"Left":5,"Center":2,"Right":5,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may return a card from your hand. If you do, draw and meld a card of value one higher than the card you returned."}],"Id":"C_Mathematics"},{"Name":"Monotheism","Age":2,"Color":3,"Top":0,"Left":6,"Center":6,"Right":6,"Actions":[{"ActionType":2,"Symbol":6,"ActionText":"I demand you transfer a top card on your board of different color from any card on my board to my score pile! If you do, draw and tuck a [1]!"},{"ActionType":1,"Symbol":6,"ActionText":"Draw and tuck a [1]."}],"Id":"C_Monotheism"},{"Name":"Philosophy","Age":2,"Color":3,"Top":0,"Left":5,"Center":5,"Right":5,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may splay left any one color of your cards."},{"ActionType":0,"Symbol":5,"ActionText":"You may score a card from your hand."}],"Id":"C_Philosophy"},{"Name":"Road Building","Age":2,"Color":4,"Top":6,"Left":6,"Center":0,"Right":6,"Actions":[{"ActionType":1,"Symbol":6,"ActionText":"Meld one or two cards from your hand. If you melded two, you may transfer your top red card to another player\'s board. If you do, transfer that player\'s top green card to your board."}],"Id":"C_RoadBuilding"}],"Age":2},{"Cards":[{"Name":"Compass","Age":3,"Color":2,"Top":0,"Left":2,"Center":2,"Right":4,"Actions":[{"ActionType":2,"Symbol":2,"ActionText":"I demand you transfer a top non-green card with a [LEAF] from your board to my board, and then you transfer a top card without a [LEAF] from my board to your board."}],"Id":"C_Compass"},{"Name":"Education","Age":3,"Color":3,"Top":5,"Left":5,"Center":5,"Right":0,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may return the highest card from your score pile. If you do, draw a card of value two higher than the highest card remaining in your score pile."}],"Id":"C_Education"},{"Name":"Engineering","Age":3,"Color":4,"Top":6,"Left":0,"Center":5,"Right":6,"Actions":[{"ActionType":2,"Symbol":6,"ActionText":"I demand you transfer all top cards with a [TOWER] from your board to my score pile!"},{"ActionType":0,"Symbol":6,"ActionText":"You may splay your red cards left."}],"Id":"C_Engineering"},{"Name":"Feudalism","Age":3,"Color":3,"Top":0,"Left":6,"Center":4,"Right":6,"Actions":[{"ActionType":2,"Symbol":6,"ActionText":"I demand you transfer a card with a [TOWER] from your hand to my hand!"},{"ActionType":0,"Symbol":6,"ActionText":"You may splay your yellow or purple cards left."}],"Id":"C_Feudalism"},{"Name":"Machinery","Age":3,"Color":5,"Top":4,"Left":4,"Center":0,"Right":6,"Actions":[{"ActionType":2,"Symbol":4,"ActionText":"I demand you exchange all the cards in your hand with all the highest cards in my hand!"},{"ActionType":1,"Symbol":4,"ActionText":"Score a card from your hand with a [TOWER]. You may splay your red cards left."}],"Id":"C_Machinery"},{"Name":"Medicine","Age":3,"Color":5,"Top":2,"Left":4,"Center":4,"Right":0,"Actions":[{"ActionType":2,"Symbol":4,"ActionText":"I demand you exchange the highest card in your score pile with the lowest card in my score pile!"}],"Id":"C_Medicine"},{"Name":"Optics","Age":3,"Color":4,"Top":2,"Left":2,"Center":2,"Right":0,"Actions":[{"ActionType":1,"Symbol":2,"ActionText":"Draw and meld a [3]. If it has a [CROWN], draw and score a [4]. Otherwise, transfer a card from your score pile to the score pile of an opponent with fewer points than you."}],"Id":"C_Optics"},{"Name":"Paper","Age":3,"Color":2,"Top":0,"Left":5,"Center":5,"Right":2,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may splay your green or blue cards left."},{"ActionType":1,"Symbol":5,"ActionText":"Draw a [4] for every color you have splayed left."}],"Id":"C_Paper"},{"Name":"Translation","Age":3,"Color":1,"Top":0,"Left":2,"Center":2,"Right":2,"Actions":[{"ActionType":0,"Symbol":2,"ActionText":"You may meld all the cards in your score pile. If you meld one, you must meld them all."},{"ActionType":1,"Symbol":2,"ActionText":"If each top card on your board has a [CROWN], claim the World achievement."}],"Id":"C_Translation"}],"Age":3},{"Cards":[{"Name":"Colonialism","Age":4,"Color":4,"Top":0,"Left":3,"Center":5,"Right":3,"Actions":[{"ActionType":1,"Symbol":3,"ActionText":"Draw and tuck a [3]. If it has a [CROWN], repeat this dogma effect."}],"Id":"C_Colonialism"},{"Name":"Enterprise","Age":4,"Color":3,"Top":0,"Left":2,"Center":2,"Right":2,"Actions":[{"ActionType":2,"Symbol":2,"ActionText":"I demand you transfer a top non-purple card with a [CROWN] from your board to my board! If you do, draw and meld a [4]!"},{"ActionType":0,"Symbol":2,"ActionText":"You may splay your green cards right."}],"Id":"C_Enterprise"},{"Name":"Experimentation","Age":4,"Color":1,"Top":0,"Left":5,"Center":5,"Right":5,"Actions":[{"ActionType":1,"Symbol":5,"ActionText":"Draw and meld a [5]."}],"Id":"C_Experimentation"},{"Name":"Gunpowder","Age":4,"Color":4,"Top":0,"Left":3,"Center":2,"Right":3,"Actions":[{"ActionType":2,"Symbol":3,"ActionText":"I demand you transfer a top card with a [TOWER] from your board to my score pile!"},{"ActionType":1,"Symbol":3,"ActionText":"If any card was transferred due to the demand, draw and score a [2]."}],"Id":"C_Gunpowder"},{"Name":"Invention","Age":4,"Color":4,"Top":0,"Left":5,"Center":5,"Right":3,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may splay right any one color of your cards currently splayed left. If you do, draw and score a [4]."},{"ActionType":1,"Symbol":5,"ActionText":"If you have five colors splayed, each in any direction, claim the Wonder achievement."}],"Id":"C_Invention"},{"Name":"Navigation","Age":4,"Color":4,"Top":0,"Left":2,"Center":2,"Right":2,"Actions":[{"ActionType":2,"Symbol":2,"ActionText":"I demand you transfer a [2] or [3] from your score pile, if it has any, to my score pile!"}],"Id":"C_Navigation"},{"Name":"Perspective","Age":4,"Color":5,"Top":0,"Left":5,"Center":5,"Right":4,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may return a card from your hand. If you do, score a card from your hand for every two [BULB] on your board."}],"Id":"C_Perspective"},{"Name":"Printing Press","Age":4,"Color":1,"Top":0,"Left":5,"Center":5,"Right":2,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may return a card from your score pile. If you do, draw a card of value two higher than the top purple card on your board."},{"ActionType":0,"Symbol":5,"ActionText":"You may splay your blue cards right."}],"Id":"C_PrintingPress"},{"Name":"Reformation","Age":4,"Color":3,"Top":4,"Left":4,"Center":0,"Right":4,"Actions":[{"ActionType":0,"Symbol":4,"ActionText":"You may tuck a card from your hand for every two [LEAF] on your board."},{"ActionType":0,"Symbol":4,"ActionText":"You may splay your yellow or purple cards right."}],"Id":"C_Reformation"}],"Age":4},{"Cards":[{"Name":"Banking","Age":5,"Color":2,"Top":3,"Left":2,"Center":0,"Right":2,"Actions":[{"ActionType":2,"Symbol":2,"ActionText":"I demand you transfer a top non-green card with a [FACTORY] from your board to my board. If you do, draw and score a [5]!"},{"ActionType":0,"Symbol":2,"ActionText":"You may splay your green cards right."}],"Id":"C_Banking"},{"Name":"Chemistry","Age":5,"Color":1,"Top":3,"Left":5,"Center":3,"Right":0,"Actions":[{"ActionType":0,"Symbol":3,"ActionText":"You may splay your blue cards right."},{"ActionType":1,"Symbol":3,"ActionText":"Draw and score a card of value one higher than the highest top card on your board and then return a card from your score pile."}],"Id":"C_Chemistry"},{"Name":"Coal","Age":5,"Color":4,"Top":3,"Left":3,"Center":3,"Right":0,"Actions":[{"ActionType":1,"Symbol":3,"ActionText":"Draw and tuck a [5]."},{"ActionType":0,"Symbol":3,"ActionText":"You may splay your red cards right."},{"ActionType":0,"Symbol":3,"ActionText":"You may score any one of your top cards. If you do, also score the card beneath it."}],"Id":"C_Coal"},{"Name":"Measurement","Age":5,"Color":2,"Top":5,"Left":4,"Center":5,"Right":0,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may return a card from your hand. If you do, splay any one color of your cards right, and draw a card of value equal to the number of cards of that color on your board."}],"Id":"C_Measurement"},{"Name":"Physics","Age":5,"Color":1,"Top":3,"Left":5,"Center":5,"Right":0,"Actions":[{"ActionType":1,"Symbol":5,"ActionText":"Draw three [6] and reveal them. If two or more of the drawn cards are the same color, return the drawn cards and all the cards in your hand. Otherwise, keep them."}],"Id":"C_Physics"},{"Name":"Societies","Age":5,"Color":3,"Top":2,"Left":0,"Center":5,"Right":2,"Actions":[{"ActionType":2,"Symbol":2,"ActionText":"I demand you transfer a top non-purple card with a [BULB] from your board to my board! If you do, draw a [5]!"}],"Id":"C_Societies"},{"Name":"Statistics","Age":5,"Color":5,"Top":4,"Left":5,"Center":4,"Right":0,"Actions":[{"ActionType":2,"Symbol":4,"ActionText":"I demand you transfer the highest card in your score pile to your hand! If you do, and have only one card in your hand afterwards, repeat this demand!"},{"ActionType":0,"Symbol":4,"ActionText":"You may splay your yellow cards right."}],"Id":"C_Statistics"},{"Name":"Steam Engine","Age":5,"Color":5,"Top":0,"Left":3,"Center":2,"Right":3,"Actions":[{"ActionType":1,"Symbol":3,"ActionText":"Draw and tuck two [4], then score your bottom yellow card."}],"Id":"C_SteamEngine"},{"Name":"The Pirate Code","Age":5,"Color":4,"Top":2,"Left":3,"Center":2,"Right":0,"Actions":[{"ActionType":2,"Symbol":2,"ActionText":"I demand you transfer two cards of value [4] or less from your score pile to my score pile!"},{"ActionType":1,"Symbol":2,"ActionText":"If any card was transferred due to the demand, score the lowest top card with a [CROWN] from your board."}],"Id":"C_ThePirateCode"}],"Age":5},{"Cards":[{"Name":"Canning","Age":6,"Color":5,"Top":0,"Left":3,"Center":4,"Right":3,"Actions":[{"ActionType":0,"Symbol":3,"ActionText":"You may draw and tuck a [6]. If you do, score all your top cards without a [FACTORY]"},{"ActionType":0,"Symbol":3,"ActionText":"You may splay your yellow cards right."}],"Id":"C_Canning"},{"Name":"Classification","Age":6,"Color":2,"Top":5,"Left":5,"Center":5,"Right":0,"Actions":[{"ActionType":1,"Symbol":5,"ActionText":"Reveal the color of a card from your hand. Take into your hand all cards of that color from all other player\'s hands. Then, meld all cards of that color from your hand."}],"Id":"C_Classification"},{"Name":"Democracy","Age":6,"Color":3,"Top":2,"Left":5,"Center":5,"Right":0,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may return any number of cards from your hand. If you have returned more cards than any other player due to Democracy so far during this dogma action, draw and score an [8]."}],"Id":"C_Democracy"},{"Name":"Emancipation","Age":6,"Color":3,"Top":3,"Left":5,"Center":3,"Right":0,"Actions":[{"ActionType":2,"Symbol":3,"ActionText":"I demand you transfer a card from your hand to my score pile! If you do, draw a [6]!"},{"ActionType":0,"Symbol":3,"ActionText":"You may splay your red or puple cards right."}],"Id":"C_Emancipation"},{"Name":"Encyclopedia","Age":6,"Color":1,"Top":0,"Left":2,"Center":2,"Right":2,"Actions":[{"ActionType":0,"Symbol":2,"ActionText":"You may meld all the highest cards in your score pile. If you meld one of the highest, you must meld all of the highest."}],"Id":"C_Encyclopedia"},{"Name":"Industrialization","Age":6,"Color":4,"Top":2,"Left":3,"Center":3,"Right":0,"Actions":[{"ActionType":1,"Symbol":3,"ActionText":"Draw and tuck a [6] for every two [FACTORY] on your board."},{"ActionType":0,"Symbol":3,"ActionText":"You may splay your red or purple cards right."}],"Id":"C_Industrialization"},{"Name":"Machine Tools","Age":6,"Color":4,"Top":3,"Left":3,"Center":0,"Right":3,"Actions":[{"ActionType":1,"Symbol":3,"ActionText":"Draw and score a card of value equal to the highest card in your score pile."}],"Id":"C_MachineTools"},{"Name":"Metric System","Age":6,"Color":2,"Top":0,"Left":3,"Center":2,"Right":2,"Actions":[{"ActionType":1,"Symbol":2,"ActionText":"If your green cards are splayed right, you may splay any one color of your cards right."},{"ActionType":0,"Symbol":2,"ActionText":"You may splay your green cards right."}],"Id":"C_MetricSystem"},{"Name":"Vaccination","Age":6,"Color":5,"Top":4,"Left":3,"Center":4,"Right":0,"Actions":[{"ActionType":2,"Symbol":4,"ActionText":"I demand you return all the lowest cards in your score pile! If you returned any, draw and meld a [6]!"},{"ActionType":1,"Symbol":4,"ActionText":"If any card was returned as a result of the demand, draw and meld a [7]."}],"Id":"C_Vaccination"}],"Age":6},{"Cards":[{"Name":"Combustion","Age":7,"Color":4,"Top":2,"Left":2,"Center":3,"Right":0,"Actions":[{"ActionType":2,"Symbol":2,"ActionText":"I demand you transfer two cards from your score pile to my score pile!"}],"Id":"C_Combustion"},{"Name":"Electricity","Age":7,"Color":2,"Top":5,"Left":3,"Center":0,"Right":3,"Actions":[{"ActionType":1,"Symbol":3,"ActionText":"Return all your top cards without a [FACTORY], then draw an [8] for each card you returned."}],"Id":"C_Electricity"},{"Name":"Publications","Age":7,"Color":1,"Top":0,"Left":5,"Center":1,"Right":5,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may rearrange the order of one color of cards on your board."},{"ActionType":0,"Symbol":5,"ActionText":"You may splay your yellow or blue cards up."}],"Id":"C_Publications"},{"Name":"Explosives","Age":7,"Color":4,"Top":0,"Left":3,"Center":3,"Right":3,"Actions":[{"ActionType":2,"Symbol":3,"ActionText":"I demand you transfer the three highest cards from your hand to my hand! If you transferred any, and then have no cards in hand, draw a [7]!"}],"Id":"C_Explosives"},{"Name":"Lightning","Age":7,"Color":3,"Top":0,"Left":4,"Center":1,"Right":4,"Actions":[{"ActionType":0,"Symbol":4,"ActionText":"You may tuck up to three cards from your hand. If you do, draw and score a [7] for every different value of card you tucked."}],"Id":"C_Lightning"},{"Name":"Railroad","Age":7,"Color":3,"Top":1,"Left":3,"Center":1,"Right":0,"Actions":[{"ActionType":1,"Symbol":1,"ActionText":"Return all cards from your hand, then draw three [6]."},{"ActionType":0,"Symbol":1,"ActionText":"You may splay up any one color of your cards currently splayed right."}],"Id":"C_Railroad"},{"Name":"Refridgeration","Age":7,"Color":5,"Top":0,"Left":4,"Center":4,"Right":2,"Actions":[{"ActionType":2,"Symbol":4,"ActionText":"I demand you return half (rounded down) of the cards in your hand!"},{"ActionType":0,"Symbol":4,"ActionText":"You may score a card from your hand."}],"Id":"C_Refridgeration"},{"Name":"Sanitation","Age":7,"Color":5,"Top":4,"Left":4,"Center":0,"Right":4,"Actions":[{"ActionType":2,"Symbol":4,"ActionText":"I demand you exchange the two highest cards in your hand with the lowest card in my hand!"}],"Id":"C_Sanitation"}],"Age":7},{"Cards":[{"Name":"Corporations","Age":8,"Color":2,"Top":0,"Left":3,"Center":3,"Right":2,"Actions":[{"ActionType":2,"Symbol":3,"ActionText":"I demand you transfer a top non-green card with a [FACTORY] from your board to my score pile! If you do, draw and meld an [8]!"},{"ActionType":1,"Symbol":3,"ActionText":"Draw and meld an [8]."}],"Id":"C_Corporations"},{"Name":"Empiricism","Age":8,"Color":3,"Top":5,"Left":5,"Center":5,"Right":0,"Actions":[{"ActionType":1,"Symbol":5,"ActionText":"Choose two colors, then draw and reveal a [9]. If it is either of the colors you chose, meld it and you may splay your cards of that color up."},{"ActionType":1,"Symbol":5,"ActionText":"If you have twenty or more [BULB] on your board, you win."}],"Id":"C_Empiricism"},{"Name":"Flight","Age":8,"Color":4,"Top":2,"Left":0,"Center":1,"Right":2,"Actions":[{"ActionType":0,"Symbol":2,"ActionText":"If you red cards are splayed up, you may splay any one color of your cards up."},{"ActionType":0,"Symbol":2,"ActionText":"You may splay your red cards up."}],"Id":"C_Flight"},{"Name":"Mass Media","Age":8,"Color":2,"Top":5,"Left":0,"Center":1,"Right":5,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may return a card from your hand. If you do, choose a value, and return all cards of that value from all score piles."},{"ActionType":0,"Symbol":5,"ActionText":"You may splay your purple cards up."}],"Id":"C_MassMedia"},{"Name":"Mobility","Age":8,"Color":4,"Top":0,"Left":3,"Center":1,"Right":3,"Actions":[{"ActionType":2,"Symbol":3,"ActionText":"I demand you transfer your two highest non-red top cards without a [FACTORY] from your board to my score pile! If you transferred any cards, draw an [8]!"}],"Id":"C_Mobility"},{"Name":"Quantum Theory","Age":8,"Color":1,"Top":1,"Left":1,"Center":1,"Right":0,"Actions":[{"ActionType":0,"Symbol":1,"ActionText":"You may return up to two cards from your hand. If you return two, draw a [10] and then draw and score a [10]."}],"Id":"C_QuantumTheory"},{"Name":"Rocketry","Age":8,"Color":1,"Top":1,"Left":1,"Center":1,"Right":0,"Actions":[{"ActionType":1,"Symbol":1,"ActionText":"Return a card in any other player\'s score pile for every two [CLOCK] on your board."}],"Id":"C_Rocketry"},{"Name":"Skyscrapers","Age":8,"Color":5,"Top":0,"Left":3,"Center":2,"Right":2,"Actions":[{"ActionType":2,"Symbol":2,"ActionText":"I demand you transfer a top non-yellow card with a [CLOCK] from your board to my board! If you do, score the card beneath it, and return all other cards from that pile!"}],"Id":"C_Skyscrapers"},{"Name":"Socialism","Age":8,"Color":3,"Top":4,"Left":0,"Center":4,"Right":4,"Actions":[{"ActionType":0,"Symbol":4,"ActionText":"You may tuck all cards from your hand. If you tuck one, you must tuck them all. If you tucked at least one purple card, take all the lowest cards in each other player\'s hand into your hand."}],"Id":"C_Socialism"}],"Age":8},{"Cards":[{"Name":"Composites","Age":9,"Color":4,"Top":3,"Left":3,"Center":0,"Right":3,"Actions":[{"ActionType":2,"Symbol":3,"ActionText":"I demand you transfer all but one card from your hand to my hand! Also, transfer the highest card from your score pile to my score pile!"}],"Id":"C_Composites"},{"Name":"Computers","Age":9,"Color":1,"Top":1,"Left":0,"Center":1,"Right":3,"Actions":[{"ActionType":0,"Symbol":1,"ActionText":"You may splay your red cards or your green cards up."},{"ActionType":1,"Symbol":1,"ActionText":"Draw and meld a [10], then execute each of its non-demand dogma effects. Do not share them."}],"Id":"C_Computers"},{"Name":"Ecology","Age":9,"Color":5,"Top":4,"Left":5,"Center":5,"Right":0,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may return a card from your hand. If you do, score a card from your hand and draw two [10]."}],"Id":"C_Ecology"},{"Name":"Fission","Age":9,"Color":4,"Top":0,"Left":1,"Center":1,"Right":1,"Actions":[{"ActionType":2,"Symbol":1,"ActionText":"I demand you draw a [10]! If it is red, remove all hands, boards, and score piles from the game! If this occurs, the dogma action is complete."},{"ActionType":1,"Symbol":1,"ActionText":"Return a top card other than Fission from any player\'s board."}],"Id":"C_Fission"},{"Name":"Genetics","Age":9,"Color":1,"Top":5,"Left":5,"Center":5,"Right":0,"Actions":[{"ActionType":1,"Symbol":5,"ActionText":"Draw and meld a [10]. Score all cards beneath it."}],"Id":"C_Genetics"},{"Name":"Satellites","Age":9,"Color":2,"Top":0,"Left":1,"Center":1,"Right":1,"Actions":[{"ActionType":1,"Symbol":1,"ActionText":"Return all cards from your hand, and draw three [8]."},{"ActionType":0,"Symbol":1,"ActionText":"You may splay your purple cards up."},{"ActionType":1,"Symbol":1,"ActionText":"Meld a card from your hand and then execute each of its non-demand dogma effects. Do not share them"}],"Id":"C_Satellites"},{"Name":"Services","Age":9,"Color":3,"Top":0,"Left":4,"Center":4,"Right":4,"Actions":[{"ActionType":2,"Symbol":4,"ActionText":"I demand you transfer all the highest cards from your score pile to my hand! If you transferred any cards, then transfer a top card from my board without a [LEAF] to your hand!"}],"Id":"C_Services"},{"Name":"Specialization","Age":9,"Color":3,"Top":0,"Left":3,"Center":4,"Right":3,"Actions":[{"ActionType":1,"Symbol":3,"ActionText":"Reveal a card from your hand. Take into your hand the top card of that color from all other players\' boards."},{"ActionType":0,"Symbol":3,"ActionText":"You may splay your yellow or blue cards up."}],"Id":"C_Specialization"},{"Name":"Suburbia","Age":9,"Color":5,"Top":0,"Left":2,"Center":4,"Right":4,"Actions":[{"ActionType":0,"Symbol":4,"ActionText":"You may tuck any number of cards from your hand. Draw and score a [1] for each card you tucked."}],"Id":"C_Suburbia"}],"Age":9},{"Cards":[{"Name":"A.I.","Age":10,"Color":3,"Top":5,"Left":5,"Center":1,"Right":0,"Actions":[{"ActionType":1,"Symbol":5,"ActionText":"Draw and score a [10]."},{"ActionType":1,"Symbol":5,"ActionText":"If Robotics and Software are top cards on any board, the single player with the lowest score wins."}],"Id":"C_AI"},{"Name":"Bioengineering","Age":10,"Color":1,"Top":5,"Left":1,"Center":1,"Right":0,"Actions":[{"ActionType":1,"Symbol":1,"ActionText":"Transfer a top card with a [LEAF] from any other player\'s board to your score pile."},{"ActionType":1,"Symbol":1,"ActionText":"If any player has fewer than three [LEAF] on their board, the single player with the most [LEAF] on their board wins."}],"Id":"C_Bioengineering"},{"Name":"Databases","Age":10,"Color":2,"Top":0,"Left":1,"Center":1,"Right":1,"Actions":[{"ActionType":2,"Symbol":1,"ActionText":"I demand you return half (rounded up) of the cards in your score pile!"}],"Id":"C_Databases"},{"Name":"Globalization","Age":10,"Color":5,"Top":0,"Left":3,"Center":3,"Right":3,"Actions":[{"ActionType":2,"Symbol":3,"ActionText":"I demand you return a top card with a [LEAF] on your board."},{"ActionType":1,"Symbol":3,"ActionText":"Draw and score a [6]. If no player has more [LEAF] than [FACTORY] on their board, the single player with the most points wins."}],"Id":"C_Globalization"},{"Name":"Miniaturization","Age":10,"Color":4,"Top":0,"Left":5,"Center":1,"Right":5,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may return a card from your hand. If you returned a [10] draw a [10] for every different value card in your score pile."}],"Id":"C_Miniaturization"},{"Name":"Robotics","Age":10,"Color":4,"Top":0,"Left":3,"Center":1,"Right":3,"Actions":[{"ActionType":1,"Symbol":3,"ActionText":"Score your top green card. Draw and meld a [10], then execute each of its non-demand dogma effects. Do not share them."}],"Id":"C_Robotics"},{"Name":"Self Service","Age":10,"Color":2,"Top":0,"Left":2,"Center":2,"Right":2,"Actions":[{"ActionType":1,"Symbol":2,"ActionText":"Execute each of the non-demand dogma effects of any other top card on your board. Do not share them."},{"ActionType":1,"Symbol":2,"ActionText":"If you have more achievements than any other player, you win."}],"Id":"C_SelfService"},{"Name":"Software","Age":10,"Color":1,"Top":1,"Left":1,"Center":1,"Right":0,"Actions":[{"ActionType":1,"Symbol":1,"ActionText":"Draw and score a [10]."},{"ActionType":1,"Symbol":1,"ActionText":"Draw and meld two [10], then execute each of the second card\'s non-demand dogma effects. Do not share them."}],"Id":"C_Software"},{"Name":"Stem Cells","Age":10,"Color":5,"Top":0,"Left":4,"Center":4,"Right":4,"Actions":[{"ActionType":0,"Symbol":4,"ActionText":"You may score all cards from your hand. If you score one, you must score them all."}],"Id":"C_StemCells"},{"Name":"The Internet","Age":10,"Color":3,"Top":0,"Left":1,"Center":1,"Right":5,"Actions":[{"ActionType":0,"Symbol":1,"ActionText":"You may splay your green cards up."},{"ActionType":1,"Symbol":1,"ActionText":"Draw and score a [10]."},{"ActionType":1,"Symbol":1,"ActionText":"Draw and meld a [10] for every two [CLOCK] on your board."}],"Id":"C_TheInternet"}],"Age":10}],"AgeAchievementDeck":{"Cards":[{"Name":"Agriculture","Age":1,"Color":5,"Top":0,"Left":4,"Center":4,"Right":4,"Actions":[{"ActionType":0,"Symbol":4,"ActionText":"You may return a card from your hand. If you do, draw and score a card of value one higher than the card you returned."}],"Id":"C_Agriculture"},{"Name":"Calendar","Age":2,"Color":1,"Top":0,"Left":4,"Center":4,"Right":5,"Actions":[{"ActionType":1,"Symbol":4,"ActionText":"If you have more cards in your score pile than in your hand, draw two [3]."}],"Id":"C_Calendar"},{"Name":"Alchemy","Age":3,"Color":1,"Top":0,"Left":4,"Center":6,"Right":6,"Actions":[{"ActionType":1,"Symbol":6,"ActionText":"Draw and reveal a [4] for every three [TOWER] on your board. If any of the drawn cards are red, return the cards drawn and return all cards in your hand. Otherwise, keep them."},{"ActionType":1,"Symbol":6,"ActionText":"Meld a card from your hand, then score a card from your hand."}],"Id":"C_Alchemy"},{"Name":"Anatomy","Age":4,"Color":5,"Top":4,"Left":4,"Center":4,"Right":0,"Actions":[{"ActionType":2,"Symbol":4,"ActionText":"I demand you return a card from your score pile! If you do, return a top card of equal value from your board!"}],"Id":"C_Anatomy"},{"Name":"Astronomy","Age":5,"Color":3,"Top":2,"Left":5,"Center":5,"Right":0,"Actions":[{"ActionType":1,"Symbol":5,"ActionText":"Draw and reveal a [6]. If the card is green or blue, meld it and repeat this dogma effect."},{"ActionType":1,"Symbol":5,"ActionText":"If all the non-purple top cards on your board are value [6] or higher, claim the Universe achievement."}],"Id":"C_Astronomy"},{"Name":"Atomic Theory","Age":6,"Color":1,"Top":5,"Left":5,"Center":5,"Right":0,"Actions":[{"ActionType":0,"Symbol":5,"ActionText":"You may splay your blue cards right."},{"ActionType":1,"Symbol":5,"ActionText":"Draw and meld a [7]."}],"Id":"C_AtomicTheory"},{"Name":"Bicycle","Age":7,"Color":2,"Top":2,"Left":2,"Center":1,"Right":0,"Actions":[{"ActionType":0,"Symbol":2,"ActionText":"You may exchange all cards in your hand with all the cards in your score pile. If you exchange one, you must exchange them all."}],"Id":"C_Bicycle"},{"Name":"Antibiotics","Age":8,"Color":5,"Top":4,"Left":4,"Center":4,"Right":0,"Actions":[{"ActionType":0,"Symbol":4,"ActionText":"You may return up to three cards from your hand. For every different value of card that you returned, draw two [8]."}],"Id":"C_Antibiotics"},{"Name":"Collaboration","Age":9,"Color":2,"Top":0,"Left":2,"Center":1,"Right":2,"Actions":[{"ActionType":2,"Symbol":2,"ActionText":"I demand you draw two [9] and reveal them! Transfer the card of my choice to my board, and meld the other!"},{"ActionType":1,"Symbol":2,"ActionText":"If you have ten or more green cards on your board, you win."}],"Id":"C_Collaboration"}],"Age":-1},"GameEnded":false}'
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
        var game = JSON.parse(message);
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

function getCardColor(colorId) {
    if (colorId == 1) return "#decafa";
    else if (colorId == 2) return "#baeaa4";
    else if (colorId == 3) return "#decafa";
    else if (colorId == 4) return "#f5a2a2";
    else if (colorId == 5) return "#d9df99";

    return "#000000";
}
function getCardColorText(colorId) {
    if (colorId == 1) return "B";
    else if (colorId == 2) return "G";
    else if (colorId == 3) return "P";
    else if (colorId == 4) return "R";
    else if (colorId == 5) return "Y";

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



function paddy(n, p, c) {
    var pad_char = typeof c !== 'undefined' ? c : '0';
    var pad = new Array(1 + p).join(pad_char);
    return (pad + n).slice(-pad.length);
}


