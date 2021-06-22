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
    UseCard: function (cardIndex) {
      ReactUnityWebGL.UseCard(cardIndex);
    },
    CreateFight: function () {
      ReactUnityWebGL.CreateFight();
    },
    OnUnityLoaded: function () {
      ReactUnityWebGL.OnUnityLoaded();
    },
  });