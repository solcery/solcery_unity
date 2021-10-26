mergeInto(LibraryManager.library, {
    OnUnityLoaded: function (message) {
      ReactUnityWebGL.OnUnityLoaded(Pointer_stringify(message));
    },
    OnGameOverPopupButtonClicked: function () {
      ReactUnityWebGL.OnGameOverPopupButtonClicked();
    },
    OpenLinkInNewTab: function (link) {
        ReactUnityWebGL.OpenLinkInNewTab(Pointer_stringify(link));
    },
    CastCard: function (cardId) {
      ReactUnityWebGL.CastCard(cardId);
  },
});