namespace Store.DataAccessLayer.Models.Response
{
    public class ResponseModel<T> where T : class
    {
        public T Data { get; set; }
        public int TotalItemsCount { get; set; }

        public ResponseModel(T data, int count)
        {
            Data = data;
            TotalItemsCount = count;
        }


    }
}
