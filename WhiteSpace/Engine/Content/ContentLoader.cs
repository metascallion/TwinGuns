using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using WhiteSpace.Components.Animation;
using System.IO;
using WhiteSpace.Components.Drawables;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using CSCore.SoundIn;
using CSCore.SoundOut;
using CSCore.Codecs;
using CSCore;
using WhiteSpace.Composite;


namespace WhiteSpace.Content
{
    public static class ContentLoader
    {
        public static ContentManager ContentManager{ get; set; }

        public static ContentType getContent<ContentType>(string name)
        {
            if (!ContentRepository<ContentType>.containsContent(name))
            {
                ContentRepository<ContentType>.addContent(name, ContentManager.Load<ContentType>(name));
            }
            return ContentRepository<ContentType>.getContentForName(name);
        }
    }
}
