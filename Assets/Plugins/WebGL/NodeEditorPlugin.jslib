mergeInto(LibraryManager.library, {
  OnNodeEditorLoaded: function () {
    ReactUnityWebGL.OnNodeEditorLoaded();
  },
  SaveBrickTree: function (brickTree) {
    ReactUnityWebGL.SaveBrickTree(Pointer_stringify(brickTree));
  }
  });