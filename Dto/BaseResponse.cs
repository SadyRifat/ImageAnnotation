namespace ImageAnnotation.Dto
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public int Code { get; set; }
        public ErrorResponse Error { get; set; }

        public BaseResponse() { }

        public BaseResponse(T data, int code, ErrorResponse error)
        {
            this.Data = data;
            this.Code = code;
            this.Error = error;
        }
    }
}
