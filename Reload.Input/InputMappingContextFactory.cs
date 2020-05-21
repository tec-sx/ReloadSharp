namespace Reload.Input
{
    using Reload.Input.Configuration;

    public static class InputMappingContextFactory
    {
        public static InputMappingContext CreateGameplayContext(InputConfiguration configuration)
        {
            var keyboard = configuration.KeyboardId;
            var mouse = configuration.MouseId;

            var context = new InputMappingContext();

            //context.MapKeyToCommand(keyboard, configuration.Jump, );

            return context;
        }
    }
}
