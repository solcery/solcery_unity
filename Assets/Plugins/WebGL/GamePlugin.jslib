mergeInto(LibraryManager.library, {
    OnUnityLoaded: function () {
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