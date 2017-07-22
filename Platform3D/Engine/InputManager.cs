using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine
{
    public class InputManager : GameComponent
    {
        public float XAxis { get; private set; }
        public float ZAxis { get; private set; }
        public bool ShouldJump { get; private set; }
        private bool jumpModifier;

        public InputManager()
            : base(Game3DPlatformer.Instance)
        {

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            XAxis = 0;
            ZAxis = 0;
            ShouldJump = false;

            if(keyboard.IsKeyDown(Keys.A))
            {
                XAxis = -1;
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                XAxis = 1;
            }

            if (keyboard.IsKeyDown(Keys.W))
            {
                ZAxis = -1;
            }

            if (keyboard.IsKeyDown(Keys.S))
            {
                ZAxis = 1;
            }

            if(keyboard.IsKeyUp(Keys.Space))
            {
                jumpModifier = false;
            }

            if(keyboard.IsKeyDown(Keys.Space) && !jumpModifier)
            {
                ShouldJump = true;
                jumpModifier = true;
            }

            base.Update(gameTime);
        }
    }
}
