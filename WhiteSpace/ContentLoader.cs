using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using WhiteSpace.Components.Animation;
using System.IO;
using WhiteSpace.Drawables;

namespace WhiteSpace
{

    public static class ContentRepository<ContentType>
    {
        private static Dictionary<string, ContentType> loadedContent = new Dictionary<string,ContentType>();

        public static void addContent(string contentName, ContentType contentToAdd)
        {
            loadedContent[contentName] = contentToAdd;
        }

        public static bool containsContent(string assetName)
        {
            if(loadedContent.Keys.Contains(assetName))
            {
                return true;
            }
            return false;
        }
        public static ContentType getContentForName(string assetName)
        {
            return loadedContent[assetName];
        }
    }

    public static class AnimationLoader
    {
    }

    public static class SpriteSheetLoader
    {
    }

    public static class AnimatorLoader
    {
    }

    public static class ContentLoader
    {
        public static ContentManager ContentManager{ get; set; }

        public static ContentType getContent<ContentType>(string name)
        {
            if(!ContentRepository<ContentType>.containsContent(name))
            {
                ContentRepository<ContentType>.addContent(name, ContentManager.Load<ContentType>(name));
            }
            return ContentRepository<ContentType>.getContentForName(name);
        }
    }
}
