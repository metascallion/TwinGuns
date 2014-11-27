using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WhiteSpace.GameLoop
{
    public static class UpdateExecuter
    {
        public delegate void Update(GameTime gameTime);
        private static event Update updates;

        public static void registerUpdateable(Update updateToRegister)
        {
            updates += updateToRegister;
        }

        public static void unregisterUpdateable(Update updateToUnregister)
        {
            updates -= updateToUnregister;
        }

        public static void executeUpdates(GameTime gameTime)
        {
            if (updates != null)
            {
                updates(gameTime);
            }
        }
    }
}
