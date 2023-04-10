namespace PrimatesWallet.Application.Middleware
{
    public  class Envelope 
    {
        public Envelope(int status, List<ErrorMessage> errors)
        {
            Errors = errors;
            HttpStatusCode = status;
        }

        public int HttpStatusCode { get; }
        public List<ErrorMessage> Errors { get; set; }
    }

}
