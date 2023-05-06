using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PLAY
{
    internal class ScrollingBackground
    {
        public Texture2D BackgrTexture;
        public Microsoft.Xna.Framework.Rectangle BackgrRectangle;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgrTexture, BackgrRectangle, Microsoft.Xna.Framework.Color.White);
        }
    }
    class MainScrolling : ScrollingBackground
    {
        public MainScrolling(Texture2D newBackgrTexture, Microsoft.Xna.Framework.Rectangle newBackgrRectangle)
        {
            BackgrTexture = newBackgrTexture;
            BackgrRectangle = newBackgrRectangle;
        }

        public void Update()
        {
            BackgrRectangle.X -= 3;
        }
    }
}
