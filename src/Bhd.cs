using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPetz
{
    public class Bhd
    {
        List<Frame> masterFrameList;
        List<Animation> animationList;

        public class Animation
        {
            List <Frame> frames;
        }
        
        public class Frame
        {
            // int is ball number
            List<KeyValuePair<int, BallFrameData>> ballData;
            // other frame info
        }

        public class BallFrameData
        {
            //ball num
            //x y z of ball
            //rotation
            //any other info 

        } 
    }
}
