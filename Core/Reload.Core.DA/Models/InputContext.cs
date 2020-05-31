namespace Reload.Core.DA.Models
{
    using System;
    using System.Collections.Generic;

    public class InputContext
    {
        public Guid Uid { get; set; }

        public IEnumerable<KeyValuePair<int, string>> KeyboardCommands { get; set; }
        public IEnumerable<KeyValuePair<int, string>> MouseButtonCommands { get; set; }
        public IEnumerable<KeyValuePair<int, string>> MouseScrollCommands { get; set; }
    }
}
