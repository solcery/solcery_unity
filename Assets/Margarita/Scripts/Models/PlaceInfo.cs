public class PlaceInfo
{
    public string Id {get;set;}
    public float TopPad { get; set; }
    public float BottomPad { get; set; }
    public float RightPad { get; set; }
    public float LeftPad { get; set; }
    public bool IsVisible {get;set; }
    public PlaceObject Object {get;set;}
    public PlaceListObject ListObject {get;set;}
}