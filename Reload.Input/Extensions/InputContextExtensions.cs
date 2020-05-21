namespace Reload.Input.Extensions
{
    using Reload.DataAccess;
    using Silk.NET.Input.Common;
    using Reload.Core.Commands;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class InputContextExtensions
    {
        public static void SaveInputContext(this InputMappingContext inputContext)
        {
            //using (var dbContext = new PersistentDb())
            //{
            //    var inputContextUid = inputContext.Uid != null ? inputContext.Uid : new Guid();

            //    var keyCommands = inputContext.KeyCommands
            //        .Select(k => new KeyValuePair<int, string>((int)k.Key, k.Value.ToString()));

            //    var mouseButtonCommands = inputContext.MouseButtonCommands
            //        .Select(k => new KeyValuePair<int, string>((int)k.Key, k.Value.ToString()));

            //    var input = new DataAccess.Models.InputContext
            //    {
            //        Uid = inputContextUid,
            //        KeyboardCommands = keyCommands,
            //        MouseButtonCommands = mouseButtonCommands,
            //        MouseScrollCommands = new List<KeyValuePair<int, string>>()
            //    };

            //    dbContext.InputContexts.Add(input);
            //    dbContext.SaveChanges();
            //}
        }

        public static void LoadInputContext(this InputMappingContext inputContext, Guid uid)
        {
            using (var dbContext = new PersistentDb())
            {
                //var dbInput = dbContext.InputContexts.FirstOrDefault(c => c.Uid == uid);

                //if (dbInput == null)
                //{
                //    return;
                //}

                //inputContext.Uid = dbInput.Uid;

                //foreach (var keyCommand in dbInput.KeyboardCommands)
                //{
                //    var key = (Key)keyCommand.Key;
                //    var command = (Command)Activator.CreateInstance(Type.GetType(keyCommand.Value));

                //    inputContext.MapKeyToCommand(key, command);
                //}

                //foreach (var mouseButtonCommand in dbInput.MouseButtonCommands)
                //{
                //    var key = (MouseButton)mouseButtonCommand.Key;
                //    var command = (Command)Activator.CreateInstance(Type.GetType(mouseButtonCommand.Value));

                //    inputContext.MapMouseButtonToCommand(key, command);
                //}

                //foreach (var mouseScrollCommand in dbInput.MouseScrollCommands)
                //{
                //    var key = new ScrollWheel(
                //        mouseScrollCommand.Key,
                //        mouseScrollCommand.Key);
                //    var command = (Command)Activator.CreateInstance(Type.GetType(mouseScrollCommand.Value));

                //    inputContext.MapMouseScrollToCommand(key, command);
                //}
            }
        }
    }
}
