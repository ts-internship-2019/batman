using System;

namespace iWasHere.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; } //test

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}