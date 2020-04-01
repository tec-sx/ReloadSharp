namespace Core.Resources.Scene
{
    using Raylib_cs;
    
    public abstract class ModelBase
    {
        public SceneGraph ParentScene { get; set; }
        public ModelBase ParentModel { get; set; }
        public Model Model { get; }
    }
}