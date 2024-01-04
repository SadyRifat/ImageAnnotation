namespace ImageAnnotation.Dto
{
    public class ErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ErrorResponse() { }

        public ErrorResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
