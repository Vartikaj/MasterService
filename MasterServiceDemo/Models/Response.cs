namespace MasterServiceDemo.Models
{
    public class Response<T>
    {
        public List<T> LSTModel { get; set; } = null;
        public T Model { get; set; }
        public Boolean Success {  get; set; }
        public int ErrorCode {  get; set; }
        public string Message {  get; set; }
    }
}
