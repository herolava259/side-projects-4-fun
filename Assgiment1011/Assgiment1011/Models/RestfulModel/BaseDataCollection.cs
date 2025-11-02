namespace Assgiment1011.Models.RestfulModel
{
    public class BaseDataCollection<T>
    {
        public IEnumerable<T> BaseDatas { get; set; }

        public int TotalRecordCount { get; set; }

        public bool HasPermission { get; set; }

        public int PageIndex { get; set; }

        public int PageCount { get; set; }
    }
}
