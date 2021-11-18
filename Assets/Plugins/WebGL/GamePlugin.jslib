mergeInto(LibraryManager.library, {
    OnUnityLoaded: function (message) {
      ReactUnityWebGL.OnUnityLoaded();
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