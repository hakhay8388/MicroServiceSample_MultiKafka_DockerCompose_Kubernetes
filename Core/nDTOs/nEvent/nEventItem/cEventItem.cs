using Core.Enums;
using Core.nDTOs.nEvent.nEventItem.nUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.nDTOs.nEvent.nEventItem
{
    public class cEventItem
    {
        [DTOField(false)]
        public Guid app;

        [DTOField(false)]
        public EEventTypes type;

        [DTOField(false)]
        public DateTime time;

        [DTOField(false)]
        public bool isSucceeded;

        [DTOField(true)]
        public Object meta;

        [DTOField(false)]
        public cUser user;

        [DTOField(true)]
        public Object attributes;
    }
}
