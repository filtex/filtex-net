using System;

namespace FiltexNet.Exceptions
{
    public class BuildException : Exception
    {
        private const string ErrCouldNotBeBuilt = "could not be built";

        private BuildException(string message) : base(message)
        {
        }

        public static BuildException NewCouldNotBeBuiltError()
        {
            return new BuildException(ErrCouldNotBeBuilt);
        }
    }
}