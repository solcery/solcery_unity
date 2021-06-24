mergeInto(LibraryManager.library, {
    LogToConsole: function (message) {
      ReactUnityWebGL.LogToConsole(Pointer_stringify(message));
    },
    OpenLinkInNewTab: function (link) {
      ReactUnityWebGL.OpenLinkInNewTab(Pointer_stringify(link));
    },
    CreateCard: function (card, cardName) {
      ReactUnityWebGL.CreateCard(Pointer_stringify(card), Pointer_stringify(cardName));
    },
    UseCard: function (cardMintAddress, cardIndex) {
      ReactUnityWebGL.UseCard(Pointer_stringify(cardMintAddress), cardIndex);
    },
    CreateBoard: function () {
      ReactUnityWebGL.CreateBoard();
    },
    JoinBoard: function (gameKey) {
      ReactUnityWebGL.JoinBoard(Pointer_stringify(gameKey));
    },
    OnUnityLoaded: function () {
      ReactUnityWebGL.OnUnityLoaded();
    },
  });