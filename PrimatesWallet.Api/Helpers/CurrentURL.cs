namespace PrimatesWallet.Api.Helpers
{
    public static class CurrentURL
    {
        /// <summary>
        /// Returns the current URL of the HTTP request received in the current context.
        /// </summary>
        /// <returns>A string representing the current URL.</returns>
        public static string Get(HttpRequest request)
        {
            var scheme = request.Scheme;
            var host = request.Host;
            var pathBase = request.PathBase;
            var path = request.Path;
            return $"{scheme}://{host}{pathBase}{path}";
        }
    }
}
