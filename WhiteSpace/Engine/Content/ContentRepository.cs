using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhiteSpace.Content
{   public static class ContentRepository<ContentType>
    {
        private static Dictionary<string, ContentType> loadedContent = new Dictionary<string, ContentType>();

        public static void addContent(string contentName, ContentType contentToAdd)
        {
            loadedContent[contentName] = contentToAdd;
        }

        public static bool containsContent(string assetName)
        {
            if (loadedContent.Keys.Contains(assetName))
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
}
