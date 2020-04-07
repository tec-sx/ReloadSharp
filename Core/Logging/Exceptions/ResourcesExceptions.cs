namespace Core.Logging.Exceptions
{
    using System;
    
    public class MusicException : Exception
    {
        public MusicException(string message)
        {
            ConsoleLog.Error("RESOURCES::MUSIC", message);            
        }
    }
    
    public class SoundException : Exception
    {
        public SoundException(string message)
        {
            ConsoleLog.Error("RESOURCES::SOUND", message);            
        }
    }
    
    public class TextureException : Exception
    {
        public TextureException(string message)
        {
            ConsoleLog.Error("RESOURCES::TEXTURE", message);            
        }
    }
    
    public class GameObjectException : Exception
    {
        public GameObjectException(string message)
        {
            ConsoleLog.Error("RESOURCES::GAME OBJECT", message);            
        }
    }
}