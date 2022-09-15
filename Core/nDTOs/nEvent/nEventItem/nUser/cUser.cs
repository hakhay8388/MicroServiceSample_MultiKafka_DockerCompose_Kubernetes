using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.nDTOs.nEvent.nEventItem.nUser
{
    public class cUser : IDTOObject
    {
        [DTOField(false)]
        public bool isAuthenticated;

        [DTOField(false)]
        public string provider;

        [DTOField(false)]
        public long id;

        [DTOField(true)]
        public string email;
    }
}
