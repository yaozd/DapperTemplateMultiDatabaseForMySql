namespace Template.Model.ModelExt
{
    public class ResultInfo
    {
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class ResultInfo<T> : ResultInfo
    {
        public T Data { get; set; }
    }
}