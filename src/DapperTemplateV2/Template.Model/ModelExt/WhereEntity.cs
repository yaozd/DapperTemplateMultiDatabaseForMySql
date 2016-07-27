namespace Template.Model.ModelExt
{
    public class WhereEntity<T>
    {
        public T Model { get; set; }
        public string Sql { get; set; }
        public string OrderBy { get; set; }
    }
}