mergeInto(LibraryManager.library, {
      SaveBrickTree: function (brickTree) {
        ReactUnityWebGL.SaveBrickTree(Pointer_stringify(brickTree));
      },
    });