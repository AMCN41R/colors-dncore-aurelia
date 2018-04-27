using Microsoft.AspNetCore.Mvc;

namespace ColorsTest.UnitTests.Helpers
{
    public static class ActionResultConverter
    {
        public static T Convert<T>(this IActionResult response)
        {
            return (T)((ObjectResult)response).Value;
        }
    }
}