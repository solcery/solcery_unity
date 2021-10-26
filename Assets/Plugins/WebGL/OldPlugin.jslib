mergeInto(LibraryManager.library, {
  OnUnityLoaded: function () {
    ReactUnityWebGL.OnUnityLoaded();
  },
    OpenLinkInNewTab: function (link) {
      ReactUnityWebGL.OpenLinkInNewTab(Pointer_stringify(link));
    },
    UpdateCard: function (card) {
      ReactUnityWebGL.UpdateCard(Pointer_stringify(card));
    },
    UpdateRuleset: function (ruleset) {
      ReactUnityWebGL.UpdateRuleset(Pointer_stringify(ruleset));
    },
    CreateBoard: function () {
      ReactUnityWebGL.CreateBoard();
    },
    JoinBoard: function (gameKey) {
      ReactUnityWebGL.JoinBoard(Pointer_stringify(gameKey));
    },
    LogAction: function (logData) {
      ReactUnityWebGL.LogAction(Pointer_stringify(logData));
    },
    GameOverCallback: function (callback) {
      ReactUnityWebGL.GameOverCallback(Pointer_stringify(callback));
    }
  });