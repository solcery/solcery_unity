mergeInto(LibraryManager.library, {
    OnUnityLoaded: function () {
      ReactUnityWebGL.OnUnityLoaded();
    },
    OpenLinkInNewTab: function (link) {
        ReactUnityWebGL.OpenLinkInNewTab(Pointer_stringify(link));
    },
});