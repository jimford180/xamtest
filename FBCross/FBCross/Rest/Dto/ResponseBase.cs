namespace FBCross.Rest.Dto
{
    public class ResponseBase
    {
        public ResponseBase() { }
        public ResponseError Error { get; set; }
    }
    public class ResponseError
    {
        public string Message { get; set; }
    }
}