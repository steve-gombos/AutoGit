namespace AutoGit.WebHooks
{
    public class WebHookConstants
    {
        public const string EventHeader = "x-github-event";
        public const string DeliveryHeader = "x-github-delivery";
        public const string HubSignatureHeader = "x-hub-signature";

        protected WebHookConstants()
        {
        }
    }
}