﻿using Reload.Core.Game;

namespace Reload.Core.Tests.Fakes
{
    internal class GameSystemFake : GameSystem
    {
        public override void Run()
        { }

        protected override void OnInitialize()
        { }

        protected override void OnShutDown()
        { }
    }
}
