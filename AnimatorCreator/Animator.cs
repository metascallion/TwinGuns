using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AnimatorCreator
{
    public class Animation
    {
        public string name;
        public int startFrame;
        public int endFrame;

        public Animation(string name, int start, int end)
        {
            this.name = name;
            startFrame = start;
            endFrame = end;
        }
    }

    public static class Animator
    {
        public static List<Animation> animations = new List<Animation>();
        public static int rows;
        public static int cols;

        public static void createFile(string fileName)
        {
            StreamWriter writer = new StreamWriter("../" + "../" + "../" + "/WhiteSpace/bin/WindowsGL/Debug/" + fileName + ".txt");
            writer.WriteLine(rows + "," + cols);
            foreach(Animation a in animations)
            {
                writer.WriteLine(a.name + "," + a.startFrame + "," + a.endFrame);
            }
            writer.Flush();
        }

    }
}
