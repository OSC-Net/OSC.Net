namespace OSC.Net.Model.GetOptions
{
    public class Result<T>
    {
        public string name { get; set; }
        public string state { get; set; }
        public Results<T> results { get; set; }
    }
}
