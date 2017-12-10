using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_SimpleSample
{
    class ScrooligBackgroundSprite : Sprite
    {
        new public void Update(GameTime gameTime)
        {

            if (position.X < -texture.Width)
            {
                position.X = texture.Width;
            }
            position.X--;
            base.updateBoundingBox();
        }

        public ScrooligBackgroundSprite(Texture2D texture, Vector2 startingPosition) : base(texture, startingPosition)
        {
        }

    }
}
