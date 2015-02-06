using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhiteSpace.Components.Animation
{
    public class Animation
    {
        public int EndFrame { get; set; }
        public int currentFrame;

        public Animation(int startFrame, int endFrame)
        {
            this.currentFrame = startFrame;
            this.EndFrame = endFrame;
        }
    }
}
