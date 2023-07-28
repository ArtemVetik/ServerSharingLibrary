namespace ServerSharingAPI
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public uint StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public string Body { get; set; }
    }
}