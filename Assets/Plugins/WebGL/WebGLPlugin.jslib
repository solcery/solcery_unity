mergeInto(LibraryManager.library, {
  OnUnityLoaded: function () {
    ReactUnityWebGL.OnUnityLoaded();
  },
    OpenLinkInNewTab: function (link) {
      ReactUnityWebGL.OpenLinkInNewTab(Pointer_stringify(link));
    },
    CreateCard: function (card, cardName) {
      ReactUnityWebGL.CreateCard(Pointer_stringify(card), Pointer_stringify(cardName));
    },
    CreateRuleset: function (ruleset) {
      ReactUnityWebGL.CreateRuleset(Pointer_stringify(ruleset));
    },
    CreateBoard: function () {
      ReactUnityWebGL.CreateBoard();
    },
    JoinBoard: function (gameKey) {
      ReactUnityWebGL.JoinBoard(Pointer_stringify(gameKey));
    },
    UseCard: function (cardMintAddress, cardIndex) {
      ReactUnityWebGL.UseCard(Pointer_stringify(cardMintAddress), cardIndex);
    },
  });