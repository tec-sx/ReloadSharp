using System;
using Reload.Editor.Properties;

namespace Reload.Editor.Exceptions
{
    public class GameEditorNotInitializedException : ApplicationException
    {
        public GameEditorNotInitializedException()
            : base(Resources.Name)
        { }
    }
}
