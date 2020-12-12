using System;
using System.Collections.Generic;
using System.Text;
using static FactoryConsole.Factory.EnumHandler;

namespace FactoryConsole.Factory
{
    public class HandlerFactory
    {
        public AbstractHandler getHandler(EType pointEarningType)
        {
            switch (pointEarningType)
            {
                case EType.Basic:
                    return new Basic();
                case EType.Other:
                    return new Other();
                
                default:
                    return null;
            }
        }
    }

    public class DoTry
    {
        public void Do() 
        {
            EType eType = EType.DoNothing;
            int typeId = 0;
            switch (typeId)
            {
                case 1: //Basic
                    eType = EType.Basic;
                    break;
                case 2: //Other
                    eType = EType.Other;
                    break;
                
                default:
                    break;
            }

            if (eType != EType.DoNothing) 
            {
                HandlerFactory handlerFactory = new HandlerFactory();
                AbstractHandler handler = null;
                handler = handlerFactory.getHandler(eType);
            }
        }
    }
}
