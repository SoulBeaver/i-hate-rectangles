using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis;
using Microsoft.Xna.Framework.Graphics;

namespace IHateRectangles.Components
{
    public abstract class TextureComponent : ComponentPoolable
    {
        public abstract Texture2D Texture();
    }
}
