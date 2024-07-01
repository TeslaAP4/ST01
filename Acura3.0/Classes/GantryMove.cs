using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JabilSDK.Controls;

namespace Acura3._0.Classes
{
    public class GantryMove
    {
        public static bool SafetyPosL = false;
        public static bool SafetyPosR = false;
        /// <summary>
        /// 工位1自动移动
        /// </summary>
        /// <param name="XPost">X点位</param>
        /// <param name="YPost">Y点位</param>
        /// <param name="ZPost">Z点位</param>
        /// <param name="ZSafety">Z安全点</param>
        /// <returns></returns>
        public static bool B_GantryMoveL(double XPost, double YPost, double ZPost,double ZSafety)
        { 
            bool InPosition = false;
            if (!SafetyPosL)
            {
                if (MiddleLayer.MCU_PCBA_Module1F.MTR_Z.GetCommandPosition() <= ZSafety)
                {
                    bool a = MiddleLayer.MCU_PCBA_Module1F.MTR_X.Goto(XPost);
                    bool b = MiddleLayer.MCU_PCBA_Module1F.MTR_Y.Goto(YPost);
                    if (a && b )
                    {
                        SafetyPosL = true;
                    }
                }
                else
                {
                    MiddleLayer.MCU_PCBA_Module1F.MTR_Z.Goto(ZSafety);
                }
            }
            else
            {
                if( MiddleLayer.MCU_PCBA_Module1F.MTR_Z.Goto(ZPost))
                {
                    SafetyPosL = false;
                    InPosition = true;
                }
            }
            return InPosition;
        }

    }
}
