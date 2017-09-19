using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Helpers;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Agilify.Models
{
	public class Item : SyncableObject, IItem
	{
        string name = string.Empty;
	    Member owner = App.User;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public Member Owner
        {
            get { return owner; }
            set { SetProperty(ref owner, value); }
        }
    }
}
