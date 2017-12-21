using System;

namespace ColorsTest.Core
{
    public static class Guard
    {
        public static void AgainstNullOrWhitespaceArgument(string arg, string paramName)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                throw new ArgumentNullException(paramName, "Argument cannot be null, empty or whitespace");
            }
        }

        public static void AgainstNullArgument<T>(T arg, string paramName) where T : class
        {
            if (arg == null)
            {
                throw new ArgumentNullException(paramName, "Argument cannot be null");
            }
        }
    }
}